// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StaticNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiStaticNonlinear = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static nonlinear load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public sealed class StaticNonlinear : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The static nonlinear API object.
        /// </summary>
        /// <value>The static nonlinear.</value>
        private ApiStaticNonlinear _apiStaticNonlinear => getApiLoadCase(_apiApp).StaticNonlinear;

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
                _loads = new LoadsAppliedHelper(Name, _apiStaticNonlinear, _apiApp, _loadPatterns);

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
                _initialCase = InitialCaseHelper.Factory(_apiApp, _apiStaticNonlinear, _loadCases, Name);

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
                _massSource = new MassSourceHelper(_apiApp, _apiStaticNonlinear, Name);
                _massSource.FillMassSource();

                return _massSource;
            }
        }

        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        private ModalCaseHelper _modalCase;
        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase
        {
            get
            {
                if (_modalCase != null) return _modalCase;
                _modalCase = ModalCaseHelper.Factory(Name, _apiStaticNonlinear, _apiApp, _loadCases, _apiStaticNonlinear);
                _modalCase.FillModalCase();

                return _modalCase;
            }
        }

        /// <summary>
        /// The nonlinear settings
        /// </summary>
        private NonlinearSettingsHelper<ApiStaticNonlinear> _nonlinearSettings;
        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public NonlinearSettingsHelper<ApiStaticNonlinear> NonlinearSettings
        {
            get
            {
                if (_nonlinearSettings != null) return _nonlinearSettings;
                _nonlinearSettings = new NonlinearSettingsHelper<ApiStaticNonlinear>(_apiStaticNonlinear, Name);
                _nonlinearSettings.FillData();

                return _nonlinearSettings;
            }
        }

        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        private ResultsSaved _resultsSaved;
        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        public ResultsSaved ResultsSaved
        {
            get
            {
                if (_resultsSaved != null) return _resultsSaved;
                FillResultsSaved();

                return _resultsSaved;
            }
        }

        /// <summary>
        /// The load application associated with the load case.
        /// </summary>
        /// <value>The load application.</value>
        private LoadApplication _loadApplication;
        /// <summary>
        /// The load application associated with the load case.
        /// </summary>
        /// <value>The load application.</value>
        public LoadApplication LoadApplication
        {
            get
            {
                if (_loadApplication != null) return _loadApplication;
                FillLoadApplication();

                return _loadApplication;
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
        internal new static StaticNonlinear Factory(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            StaticNonlinear loadCase = new StaticNonlinear(app, analyzer, loadPatterns, loadCases, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        private StaticNonlinear(
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
        /// <returns>StaticNonlinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static StaticNonlinear Add(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            ApiStaticNonlinear apiStaticNonlinear = getApiLoadCase(app).StaticNonlinear;
            apiStaticNonlinear?.SetCase(uniqueName);
            return Factory(app, analyzer, loadPatterns, loadCases, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiStaticNonlinear?.SetCase(Name);
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
            if (_apiStaticNonlinear == null) return;
            _apiStaticNonlinear.GetResultsSaved(Name,
                out var saveMultipleSteps,
                out var minSavedStates,
                out var maxSavedStates,
                out var savePositiveDisplacementIncrementsOnly);

            _resultsSaved = new ResultsSaved
            {
                SaveMultipleSteps = saveMultipleSteps,
                MinSavedStates = minSavedStates,
                MaxSavedStates = maxSavedStates,
                SavePositiveDisplacementIncrementsOnly = savePositiveDisplacementIncrementsOnly
            };
        }

        /// <summary>
        /// Sets the results saved parameters for the analysis case.
        /// </summary>
        /// <param name="resultsSaved">The results saved.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultsSaved(ResultsSaved resultsSaved)
        {
            _apiStaticNonlinear?.SetResultsSaved(Name,
                resultsSaved.SaveMultipleSteps,
                resultsSaved.MinSavedStates,
                resultsSaved.MaxSavedStates,
                resultsSaved.SavePositiveDisplacementIncrementsOnly);
            _resultsSaved = resultsSaved;
        }

        /// <summary>
        /// Retrieves the load application control parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadApplication()
        {
            if (_apiStaticNonlinear == null) return;
            _apiStaticNonlinear.GetLoadApplication(Name,
                out var loadControl,
                out var controlDisplacementType,
                out var targetDisplacement,
                out var monitoredDisplacementType,
                out var degreeOfFreedom,
                out var namePoint,
                out var nameGeneralizedDisplacement);

            _loadApplication = new LoadApplication
            {
                LoadControl = loadControl,
                ControlDisplacementType = controlDisplacementType,
                TargetDisplacement = targetDisplacement,
                MonitoredDisplacementType = monitoredDisplacementType,
                DegreeOfFreedom = degreeOfFreedom,
                NamePoint = namePoint,
                NameGeneralizedDisplacement = nameGeneralizedDisplacement
            };
        }

        /// <summary>
        /// Sets the load application control parameters for the analysis case.
        /// </summary>
        /// <param name="loadApplication">The load application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadApplication(LoadApplication loadApplication)
        {
            _apiStaticNonlinear?.SetLoadApplication(Name,
                loadApplication.LoadControl,
                loadApplication.ControlDisplacementType,
                loadApplication.TargetDisplacement,
                loadApplication.MonitoredDisplacementType,
                loadApplication.DegreeOfFreedom,
                loadApplication.NamePoint,
                loadApplication.NameGeneralizedDisplacement);
            _loadApplication = loadApplication;
        }
        #endregion
    }
}
