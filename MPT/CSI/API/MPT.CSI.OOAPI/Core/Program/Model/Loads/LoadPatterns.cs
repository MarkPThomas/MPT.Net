// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadPatterns.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadPattern = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPatterns;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Class LoadPatterns.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{LoadPattern}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class LoadPatterns : ObjectLists<LoadPattern>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The load pattern.</value>
        private ApiLoadPattern _apiLoadPattern => getApiLoadPattern(_apiApp);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPatterns"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal LoadPatterns(ApiCSiApplication app) : base(app)
        {
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a new load pattern.
        /// </summary>
        /// <param name="uniqueName">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        /// <param name="addLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(string uniqueName,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(LoadPattern.Add(_apiApp, uniqueName, loadPatternType, selfWeightMultiplier, addLoadCase));
            return true;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override LoadPattern fillNewItem(string uniqueName)
        {
            return LoadPattern.Factory(_apiApp, uniqueName);
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return LoadPattern.GetNameList(_apiLoadPattern);
        }
        #endregion
    }
}
