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
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load combination.
    /// </summary>
    public class LoadCombination : CSiOOAPiName
    {
        /// <summary>
        /// Gets the load combination API object.
        /// </summary>
        /// <value>The load combinations.</value>
        protected static LoadCombinations _loadCombinations => Registry.ProgramDefinitions?.LoadCombinations;


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public string Note { get; set; }
#endif

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadComboType Type { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this combination is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; protected set; }

        /// <summary>
        /// Gets or sets the cases.
        /// </summary>
        /// <value>The cases.</value>
        public List<LoadTuple<LoadCase>> Cases { get; protected set; }
        
        /// <summary>
        /// Gets or sets the combinations.
        /// </summary>
        /// <value>The combinations.</value>
        public List<LoadTuple<LoadCombination>> Combinations { get; protected set; }

        /// <summary>
        /// True: Default steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design steel]; otherwise, <c>false</c>.</value>
        public bool DesignSteel { get; protected set; }

        /// <summary>
        /// True: Default concrete design combinations are to be added to the model..
        /// </summary>
        /// <value><c>true</c> if [design concrete]; otherwise, <c>false</c>.</value>
        public bool DesignConcrete { get; protected set; }

        /// <summary>
        /// True: Default aluminum design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design aluminum]; otherwise, <c>false</c>.</value>
        public bool DesignAluminum { get; protected set; }

        /// <summary>
        /// True: Default cold-formed steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design cold formed]; otherwise, <c>false</c>.</value>
        public bool DesignColdFormed { get; protected set; }


        /// <summary>
        /// Gets all load combinations.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<LoadCombination> GetAll()
        {
            List<LoadCombination> loadCombinations = new List<LoadCombination>();
            List<string> loadCombinationNames = GetNameList();
            foreach (var loadCombinationName in loadCombinationNames)
            {
                LoadCombination loadCombination = Factory(loadCombinationName);

                loadCombinations.Add(loadCombination);
            }

            return loadCombinations;
        }

        /// <summary>
        /// Returns a new load combination class.
        /// </summary>
        /// <param name="uniqueName">Unique load combination name.</param>
        /// <returns>Steel.</returns>
        public static LoadCombination Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return Registry.LoadCombinations[uniqueName];

            LoadCombination loadCombination = new LoadCombination(uniqueName);
            if (_loadCombinations != null)
            {
                loadCombination.FillData();
            }
            Registry.LoadCombinations.Add(uniqueName, loadCombination);
            return loadCombination;
        }


        /// <summary>
        /// Gets the name list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetNameList()
        {
            return new List<string>(_loadCombinations.GetNameList());
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombination"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LoadCombination(string name) : base(name) { }


        /// <summary>
        /// Selects for analysis.
        /// </summary>
        public void SelectForAnalysis()
        {
            IsSelectedForAnalysis = true;
            Registry.Analyzer.SetComboSelectedForOutput(this);
        }

        /// <summary>
        /// Deselects for analysis.
        /// </summary>
        public void DeselectForAnalysis()
        {
            IsSelectedForAnalysis = false;
            Registry.Analyzer.SetComboSelectedForOutput(this);
        }
        
        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillType();
            FillCases();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillNote();
#endif
        }

        public override void ChangeName(string newName)
        {
            throw new System.NotImplementedException();
        }

        // TODO: Work into factory
        /// <summary>
        /// Adds a new load combination.
        /// The new load combination must have a different name from all other load combinations and all load cases.
        /// If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(string nameLoadCombo,
            eLoadComboType comboType)
        {
            // TODO: Handle this: If the name is not unique, an error will be returned.
            _loadCombinations?.Add(nameLoadCombo, comboType);
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

        /// <summary>
        /// Adds new default design load combinations to the model.
        /// </summary>
        protected void setDesignDefaultCombos()
        {
            _loadCombinations?.AddDesignDefaultCombos(DesignSteel, DesignConcrete, DesignAluminum, DesignColdFormed);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Change name of load combination.
        /// The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="newName">The new name for the combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            // TODO: Handle this: The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
            _loadCombinations?.ChangeName(Name, newName);
            Name = newName;
        }
#endif

        /// <summary>
        /// Deletes the specified load combination.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public override void Delete()
        {
            _loadCombinations?.Delete(Name);
        }
        
        /// <summary>
        /// Gets the cases.
        /// </summary>
        public void FillCases()
        {
            if (_loadCombinations == null) return;
            _loadCombinations.GetCaseList(Name,
                out var caseComboTypes,
                out var caseComboNames,
                out var scaleFactors);

            for (int i = 0; i < scaleFactors.Length; i++)
            {
                switch (caseComboTypes[i])
                {
                    case eCaseComboType.LoadCase:
                        LoadCase loadCase = LoadCase.Factory(caseComboNames[i]);
                        Cases.Add(new LoadTuple<LoadCase>(loadCase, scaleFactors[i]));
                        break;
                    case eCaseComboType.LoadCombo:
                        LoadCombination loadCombo = Factory(caseComboNames[i]);
                        Combinations.Add(new LoadTuple<LoadCombination>(loadCombo, scaleFactors[i]));
                        break;
                    default:
                        break;
                }
            }
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
            _loadCombinations.SetCaseList(Name, eCaseComboType.LoadCase, loadCase.Name, scaleFactor);
            if (Cases.Any(c => c.Load == loadCase))
            {
                var cases = new List<LoadTuple<LoadCase>>(Cases.Where(c => c.Load == loadCase));
                cases[0].Scale = scaleFactor;
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
            _loadCombinations.SetCaseList(Name, eCaseComboType.LoadCombo, loadCombination.Name, scaleFactor);
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

        /// <summary>
        /// Deletes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCase">The load case included within the load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteCase(LoadCase loadCase)
        {
            _loadCombinations?.DeleteCase(Name, eCaseComboType.LoadCase, loadCase.Name);
        }

        /// <summary>
        /// Deletes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCombination">The load combination included within the load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteCombo(LoadCombination loadCombination)
        {
            _loadCombinations?.DeleteCase(Name, eCaseComboType.LoadCombo, loadCombination.Name);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillNote()
        {
            Note = _loadCombinations?.GetNote(Name);
        }

        /// <summary>
        /// Sets the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNote(string note)
        {
            _loadCombinations?.SetNote(Name, note);
        }
#endif

        /// <summary>
        /// Gets the type.
        /// </summary>
        public void FillType()
        {
            if (_loadCombinations == null) return;
            Type = _loadCombinations.GetType(Name);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the combination type for specified load combination.
        /// </summary>
        /// <param name="comboType">The load combination type of the specified load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetType(eLoadComboType comboType)
        {
            _loadCombinations?.SetType(Name, comboType);
            LoadType = comboType;
        }
#endif
    }
}
