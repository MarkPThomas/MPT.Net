// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-20-2018
// ***********************************************************************
// <copyright file="LoadCombination.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadCombination = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCombinations;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load combination.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public sealed class LoadCombination : CSiOOAPiName
    {
        #region Fields & Properties

        /// <summary>
        /// Gets the load combination API object.
        /// </summary>
        /// <value>The load combinations.</value>
        private ApiLoadCombination _apiLoadCombinations => getApiLoadCombination(_apiApp);

        /// <summary>
        /// The analyzer
        /// </summary>
        private readonly Analyzer _analyzer;

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The load combinations
        /// </summary>
        private readonly LoadCombinations _loadCombinations;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public string Note { get; set; }
#endif
        /// <summary>
        /// The type
        /// </summary>
        private eLoadComboType _type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadComboType Type
        {
            get
            {
                if (_type == 0)
                {
                    FillType();
                }

                return _type;
            }
        }


        /// <summary>
        /// The case names
        /// </summary>
        private List<Tuple<string, double>> _caseNames;
        /// <summary>
        /// Gets the case names.
        /// </summary>
        /// <value>The case names.</value>
        internal List<Tuple<string, double>> CaseNames
        {
            get
            {
                if (_caseNames == null)
                {
                    FillCaseNames();
                }

                return _caseNames;
            }
        }

        /// <summary>
        /// The cases
        /// </summary>
        private List<LoadTuple<LoadCase>> _cases;
        /// <summary>
        /// Gets the cases.
        /// </summary>
        /// <value>The cases.</value>
        public List<LoadTuple<LoadCase>> Cases
        {
            get
            {
                if (_cases == null)
                {
                    FillCases();
                }

                return _cases;
            }
        }


        /// <summary>
        /// The combination names
        /// </summary>
        private List<Tuple<string, double>> _combinationNames;
        /// <summary>
        /// Gets the combination names.
        /// </summary>
        /// <value>The combination names.</value>
        internal List<Tuple<string, double>> CombinationNames
        {
            get
            {
                if (_combinationNames == null)
                {
                    FillCaseNames();
                }

                return _combinationNames;
            }
        }

        /// <summary>
        /// The combinations
        /// </summary>
        private List<LoadTuple<LoadCombination>> _combinations;
        /// <summary>
        /// Gets or sets the combinations.
        /// </summary>
        /// <value>The combinations.</value>
        public List<LoadTuple<LoadCombination>> Combinations
        {
            get
            {
                if (_combinations == null)
                {
                    FillCases();
                }

                return _combinations;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this combination is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; private set; }

        /// <summary>
        /// True: Default steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design steel]; otherwise, <c>false</c>.</value>
        public bool DesignSteel { get; private set; }

        /// <summary>
        /// True: Default concrete design combinations are to be added to the model..
        /// </summary>
        /// <value><c>true</c> if [design concrete]; otherwise, <c>false</c>.</value>
        public bool DesignConcrete { get; private set; }

        /// <summary>
        /// True: Default aluminum design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design aluminum]; otherwise, <c>false</c>.</value>
        public bool DesignAluminum { get; private set; }

        /// <summary>
        /// True: Default cold-formed steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design cold formed]; otherwise, <c>false</c>.</value>
        public bool DesignColdFormed { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load combination class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="uniqueName">Unique load combination name.</param>
        /// <returns>Steel.</returns>
        internal static LoadCombination Factory(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            string uniqueName)
        {
            LoadCombination loadCombination = new LoadCombination(app, analyzer, loadCases, loadCombinations, uniqueName);
            loadCombination.FillData();

            return loadCombination;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombination" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="name">The name.</param>
        private LoadCombination(
            ApiCSiApplication app, 
            Analyzer analyzer, 
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            string name) : base(app, name)
        {
            _analyzer = analyzer;
            _loadCases = loadCases;
            _loadCombinations = loadCombinations;
        }

        /// <summary>
        /// Adds a new load combination.
        /// The new load combination must have a different name from all other load combinations and all load cases.
        /// If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="uniqueNameLoadCombo">The name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        /// <returns>LoadCombination.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static LoadCombination Add(
            ApiCSiApplication app,
            Analyzer analyzer,
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            string uniqueNameLoadCombo,
            eLoadComboType comboType)
        {
            ApiLoadCombination apiLoadCombinations = getApiLoadCombination(app);
            apiLoadCombinations.Add(uniqueNameLoadCombo, comboType);
            return Factory(app, analyzer, loadCases, loadCombinations, uniqueNameLoadCombo);
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillCases();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillNote();
#endif
        }

        #endregion

        #region Fill/Set
        /// <summary>
        /// Gets the cases.
        /// </summary>
        public void FillCases()
        {
            _cases = new List<LoadTuple<LoadCase>>();
            foreach (var caseName in CaseNames)
            {
                LoadCase loadCase = _loadCases.FillItem(caseName.Item1);
                _cases.Add(new LoadTuple<LoadCase>(loadCase, caseName.Item2));
            }

            _combinations = new List<LoadTuple<LoadCombination>>();
            foreach (var combinationName in CombinationNames)
            {
                LoadCombination loadCombo = _loadCombinations.FillItem(combinationName.Item1);
                _combinations.Add(new LoadTuple<LoadCombination>(loadCombo, combinationName.Item2));
            }
        }

        /// <summary>
        /// Fills the case names.
        /// </summary>
        internal void FillCaseNames()
        {
            GetCaseNameLists(_apiLoadCombinations, Name, out var caseNames, out var combinationNames);
            _caseNames = caseNames;
            _combinationNames = combinationNames;
        }


        /// <summary>
        /// Adds or modifies one load case or response combination in the list of cases included in the load combination.
        /// </summary>
        /// <param name="loadCase">The load case included within the load combination.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCaseList(
            LoadCase loadCase,
            double scaleFactor)
        {
            _apiLoadCombinations.SetCaseList(Name, eCaseComboType.LoadCase, loadCase.Name, scaleFactor);
            if (Cases.Any(c => c.Load == loadCase))
            {
                var cases = Cases.Where(c => c.Load == loadCase);
                foreach (var loadTuple in cases)
                {
                    loadTuple.Scale = scaleFactor;
                }
            }
            else
            {
                Cases.Add(new LoadTuple<LoadCase>(loadCase, scaleFactor));
            }
        }

        /// <summary>
        /// Adds or modifies one response combination in the list of cases included in the load combination.
        /// </summary>
        /// <param name="loadCombination">The load combination included within the load combination.</param>
        /// <param name="scaleFactor">The scale factor multiplying the combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetComboList(
            LoadCombination loadCombination,
            double scaleFactor)
        {
            _apiLoadCombinations.SetCaseList(Name, eCaseComboType.LoadCombo, loadCombination.Name, scaleFactor);
            if (Combinations.Any(c => c.Load == loadCombination))
            {
                var combinations = new List<LoadTuple<LoadCombination>>(Combinations.Where(c => c.Load == loadCombination));
                combinations[0].Scale = scaleFactor;
            }
            else
            {
                Combinations.Add(new LoadTuple<LoadCombination>(loadCombination, scaleFactor));
            }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
/// <summary>
/// Returns the user note for specified response combination. The note may be blank.
/// </summary>
/// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillNote()
        {
            Note = _apiLoadCombinations?.GetNote(Name);
        }

        /// <summary>
        /// Sets the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNote(string note)
        {
            _apiLoadCombinations?.SetNote(Name, note);
        }
#endif

        /// <summary>
        /// Gets the type.
        /// </summary>
        public void FillType()
        {
            if (_apiLoadCombinations == null) return;
            _type = _apiLoadCombinations.GetType(Name);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the combination type for specified load combination.
        /// </summary>
        /// <param name="comboType">The load combination type of the specified load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetType(eLoadComboType comboType)
        {
            _apiLoadCombinations?.SetType(Name, comboType);
            LoadType = comboType;
        }
#endif
        #endregion

        #region CRUD


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Change name of load combination.
        /// The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="newName">The new name for the combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            // TODO: SAP2000 - Handle this: The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
            _apiLoadCombinations?.ChangeName(Name, newName);
            Name = newName;
        }
#else
        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ChangeName(string newName)
        {
            throw new NotImplementedException();
        }
#endif

        /// <summary>
        /// Deletes the specified load combination.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal override void Delete()
        {
            _apiLoadCombinations?.Delete(Name);
        }


        /// <summary>
        /// Removes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCase">The load case included within the load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveCase(LoadCase loadCase)
        {
            _apiLoadCombinations?.DeleteCase(Name, eCaseComboType.LoadCase, loadCase.Name);
            CaseNames.Remove(CaseNames.FirstOrDefault(o => o.Item1 == loadCase.Name));
            Cases.Remove(Cases.FirstOrDefault(o => o.Load == loadCase));
        }

        /// <summary>
        /// Removes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCombination">The load combination included within the load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveCombo(LoadCombination loadCombination)
        {
            _apiLoadCombinations?.DeleteCase(Name, eCaseComboType.LoadCombo, loadCombination.Name);
            CombinationNames.Remove(CombinationNames.FirstOrDefault(o => o.Item1 == loadCombination.Name));
            Combinations.Remove(Combinations.FirstOrDefault(o => o.Load == loadCombination));
        }

        /// <summary>
        /// Adds new default steel design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddDesignDefaultComboSteel()
        {
            DesignSteel = true;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default steel design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveDesignDefaultComboSteel()
        {
            DesignSteel = false;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default concrete design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddDesignDefaultComboConcrete()
        {
            DesignConcrete = true;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default concrete design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveDesignDefaultComboConcrete()
        {
            DesignConcrete = false;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default aluminum design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddDesignDefaultComboAluminum()
        {
            DesignAluminum = true;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default aluminum design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveDesignDefaultComboAluminum()
        {
            DesignAluminum = false;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default cold formed design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddDesignDefaultComboColdFormed()
        {
            DesignColdFormed = true;
            setDesignDefaultCombos();
        }

        /// <summary>
        /// Adds new default cold formed design load combinations to the model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveDesignDefaultComboColdFormed()
        {
            DesignColdFormed = false;
            setDesignDefaultCombos();
        }
        #endregion

        #region Selection
        /// <summary>
        /// Selects for analysis.
        /// </summary>
        public void SelectForAnalysis()
        {
            IsSelectedForAnalysis = true;
            _analyzer.SetComboSelectedForOutput(this);
        }

        /// <summary>
        /// Deselects for analysis.
        /// </summary>
        public void DeselectForAnalysis()
        {
            IsSelectedForAnalysis = false;
            _analyzer.SetComboSelectedForOutput(this);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the name list.
        /// </summary>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        internal static List<string> GetNameList(ApiLoadCombination loadCombinations)
        {
            return new List<string>(loadCombinations.GetNameList());
        }


        /// <summary>
        /// Gets the cases.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of the parent load combination.</param>
        /// <param name="caseNames">The case names.</param>
        /// <param name="combinationNames">The combination names.</param>
        public static void GetCaseNameLists(ApiLoadCombination app,
            string name,
            out List<Tuple<string, double>> caseNames,
            out List<Tuple<string, double>> combinationNames)
        {
            caseNames = new List<Tuple<string, double>>();
            combinationNames = new List<Tuple<string, double>>();
            if (app == null) return;

            app.GetCaseList(name,
                out var caseComboTypes,
                out var caseComboNames,
                out var scaleFactors);
            for (int i = 0; i < scaleFactors.Length; i++)
            {
                switch (caseComboTypes[i])
                {
                    case eCaseComboType.LoadCase:
                        caseNames.Add(new Tuple<string, double>(caseComboNames[i], scaleFactors[i]));
                        break;
                    case eCaseComboType.LoadCombo:
                        combinationNames.Add(new Tuple<string, double>(caseComboNames[i], scaleFactors[i]));
                        break;
                }
            }
        }
        #endregion

        #region Protected

        #endregion

        #region API Functions
        /// <summary>
        /// Adds new default design load combinations to the model.
        /// </summary>
        private void setDesignDefaultCombos()
        {
            _apiLoadCombinations?.AddDesignDefaultCombos(DesignSteel, DesignConcrete, DesignAluminum, DesignColdFormed);
        }
        #endregion
        
    }
}
