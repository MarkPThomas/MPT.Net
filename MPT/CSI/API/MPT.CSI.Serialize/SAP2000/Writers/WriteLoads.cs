using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.Loads.Cases;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteLoads
    {
        internal static Model _model { get; set; }
        internal static Dictionary<string, List<Dictionary<string, string>>> _tables { get; set; }

        internal static void DefineLoads(
            SAP2000Writer writer, 
            Model model,
            Dictionary<string, List<Dictionary<string, string>>> tables)
        {
            _model = model;
            _tables = tables;

            writer.WriteSingleTable(SAP2000Tables.LOAD_PATTERN_DEFINITIONS, SetLOAD_PATTERN_DEFINITIONS);
            SetLOAD_CASE_DEFINITIONS();
            writer.WriteSingleTable(SAP2000Tables.COMBINATION_DEFINITIONS, SetCOMBINATION_DEFINITIONS);
            writer.WriteSingleTable(SAP2000Tables.AUTO_COMBINATION_OPTION_DATA_01_GENERAL, SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL);
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

        /// <summary>
        /// Sets the load pattern definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetLOAD_PATTERN_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (LoadPattern loadPattern in model.Loading.Patterns)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"LoadPat", loadPattern.Name},
                    {"DesignType", Adaptor.fromEnum(loadPattern.Type)},
                    {"SelfWtMult", Adaptor.fromDouble(loadPattern.SelfWeightMultiplier)}
                };
                if (loadPattern.AutoLoadPattern != null)
                {
                    tableRow["AutoLoad"] = loadPattern.AutoLoadPattern.Name;
                }

                table.Add(tableRow);
            }
        }

        #region Load Cases
        /// <summary>
        /// Sets the load case definitions.
        /// </summary>
        internal static void SetLOAD_CASE_DEFINITIONS()
        {
            List<Dictionary<string, string>> tableContent = new List<Dictionary<string, string>>();
            foreach (LoadCase loadCase in _model.Loading.Cases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Case", loadCase.Name },
                    { "Type", convertFromLoadCaseType(loadCase.Type) },
                    { "DesTypeOpt", Adaptor.ToStringEntryLimited(convertFromSpecificationSource(loadCase.DesignTypeOption)) },
                    { "DesignType", Adaptor.fromEnum(loadCase.DesignType) },
                    { "DesActOpt", Adaptor.ToStringEntryLimited(convertFromSpecificationSource(loadCase.BridgeDesignTypeOption)) },
                    { "DesignAct", Adaptor.fromEnum(loadCase.BridgeDesignType) },
                    { "AutoType", convertFromAutoCreatedCase(loadCase.AutoCreatedCase) },
                    { "RunCase", Adaptor.toYesNo(loadCase.IsSelectedForAnalysis) },
                    { "Notes", Adaptor.ToStringEntryLimited(loadCase.Notes) },
                };

                switch (loadCase)
                {
                    case IInitialCase loadCaseWithInitialCase when loadCaseWithInitialCase.InitialCase == null ||
                                                                   string.IsNullOrEmpty(loadCaseWithInitialCase.InitialCase.InitialCase) ||
                                                                   loadCaseWithInitialCase.InitialCase.InitialCase == Constants.NONE:
                        tableRow["InitialCond"] = "Zero";
                        break;
                    case IInitialCase loadCaseWithInitialCase:
                        tableRow["InitialCond"] = loadCaseWithInitialCase.InitialCase.InitialCase;
                        break;
                }
                tableContent.Add(tableRow);
            }
            _tables.Add(SAP2000Tables.LOAD_CASE_DEFINITIONS, tableContent);

            // Fill case Properties
            foreach (LoadCase loadCase in _model.Loading.Cases)
            {
                switch (loadCase.Type)
                {
                    case eLoadCaseType.Modal:
                        setTable<Modal>(
                            SAP2000Tables.CASE_MODAL_1_GENERAL,
                            SetCASE_MODAL_1_GENERAL);
                        break;

                    case eLoadCaseType.LinearStatic:
                        setTableByInterface<StaticLinear>(
                            SAP2000Tables.CASE_STATIC_1_LOAD_ASSIGNMENTS,
                            SetCASE_STATIC_1_LOAD_ASSIGNMENTS);
                        break;

                    case eLoadCaseType.NonlinearStatic:
                        setTableByInterface<StaticNonlinear>(
                            SAP2000Tables.CASE_STATIC_1_LOAD_ASSIGNMENTS,
                            SetCASE_STATIC_1_LOAD_ASSIGNMENTS);

                        setTableByInterface<StaticNonlinear>(
                            SAP2000Tables.CASE_STATIC_2_NONLINEAR_LOAD_APPLICATION,
                            SetCASE_STATIC_2_NONLINEAR_LOAD_APPLICATION);

                        setTableByInterface<StaticNonlinear>(
                            SAP2000Tables.CASE_STATIC_4_NONLINEAR_PARAMETERS,
                            SetCASE_STATIC_4_NONLINEAR_PARAMETERS);

                        break;

                    case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                        setTableByInterface<TimeHistoryDirectLinear>(
                            SAP2000Tables.CASE_DIRECT_HISTORY_1_GENERAL,
                            SetCASE_DIRECT_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                        setTableByInterface<TimeHistoryDirectNonlinear>(
                            SAP2000Tables.CASE_DIRECT_HISTORY_1_GENERAL,
                            SetCASE_DIRECT_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.LinearModalTimeHistory:
                        setTable<TimeHistoryModalLinear>(
                            SAP2000Tables.CASE_MODAL_HISTORY_1_GENERAL,
                            SetCASE_MODAL_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.NonlinearModalTimeHistory:
                        setTable<TimeHistoryModalNonlinear>(
                            SAP2000Tables.CASE_MODAL_HISTORY_1_GENERAL,
                            SetCASE_MODAL_HISTORY_1_GENERAL);
                        break;

                    case eLoadCaseType.ResponseSpectrum:
                        setTable<ResponseSpectrum>(
                            SAP2000Tables.CASE_RESPONSE_SPECTRUM_1_GENERAL,
                            SetCASE_RESPONSE_SPECTRUM_1_GENERAL);
                        break;
                }
            }
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
            List<Modal> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (Modal loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                    { "MaxNumModes", Adaptor.fromInteger(loadCase.MaxNumberOfModes) },
                    { "MinNumModes", Adaptor.fromInteger(loadCase.MinNumberOfModes) }
                };

                switch (loadCase)
                {
                    case ModalRitz modalRitz:
                        tableRow["ModeType"] = "Ritz";
                        setTable<ModalRitz>(
                            SAP2000Tables.CASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ,
                            SetCASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ);
                        break;
                    case ModalEigen modalEigen:
                        tableRow["ModeType"] = "Eigen";
                        tableRow["EigenShift"] = Adaptor.fromDouble(modalEigen.ShiftFrequency);
                        tableRow["EigenCutoff"] = Adaptor.fromDouble(modalEigen.CutoffFrequencyRadius);
                        tableRow["EigenTol"] = Adaptor.fromDouble(modalEigen.ConvergenceTolerance);
                        tableRow["AutoShift"] = Adaptor.toYesNo(modalEigen.AllowAutoFrequencyShifting);

                        setTable<ModalEigen>(
                            SAP2000Tables.CASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN,
                            SetCaseCASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN);
                        break;
                }
                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the case case modal 2 load assignments eigen.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCaseCASE_MODAL_2_LOAD_ASSIGNMENTS_EIGEN(
            List<ModalEigen> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (ModalEigen loadCase in loadCases)
            {
                foreach (ModalEigenLoad modalLoad in loadCase.ModalEigenLoads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                            { "LoadType", Adaptor.ToStringEntryLimited(modalLoad.LoadType) },
                            { "LoadName", Adaptor.ToStringEntryLimited(modalLoad.LoadName) },
                            { "TargetPar", Adaptor.fromDouble(modalLoad.TargetMassParticipationRatio) },
                            { "StatCorrect", Adaptor.toYesNo(modalLoad.IsStaticCorrectionModeCalculated) }
                        });
                }
            }
        }

        /// <summary>
        /// Sets the case modal 3 load assignments ritz.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_3_LOAD_ASSIGNMENTS_RITZ(
            List<ModalRitz> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (ModalRitz loadCase in loadCases)
            {
                foreach (ModalRitzLoad modalLoad in loadCase.ModalRitzLoads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                            { "LoadType", Adaptor.ToStringEntryLimited(modalLoad.LoadType) },
                            { "LoadName", Adaptor.ToStringEntryLimited(modalLoad.LoadName) },
                            { "MaxCycles", Adaptor.fromDouble(modalLoad.MaximumCycles) },
                            { "TargetPar", Adaptor.fromDouble(modalLoad.TargetDynamicParticipationRatio) }
                        });
                }
            }
        }
        #endregion

        #region Load Cases: Static Linear/Nonlinear
        /// <summary>
        /// Sets the case static 1 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_1_LOAD_ASSIGNMENTS<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T : LoadCase, ILoadsApplied
        {
            foreach (T loadCase in loadCases)
            {
                foreach (LoadPatternTuple load in loadCase.Loads.Loads)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"LoadName", Adaptor.ToStringEntryLimited(load.Load.Name)},
                        {"LoadSF", Adaptor.fromDouble(load.ScaleFactor)},
                        {"LoadType", Adaptor.fromEnum(load.LoadType)},
                    };
                    if (loadCase is IUniqueName uniqueLoadCase)
                    {
                        tableRow["Case"] = Adaptor.ToStringEntryLimited(uniqueLoadCase.Name);
                    }
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the case static 2 nonlinear load application.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_2_NONLINEAR_LOAD_APPLICATION<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T: LoadCase, INonlinearLoadApplication
        {
            foreach (T loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"LoadApp", Adaptor.ToStringEntryLimited(convertFromLoadControl(loadCase.LoadApplication.LoadControl))},
                    {"MonitorJt", Adaptor.ToStringEntryLimited(loadCase.LoadApplication.MonitoredPoint.NamePoint)},
                    {"MonitorDOF", Adaptor.fromEnum(loadCase.LoadApplication.MonitoredPoint.DegreeOfFreedom)}
                };
                if (loadCase is IUniqueName uniqueLoadCase)
                {
                    tableRow["Case"] = Adaptor.ToStringEntryLimited(uniqueLoadCase.Name);
                }

                if (Math.Abs(loadCase.LoadApplication.TargetDisplacement) > Constants.Tolerance)
                {
                    tableRow["TargetDispl"] = Adaptor.fromDouble(loadCase.LoadApplication.TargetDisplacement);
                    tableRow["DisplType"] = Adaptor.fromEnum(loadCase.LoadApplication.MonitoredDisplacementType);
                }

                table.Add(tableRow);
            }

            setTable<StaticNonlinear>(
                SAP2000Tables.CASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS,
                SetCASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS);
        }

        /// <summary>
        /// Sets the case static 4 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_4_NONLINEAR_PARAMETERS<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T : LoadCase, INonlinearSettings
        {
            foreach (INonlinearSettings loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"GeoNonLin", Adaptor.ToStringEntryLimited(convertFromGeometricNonlinearity(loadCase.NonlinearSettings.GeometricNonlinearityType))},
                    {"MaxTotal", Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxTotalSteps)},
                    {"MaxNull", Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNullSteps)},
                    {"UseEvStep", Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)},
                };
                if (loadCase is IUniqueName uniqueLoadCase)
                {
                    tableRow["Case"] = Adaptor.ToStringEntryLimited(uniqueLoadCase.Name);
                }

                if (loadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)
                {
                    tableRow["EvLumpTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeEventLumpingTolerance);
                    tableRow["MaxEvPerStp"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNumberEventsPerStep);
                }

                tableRow["UseIter"] = Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseIteration);
                if (loadCase.NonlinearSettings.SolutionControlParameters.UseIteration)
                {
                    tableRow["ItConvTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeIterationConvergenceTolerance);
                    tableRow["MaxIterNR"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNewtonRaphsonIterationsPerStep);
                    tableRow["MaxIterCS"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxConstantStiffnessIterationsPerStep);

                    tableRow["UseLineSrch"] = Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch);
                    if (loadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch)
                    {
                        tableRow["LSStepFact"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.LineSearchStepFactor);
                        tableRow["LSPerIter"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNumberLineSearches);
                        tableRow["LSTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeLineSearchAcceptanceTolerance);
                    }
                }

                tableRow["TFMaxIter"] = Adaptor.fromInteger(loadCase.NonlinearSettings.TargetForceParameters.MaxIterations);
                tableRow["TFTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.TargetForceParameters.ConvergenceTolerance);
                tableRow["TFAccelFact"] = Adaptor.fromDouble(loadCase.NonlinearSettings.TargetForceParameters.AccelerationFactor);
                tableRow["TFNoStop"] = Adaptor.toYesNo(loadCase.NonlinearSettings.TargetForceParameters.ContinueIfNoConvergence);

                if (loadCase is StaticNonlinearStaged loadCaseStaged)
                {
                    tableRow["StageSave"] = Adaptor.fromEnum(loadCaseStaged.ResultsSaved.StageSavedOption);
                    tableRow["StageMinIns"] = Adaptor.fromInteger(loadCaseStaged.ResultsSaved.MinStepsForInstantanousLoad);
                    tableRow["StageMinTD"] = Adaptor.fromInteger(loadCaseStaged.ResultsSaved.MinStepsForTimeDependentItems);
                    tableRow["HasTimeDependentMaterial"] = Adaptor.toYesNo(loadCaseStaged.ResultsSaved.HasTimeDependentMaterial);
                }

                table.Add(tableRow);
            }

            setTable<StaticNonlinearStaged>(
                SAP2000Tables.CASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS,
                SetCASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS);
        }

        /// <summary>
        /// Sets the case static 5 nonlinear stage definitions.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS(
            List<StaticNonlinearStaged> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (StaticNonlinearStaged loadCase in loadCases)
            {
                foreach (StageDefinition stageDefinition in loadCase.StageDefinitions)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"Duration", Adaptor.fromDouble(stageDefinition.Duration)},
                        {"Output", Adaptor.toYesNo(stageDefinition.OutputIsToBeSaved)},
                    };
                    tableRow["Stage"] = Adaptor.fromInteger(stageDefinition.Stage);

                    if (!string.IsNullOrEmpty(stageDefinition.NameOutput))
                    {
                        tableRow["OutputName"] = stageDefinition.NameOutput;
                    }
                    if (!string.IsNullOrEmpty(stageDefinition.Comment))
                    {
                        tableRow["Comment"] = stageDefinition.Comment;
                    }
                    table.Add(tableRow);
                }
            }

            setTable<StaticNonlinearStaged>(
                SAP2000Tables.CASE_STATIC_5_NONLINEAR_STAGE_DEFINITIONS,
                SetCASE_STATIC_6_NONLINEAR_STAGE_DATA);
        }

        /// <summary>
        /// Sets the case static 6 nonlinear stage data.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_6_NONLINEAR_STAGE_DATA(
            List<StaticNonlinearStaged> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (StaticNonlinearStaged loadCase in loadCases)
            {
                foreach (StageDefinition stageDefinition in loadCase.StageDefinitions)
                {
                    for (int i = 0; i < stageDefinition.StageOperations.Count; i++)
                    {
                        StageOperation stageOperation = stageDefinition.StageOperations[i];
                        Dictionary<string, string> tableRow = new Dictionary<string, string>
                        {
                            {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                            {"Stage", Adaptor.fromInteger(i + 1)},
                            {"Operation", Adaptor.fromEnum(stageOperation.Operation)},
                            {"ObjType", Adaptor.fromEnum(stageOperation.ObjectType)},
                            {"ObjName", Adaptor.ToStringEntryLimited(stageOperation.NameObject)},
                        };

                        if (Math.Abs(stageOperation.Age) > Constants.Tolerance)
                        {
                            tableRow["Age"] = Adaptor.fromDouble(stageOperation.Age);
                        }

                        if (Math.Abs(stageOperation.ScaleFactor) > Constants.Tolerance)
                        {
                            tableRow["LoadType"] = Adaptor.ToStringEntryLimited(stageOperation.LoadOrObjectType);
                            tableRow["LoadName"] = Adaptor.ToStringEntryLimited(stageOperation.LoadOrObjectName);
                            tableRow["LoadSF"] = Adaptor.fromDouble(stageOperation.Age);
                        }
                        else if (!string.IsNullOrEmpty(stageOperation.LoadOrObjectName))
                        {
                            tableRow["ItemType"] = Adaptor.ToStringEntryLimited(stageOperation.LoadOrObjectType);
                            tableRow["ItemName"] = Adaptor.ToStringEntryLimited(stageOperation.LoadOrObjectName);
                        }

                        table.Add(tableRow);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the case static 7 additional controlled displacements.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_STATIC_7_ADDITIONAL_CONTROLLED_DISPLACEMENTS<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T : LoadCase, INonlinearLoadApplication
        {
            foreach (T loadCase in loadCases)
            {
                foreach (MonitoredPointDOFTuple monitoredPoint in loadCase.LoadApplication.AdditionalMonitoredPoints)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Joint", Adaptor.ToStringEntryLimited(monitoredPoint.NamePoint)},
                        {"DOF", Adaptor.fromEnum(monitoredPoint.DegreeOfFreedom)},
                    };
                    if (loadCase is IUniqueName uniqueLoadCase)
                    {
                        tableRow["Case"] = Adaptor.ToStringEntryLimited(uniqueLoadCase.Name);
                    }
                    table.Add(tableRow);
                }
            }
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
        internal static void SetCASE_DIRECT_HISTORY_1_GENERAL<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T : TimeHistoryDirectIntegration
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                    {"OutSteps", Adaptor.fromInteger(loadCase.OutputSteps)},
                    {"StepSize", Adaptor.fromDouble(loadCase.StepSize)},
                };

                if (Math.Abs(loadCase.MaxModalFrequency) > Constants.Tolerance)
                {
                    tableRow["MaxModFreq"] = Adaptor.fromDouble(loadCase.MaxModalFrequency);
                }

                setTable<TimeHistoryDirectIntegration>(
                    SAP2000Tables.CASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS,
                    SetCASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS);

                setTable<TimeHistoryDirectIntegration>(
                    SAP2000Tables.CASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING,
                    SetCASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING);

                setTable<TimeHistoryDirectIntegration>(
                    SAP2000Tables.CASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS,
                    SetCASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS);

                setTable<TimeHistoryDirectIntegration>(
                    SAP2000Tables.CASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING,
                    SetCASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING);

                setTable<TimeHistoryDirectIntegration>(
                    SAP2000Tables.CASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES,
                    SetCASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES);

                if (loadCase is TimeHistoryDirectNonlinear LoadCaseNonlinear)
                {
                    setTable<TimeHistoryDirectNonlinear>(
                        SAP2000Tables.CASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS,
                        SetCASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS);
                }
            }
        }

        /// <summary>
        /// Sets the case direct history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_2_LOAD_ASSIGNMENTS(
            List<TimeHistoryDirectIntegration> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                foreach (LoadTimeHistory load in loadCase.Loads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                            {"LoadName", Adaptor.ToStringEntryLimited(load.Load.Name)},
                            {"LoadSF", Adaptor.fromDouble(load.ScaleFactor)},
                            {"LoadType", Adaptor.fromEnum(load.LoadType)},
                            {"LFunction", Adaptor.ToStringEntryLimited(load.Function)},
                            {"TimeFactor", Adaptor.fromDouble(load.TimeFactor)},
                            {"ArrivalTime", Adaptor.fromDouble(load.ArrivalTime)}
                        });
                }
            }
        }

        /// <summary>
        /// Sets the case direct history 3 viscous proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_3_VISCOUS_PROPORTIONAL_DAMPING(
            List<TimeHistoryDirectIntegration> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"MassCoeff", Adaptor.fromDouble(loadCase.DampingProportional.MassProportionalDampingCoefficient)},
                        {"StiffCoeff", Adaptor.fromDouble(loadCase.DampingProportional.StiffnessProportionalDampingCoefficient)},
                    });
            }
        }

        /// <summary>
        /// Sets the case direct history 4 integration parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_4_INTEGRATION_PARAMETERS(
            List<TimeHistoryDirectIntegration> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"IntMethod", Adaptor.fromEnum(loadCase.TimeIntegration.IntegrationType)},
                        {"Alpha", Adaptor.fromDouble(loadCase.TimeIntegration.Alpha)},
                        {"Beta", Adaptor.fromDouble(loadCase.TimeIntegration.Beta)},
                        {"Gamma", Adaptor.fromDouble(loadCase.TimeIntegration.Gamma)},
                        {"Theta", Adaptor.fromDouble(loadCase.TimeIntegration.Theta)},
                        {"AlphaM", Adaptor.fromDouble(loadCase.TimeIntegration.AlphaM)}
                    });
            }
        }

        /// <summary>
        /// Sets the case direct history 5 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_5_NONLINEAR_PARAMETERS(
            List<TimeHistoryDirectNonlinear> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectNonlinear loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                    {
                        "GeoNonLin", Adaptor.ToStringEntryLimited(
                            convertFromGeometricNonlinearity(
                                loadCase.NonlinearSettings.GeometricNonlinearityType))
                    },
                    {"DTMin", Adaptor.fromDouble(loadCase.NonlinearSettings.MinSubstepSize)},
                    {"DTMax", Adaptor.fromDouble(loadCase.NonlinearSettings.MaxSubstepSize)},
                    {"UseEvStep", Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)}
                };

                if (loadCase.NonlinearSettings.SolutionControlParameters.UseEventStepping)
                {
                    tableRow["EvLumpTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeEventLumpingTolerance);
                    tableRow["MaxEvPerStp"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNumberEventsPerStep);
                }

                tableRow["UseIter"] = Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseIteration);
                if (!loadCase.NonlinearSettings.SolutionControlParameters.UseIteration) continue;

                tableRow["ItConvTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeIterationConvergenceTolerance);
                tableRow["MaxIterNR"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNewtonRaphsonIterationsPerStep);
                tableRow["MaxIterCS"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxConstantStiffnessIterationsPerStep);

                tableRow["UseLineSrch"] = Adaptor.toYesNo(loadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch);
                if (!loadCase.NonlinearSettings.SolutionControlParameters.UseLineSearch) continue;

                tableRow["LSStepFact"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.LineSearchStepFactor);
                tableRow["LSPerIter"] = Adaptor.fromInteger(loadCase.NonlinearSettings.SolutionControlParameters.MaxNumberLineSearches);
                tableRow["LSTol"] = Adaptor.fromDouble(loadCase.NonlinearSettings.SolutionControlParameters.RelativeLineSearchAcceptanceTolerance);

                table.Add(tableRow);
            }
        }

        //TABLE:  "case - DIRECT HISTORY 6 - MODAL PROPORTIONAL DAMPING"

        /// <summary>
        /// Sets the case direct history 7 modal proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_7_MODAL_PROPORTIONAL_DAMPING(
            List<TimeHistoryDirectIntegration> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"Period", Adaptor.fromDouble(loadCase.DampingProportional.PeriodOrFrequencyPt1)},
                        {"Damping", Adaptor.fromDouble(loadCase.DampingProportional.DampingPt1)}
                    });
            }
        }

        /// <summary>
        /// Sets the case direct history 8 modal damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_DIRECT_HISTORY_8_MODAL_DAMPING_OVERRIDES(
            List<TimeHistoryDirectIntegration> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryDirectIntegration loadCase in loadCases)
            {
                foreach (DampingOverride dampingOverride in loadCase.DampingOverrides)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                            {"Mode", Adaptor.fromInteger(dampingOverride.ModeNumber)},
                            {"Damping", Adaptor.fromDouble(dampingOverride.Damping)}
                        });
                }
            }
        }
        #endregion

        #region Load Cases: Time History Modal
        /// <summary>
        /// Sets the case modal history 1 general.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_1_GENERAL<T>(
            List<T> loadCases,
            List<Dictionary<string, string>> table) where T : TimeHistoryModal
        {
            foreach (TimeHistoryModal loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"OutSteps", Adaptor.fromInteger(loadCase.OutputSteps)},
                        {"StepSize", Adaptor.fromDouble(loadCase.StepSize)},
                        {"HistoryType", Adaptor.fromEnum(loadCase.MotionType)},
                    };

                if (Math.Abs(loadCase.Damping.DampingConstant) > Constants.Tolerance)
                {
                    tableRow["ConstDamp"] = Adaptor.fromDouble(loadCase.Damping.DampingConstant);
                }
                table.Add(tableRow);
                
                setTable<TimeHistoryModal>(
                    SAP2000Tables.CASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS,
                    SetCASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS);

                setTable<TimeHistoryModal>(
                    SAP2000Tables.CASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING,
                    SetCASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING);

                setTable<TimeHistoryModal>(
                    SAP2000Tables.CASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING,
                    SetCASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING);

                setTable<TimeHistoryModal>(
                    SAP2000Tables.CASE_MODAL_HISTORY_5_DAMPING_OVERRIDES,
                    SetCASE_MODAL_HISTORY_5_DAMPING_OVERRIDES);

                if (loadCase is TimeHistoryModalNonlinear LoadCaseNonlinear)
                {
                    setTable<TimeHistoryModalNonlinear>(
                        SAP2000Tables.CASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS,
                        SetCASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS);
                }
            }
        }

        /// <summary>
        /// Sets the case modal history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_2_LOAD_ASSIGNMENTS(
            List<TimeHistoryModal> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryModalNonlinear loadCase in loadCases)
            {
                foreach (LoadTimeHistory load in loadCase.Loads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                            {"LoadName", Adaptor.ToStringEntryLimited(load.Load.Name)},
                            {"LoadSF", Adaptor.fromDouble(load.ScaleFactor)},
                            {"LoadType", Adaptor.fromEnum(load.LoadType)},
                            {"LFunction", Adaptor.ToStringEntryLimited(load.Function)},
                            {"TimeFactor", Adaptor.fromDouble(load.TimeFactor)},
                            {"ArrivalTime", Adaptor.fromDouble(load.ArrivalTime)}
                        });
                }
            }
        }

        /// <summary>
        /// Sets the case modal history 3 interpolated damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_3_INTERPOLATED_DAMPING(
            List<TimeHistoryModal> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryModalNonlinear loadCase in loadCases)
            {
                foreach (DampingInterpolated dampingInterpolated in loadCase.Damping.DampingInterpolated)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"Damping", Adaptor.fromDouble(dampingInterpolated.Damping)},
                    };
                    if (dampingInterpolated.DampingType == eDampingTypeInterpolated.InterpolatedByFrequency)
                    {
                        tableRow["Period"] = Adaptor.fromDouble(dampingInterpolated.PeriodOrFrequency);
                    }
                    else
                    {
                        tableRow["Frequency"] = Adaptor.fromDouble(dampingInterpolated.PeriodOrFrequency);
                    }
                    table.Add(tableRow);
                }
            }
        }

        /// <summary>
        /// Sets the case modal history 4 proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_4_PROPORTIONAL_DAMPING(
            List<TimeHistoryModal> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryModalNonlinear loadCase in loadCases)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"MassCoeff", Adaptor.fromDouble(loadCase.Damping.DampingProportional.MassProportionalDampingCoefficient)},
                        {"StiffCoeff", Adaptor.fromDouble(loadCase.Damping.DampingProportional.StiffnessProportionalDampingCoefficient)}
                    });
            }
        }

        /// <summary>
        /// Sets the case modal history 5 damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_5_DAMPING_OVERRIDES(
            List<TimeHistoryModal> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryModalNonlinear loadCase in loadCases)
            {
                foreach (DampingOverride dampingOverride in loadCase.Damping.DampingOverrides)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"Case",  Adaptor.ToStringEntryLimited(loadCase.Name)},
                            {"Mode",  Adaptor.fromInteger(dampingOverride.ModeNumber)},
                            {"Damping",  Adaptor.fromDouble(dampingOverride.Damping)}
                        });
                }
            }
        }

        /// <summary>
        /// Sets the case modal history 6 nonlinear parameters.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_MODAL_HISTORY_6_NONLINEAR_PARAMETERS(
            List<TimeHistoryModalNonlinear> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (TimeHistoryModalNonlinear loadCase in loadCases)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Case",  Adaptor.ToStringEntryLimited(loadCase.Name)},
                        { "DTMax",  Adaptor.fromDouble(loadCase.NonlinearParameters.MaxSubstepSize)},
                        { "DTMin",  Adaptor.fromDouble(loadCase.NonlinearParameters.MinSubstepSize)},
                        { "PeriodStat",  Adaptor.fromDouble(loadCase.NonlinearParameters.StaticPeriod)},
                        { "FConvTol",  Adaptor.fromDouble(loadCase.NonlinearParameters.RelativeForceConvergenceTolerance)},
                        { "EConvTol",  Adaptor.fromDouble(loadCase.NonlinearParameters.RelativeEnergyConvergenceTolerance)},
                        { "ForceItMax",  Adaptor.fromDouble(loadCase.NonlinearParameters.MaxForceIterationLimit)},
                        { "ForceItMin",  Adaptor.fromDouble(loadCase.NonlinearParameters.MinForceIterationLimit)},
                        { "ConvFactor",  Adaptor.fromDouble(loadCase.NonlinearParameters.ConvergenceFactor)}
                    });
            }
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
            List<ModalRitz> LoadCases,
            List<Dictionary<string, string>> table)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_1_GENERAL
            //LoadCase.OutputSteps = Adaptor.fromInteger(tableRow["OutSteps"]);
            //LoadCase.StepSize = Adaptor.fromDouble(tableRow["StepSize"]);
            //LoadCase.MotionType = Enums.EnumLibrary.GetEnumDescription<eMotionType>(tableRow["HistoryType"]);

            //LoadCase.Damping.DampingConstant = Adaptor.fromDouble(tableRow["ConstDamp"]);


            // TABLE:  "case - FREQUENCY HISTORY 1 - GENERAL"
            // Case=ACase2-4   DampingType=Constant
        }

        /// <summary>
        /// Sets the case frequency history 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_FREQUENCY_HISTORY_2_LOAD_ASSIGNMENTS(
            List<ModalRitz> LoadCases,
            List<Dictionary<string, string>> table)
        {
            // TODO: Complete SetCASE_FREQUENCY_HISTORY_2_LOAD_ASSIGNMENTS
            //LoadPattern loadPattern = _model.Loading.Patterns[tableRow["LoadName"]];
            //LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
            //{
            //    Load = loadPattern,
            //    ScaleFactor = Adaptor.fromDouble(tableRow["LoadSF"]),
            //    LoadType = Enums.EnumLibrary.GetEnumDescription<eLoadType>(tableRow["LoadType"]),
            //    Function = tableRow["LFunction"],
            //    TimeFactor = Adaptor.fromDouble(tableRow["TimeFactor"]),
            //    ArrivalTime = Adaptor.fromDouble(tableRow["ArrivalTime"])
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
            List<ModalRitz> LoadCases,
            List<Dictionary<string, string>> table)
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
            List<ModalRitz> LoadCases,
            List<Dictionary<string, string>> table)
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
            List<ModalRitz> LoadCases,
            List<Dictionary<string, string>> table)
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
        internal static void SetCASE_RESPONSE_SPECTRUM_1_GENERAL(
            List<ResponseSpectrum> loadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in loadCases)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Case",  Adaptor.ToStringEntryLimited(loadCase.Name)},
                    {"ModalCombo", Adaptor.fromEnum(loadCase.ModalCombination.Combination) },
                    {"GMCf1", Adaptor.fromDouble(loadCase.ModalCombination.GmcF1) },
                    {"GMCf2", Adaptor.fromDouble(loadCase.ModalCombination.GmcF2) },
                    {"td", Adaptor.fromDouble(loadCase.ModalCombination.Td) },
                    {"PerRigid", Adaptor.fromEnum(loadCase.ModalCombination.PeriodicPlusRigidModalCombination) },
                    {"DirCombo", Adaptor.fromEnum(loadCase.DirectionCombination.DirectionalCombination) },
                    {"EccenRatio", Adaptor.fromDouble(loadCase.Eccentricity) },
                };
                if (Math.Abs(loadCase.DirectionCombination.ScaleFactor) > Constants.Tolerance)
                {
                    tableRow["ABSSF"] = Adaptor.fromDouble(loadCase.DirectionCombination.ScaleFactor);
                }
                if (Math.Abs(loadCase.DirectionCombination.ScaleFactor) > Constants.Tolerance)
                {
                    tableRow["ConstDamp"] = Adaptor.fromDouble(loadCase.Damping.DampingConstant);
                }
                table.Add(tableRow);
            }

            setTable<ResponseSpectrum>(
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS,
                SetCASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS);

            setTable<ResponseSpectrum>(
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING,
                SetCASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING);

            setTable<ResponseSpectrum>(
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING,
                SetCASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING);

            setTable<ResponseSpectrum>(
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES,
                SetCASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES);

            setTable<ResponseSpectrum>(
                SAP2000Tables.CASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES,
                SetCASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES);
        }

        /// <summary>
        /// Sets the case response spectrum 2 load assignments.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_2_LOAD_ASSIGNMENTS(
            List<ResponseSpectrum> LoadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in LoadCases)
            {
                foreach (LoadResponseSpectrum loadResponseSpectrum in loadCase.Loads)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Case",  Adaptor.ToStringEntryLimited(loadCase.Name)},
                        {"LoadName", Adaptor.ToStringEntryLimited(loadResponseSpectrum.LoadName) },
                        {"Function", Adaptor.ToStringEntryLimited(loadResponseSpectrum.FunctionName) },
                        {"Angle", Adaptor.fromDouble(loadResponseSpectrum.Angle) }
                    };

                    if (!string.IsNullOrEmpty(loadResponseSpectrum.CoordinateSystem))
                    {
                        tableRow["CoordSys"] = Adaptor.ToStringEntryLimited(loadResponseSpectrum.CoordinateSystem);
                        tableRow["TransAccSF"] = Adaptor.fromDouble(loadResponseSpectrum.ScaleFactor);
                    }
                    else
                    {
                        tableRow["LoadSF"] = Adaptor.fromDouble(loadResponseSpectrum.ScaleFactor);
                    }
                    table.Add(tableRow);
                }
            }
        }

        /// <summary>
        /// Sets the case response spectrum 3 interpolated damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_3_INTERPOLATED_DAMPING(
            List<ResponseSpectrum> LoadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in LoadCases)
            {
                foreach (DampingInterpolated damping in loadCase.Damping.DampingInterpolated)
                {
                    string periodOrFrequencyLabel =
                        (damping.DampingType == eDampingTypeInterpolated.InterpolatedByFrequency)
                            ? "Frequency"
                            : "Period";
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                            { periodOrFrequencyLabel, Adaptor.fromDouble(damping.PeriodOrFrequency) },
                            { "Damping", Adaptor.fromDouble(damping.Damping) }
                        });
                }
            }
        }

        /// <summary>
        /// Sets the case response spectrum 4 proportional damping.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_4_PROPORTIONAL_DAMPING(
            List<ResponseSpectrum> LoadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in LoadCases)
            {
                DampingProportional damping = loadCase.Damping.DampingProportional;
                table.Add(new Dictionary<string, string>
                {
                    {"Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                    {"MassCoeff", Adaptor.fromDouble(damping.MassProportionalDampingCoefficient) },
                    {"StiffCoeff", Adaptor.fromDouble(damping.StiffnessProportionalDampingCoefficient) }
                });
            }
        }

        /// <summary>
        /// Sets the case response spectrum 5 damping overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_5_DAMPING_OVERRIDES(
            List<ResponseSpectrum> LoadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in LoadCases)
            {
                foreach (DampingOverride overwrite in loadCase.Damping.DampingOverrides)
                {
                    table.Add(new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                        {"Mode", Adaptor.fromInteger(overwrite.ModeNumber) },
                        {"Damping", Adaptor.fromDouble(overwrite.Damping) }
                    });
                }
            }
        }

        /// <summary>
        /// Sets the case response spectrum 6 eccentricity overrides.
        /// </summary>
        /// <param name="LoadCase">The load Case.</param>
        /// <param name="tableRow">The table row.</param>
        internal static void SetCASE_RESPONSE_SPECTRUM_6_ECCENTRICITY_OVERRIDES(
            List<ResponseSpectrum> LoadCases,
            List<Dictionary<string, string>> table)
        {
            foreach (var loadCase in LoadCases)
            {
                foreach (DiaphragmEccentricityOverride overwrite in loadCase.DiaphragmEccentricityOverwrites)
                {
                    table.Add(new Dictionary<string, string>
                    {
                        {"Case", Adaptor.ToStringEntryLimited(loadCase.Name) },
                        {"Diaphragm", Adaptor.ToStringEntryLimited(overwrite.DiaphragmName) },
                        {"Eccen", Adaptor.fromDouble(overwrite.Eccentricity) }
                    });
                }
            }
        }
        #endregion

        #region Load Cases: Helper Functions
        /// <summary>
        /// Converts to geometric nonlinearity.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eGeometricNonlinearity.</returns>
        private static string convertFromGeometricNonlinearity(eGeometricNonlinearity value)
        {
            switch (value)
            {
                case eGeometricNonlinearity.PDeltaLargeDisp:
                    return "Large Displ";
                case eGeometricNonlinearity.PDelta:
                    return "P-Delta";
                case eGeometricNonlinearity.None:
                    return "None";
                default:
                    return "None";
            }
        }

        /// <summary>
        /// Converts the type of to load Case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eLoadCaseType.</returns>
        private static string convertFromLoadCaseType(eLoadCaseType value)
        {
            switch (value)
            {
                case eLoadCaseType.LinearStatic:
                    return "LinStatic";
                case eLoadCaseType.NonlinearStatic:
                    return "NonStatic";
                case eLoadCaseType.Modal:
                    return "LinModal";
                case eLoadCaseType.LinearModalTimeHistory:
                    return "LinModHist";
                case eLoadCaseType.NonlinearModalTimeHistory:
                    return "NonModHist";
                case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                    return "LinDirHist";
                case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                    return "NonDirHist";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts to automatic created Case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eAutoCreatedCase.</returns>
        private static string convertFromAutoCreatedCase(eAutoCreatedCase value)
        {
            switch (value)
            {
                case eAutoCreatedCase.NotAutomaticallyCreated:
                    return "None";
                default:
                    return string.Empty;
                    //return eAutoCreatedCase.AutomaticallyCreated;
            }
        }

        /// <summary>
        /// Converts to specification source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eSpecificationSource.</returns>
        private static string convertFromSpecificationSource(eSpecificationSource value)
        {
            switch (value)
            {
                case eSpecificationSource.ProgramDetermined:
                    return "Prog Det";
                case eSpecificationSource.UserSpecified:
                    return "UserCoordinateSystemNames";
                default:
                    return "Prog Det";
            }
        }

        /// <summary>
        /// Converts to load control.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eLoadControl.</returns>
        private static string convertFromLoadControl(eLoadControl value)
        {
            switch (value)
            {
                case eLoadControl.FullLoad:
                    return "Full Load";
                case eLoadControl.DisplacementControl:
                    return "Displ Ctrl";
                default:
                    return "Full Load";
            }
        }
        #endregion

        // : ILoadCase

        #region Load Combinations
        /// <summary>
        /// Sets the combination definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetCOMBINATION_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (LoadCombination loadCombination in model.Loading.Combinations)
            {
                // First Row
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"ComboName",  Adaptor.ToStringEntryLimited(loadCombination.Name)},
                    {"ComboType",  Adaptor.fromEnum(loadCombination.Type)},
                    {"AutoDesign",  "No"},
                    {"SteelDesign", fromDesignCombination<SteelDesigner>(model.Design.SteelDesigner, loadCombination.Name)},
                    {"ConcDesign",  fromDesignCombination<ConcreteDesigner>(model.Design.ConcreteDesigner, loadCombination.Name)},
                    {"AlumDesign",  fromDesignCombination<AluminumDesigner>(model.Design.AluminumDesigner, loadCombination.Name)},
                    {"ColdDesign",  fromDesignCombination<SteelColdFormedDesigner>(model.Design.SteelColdFormedDesigner, loadCombination.Name)},
                    {"Notes",  Adaptor.ToStringEntryLimited(loadCombination.Notes)}
                };
                if (!string.IsNullOrEmpty(loadCombination.GUID)) tableRow["GUID"] = loadCombination.GUID;

                if (loadCombination.CaseNames.Count > 0)
                {
                    tableRow["CaseName"] = Adaptor.ToStringEntryLimited(loadCombination.CaseNames[0].Item1);
                    tableRow["ScaleFactor"] = Adaptor.fromDouble(loadCombination.CaseNames[0].Item2);
                }
                else if (loadCombination.CombinationNames.Count > 0)
                {
                    tableRow["CaseName"] = Adaptor.ToStringEntryLimited(loadCombination.CombinationNames[0].Item1);
                    tableRow["ScaleFactor"] = Adaptor.fromDouble(loadCombination.CombinationNames[0].Item2);
                }
                table.Add(tableRow);

                // Additional rows
                if (loadCombination.CaseNames.Count > 1)
                {
                    for (int i = 1; i < loadCombination.CaseNames.Count; i++)
                    {
                        table.Add(
                            new Dictionary<string, string>
                            {
                                { "ComboName",  Adaptor.ToStringEntryLimited(loadCombination.Name)},
                                { "CaseName", Adaptor.ToStringEntryLimited(loadCombination.CaseNames[i].Item1)},
                                { "ScaleFactor", Adaptor.fromDouble(loadCombination.CaseNames[i].Item2)}
                            });
                    }
                }

                if (loadCombination.CombinationNames.Count <= 1) continue;
                for (int i = 1; i < loadCombination.CombinationNames.Count; i++)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "ComboName",  Adaptor.ToStringEntryLimited(loadCombination.Name)},
                            { "CaseName", Adaptor.ToStringEntryLimited(loadCombination.CombinationNames[i].Item1)},
                            { "ScaleFactor", Adaptor.fromDouble(loadCombination.CombinationNames[i].Item2)}
                        });
                }
            }
        }

        private static string fromDesignCombination<T>(Designer designer, string loadCombinationName)
        {
            if (designer.LoadCombinationsStrength.CombinationNames.Contains(loadCombinationName))
            {
                return "Strength";
            }

            if (designer is DesignerMetal<T> metalDesigner &&
                metalDesigner.LoadCombinationsDeflection.CombinationNames.Contains(loadCombinationName))
            {
                return "Deflection";
            }
            return "None";
        }
        
        /// <summary>
        /// Sets the automatic combination option data 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(table, model.Design.ConcreteDesigner, "Concrete");
            SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(table, model.Design.SteelDesigner, "Steel");
            SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(table, model.Design.AluminumDesigner, "Aluminum");
            SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(table, model.Design.SteelColdFormedDesigner, "Cold Formed");
        }

        private static void SetAUTO_COMBINATION_OPTION_DATA_01_GENERAL(
            List<Dictionary<string, string>> table,
            Designer designer,
            string designerType)
        {
            if (designer != null)
            {
                table.Add(new Dictionary<string, string>
                {
                    {"DesignType",  Adaptor.ToStringEntryLimited(designerType)},
                    {"AutoGen",  Adaptor.toYesNo(designer.AutogenerateLoadCombinations)}
                });
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
        private static string convertFromLoadComboType(eLoadComboType value)
        {
            switch (value)
            {
                case eLoadComboType.AbsoluteAdditive:
                    return "Abs Add";
                default:
                    return Enums.EnumLibrary.GetEnumDescription(value);
            }
        }
        #endregion


        private static void setTable<T>(
            string tableName,
            Action<List<T>, List<Dictionary<string, string>>> function) where T : LoadCase
        {
            List<T> loadCases = new List<T>();
            foreach (LoadCase loadCase in _model.Loading.Cases)
            {
                if (!(loadCase is T validLoadCase)) continue;
                loadCases.Add(validLoadCase);
            }
            function(loadCases, _tables[tableName]);
        }

        private static void setTableByInterface<T>(
            string tableName,
            Action<List<T>, List<Dictionary<string, string>>> function) where T : LoadCase, ILoadCase
        {
            List<T> loadCases = new List<T>();
            foreach (LoadCase loadCase in _model.Loading.Cases)
            {
                if (!(loadCase is T validLoadCase)) continue;
                loadCases.Add(validLoadCase);
            }
            function(loadCases, _tables[tableName]);
        }
    }
}
