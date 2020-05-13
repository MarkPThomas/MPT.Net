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
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadCombination = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCombinations;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Class LoadCombinations.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{LoadCombination}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public sealed class LoadCombinations : ObjectLists<LoadCombination>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The load combinations.</value>
        private ApiLoadCombination _apiLoadCombinations => getApiLoadCombination(_apiApp);

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
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        internal LoadCombinations(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases) : base(app)
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
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(
            string uniqueNameLoadCombo,
            eLoadComboType comboType)
        {
            if (Contains(uniqueNameLoadCombo)) return false;
            _items.Add(LoadCombination.Add(_apiApp, _analyzer, _loadCases, this, uniqueNameLoadCombo, comboType));
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
            return LoadCombination.Factory(_apiApp, _analyzer, _loadCases, this, uniqueName);
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return LoadCombination.GetNameList(_apiLoadCombinations);
        }
        #endregion
    }
}
