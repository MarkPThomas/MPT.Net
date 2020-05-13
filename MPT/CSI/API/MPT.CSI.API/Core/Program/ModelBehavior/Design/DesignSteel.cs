// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="DesignSteel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Represents Steel design in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignSteel" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class DesignSteel : CSiApiBase, IDesignSteel
    {
        #region Properties
        /// <summary>
        /// Gets the steel design code.
        /// </summary>
        /// <value>The steel design code.</value>
        public DesignSteel Code { get; private set; }
        #endregion


        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignSteel" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public DesignSteel(CSiApiSeed seed) : base(seed)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignSteel" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="code">The design code to use.</param>
        public DesignSteel(CSiApiSeed seed, DesignSteel code) : base(seed)
        {
            Code = code;
        }
        #endregion

        #region Methods: Interface

        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        public void SetCode(DesignSteel code)
        {
            Code = code;
        }

        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteResults()
        {
            _callCode = _sapModel.DesignSteel.DeleteResults();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ResetOverwrites()
        {
            _callCode = _sapModel.DesignSteel.ResetOverwrites();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void StartDesign()
        {
            _callCode = _sapModel.DesignSteel.StartDesign();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public bool ResultsAreAvailable()
        {
            return _sapModel.DesignSteel.GetResultsAvailable();
        }
#endif

        /// <summary>
        /// Returns the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void VerifyPassed(out int numberNotPassedOrChecked,
            out int numberDidNotPass,
            out int numberNotChecked,
            out string[] namesNotPassedOrChecked)
        {
            numberNotPassedOrChecked = -1;
            numberDidNotPass = -1;
            numberNotChecked = -1;
            namesNotPassedOrChecked = new string[0];

            _callCode = _sapModel.DesignSteel.VerifyPassed(ref numberNotPassedOrChecked, ref numberDidNotPass, ref numberNotChecked, ref namesNotPassedOrChecked);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        /// <param name="numberDifferentSections">The number of frame objects that have different analysis and design sections.</param>
        /// <param name="namesDifferentSections">This is an array that includes the name of each frame object that has different analysis and design sections.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void VerifySections(out int numberDifferentSections,
            out string[] namesDifferentSections)
        {
            numberDifferentSections = -1;
            namesDifferentSections = new string[0];

            _callCode = _sapModel.DesignSteel.VerifySections(ref numberDifferentSections, ref namesDifferentSections);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // === Get/Set ===
        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code name.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"></exception>
        public string GetCode()
        {
            string codeName = string.Empty;
            _callCode = _sapModel.DesignSteel.GetCode(ref codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return codeName;
        }

        // TODO: Consider how to flexibly apply this as an enum.
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCode(string codeName)
        {
            _callCode = _sapModel.DesignSteel.SetCode(codeName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"></exception>
        public string GetDesignSection(string nameFrame)
        {
            string nameSection = string.Empty;
            _callCode = _sapModel.DesignSteel.GetDesignSection(nameFrame, ref nameSection);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameSection;
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object.
        /// False: The design section is set to that specified by nameFrame.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDesignSection(string itemName, 
            string nameSection, 
            bool resetToLastAnalysisSection, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignSteel.SetDesignSection(itemName, 
                            nameSection, resetToLastAnalysisSection, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // ===
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <returns>System.String[].</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetGroup()
        {
            string[] nameGroups = new string[0];
            _callCode = _sapModel.DesignSteel.GetGroup(ref _numberOfItems, ref nameGroups);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameGroups;
        }

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design.
        /// False: The group is not selected for steel design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGroup(string nameGroup,
            bool selectForDesign)
        {
            _callCode = _sapModel.DesignSteel.SetGroup(nameGroup, selectForDesign);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool GetComboAutoGenerate()
        {
            bool autoGenerate = false;
            _callCode = _sapModel.DesignSteel.GetComboAutoGenerate(ref autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return autoGenerate;
        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for steel frame design is turned on.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetComboAutoGenerate(bool autoGenerate)
        {
            _callCode = _sapModel.DesignSteel.SetComboAutoGenerate(autoGenerate);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#endif

        /// <summary>
        /// Gets the names of all load combinations used for deflection design.
        /// </summary>
        /// <returns>System.String[].</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetComboDeflection()
        {
            string[] nameLoadCombinations = new string[0];
            _callCode = _sapModel.DesignSteel.GetComboDeflection(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameLoadCombinations;
        }

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design.
        /// False: The combination is not selected for deflection design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetComboDeflection(string nameLoadCombination,
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignSteel.SetComboDeflection(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <returns>System.String[].</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetComboStrength()
        {
            string[] nameLoadCombinations = new string[0];
            _callCode = _sapModel.DesignSteel.GetComboStrength(ref _numberOfItems, ref nameLoadCombinations);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameLoadCombinations;
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design.
        /// False: The combination is not selected for strength design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetComboStrength(string nameLoadCombination,
            bool selectLoadCombination)
        {
            _callCode = _sapModel.DesignSteel.SetComboStrength(nameLoadCombination, selectLoadCombination);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get ===
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="frameNames">This is an array that includes each frame object name for which results are obtained.</param>
        /// <param name="ratios">This is an array that includes the controlling stress or capacity ratio for each frame object.</param>
        /// <param name="ratioTypes">This is an array that includes the controlling stress or capacity ratio type for each frame object.</param>
        /// <param name="locations">This is an array that includes the distance from the I-end of the frame object to the location where the controlling stress or capacity ratio occurs. [L]</param>
        /// <param name="comboNames">This is an array that includes the name of the design combination for which the controlling stress or capacity ratio occurs.</param>
        /// <param name="errorSummaries">This is an array that includes the design error messages for the frame object, if any.</param>
        /// <param name="warningSummaries">This is an array that includes the design warning messages for the frame object, if any.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration: Object = 0, Group = 1, SelectedObjects = 2
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSummaryResults(string name, 
            out string[] frameNames,
            out double[] ratios,
            out eRatioType[] ratioTypes,
            out double[] locations,
            out string[] comboNames,
            out string[] errorSummaries,
            out string[] warningSummaries, 
            eItemType itemType = eItemType.Object)
        {
            frameNames = new string[0];
            ratios = new double[0];
            locations = new double[0];
            comboNames = new string[0];
            errorSummaries = new string[0];
            warningSummaries = new string[0];
            int[] csiRatioTypes = new int[0];

            _callCode = _sapModel.DesignSteel.GetSummaryResults(name, ref _numberOfItems, 
                            ref frameNames, ref ratios, ref csiRatioTypes, ref locations, ref comboNames, 
                            ref errorSummaries, ref warningSummaries, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            ratioTypes = new eRatioType[csiRatioTypes.Length];
            for (int i = 0; i < csiRatioTypes.Length; i++)
            {
                ratioTypes[i] = (eRatioType)csiRatioTypes[i];
            }
        }

#if BUILD_ETABS2017
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="frameTypes">The frame types.</param>
        /// <param name="designSections">The design sections.</param>
        /// <param name="status">The status.</param>
        /// <param name="PMMCombo">The PMM combo.</param>
        /// <param name="PMMRatio">The PMM ratio.</param>
        /// <param name="PRatio">The p ratio.</param>
        /// <param name="MMajorRatio">The m major ratio.</param>
        /// <param name="MMinorRatio">The m minor ratio.</param>
        /// <param name="VMajorCombo">The v major combo.</param>
        /// <param name="VMajorRatio">The v major ratio.</param>
        /// <param name="VMinorCombo">The v minor combo.</param>
        /// <param name="VMinorRatio">The v minor ratio.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration: Object = 0, Group = 1, SelectedObjects = 2
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSummaryResults(string name,
            out string[] frameTypes,
            out string[] designSections,
            out string[] status,
            out string[] PMMCombo,
            out double[] PMMRatio,
            out double[] PRatio,
            out double[] MMajorRatio,
            out double[] MMinorRatio,
            out string[] VMajorCombo,
            out double[] VMajorRatio,
            out string[] VMinorCombo,
            out double[] VMinorRatio,
            eItemType itemType = eItemType.Object)
        {
            frameTypes = new string[0];
            designSections = new string[0];
            status = new string[0];
            PMMCombo = new string[0];
            PMMRatio = new double[0];
            PRatio = new double[0];
            MMajorRatio = new double[0];
            MMinorRatio = new double[0];
            VMajorCombo = new string[0];
            VMajorRatio = new double[0];
            VMinorCombo = new string[0];
            VMinorRatio = new double[0];

            _callCode = _sapModel.DesignSteel.GetSummaryResults_2(name, ref _numberOfItems,
                            ref frameTypes, ref designSections, ref status, 
                            ref PMMCombo, ref PMMRatio, ref PRatio, ref MMajorRatio, ref MMinorRatio,
                            ref VMajorCombo, ref VMajorRatio,
                            ref VMinorCombo, ref VMinorRatio,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // === Set
        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoSelectNull(string itemName, 
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignSteel.SetAutoSelectNull(itemName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion


        #region Methods: Public

        // === Local ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Note that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with TEXT output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        /// <param name="frameNames">Frame object names for which results are obtained.</param>
        /// <param name="textResults">Design results with TEXT output data type of the request field in the request table for the specified frame objects.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetDetailedResultsText(string itemName,
                                           int table,
                                           string field,
                                           out string[] frameNames,
                                           out string[] textResults, 
                                           eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignSteel.GetDetailResultsText(itemName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            table, field, ref _numberOfItems, ref frameNames, ref textResults);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Note that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Selection type to use for applying the method.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with Numerical output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        /// <param name="frameNames">Frame object names for which results are obtained.</param>
        /// <param name="numericalResults">Design results with Numerical output data type of the request field in the request table for the specified frame objects.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetDetailedResultsNumerical(string itemName,
                                           int table,
                                           string field,
                                           out string[] frameNames,
                                           out double[] numericalResults, 
                                           eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignSteel.GetDetailResultsValue(itemName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            table, field, ref _numberOfItems, ref frameNames, ref numericalResults);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // === Get/Set ===
        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <param name="loadCase">Name of the static linear load case associated with each lateral displacement target.</param>
        /// <param name="namePoint">Name of the point object associated to which the lateral displacement target applies.</param>
        /// <param name="displacementTargets">Lateral displacement targets. [L]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified lateral displacement targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// =
        public void GetTargetDisplacement(
            out string[] loadCase,
            out string[] namePoint,
            out double[] displacementTargets,
            out bool allSpecifiedTargetsActive)
        {
            loadCase = new string[0];
            namePoint = new string[0];
            displacementTargets = new double[0];
            allSpecifiedTargetsActive = false;

            _callCode = _sapModel.DesignSteel.GetTargetDispl(ref _numberOfItems, ref loadCase, ref namePoint, ref displacementTargets, ref allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets lateral displacement targets for steel design.
        /// </summary>
        /// <param name="loadCase">Name of the static linear load case associated with each lateral displacement target.</param>
        /// <param name="namePoint">Name of the point object associated to which the lateral displacement target applies.</param>
        /// <param name="displacementTargets">Lateral displacement targets. [L]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified lateral displacement targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTargetDisplacement(
            string[] loadCase,
            string[] namePoint,
            double[] displacementTargets,
            bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignSteel.SetTargetDispl(namePoint.Length, ref loadCase, ref namePoint, ref displacementTargets, allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTargetPeriod(
            out string modalCase,
            out int[] modeNumbers,
            out double[] periodTargets,
            out bool allSpecifiedTargetsActive)
        {
            modeNumbers = new int[0];
            periodTargets = new double[0];
            modalCase = string.Empty;
            allSpecifiedTargetsActive = false;

            _callCode = _sapModel.DesignSteel.GetTargetPeriod(ref _numberOfItems, ref modalCase, ref modeNumbers, ref periodTargets, ref allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTargetPeriod(
            string modalCase,
            int[] modeNumbers,
            double[] periodTargets,
            bool allSpecifiedTargetsActive)
        {
            _callCode = _sapModel.DesignSteel.SetTargetPeriod(modeNumbers.Length, modalCase, ref modeNumbers, ref periodTargets, allSpecifiedTargetsActive);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
