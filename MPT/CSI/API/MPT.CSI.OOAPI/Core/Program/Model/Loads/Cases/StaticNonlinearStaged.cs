// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StaticNonlinearStaged.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiStaticNonlinearStaged = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticNonlinearStaged;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static nonlinear staged construction load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public sealed class StaticNonlinearStaged : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The static modal eigen API object.
        /// </summary>
        /// <value>The static nonlinear staged.</value>
        private ApiStaticNonlinearStaged _apiStaticNonlinearStaged => getApiLoadCase(_apiApp).StaticNonlinearStaged;

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

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
                _initialCase = InitialCaseHelper.Factory(_apiApp, _apiStaticNonlinearStaged, _loadCases, Name);

                return _initialCase;
            }
        }

        /// <summary>
        /// The mass source
        /// </summary>
        private MassSourceHelper _massSource;
        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSourceHelper MassSource
        {
            get
            {
                if (_massSource != null) return _massSource;
                _massSource = new MassSourceHelper(_apiApp, _apiStaticNonlinearStaged, Name);
                _massSource.FillMassSource();

                return _massSource;
            }
        }

        /// <summary>
        /// The nonlinear settings
        /// </summary>
        private NonlinearSettingsHelper<ApiStaticNonlinearStaged> _nonlinearSettings;
        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public NonlinearSettingsHelper<ApiStaticNonlinearStaged> NonlinearSettings
        {
            get
            {
                if (_nonlinearSettings != null) return _nonlinearSettings;
                _nonlinearSettings = new NonlinearSettingsHelper<ApiStaticNonlinearStaged>(_apiStaticNonlinearStaged, Name);
                _nonlinearSettings.FillData();

                return _nonlinearSettings;
            }
        }

        /// <summary>
        /// The results saved
        /// </summary>
        private StageResultsSaved _resultsSaved;
        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        public StageResultsSaved ResultsSaved
        {
            get
            {
                if (_resultsSaved == null)
                {
                    FillResultsSaved();
                }

                return _resultsSaved;
            }
        }

        /// <summary>
        /// The stage definitions
        /// </summary>
        private StageDefinitions _stageDefinitions;
        /// <summary>
        /// The stage definitions associated with the load case.
        /// </summary>
        /// <value>The stage definitions.</value>
        public StageDefinitions StageDefinitions
        {
            get
            {
                if (_stageDefinitions != null) return _stageDefinitions;
                _stageDefinitions = new StageDefinitions(_apiApp, _apiStaticNonlinearStaged, Name);
                _stageDefinitions.FillData();

                return _stageDefinitions;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static StaticNonlinearStaged Factory(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases, 
            string uniqueName)
        {
            StaticNonlinearStaged loadCase = new StaticNonlinearStaged(app, analyzer, loadCases, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinearStaged" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        private StaticNonlinearStaged(
            ApiCSiApplication app, 
            Analyzer analyzer, 
            LoadCases loadCases,
            string name) 
            : base(app, analyzer, name)
        {
            _loadCases = loadCases;
        }

        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static StaticNonlinearStaged Add(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases,
            string uniqueName)
        {
            ApiStaticNonlinearStaged apiStaticNonlinearStaged = getApiLoadCase(app).StaticNonlinearStaged;
            apiStaticNonlinearStaged?.SetCase(uniqueName);
            return Factory(app, analyzer, loadCases, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiStaticNonlinearStaged?.SetCase(Name);
            FillData();
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the results saved parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillResultsSaved()
        {
            if (_apiStaticNonlinearStaged == null) return;
            _apiStaticNonlinearStaged.GetResultsSaved(Name,
                out var stageSavedOption,
                out var minStepsForInstantanousLoad,
                out var minStepsForTimeDependentItems);

            _resultsSaved = new StageResultsSaved
            {
                StageSavedOption = stageSavedOption,
                MinStepsForInstantanousLoad = minStepsForInstantanousLoad,
                MinStepsForTimeDependentItems = minStepsForTimeDependentItems
            };
        }

        /// <summary>
        /// Sets the results saved parameters for the analysis case.
        /// </summary>
        /// <param name="resultsSaved">The results saved.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultsSaved(StageResultsSaved resultsSaved)
        {
            _apiStaticNonlinearStaged?.SetResultsSaved(Name,
                resultsSaved.StageSavedOption,
                resultsSaved.MinStepsForInstantanousLoad,
                resultsSaved.MinStepsForTimeDependentItems);
            _resultsSaved = resultsSaved;
        }
        #endregion
    }
}
