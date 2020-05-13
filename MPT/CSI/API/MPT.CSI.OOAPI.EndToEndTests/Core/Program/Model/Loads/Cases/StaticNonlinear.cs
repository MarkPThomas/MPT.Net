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
using System.Linq;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiStaticNonlinear = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static nonlinear load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class StaticNonlinear : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The static nonlinear API object.
        /// </summary>
        protected static ApiStaticNonlinear _staticNonlinear = _loadCases?.StaticNonlinear;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        /// <value>The loads.</value>
        public LoadsAppliedHelper Loads { get; protected set; }

        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase { get; protected set; }

        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSourceHelper MassSource { get; protected set; }

        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase { get; protected set; }

        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public NonlinearSettingsHelper<ApiStaticNonlinear> NonlinearSettings { get; protected set; }

        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        public ResultsSaved ResultsSaved { get; protected set; } = new ResultsSaved();

        /// <summary>
        /// The load application associated with the load case.
        /// </summary>
        /// <value>The load application.</value>
        public LoadApplication LoadApplication { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static StaticNonlinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (StaticNonlinear)Registry.LoadCases[uniqueName];

            StaticNonlinear loadCase = new StaticNonlinear(uniqueName);
            if (_staticNonlinear != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinear" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        public StaticNonlinear(string name) : base(name)
        {
            Loads = new LoadsAppliedHelper(name, _staticNonlinear);
            InitialCase = new InitialCaseHelper(name, _staticNonlinear);
            MassSource = new MassSourceHelper(name, _staticNonlinear);
            ModalCase = new ModalCaseHelper(name, _staticNonlinear, _staticNonlinear);
            NonlinearSettings = new NonlinearSettingsHelper<ApiStaticNonlinear>(name, _staticNonlinear);
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
            Loads.FillLoads();
            InitialCase.FillInitialCase();
            MassSource.FillMassSource();
            ModalCase.FillModalCase();
            NonlinearSettings.FillData();
            FillResultsSaved();
            FillLoadApplication();
        }

        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _staticNonlinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _staticNonlinear?.SetCase(Name);
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
            if (_staticNonlinear == null) return;
            _staticNonlinear.GetResultsSaved(Name,
                out var saveMultipleSteps,
                out var minSavedStates,
                out var maxSavedStates,
                out var savePositiveDisplacementIncrementsOnly);

            ResultsSaved = new ResultsSaved
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
            _staticNonlinear?.SetResultsSaved(Name,
                resultsSaved.SaveMultipleSteps,
                resultsSaved.MinSavedStates,
                resultsSaved.MaxSavedStates,
                resultsSaved.SavePositiveDisplacementIncrementsOnly);
            ResultsSaved = resultsSaved;
        }

        /// <summary>
        /// Retrieves the load application control parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadApplication()
        {
            if (_staticNonlinear == null) return;
            _staticNonlinear.GetLoadApplication(Name,
                out var loadControl,
                out var controlDisplacementType,
                out var targetDisplacement,
                out var monitoredDisplacementType,
                out var degreeOfFreedom,
                out var namePoint,
                out var nameGeneralizedDisplacement);

            LoadApplication = new LoadApplication
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
            _staticNonlinear?.SetLoadApplication(Name,
                loadApplication.LoadControl,
                loadApplication.ControlDisplacementType,
                loadApplication.TargetDisplacement,
                loadApplication.MonitoredDisplacementType,
                loadApplication.DegreeOfFreedom,
                loadApplication.NamePoint,
                loadApplication.NameGeneralizedDisplacement);
            LoadApplication = loadApplication;
        }
        #endregion
    }
}
