using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryDirectNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Nonlinear direct integration time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryDirectNonlinear : TimeHistoryDirectIntegration
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        protected static ApiTimeHistory _timeHistoryDirectNonlinear = _loadCases?.TimeHistoryDirectNonlinear;


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSourceHelper MassSource { get; protected set; }
#endif

        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static TimeHistoryDirectNonlinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (TimeHistoryDirectNonlinear)Registry.LoadCases[uniqueName];

            TimeHistoryDirectNonlinear loadCase = new TimeHistoryDirectNonlinear(uniqueName);
            if (_loadCases != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        public TimeHistoryDirectNonlinear(string name) : base(name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            InitialCase = new InitialCaseHelper(name, _timeHistoryDirectNonlinear);
            MassSource = new MassSourceHelper(name, _staticNonlinear);
#endif
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            MassSource.FillMassSource();
#endif
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _timeHistoryDirectNonlinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _timeHistoryDirectNonlinear?.SetCase(Name);
            FillData();
        }
#endif
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        public override void FillLoads()
        {
            fillLoads(_timeHistoryDirectNonlinear);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the loads.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public override void SetLoads(LoadsTimeHistory loads)
        {
        }

        public override void FillDampingProportional()
        {
            fillDampingProportional(_timeHistoryDirectNonlinear);
        }

        public override void SetDampingProportional()
        {
            setDampingProportional(_timeHistoryDirectNonlinear);
        }


        /// <summary>
        /// Returns the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSolutionControlParameters(string name,
            out double maxSubstepSize,
            out double minSubstepSize,
            out int maxConstantStiffnessIterationsPerStep,
            out int maxNewtonRaphsonIterationsPerStep,
            out double relativeIterationConvergenceTolerance,
            out bool useEventStepping,
            out double relativeEventLumpingTolerance,
            out int maxNumberLineSearches,
            out double relativeLineSearchAcceptanceTolerance,
            out double lineSearchStepFactor)
        {

        }

        /// <summary>
        /// Sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSolutionControlParameters(string name,
            double maxSubstepSize,
            double minSubstepSize,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {

        }


        /// <summary>
        /// Returns the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eGeometricNonlinearity FillGeometricNonlinearity)
        {

        }

        /// <summary>
        /// Sets the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGeometricNonlinearity(eGeometricNonlinearity geometricNonlinearityType)
        {

        }
#endif
        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

#endif
    }
}
