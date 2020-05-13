// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadCases.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadCase = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCases;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Class LoadCases.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{LoadCase}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class LoadCases : ObjectLists<LoadCase>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The load cases.</value>
        private ApiLoadCase _apiLoadCases => getApiLoadCase(_apiApp);

        /// <summary>
        /// The load patterns
        /// </summary>
        private readonly LoadPatterns _loadPatterns;

        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        private readonly Analyzer _analyzer;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCases" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        internal LoadCases(ApiCSiApplication app, Analyzer analyzer, LoadPatterns loadPatterns) : base(app)
        {
            _analyzer = analyzer;
            _loadPatterns = loadPatterns;
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddStaticLinear(string uniqueName)
        {
            return add(uniqueName, StaticLinear.Add);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddStaticNonlinear(string uniqueName)
        {
            return add(uniqueName, StaticNonlinear.Add);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddStaticNonlinearStaged(string uniqueName)
        {
            return add(uniqueName, StaticNonlinearStaged.Add);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddResponseSpectrum(string uniqueName)
        {
            return add(uniqueName, ResponseSpectrum.Add);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTimeHistoryDirectLinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryDirectLinear.Add);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTimeHistoryDirectNonlinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryDirectNonlinear.Add);
        }
#endif

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTimeHistoryModalLinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryModalLinear.Add);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTimeHistoryModalNonlinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryModalNonlinear.Add);
        }
#endif

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddModalEigen(string uniqueName)
        {
            return add(uniqueName, ModalEigen.Add);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddModalRitz(string uniqueName)
        {
            return add(uniqueName, ModalRitz.Add);
        }
#endif

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<ApiCSiApplication, Analyzer, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_apiApp, _analyzer, uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<ApiCSiApplication, Analyzer, LoadCases, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_apiApp, _analyzer, this, uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<ApiCSiApplication, Analyzer, LoadPatterns, LoadCases, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_apiApp, _analyzer, _loadPatterns, this, uniqueName));
            return true;
        }
        #endregion

        #region Fill
        /// <summary>
        /// Gets the item from the list, or fills it from the application if it doesn't yet exist.
        /// This item is further constrained to be a sub-type of the list type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        public T FillItem<T>(string uniqueName) where T : LoadCase
        {
            return (T) FillItem(uniqueName);
        }

        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override LoadCase fillNewItem(string uniqueName)
        {
            return LoadCase.Factory(_apiApp, _analyzer, _loadPatterns, this, uniqueName);
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return LoadCase.GetNameList(_apiLoadCases);
        }


        /// <summary>
        /// Returns the names of all defined load cases of the specified type.
        /// </summary>
        /// <param name="caseType">Load case type for which names are desired.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetNameList(eLoadCaseType caseType)
        {
            return LoadCase.GetNameList(_apiLoadCases, caseType);
        }
        #endregion
    }
}
