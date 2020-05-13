using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryModalNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Nonlinear modal time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryModalNonlinear : TimeHistory
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        protected static ApiTimeHistory _timeHistoryModalNonlinear = _loadCases?.TimeHistoryModalNonlinear;


        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static TimeHistoryModalNonlinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (TimeHistoryModalNonlinear)Registry.LoadCases[uniqueName];

            TimeHistoryModalNonlinear loadCase = new TimeHistoryModalNonlinear(uniqueName);
            if (_loadCases != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        public TimeHistoryModalNonlinear(string name) : base(name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            ModalCase = new ModalCaseHelper(name, _timeHistoryModalNonlinear, _timeHistoryModalNonlinear);
            Damping = new DampingHelper(name, _timeHistoryModalNonlinear);
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
            _timeHistoryModalNonlinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _timeHistoryModalNonlinear?.SetCase(Name);
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
            fillLoads(_timeHistoryModalNonlinear);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the loads.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public override void SetLoads(LoadsTimeHistory loads)
        {
        }

        /// <summary>
        /// Returns the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSolutionControlParameters(
            out double staticPeriod,
            out double maxSubstepSize,
            out double minSubstepSize,
            out double relativeForceConvergenceTolerance,
            out double relativeEnergyConvergenceTolerance,
            out int maxIterationLimit,
            out int minIterationLimit,
            out double convergenceFactor)
        {
        // TODO: Create SolutionControlParametersTimeHistoryModal
        }

        /// <summary>
        /// Sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSolutionControlParameters(
            double staticPeriod,
            double maxSubstepSize,
            double minSubstepSize,
            double relativeForceConvergenceTolerance,
            double relativeEnergyConvergenceTolerance,
            int maxIterationLimit,
            int minIterationLimit,
            double convergenceFactor)
        {

        }
#endif
        #endregion

        #region Damping
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
// TODO: SAP2000 complete time history
#endif
        #endregion
    }
}
