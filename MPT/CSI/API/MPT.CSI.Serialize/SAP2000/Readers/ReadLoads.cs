using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.Loads.Cases;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadLoads
    {
        internal static Model _model { get; set; }
        internal static Dictionary<string, List<Dictionary<string, string>>> _tables { get; set; }

        internal static void DefineLoads(
            SAP2000Reader reader, 
            Model model,
            Dictionary<string, List<Dictionary<string, string>>> tables)
        {
            _model = model;
            _tables = tables;

            reader.ReadSingleTable(SAP2000Tables.LOAD_PATTERN_DEFINITIONS, SetLOAD_PATTERN_DEFINITIONS);
            SetLOAD_CASE_DEFINITIONS();
            reader.ReadSingleTable(SAP2000Tables.COMBINATION_DEFINITIONS, SetCOMBINATION_DEFINITIONS);
            reader.ReadSingleTable(SAP2000Tables.AUTO_COMBINATION_OPTION_DATA_01_GENERAL, SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL);
        }

        #region Auto Wind
        //TABLE:  "AUTO WIND - UBC94"
        //LoadPat=Wind ExposeFrom = Diaphragms   Angle=0   WindwardCq=0.8   LeewardCq=0.5   UserZ=No WindSpeed = 70   Exposure=B I = 1   ExpWidth="From diaphs"

        //TABLE:  "AUTO WIND EXPOSURE FOR HORIZONTAL DIAPHRAGMS"
        //LoadPat=Wind Diaphragm = DIAPH1   X=0   Y=0   TribWidth=12   TribDepth=18   TribHeight=1.5
        //LoadPat=Wind Diaphragm = DIAPH1_0.X = 0   Y=0   TribWidth=12   TribDepth=18   TribHeight=1.5
        //LoadPat=Wind Diaphragm = DIAPH1_3.X = 0   Y=0   TribWidth=12   TribDepth=18   TribHeight=1.5
        //LoadPat=Wind Diaphragm = DIAPH1_6.X = 0   Y=0   TribWidth=12   TribDepth=18   TribHeight=1.5
        #endregion

        #region Auto Seismic



        #endregion

        /// <summary>SetCASE_STATIC_6_NONLINEAR_STAGE_DATA
        /// Sets the load pattern definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetLOAD_PATTERN_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> loadPatternRow in table)
            {
                string loadPatternName = loadPatternRow["LoadPat"];
                model.Loading.Patterns.Add(
                    loadPatternName,
                    Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadPatternType>(loadPatternRow["DesignType"]),
                    Adaptor.toDouble(loadPatternRow["SelfWtMult"])
                );

                string autoLoadKey = "AutoLoad";
                if (loadPatternRow.ContainsKey(autoLoadKey))
                {
                    string autoLoadName = loadPatternRow[autoLoadKey];
                    LoadPattern loadPattern = model.Loading.Patterns[loadPatternName];
                    // TODO: Complete AutoLoadPattern implementation
                    //loadPattern.AutoLoadPattern = AutoLoadPattern.Factory(autoLoadName);
                }
            }
        }

        #region Load Cases
        /// <summary>
        /// Sets the load case definitions.
        /// </summary>
        internal static void SetLOAD_CASE_DEFINITIONS()
        {
            List<Dictionary<string, string>> LoadCaseTable = _tables[SAP2000Tables.LOAD_CASE_DEFINITIONS];

            // Initial case Initialization
            foreach (Dictionary<string, string> LoadCaseRow in LoadCaseTable)
            {
                eLoadCaseType LoadCaseType = convertToLoadCaseType(LoadCaseRow["Type"]);
                string LoadCaseName = LoadCaseRow["Case"];
                eBridgeDesignAction bridgeDesignAction = Enums.EnumLibrary.ConvertStringToEnumByDescription<eBridgeDesignAction>(LoadCaseRow["DesignAct"]);
                eLoadCaseSubType LoadCaseSubType = eLoadCaseSubType.None;
                switch (LoadCaseType)
                {
                    case eLoadCaseType.Modal:
                        LoadCaseSubType = getCASE_MODAL_1_GENERAL_SubType(LoadCaseName);
                        break;
                    case eLoadCaseType.NonlinearStatic:
                        LoadCaseSubType = getNonLinearSubType(bridgeDesignAction);
                        break;
                }

                // Add Case
                _model.Loading.Cases.Add(
                    LoadCaseName,
                    LoadCaseType,
                    LoadCaseSubType);

                // Define basic properties
                LoadCase LoadCase = _model.Loading.Cases[LoadCaseName];
                LoadCase.DesignTypeOption = convertToSpecificationSource(LoadCaseRow["DesTypeOpt"]);
                LoadCase.DesignType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadPatternType>(LoadCaseRow["DesignType"]);
                LoadCase.BridgeDesignTypeOption = convertToSpecificationSource(LoadCaseRow["DesActOpt"]);
                LoadCase.BridgeDesignType = bridgeDesignAction;
                LoadCase.AutoCreatedCase = convertToAutoCreatedCase(LoadCaseRow["AutoType"]);
                LoadCase.IsSelectedForAnalysis = Adaptor.fromYesNo(LoadCaseRow["RunCase"]);
                LoadCase.Notes = LoadCaseRow["Notes"];
            }

            // Fill case Properties
            string keyHeader = "Case";
            foreach (Dictionary<string, string> LoadCaseRow in LoadCaseTable)
            {
                string LoadCaseName = LoadCaseRow[keyHeader];
                LoadCase LoadCase = _model.Loading.Cases[LoadCaseName];

                // Set Initial Load Cases
                if (LoadCase is IInitialCase LoadCaseWithInitialCase &&
                    LoadCaseRow["InitialCond"] != "Zero")
                {
                    LoadCaseWithInitialCase.InitialCase.SetInitialCase(_model.Loading.Cases[LoadCaseRow["InitialCond"]]);
                }

                // Fill in additional properties from dependent tables
                eLoadCaseType LoadCaseType = convertToLoadCaseType(LoadCaseRow["Type"]);
                switch (LoadCaseType)
                {
                    case eLoadCaseType.Modal:
                        SetPartialTableWithChildren<Modal>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_MODAL_1_GENERAL,
                            SetCASE_MODAL_1_GENERAL);
                        break;

                    case eLoadCaseType.LinearStatic:
                        SetPartialTable<StaticLinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_STATIC_1_LOAD_ASSIGNMENTS,
                            SetCASE_STATIC_1_LOAD_ASSIGNMENTS);
                        break;

                    case eLoadCaseType.NonlinearStatic:
                        SetPartialTable<StaticNonlinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_STATIC_1_LOAD_ASSIGNMENTS,
                            SetCASE_STATIC_1_LOAD_ASSIGNMENTS);

                        SetPartialTableWithChildren<StaticNonlinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_STATIC_2_NONLINEAR_LOAD_APPLICATION,
                            SetCASE_STATIC_2_NONLINEAR_LOAD_APPLICATION);

                        SetPartialTableWithChildren<StaticNonlinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_STATIC_4_NONLINEAR_PARAMETERS,
                            SetCASE_STATIC_4_NONLINEAR_PARAMETERS);

                        break;

                    case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                        SetPartialTableWithChildren<TimeHistoryDirectLinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_DIRECT_HISTORY_1_GENERAL,
                            SetCASE_DIRECT_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                        SetPartialTableWithChildren<TimeHistoryDirectNonlinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_DIRECT_HISTORY_1_GENERAL,
                            SetCASE_DIRECT_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.LinearModalTimeHistory:
                        SetPartialTableWithChildren<TimeHistoryModalLinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_MODAL_HISTORY_1_GENERAL,
                            SetCASE_MODAL_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.NonlinearModalTimeHistory:
                        SetPartialTableWithChildren<TimeHistoryModalNonlinear>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_MODAL_HISTORY_1_GENERAL,
                            SetCASE_MODAL_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.ResponseSpectrum:
                        SetPartialTableWithChildren<ResponseSpectrum>(
                            keyHeader,
                            LoadCaseName,
                            SAP2000Tables.CASE_RESPONSE_SPECTRUM_1_GENERAL,
                            SetCASE_RESPONSE_SPECTRUM_1_GENERAL);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the type of the case modal 1 general sub.
        /// </summary>
        /// <param name="LoadCaseName">Name of the load Case.</param>
        /// <returns>eLoadCaseSubType.</returns>
        private static eLoadCaseSubType getCASE_MODAL_1_GENERAL_SubType(string LoadCaseName)
        {
            List<Dictionary<string, string>> LoadCaseTable = _tables[SAP2000Tables.CASE_MODAL_1_GENERAL];
            eLoadCaseSubType LoadCaseSubType = eLoadCaseSubType.None;
            foreach (Dictionary<string, string> tableRow in LoadCaseTable)
            {
                if (tableRow["Case"] != LoadCaseName) continue;
                LoadCaseSubType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadCaseSubType>(tableRow["ModeType"]);
            }
            return LoadCaseSubType;
        }

        /// <summary>
        /// Gets the type of the non linear sub.
        /// </summary>
        /// <param name="bridgeDesignAction">The bridge design action.</param>
        /// <returns>eLoadCaseSubType.</returns>
        private static eLoadCaseSubType getNonLinearSubType(eBridgeDesignAction bridgeDesignAction)
        {
            return bridgeDesignAction == eBridgeDesignAction.Staged ?
                eLoadCaseSubType.NonlinearStagedConstruction :
                eLoadCaseSubType.Nonlinear;
        }
        #endregion

        #region Load Cases: Modal
        /// <summary>
        /// Sets the case modal 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_MODAL_1_GENERAL(
            Modal LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            eLoadCaseSubType LoadCaseSubType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadCaseSubType>(tableRow["ModeType"]);
            LoadCase.MaxNumberOfModes = Adaptor.toInteger(tableRow["MaxNumModes"]);
            LoadCase.MinNumberOfModes = Adaptor.toInteger(tableRow["MinNumModes"]);

            if (LoadCaseSubType == eLoadCaseSubType.Ritz)
            {
                SetPartialTable<ModalRitz>(
                    keyHeader,
                    keyValue,
                    SAP2000Tables.CASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ,
                    SetCASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ);
            }
            else
            {
                ModalEigen LoadCaseEigen = (ModalEigen)LoadCase;
                LoadCaseEigen.ShiftFrequency = Adaptor.toDouble(tableRow["EigenShift"]);
                LoadCaseEigen.CutoffFrequencyRadius = Adaptor.toDouble(tableRow["EigenCutoff"]);
                LoadCaseEigen.ConvergenceTolerance = Adaptor.toDouble(tableRow["EigenTol"]);
                LoadCaseEigen.AllowAutoFrequencyShifting = Adaptor.fromYesNo(tableRow["AutoShift"]);

                SetPartialTable<ModalEigen>(
                    keyHeader,
                    keyValue,
                    SAP2000Tables.CASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN,
                    SetCaseCASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN);
            }
        }

        /// <summary>
        /// Sets the case case modal 2 load assignments eigen.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCaseCASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN(
            ModalEigen LoadCase,
            Dictionary<string, string> tableRow)
        {
            ModalEigenLoad modalEigenLoad = new ModalEigenLoad()
            {
                LoadType = tableRow["LoadType"],
                LoadName = tableRow["LoadName"],
                TargetMassParticipationRatio = Adaptor.toDouble(tableRow["TargetPar"]),
                IsStaticCorrectionModeCalculated = Adaptor.fromYesNo(tableRow["StatCorrect"])
            };

            LoadCase.ModalEigenLoads.Add(modalEigenLoad);
        }

        /// <summary>
        /// Sets the case modal 3 load assignments ritz.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow)
        {
            ModalRitzLoad modalRitzLoad = new ModalRitzLoad()
            {
                LoadType = tableRow["LoadType"],
                LoadName = tableRow["LoadName"],
                MaximumCycles = Adaptor.toDouble(tableRow["MaxCycles"]),
                TargetDynamicParticipationRatio = Adaptor.toDouble(tableRow["TargetPar"])
            };

            LoadCase.ModalRitzLoads.Add(modalRitzLoad);
        }
        #endregion

        #region Load Cases: Static Linear/Nonlinear
        /// <summary>
        /// Sets the case static 1 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_1_LOAD_ASSIGNMENTS(
            ILoadsApplied LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadPattern loadPattern = _model.Loading.Patterns[tableRow["LoadName"]];
            LoadPatternTuple loadPatternTuple = new LoadPatternTuple()
            {
                Load = loadPattern,
                ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]),
                LoadType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(tableRow["LoadType"]),
            };
            LoadCase.Loads.AddLoad(loadPatternTuple);
        }


        /// <summary>
        /// Sets the case static 2 nonlinear load application.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_STATIC_2_NONLINEAR_LOAD_APPLICATION(
            INonlinearLoadApplication LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            LoadCase.LoadApplication = new LoadApplication
            {
                LoadControl = convertToLoadControl(tableRow["LoadApp"]),
                MonitoredPoint =
                {
                    NamePoint = tableRow["MonitorJt"],
                    DegreeOfFreedom = Enums.EnumLibrary.ConvertStringToEnumByDescription<eDegreeOfFreedom>(tableRow["MonitorDOF"])
                },
            };

            if (!tableRow.ContainsKey("DisplType") || !tableRow.ContainsKey("TargetDispl")) return;

            LoadCase.LoadApplication.TargetDisplacement = Adaptor.toDouble(tableRow["TargetDispl"]);
            LoadCase.LoadApplication.MonitoredDisplacementType =
                Enums.EnumLibrary.ConvertStringToEnumByDescription<eMonitoredDisplacementType>(tableRow["DisplType"]);

            SetPartialTable<StaticNonlinear>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS,
                SetCASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS);
        }

        /// <summary>
        /// Sets the case static 4 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_STATIC_4_NONLINEAR_PARAMETERS(
            INonlinearSettings LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            LoadCase.NonlinearSettings.GeometricNonlinearityType = convertToGeometricNonlinearity(tableRow["GeoNonLin"]);
            //LoadCase.NonlinearSettings.HingeUnloadingType = tableRow["GeoNonLin"];
            LoadCase.NonlinearSettings.SolutionControlParameters.MaxTotalSteps = Adaptor.toInteger(tableRow["MaxTotal"]);
            LoadCase.NonlinearSettings.SolutionControlParameters.MaxNullSteps = Adaptor.toInteger(tableRow["MaxNull"]);

            LoadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping = Adaptor.fromYesNo(tableRow["UseEvStep"]);
            if (LoadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)
            {
                LoadCase.NonlinearSettings.SolutionControlParameters.RelativeEventLumpingTolerance = Adaptor.toDouble(tableRow["EvLumpTol"]);
                LoadCase.NonlinearSettings.SolutionControlParameters.MaxNumberEventsPerStep = Adaptor.toInteger(tableRow["MaxEvPerStp"]);
            }

            LoadCase.NonlinearSettings.SolutionControlParameters.UseIteration = Adaptor.fromYesNo(tableRow["UseIter"]);
            if (LoadCase.NonlinearSettings.SolutionControlParameters.UseIteration)
            {
                LoadCase.NonlinearSettings.SolutionControlParameters.RelativeIterationConvergenceTolerance = Adaptor.toDouble(tableRow["ItConvTol"]);
                LoadCase.NonlinearSettings.SolutionControlParameters.MaxNewtonRaphsonIterationsPerStep = Adaptor.toInteger(tableRow["MaxIterNR"]);
                LoadCase.NonlinearSettings.SolutionControlParameters.MaxConstantStiffnessIterationsPerStep = Adaptor.toInteger(tableRow["MaxIterCS"]);

                LoadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch = Adaptor.fromYesNo(tableRow["UseLineSrch"]);
                if (LoadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch)
                {
                    LoadCase.NonlinearSettings.SolutionControlParameters.LineSearchStepFactor = Adaptor.toDouble(tableRow["LSStepFact"]);
                    LoadCase.NonlinearSettings.SolutionControlParameters.MaxNumberLineSearches = Adaptor.toInteger(tableRow["LSPerIter"]);
                    LoadCase.NonlinearSettings.SolutionControlParameters.RelativeLineSearchAcceptanceTolerance = Adaptor.toDouble(tableRow["LSTol"]);
                }
            }

            LoadCase.NonlinearSettings.TargetForceParameters.MaxIterations = Adaptor.toInteger(tableRow["TFMaxIter"]);
            LoadCase.NonlinearSettings.TargetForceParameters.ConvergenceTolerance = Adaptor.toDouble(tableRow["TFTol"]);
            LoadCase.NonlinearSettings.TargetForceParameters.AccelerationFactor = Adaptor.toDouble(tableRow["TFAccelFact"]);
            LoadCase.NonlinearSettings.TargetForceParameters.ContinueIfNoConvergence = Adaptor.fromYesNo(tableRow["TFNoStop"]);

            if (!(LoadCase is StaticNonlinearStaged LoadCaseStaged)) return;

            LoadCaseStaged.ResultsSaved.StageSavedOption = Enums.EnumLibrary.ConvertStringToEnumByDescription<eStageSavedOption>(tableRow["StageSave"]);
            LoadCaseStaged.ResultsSaved.MinStepsForInstantanousLoad = Adaptor.toInteger(tableRow["StageMinIns"]);
            LoadCaseStaged.ResultsSaved.MinStepsForTimeDependentItems = Adaptor.toInteger(tableRow["StageMinTD"]);
            LoadCaseStaged.ResultsSaved.HasTimeDependentMaterial = Adaptor.fromYesNo(tableRow["HasTimeDependentMaterial"]);

            SetPartialTableWithChildren<StaticNonlinearStaged>(
                keyHeader,
                LoadCaseStaged.Name,
                SAP2000Tables.CASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS,
                SetCASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS);
        }

        /// <summary>
        /// Sets the case static 5 nonlinear stage definitions.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS(
            StaticNonlinearStaged LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            StageDefinition stageDefinition = new StageDefinition
            {
                Duration = Adaptor.toDouble(tableRow["Duration"]),
                OutputIsToBeSaved = Adaptor.fromYesNo(tableRow["Output"])
            };
            stageDefinition.FillStageOperations(LoadCase.Name, Adaptor.toInteger(tableRow["Stage"]));
            if (tableRow.ContainsKey("OutputName")) stageDefinition.NameOutput = tableRow["OutputName"];
            if (tableRow.ContainsKey("Comment")) stageDefinition.Comment = tableRow["Comment"];

            LoadCase.StageDefinitions.Add(stageDefinition);

            SetPartialTable<StaticNonlinearStaged>(
                keyHeader,
                LoadCase.Name,
                SAP2000Tables.CASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS,
                SetCASE_STATIC_6_NONLINEAR_STAGE_DATA);
        }

        /// <summary>
        /// Sets the case static 6 nonlinear stage data.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_6_NONLINEAR_STAGE_DATA(
            StaticNonlinearStaged LoadCase,
            Dictionary<string, string> tableRow)
        {
            StageOperation stageOperation = new StageOperation
            {
                Operation = Enums.EnumLibrary.ConvertStringToEnumByDescription<eStageOperations>(tableRow["Operation"]),
                ObjectType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eObjectType>(tableRow["ObjType"]),
                NameObject = tableRow["ObjName"]
            };
            if (tableRow.ContainsKey("Age")) stageOperation.Age = Adaptor.toDouble(tableRow["Age"]);
            if (tableRow.ContainsKey("ItemType"))
            {
                stageOperation.LoadOrObjectType = tableRow["ItemType"];
                stageOperation.LoadOrObjectName = tableRow["ItemName"];
            }
            if (tableRow.ContainsKey("LoadType"))
            {
                stageOperation.LoadOrObjectType = tableRow["LoadType"];
                stageOperation.LoadOrObjectName = tableRow["LoadName"];
                stageOperation.ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]);
            }

            LoadCase.StageDefinitions.AddStageOperation(Adaptor.toInteger(tableRow["Stage"]), stageOperation);
        }

        /// <summary>
        /// Sets the case static 7 additional controlled displacements.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS(
            INonlinearLoadApplication LoadCase,
            Dictionary<string, string> tableRow)
        {
            MonitoredPointDOFTuple monitoredPointDofTuple = new MonitoredPointDOFTuple()
            {
                NamePoint = tableRow["Joint"],
                DegreeOfFreedom = Enums.EnumLibrary.ConvertStringToEnumByDescription<eDegreeOfFreedom>(tableRow["DOF"])
            };
            LoadCase.LoadApplication.AdditionalMonitoredPoints.Add(monitoredPointDofTuple);
        }
        #endregion

        #region Load Cases: Time History Direct Integration
        /// <summary>
        /// Sets the case direct history 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_DIRECT_HISTORY_1_GENERAL(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            LoadCase.OutputSteps = Adaptor.toInteger(tableRow["OutSteps"]);
            LoadCase.StepSize = Adaptor.toDouble(tableRow["StepSize"]);
            if (tableRow.ContainsKey("MaxModFreq")) LoadCase.MaxModalFrequency = Adaptor.toDouble(tableRow["MaxModFreq"]);

            SetPartialTable<TimeHistoryDirectIntegration>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS,
                SetCASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS);

            SetPartialTable<TimeHistoryDirectIntegration>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING,
                SetCASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING);

            SetPartialTable<TimeHistoryDirectIntegration>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS,
                SetCASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS);

            SetPartialTable<TimeHistoryDirectIntegration>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING,
                SetCASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING);

            SetPartialTable<TimeHistoryDirectIntegration>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES,
                SetCASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES);

            if (LoadCase is TimeHistoryDirectNonlinear LoadCaseNonlinear)
            {
                SetPartialTable<TimeHistoryDirectNonlinear>(
                    keyHeader,
                    LoadCaseNonlinear.Name,
                    SAP2000Tables.CASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS,
                    SetCASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS);
            }
        }

        /// <summary>
        /// Sets the case direct history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadPattern loadPattern = _model.Loading.Patterns[tableRow["LoadName"]];
            LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
            {
                Load = loadPattern,
                ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]),
                LoadType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(tableRow["LoadType"]),
                Function = tableRow["LFunction"],
                TimeFactor = Adaptor.toDouble(tableRow["TimeFactor"]),
                ArrivalTime = Adaptor.toDouble(tableRow["ArrivalTime"])
            };
            LoadCase.Loads.Add(loadTimeHistory);
        }

        /// <summary>
        /// Sets the case direct history 3 viscous proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.DampingProportional.MassProportionalDampingCoefficient = Adaptor.toDouble(tableRow["MassCoeff"]);
            LoadCase.DampingProportional.StiffnessProportionalDampingCoefficient = Adaptor.toDouble(tableRow["StiffCoeff"]);
        }

        /// <summary>
        /// Sets the case direct history 4 integration parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.TimeIntegration.IntegrationType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eTimeIntegrationType>(tableRow["IntMethod"]);
            if (tableRow.ContainsKey("Alpha")) LoadCase.TimeIntegration.Alpha = Adaptor.toDouble(tableRow["Alpha"]);
            if (tableRow.ContainsKey("Beta")) LoadCase.TimeIntegration.Beta = Adaptor.toDouble(tableRow["Beta"]);
            if (tableRow.ContainsKey("Gamma")) LoadCase.TimeIntegration.Gamma = Adaptor.toDouble(tableRow["Gamma"]);
            if (tableRow.ContainsKey("Theta")) LoadCase.TimeIntegration.Theta = Adaptor.toDouble(tableRow["Theta"]);
            if (tableRow.ContainsKey("AlphaM")) LoadCase.TimeIntegration.AlphaM = Adaptor.toDouble(tableRow["AlphaM"]);
        }

        /// <summary>
        /// Sets the case direct history 5 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS(
            TimeHistoryDirectNonlinear LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.NonlinearSettings.GeometricNonlinearityType = convertToGeometricNonlinearity(tableRow["GeoNonLin"]);
            LoadCase.NonlinearSettings.MinSubstepSize = Adaptor.toDouble(tableRow["DTMin"]);
            LoadCase.NonlinearSettings.MaxSubstepSize = Adaptor.toDouble(tableRow["DTMax"]);

            LoadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping = Adaptor.fromYesNo(tableRow["UseEvStep"]);
            if (LoadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)
            {
                LoadCase.NonlinearSettings.SolutionControlParameters.RelativeEventLumpingTolerance = Adaptor.toDouble(tableRow["EvLumpTol"]);
                LoadCase.NonlinearSettings.SolutionControlParameters.MaxNumberEventsPerStep = Adaptor.toInteger(tableRow["MaxEvPerStp"]);
            }

            LoadCase.NonlinearSettings.SolutionControlParameters.UseIteration = Adaptor.fromYesNo(tableRow["UseIter"]);

            if (!LoadCase.NonlinearSettings.SolutionControlParameters.UseIteration) return;
            LoadCase.NonlinearSettings.SolutionControlParameters.RelativeIterationConvergenceTolerance = Adaptor.toDouble(tableRow["ItConvTol"]);
            LoadCase.NonlinearSettings.SolutionControlParameters.MaxNewtonRaphsonIterationsPerStep = Adaptor.toInteger(tableRow["MaxIterNR"]);
            LoadCase.NonlinearSettings.SolutionControlParameters.MaxConstantStiffnessIterationsPerStep = Adaptor.toInteger(tableRow["MaxIterCS"]);

            LoadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch = Adaptor.fromYesNo(tableRow["UseLineSrch"]);

            if (!LoadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch) return;
            LoadCase.NonlinearSettings.SolutionControlParameters.LineSearchStepFactor = Adaptor.toDouble(tableRow["LSStepFact"]);
            LoadCase.NonlinearSettings.SolutionControlParameters.MaxNumberLineSearches = Adaptor.toInteger(tableRow["LSPerIter"]);
            LoadCase.NonlinearSettings.SolutionControlParameters.RelativeLineSearchAcceptanceTolerance = Adaptor.toDouble(tableRow["LSTol"]);

        }

        //TABLE:  "case - DIRECT HISTORY 6 - MODAL PROPORTIONAL DAMPING"

        /// <summary>
        /// Sets the case direct history 7 modal proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.DampingProportional.PeriodOrFrequencyPt1 = Adaptor.toDouble(tableRow["Period"]);
            LoadCase.DampingProportional.DampingPt1 = Adaptor.toDouble(tableRow["Damping"]);
        }

        /// <summary>
        /// Sets the case direct history 8 modal damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES(
            TimeHistoryDirectIntegration LoadCase,
            Dictionary<string, string> tableRow)
        {
            DampingOverride dampingOverride = new DampingOverride
            {
                ModeNumber = Adaptor.toInteger(tableRow["Mode"]),
                Damping = Adaptor.toDouble(tableRow["Damping"])
            };
            LoadCase.DampingOverrides.Add(dampingOverride);
        }
        #endregion

        #region Load Cases: Time History Modal
        /// <summary>
        /// Sets the case modal history 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_MODAL_HISTORY_1_GENERAL(
            TimeHistoryModal LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            LoadCase.OutputSteps = Adaptor.toInteger(tableRow["OutSteps"]);
            LoadCase.StepSize = Adaptor.toDouble(tableRow["StepSize"]);
            LoadCase.MotionType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMotionType>(tableRow["HistoryType"]);

            if (tableRow.ContainsKey("ConstDamp")) LoadCase.Damping.DampingConstant = Adaptor.toDouble(tableRow["ConstDamp"]);

            SetPartialTable<TimeHistoryModal>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS,
                SetCASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS);

            SetPartialTable<TimeHistoryModal>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING,
                SetCASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING);

            SetPartialTable<TimeHistoryModal>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING,
                SetCASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING);

            SetPartialTable<TimeHistoryModal>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_MODAL_HISTORY_5_DAMPING_OVERRIDES,
                SetCASE_MODAL_HISTORY_5_DAMPING_OVERRIDES);

            if (LoadCase is TimeHistoryModalNonlinear LoadCaseNonlinear)
            {
                SetPartialTable<TimeHistoryModalNonlinear>(
                    keyHeader,
                    LoadCaseNonlinear.Name,
                    SAP2000Tables.CASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS,
                    SetCASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS);
            }
        }

        /// <summary>
        /// Sets the case modal history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS(
            TimeHistoryModal LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadPattern loadPattern = _model.Loading.Patterns[tableRow["LoadName"]];
            LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
            {
                Load = loadPattern,
                ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]),
                LoadType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(tableRow["LoadType"]),
                Function = tableRow["LFunction"],
                TimeFactor = Adaptor.toDouble(tableRow["TimeFactor"]),
                ArrivalTime = Adaptor.toDouble(tableRow["ArrivalTime"])
            };
            LoadCase.Loads.Add(loadTimeHistory);
        }

        /// <summary>
        /// Sets the case modal history 3 interpolated damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING(
            TimeHistoryModal LoadCase,
            Dictionary<string, string> tableRow)
        {
            DampingInterpolated interpolatedFrequencyOrPeriodDamping = new DampingInterpolated();
            if (tableRow.ContainsKey("Period"))
            {
                interpolatedFrequencyOrPeriodDamping = new DampingInterpolated
                {
                    DampingType = eDampingTypeInterpolated.InterpolatedByPeriod,
                    PeriodOrFrequency = Adaptor.toDouble(tableRow["Period"]),
                    Damping = Adaptor.toDouble(tableRow["Damping"])
                };
            }
            if (tableRow.ContainsKey("Frequency"))
            {
                interpolatedFrequencyOrPeriodDamping = new DampingInterpolated
                {
                    DampingType = eDampingTypeInterpolated.InterpolatedByFrequency,
                    PeriodOrFrequency = Adaptor.toDouble(tableRow["Frequency"]),
                    Damping = Adaptor.toDouble(tableRow["Damping"])
                };
            }
            LoadCase.Damping.DampingInterpolated.Add(interpolatedFrequencyOrPeriodDamping);
        }

        /// <summary>
        /// Sets the case modal history 4 proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING(
            TimeHistoryModal LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.Damping.DampingProportional.MassProportionalDampingCoefficient = Adaptor.toDouble(tableRow["MassCoeff"]);
            LoadCase.Damping.DampingProportional.StiffnessProportionalDampingCoefficient = Adaptor.toDouble(tableRow["StiffCoeff"]);
        }

        /// <summary>
        /// Sets the case modal history 5 damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_5_DAMPING_OVERRIDES(
            TimeHistoryModal LoadCase,
            Dictionary<string, string> tableRow)
        {
            DampingOverride dampingOverride = new DampingOverride
            {
                ModeNumber = Adaptor.toInteger(tableRow["Mode"]),
                Damping = Adaptor.toDouble(tableRow["Damping"])
            };
            LoadCase.Damping.DampingOverrides.Add(dampingOverride);
        }

        /// <summary>
        /// Sets the case modal history 6 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS(
            TimeHistoryModalNonlinear LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.NonlinearParameters.MaxSubstepSize = Adaptor.toDouble(tableRow["DTMax"]);
            LoadCase.NonlinearParameters.MinSubstepSize = Adaptor.toDouble(tableRow["DTMin"]);
            LoadCase.NonlinearParameters.StaticPeriod = Adaptor.toDouble(tableRow["PeriodStat"]);
            LoadCase.NonlinearParameters.RelativeForceConvergenceTolerance = Adaptor.toDouble(tableRow["FConvTol"]);
            LoadCase.NonlinearParameters.RelativeEnergyConvergenceTolerance = Adaptor.toDouble(tableRow["EConvTol"]);
            LoadCase.NonlinearParameters.MaxForceIterationLimit = Adaptor.toInteger(tableRow["ForceItMax"]);
            LoadCase.NonlinearParameters.MinForceIterationLimit = Adaptor.toInteger(tableRow["ForceItMin"]);
            LoadCase.NonlinearParameters.ConvergenceFactor = Adaptor.toDouble(tableRow["ConvFactor"]);
        }
        #endregion

        #region Load Cases: Time History Frequency
        /// <summary>
        /// Sets the case frequency history 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_1_GENERAL(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_1_GENERAL
            //LoadCase.OutputSteps = Adaptor.toInteger(tableRow["OutSteps"]);
            //LoadCase.StepSize = Adaptor.toDouble(tableRow["StepSize"]);
            //LoadCase.MotionType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMotionType>(tableRow["HistoryType"]);

            //LoadCase.Damping.DampingConstant = Adaptor.toDouble(tableRow["ConstDamp"]);


            // TABLE:  "case - FREQUENCY HISTORY 1 - GENERAL"
            // Case=ACase2-4   DampingType=Constant
        }

        /// <summary>
        /// Sets the case frequency history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_2_LOAD_ASSIGNMENTS(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_2_LOAD_ASSIGNMENTS
            //LoadPattern loadPattern = _model.Loading.Patterns[tableRow["LoadName"]];
            //LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
            //{
            //    Load = loadPattern,
            //    ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]),
            //    LoadType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(tableRow["LoadType"]),
            //    Function = tableRow["LFunction"],
            //    TimeFactor = Adaptor.toDouble(tableRow["TimeFactor"]),
            //    ArrivalTime = Adaptor.toDouble(tableRow["ArrivalTime"])
            //};
            //LoadCase.Loads.Add(loadTimeHistory);

            // TABLE:  "case - FREQUENCY HISTORY 2 - LOAD ASSIGNMENTS"
            // Case=ACase2-4   LoadType="Load pattern"   LoadName=DEAD   Function=RAMPTH   LoadSF=1   TimeFactor=1   ArrivalTime=0
        }

        /// <summary>
        /// Sets the case frequency history 3 constant damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_3_CONSTANT_DAMPING(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_3_CONSTANT_DAMPING
            // TABLE:  "case - FREQUENCY HISTORY 3 - CONSTANT DAMPING"
            // Case=ACase2-4   MassCoeff=0   StiffCoeff=0.05
        }

        /// <summary>
        /// Sets the case frequency history 4 interpolated damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_4_INTERPOLATED_DAMPING(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_4_INTERPOLATED_DAMPING
            //TABLE:  "case - FREQUENCY HISTORY 4 - INTERPOLATED DAMPING"
            //Case=ACase2-10   FreqUnits=Hz Frequency = 1   MassCoeff=0   StiffCoeff=0.05
        }

        /// <summary>
        /// Sets the case frequency history 5 integration parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_5_INTEGRATION_PARAMETERS(
            ModalRitz LoadCase,
            Dictionary<string, string> tableRow)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_5_INTEGRATION_PARAMETERS
            // TABLE:  "case - FREQUENCY HISTORY 5 - INTEGRATION PARAMETERS"
            // Case=ACase2-4   SubSteps=1   PadFact=2   TimeStepAdj=None
        }
        #endregion

        #region Load Cases: Response Spectrum
        /// <summary>
        /// Sets the case response spectrum 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_1_GENERAL(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow,
            string keyHeader,
            string keyValue,
            string tableName)
        {
            LoadCase.ModalCombination.Combination = Enums.EnumLibrary.ConvertStringToEnumByDescription<eModalCombination>(tableRow["ModalCombo"]);
            LoadCase.ModalCombination.GmcF1 = Adaptor.toDouble(tableRow["GMCf1"]);
            LoadCase.ModalCombination.GmcF2 = Adaptor.toDouble(tableRow["GMCf2"]);
            LoadCase.ModalCombination.Td = Adaptor.toDouble(tableRow["td"]);
            LoadCase.ModalCombination.PeriodicPlusRigidModalCombination =
                Enums.EnumLibrary.ConvertStringToEnumByDescription<ePeriodicPlusRigidModalCombination>(tableRow["PerRigid"]);

            LoadCase.DirectionCombination.DirectionalCombination = Enums.EnumLibrary.ConvertStringToEnumByDescription<eDirectionalCombination>(tableRow["DirCombo"]);
            if (tableRow.ContainsKey("ABSSF")) LoadCase.DirectionCombination.ScaleFactor = Adaptor.toDouble(tableRow["ABSSF"]);

            LoadCase.Eccentricity = Adaptor.toDouble(tableRow["EccenRatio"]);
            if (tableRow.ContainsKey("ConstDamp")) LoadCase.Damping.DampingConstant = Adaptor.toDouble(tableRow["ConstDamp"]);

            SetPartialTable<ResponseSpectrum>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS,
                SetCASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS);

            SetPartialTable<ResponseSpectrum>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING,
                SetCASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING);

            SetPartialTable<ResponseSpectrum>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING,
                SetCASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING);

            SetPartialTable<ResponseSpectrum>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES,
                SetCASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES);

            SetPartialTable<ResponseSpectrum>(
                keyHeader,
                keyValue,
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES,
                SetCASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES);
        }

        /// <summary>
        /// Sets the case response spectrum 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadResponseSpectrum loadResponseSpectrum = new LoadResponseSpectrum
            {
                LoadName = tableRow["LoadName"],
                Function = tableRow["Function"],
                FunctionName = tableRow["Function"],
                Angle = Adaptor.toDouble(tableRow["Angle"])
            };

            if (tableRow.ContainsKey("CoordSys"))
            {
                loadResponseSpectrum.Direction = Enums.EnumLibrary.ConvertStringToEnumByDescription<eDegreeOfFreedom>(tableRow["LoadName"]);
                loadResponseSpectrum.CoordinateSystem = tableRow["CoordSys"];
                loadResponseSpectrum.ScaleFactor = Adaptor.toDouble(tableRow["TransAccSF"]);
            }
            else
            {
                loadResponseSpectrum.ScaleFactor = Adaptor.toDouble(tableRow["LoadSF"]);
            }
            LoadCase.Loads.Add(loadResponseSpectrum);
        }

        /// <summary>
        /// Sets the case response spectrum 3 interpolated damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow)
        {
            DampingInterpolated interpolatedFrequencyOrPeriodDamping = new DampingInterpolated();
            if (tableRow.ContainsKey("Period"))
            {
                interpolatedFrequencyOrPeriodDamping = new DampingInterpolated
                {
                    DampingType = eDampingTypeInterpolated.InterpolatedByPeriod,
                    PeriodOrFrequency = Adaptor.toDouble(tableRow["Period"]),
                    Damping = Adaptor.toDouble(tableRow["Damping"])
                };
            }
            if (tableRow.ContainsKey("Frequency"))
            {
                interpolatedFrequencyOrPeriodDamping = new DampingInterpolated
                {
                    DampingType = eDampingTypeInterpolated.InterpolatedByFrequency,
                    PeriodOrFrequency = Adaptor.toDouble(tableRow["Frequency"]),
                    Damping = Adaptor.toDouble(tableRow["Damping"])
                };
            }
            LoadCase.Damping.DampingInterpolated.Add(interpolatedFrequencyOrPeriodDamping);
        }

        /// <summary>
        /// Sets the case response spectrum 4 proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow)
        {
            LoadCase.Damping.DampingProportional.MassProportionalDampingCoefficient = Adaptor.toDouble(tableRow["MassCoeff"]);
            LoadCase.Damping.DampingProportional.StiffnessProportionalDampingCoefficient = Adaptor.toDouble(tableRow["StiffCoeff"]);
        }

        /// <summary>
        /// Sets the case response spectrum 5 damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow)
        {
            DampingOverride dampingOverride = new DampingOverride
            {
                ModeNumber = Adaptor.toInteger(tableRow["Mode"]),
                Damping = Adaptor.toDouble(tableRow["Damping"])
            };
            LoadCase.Damping.DampingOverrides.Add(dampingOverride);
        }

        /// <summary>
        /// Sets the case response spectrum 6 eccentricity overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES(
            ResponseSpectrum LoadCase,
            Dictionary<string, string> tableRow)
        {
            DiaphragmEccentricityOverride diaphragmEccentricityOverride =
                new DiaphragmEccentricityOverride
                {
                    DiaphragmName = tableRow["Diaphragm"],
                    Eccentricity = Adaptor.toDouble(tableRow["Eccen"])
                };

            LoadCase.DiaphragmEccentricityOverwrites.Add(diaphragmEccentricityOverride);
        }
        #endregion

        #region Load Cases: Helper Functions
        /// <summary>
        /// Converts to geometric nonlinearity.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eGeometricNonlinearity.</returns>
        private static eGeometricNonlinearity convertToGeometricNonlinearity(string value)
        {
            switch (value)
            {
                case "Large Displ":
                    return eGeometricNonlinearity.PDeltaLargeDisp;
                case "P-Delta":
                    return eGeometricNonlinearity.PDelta;
                case "None":
                    return eGeometricNonlinearity.None;
                default:
                    return eGeometricNonlinearity.None;
            }
        }

        /// <summary>
        /// Converts the type of to load Case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eLoadCaseType.</returns>
        private static eLoadCaseType convertToLoadCaseType(string value)
        {
            switch (value)
            {
                case "LinStatic":
                    return eLoadCaseType.LinearStatic;
                case "NonStatic":
                    return eLoadCaseType.NonlinearStatic;
                case "LinModal":
                    return eLoadCaseType.Modal;
                case "LinModHist":
                    return eLoadCaseType.LinearModalTimeHistory;
                case "NonModHist":
                    return eLoadCaseType.NonlinearModalTimeHistory;
                case "LinDirHist":
                    return eLoadCaseType.LinearDirectIntegrationTimeHistory;
                case "NonDirHist":
                    return eLoadCaseType.NonlinearDirectIntegrationTimeHistory;
                default:
                    return eLoadCaseType.Error;
            }
        }

        /// <summary>
        /// Converts to automatic created Case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eAutoCreatedCase.</returns>
        private static eAutoCreatedCase convertToAutoCreatedCase(string value)
        {
            switch (value)
            {
                case "None":
                    return eAutoCreatedCase.NotAutomaticallyCreated;
                default:
                    return eAutoCreatedCase.AutomaticallyCreated;
            }
        }

        /// <summary>
        /// Converts to specification source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eSpecificationSource.</returns>
        private static eSpecificationSource convertToSpecificationSource(string value)
        {
            switch (value)
            {
                case "Prog Det":
                    return eSpecificationSource.ProgramDetermined;
                case "UserCoordinateSystemNames":
                    return eSpecificationSource.UserSpecified;
                default:
                    return eSpecificationSource.ProgramDetermined;
            }
        }

        /// <summary>
        /// Converts to load control.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eLoadControl.</returns>
        private static eLoadControl convertToLoadControl(string value)
        {
            switch (value)
            {
                case "Full Load":
                    return eLoadControl.FullLoad;
                case "Displ Ctrl":
                    return eLoadControl.DisplacementControl;
                default:
                    return eLoadControl.FullLoad;
            }
        }
        #endregion


        #region Load Combinations
        /// <summary>
        /// Sets the combination definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetCOMBINATION_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string loadComboName = tableRow["ComboName"];

                if (tableRow.ContainsKey("ComboType"))
                {
                    // Add Case
                    eLoadComboType LoadCaseType = convertToLoadComboType(tableRow["ComboType"]);
                    model.Loading.Combinations.Add(
                        loadComboName,
                        LoadCaseType);

                    // Define basic properties
                    LoadCombination newLoadCombination = model.Loading.Combinations[loadComboName];
                    newLoadCombination.Notes = tableRow["Notes"];
                    if (tableRow.ContainsKey("GUID")) newLoadCombination.GUID = tableRow["GUID"];
                    //Adaptor.fromYesNo(tableRow["AutoDesign"]);
                    //newLoadCombination.DesignSteel =
                    // AutoDesign=No

                    addDesignCombination(
                        newLoadCombination,
                        tableRow["SteelDesign"],
                        model.Design.SteelDesigner.LoadCombinationsStrength,
                        model.Design.SteelDesigner.LoadCombinationsDeflection);

                    addDesignCombination(
                        newLoadCombination,
                        tableRow["ConcDesign"],
                        model.Design.ConcreteDesigner.LoadCombinationsStrength);

                    addDesignCombination(
                        newLoadCombination,
                        tableRow["AlumDesign"],
                        model.Design.AluminumDesigner.LoadCombinationsStrength,
                        model.Design.AluminumDesigner.LoadCombinationsDeflection);

                    addDesignCombination(
                        newLoadCombination,
                        tableRow["ColdDesign"],
                        model.Design.SteelColdFormedDesigner.LoadCombinationsStrength,
                        model.Design.SteelColdFormedDesigner.LoadCombinationsDeflection);
                }

                LoadCombination loadCombination = model.Loading.Combinations[loadComboName];
                string CaseName = tableRow["CaseName"];
                Tuple<string, double> loadCombinationComponent = new Tuple<string, double>(CaseName, Adaptor.toDouble(tableRow["ScaleFactor"]));

                // It is assumed that all load Cases have been added before any load combinations
                if (model.Loading.Cases.Contains(CaseName))
                {
                    loadCombination.CaseNames.Add(loadCombinationComponent);
                }
                else
                {
                    loadCombination.CombinationNames.Add(loadCombinationComponent);
                }
            }
        }

        /// <summary>
        /// Adds the design combination.
        /// </summary>
        /// <param name="loadCombination">The load combination.</param>
        /// <param name="combinationDesignType">Type of the combination design.</param>
        /// <param name="loadCombinationsStrength">The load combinations strength.</param>
        /// <param name="loadCombinationsDeflection">The load combinations deflection.</param>
        private static void addDesignCombination(
            LoadCombination loadCombination,
            string combinationDesignType,
            LoadCombinationsStrength loadCombinationsStrength,
            LoadCombinationsDeflection loadCombinationsDeflection = null)
        {
            switch (combinationDesignType)
            {
                case "None":
                    return;
                case "Strength":
                    loadCombinationsStrength.Add(loadCombination);
                    break;
                case "Deflection":
                    loadCombinationsDeflection?.Add(loadCombination);
                    break;
            }
        }

        /// <summary>
        /// Sets the automatic combination option data 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> code in table)
            {
                Designer designer = null;
                switch (code["DesignType"])
                {
                    case "Concrete":
                        designer = model.Design.ConcreteDesigner;
                        break;
                    case "Steel":
                        designer = model.Design.SteelDesigner;
                        break;
                    case "Aluminum":
                        designer = model.Design.AluminumDesigner;
                        break;
                    case "Cold Formed":
                        designer = model.Design.SteelColdFormedDesigner;
                        break;
                }

                if (designer == null) continue;

                designer.AutogenerateLoadCombinations = Adaptor.fromYesNo(code["AutoGen"]);
            }
        }

        //TABLE:  "AUTO COMBINATION OPTION DATA 02 - USER DATA"
        //    DesignType=Steel     LSType = Strength   LCName=DEAD
        //    DesignType = Steel   LSType=Strength     LCName = Wind
        // TODO: Complete AUTO COMBINATION OPTION DATA 02 - USER DATA

        /// <summary>
        /// Converts the type of to load combo.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eLoadComboType.</returns>
        private static eLoadComboType convertToLoadComboType(string value)
        {
            switch (value)
            {
                case "Abs Add":
                    return eLoadComboType.AbsoluteAdditive;
                default:
                    return Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadComboType>(value);
            }
        }
        #endregion

        /// <summary>
        /// Sets the partial table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="function">The function.</param>
        private static void SetPartialTable<T>(
            string keyHeader,
            string keyValue,
            string tableName,
            Action<T, Dictionary<string, string>> function) where T : LoadCase
        {
            List<Dictionary<string, string>> table = _tables[tableName];
            T loadCase = (T)_model.Loading.Cases[keyValue];

            foreach (Dictionary<string, string> tableRow in table)
            {
                if (tableRow[keyHeader] != keyValue) continue;
                function(loadCase, tableRow);
            }
        }

        /// <summary>
        /// Sets the partial table with children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyHeader">The key header.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="function">The function.</param>
        private static void SetPartialTableWithChildren<T>(
            string keyHeader,
            string keyValue,
            string tableName,
            Action<T, Dictionary<string, string>, string, string, string> function) where T : LoadCase
        {
            List<Dictionary<string, string>> table = _tables[tableName];
            T loadCase = (T)_model.Loading.Cases[keyValue];

            foreach (Dictionary<string, string> tableRow in table)
            {
                if (tableRow[keyHeader] != keyValue) continue;
                function(loadCase, tableRow, keyHeader, keyValue, tableName);
            }
        }
    }
}
