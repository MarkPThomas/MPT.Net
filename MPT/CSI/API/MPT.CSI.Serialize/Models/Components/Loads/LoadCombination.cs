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
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Represents a load combination.
    /// </summary>
    /// <seealso cref="UniqueName" />
    public sealed class LoadCombination : UniqueName
    {
        #region Fields & Properties
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

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadComboType Type { get; internal set; }

        /// <summary>
        /// Gets the case names.
        /// </summary>
        /// <value>The case names.</value>
        internal List<Tuple<string, double>> CaseNames { get; set; }

        /// <summary>
        /// Gets the cases.
        /// </summary>
        /// <value>The cases.</value>
        public List<LoadTuple<LoadCase>> Cases { get; internal set; }

        /// <summary>
        /// Gets the combination names.
        /// </summary>
        /// <value>The combination names.</value>
        internal List<Tuple<string, double>> CombinationNames { get; set; }
        
        /// <summary>
        /// Gets or sets the combinations.
        /// </summary>
        /// <value>The combinations.</value>
        public List<LoadTuple<LoadCombination>> Combinations { get; internal set; }


        /// <summary>
        /// Gets or sets a value indicating whether this combination is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; internal set; }

        /// <summary>
        /// True: Default steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design steel]; otherwise, <c>false</c>.</value>
        public bool DesignSteel { get; internal set; }

        /// <summary>
        /// True: Default concrete design combinations are to be added to the model..
        /// </summary>
        /// <value><c>true</c> if [design concrete]; otherwise, <c>false</c>.</value>
        public bool DesignConcrete { get; internal set; }

        /// <summary>
        /// True: Default aluminum design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design aluminum]; otherwise, <c>false</c>.</value>
        public bool DesignAluminum { get; internal set; }

        /// <summary>
        /// True: Default cold-formed steel design combinations are to be added to the model.
        /// </summary>
        /// <value><c>true</c> if [design cold formed]; otherwise, <c>false</c>.</value>
        public bool DesignColdFormed { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load combination class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="uniqueName">Unique load combination name.</param>
        /// <returns>Steel.</returns>
        internal static LoadCombination Factory(
            Analyzer analyzer,
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            string uniqueName)
        {
            LoadCombination loadCombination = new LoadCombination(analyzer, loadCases, loadCombinations, uniqueName);

            return loadCombination;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombination" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="name">The name.</param>
        private LoadCombination(
            Analyzer analyzer, 
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            string name) : base(name)
        {
            _analyzer = analyzer;
            _loadCases = loadCases;
            _loadCombinations = loadCombinations;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Adds or modifies one load case or response combination in the list of cases included in the load combination.
        /// </summary>
        /// <param name="loadCase">The load case included within the load combination.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case.</param>
        public void SetCaseList(
            LoadCase loadCase,
            double scaleFactor)
        {
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
        public void SetComboList(
            LoadCombination loadCombination,
            double scaleFactor)
        {
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
        
        #endregion

        #region CRUD
        /// <summary>
        /// Removes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCase">The load case included within the load combination.</param>
        public void RemoveCase(LoadCase loadCase)
        {
            CaseNames.Remove(CaseNames.FirstOrDefault(o => o.Item1 == loadCase.Name));
            Cases.Remove(Cases.FirstOrDefault(o => o.Load == loadCase));
        }

        /// <summary>
        /// Removes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="loadCombination">The load combination included within the load combination.</param>
        public void RemoveCombo(LoadCombination loadCombination)
        {
            CombinationNames.Remove(CombinationNames.FirstOrDefault(o => o.Item1 == loadCombination.Name));
            Combinations.Remove(Combinations.FirstOrDefault(o => o.Load == loadCombination));
        }

        /// <summary>
        /// Adds new default steel design load combinations to the model.
        /// </summary>
        public void AddDesignDefaultComboSteel()
        {
            DesignSteel = true;
        }

        /// <summary>
        /// Adds new default steel design load combinations to the model.
        /// </summary>
        public void RemoveDesignDefaultComboSteel()
        {
            DesignSteel = false;
        }

        /// <summary>
        /// Adds new default concrete design load combinations to the model.
        /// </summary>
        public void AddDesignDefaultComboConcrete()
        {
            DesignConcrete = true;
        }

        /// <summary>
        /// Adds new default concrete design load combinations to the model.
        /// </summary>
        public void RemoveDesignDefaultComboConcrete()
        {
            DesignConcrete = false;
        }

        /// <summary>
        /// Adds new default aluminum design load combinations to the model.
        /// </summary>
        public void AddDesignDefaultComboAluminum()
        {
            DesignAluminum = true;
        }

        /// <summary>
        /// Adds new default aluminum design load combinations to the model.
        /// </summary>
        public void RemoveDesignDefaultComboAluminum()
        {
            DesignAluminum = false;
        }

        /// <summary>
        /// Adds new default cold formed design load combinations to the model.
        /// </summary>
        public void AddDesignDefaultComboColdFormed()
        {
            DesignColdFormed = true;
        }

        /// <summary>
        /// Adds new default cold formed design load combinations to the model.
        /// </summary>
        public void RemoveDesignDefaultComboColdFormed()
        {
            DesignColdFormed = false;
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
    }
}
