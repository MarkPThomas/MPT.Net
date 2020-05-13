// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryModalNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryModalNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Nonlinear modal time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistory" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryModalNonlinear : TimeHistoryModal
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        /// <value>The time history modal nonlinear.</value>
        private ApiTimeHistory _apiTimeHistoryModalNonlinear => getApiLoadCase(_apiApp).TimeHistoryModalNonlinear;
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryModalNonlinear Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            TimeHistoryModalNonlinear loadCase = new TimeHistoryModalNonlinear(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModalNonlinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryModalNonlinear(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, analyzer, name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _apiModal = _apiTimeHistoryModalNonlinear;
            _apiDampingModal = _apiTimeHistoryModalNonlinear;
#endif
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillSolutionControlParameters();
#endif
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static TimeHistoryModalNonlinear Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiTimeHistory apiTimeHistoryModalNonlinear = getApiLoadCase(app).TimeHistoryModalNonlinear;
            apiTimeHistoryModalNonlinear?.SetCase(uniqueName);
            return Factory(app, analyzer, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiTimeHistoryModalNonlinear?.SetCase(Name);
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
            fillLoads(_apiTimeHistoryModalNonlinear);
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
