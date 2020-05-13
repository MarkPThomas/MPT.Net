// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="StaticLinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiStaticLinear = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static linear load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    /// <seealso cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public sealed class StaticLinear : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The linear static API object.
        /// </summary>
        /// <value>The static linear.</value>
        private ApiStaticLinear _apiStaticLinear => getApiLoadCase(_apiApp).StaticLinear;

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The load patterns
        /// </summary>
        private readonly LoadPatterns _loadPatterns;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        /// <value>The loads.</value>
        private LoadsAppliedHelper _loads;
        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        /// <value>The loads.</value>
        public LoadsAppliedHelper Loads
        {
            get
            {
                if (_loads != null) return _loads;
                _loads = new LoadsAppliedHelper(Name, _apiStaticLinear, _apiApp, _loadPatterns);
                _loads.FillLoads();

                return _loads;
            }
        }

        /// <summary>
        /// The initial case
        /// </summary>
        private InitialCaseHelper _initialCase;
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase
        {
            get
            {
                if (_initialCase != null) return _initialCase;
                _initialCase = InitialCaseHelper.Factory(_apiApp, _apiStaticLinear, _loadCases, Name);

                return _initialCase;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal new static StaticLinear Factory(
            ApiCSiApplication app,
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            StaticLinear loadCase = new StaticLinear(app, analyzer, loadPatterns, loadCases, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticLinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        private StaticLinear(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string name) 
            : base(app, analyzer, name)
        {
            _loadPatterns = loadPatterns;
            _loadCases = loadCases;
        }

        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static StaticLinear Add(
            ApiCSiApplication app, 
            Analyzer analyzer, 
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            ApiStaticLinear apiStaticLinear = getApiLoadCase(app).StaticLinear;
            apiStaticLinear?.SetCase(uniqueName);
            return Factory(app, analyzer, loadPatterns, loadCases, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiStaticLinear?.SetCase(Name);
            FillData();
        }
        #endregion
    }
}
