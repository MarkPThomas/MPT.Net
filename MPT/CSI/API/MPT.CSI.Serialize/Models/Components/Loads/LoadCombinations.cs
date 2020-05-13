// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadCombinations.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class LoadCombinations.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public sealed class LoadCombinations : ObjectLists<LoadCombination>
    {
        #region Fields & Properties

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        private readonly Analyzer _analyzer;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombinations" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        internal LoadCombinations(
            Analyzer analyzer,
            LoadCases loadCases)
        {
            _analyzer = analyzer;
            _loadCases = loadCases;
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a new load combination.
        /// </summary>
        /// <param name="uniqueNameLoadCombo">The unique name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(
            string uniqueNameLoadCombo,
            eLoadComboType comboType)
        {
            if (Contains(uniqueNameLoadCombo)) return false;
            _items.Add(LoadCombination.Factory(_analyzer, _loadCases, this, uniqueNameLoadCombo));
            return true;
        }

        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override LoadCombination fillNewItem(string uniqueName)
        {
            return LoadCombination.Factory(_analyzer, _loadCases, this, uniqueName);
        }
        #endregion
    }
}
