// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryDirectNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryDirectNonlinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Nonlinear direct integration time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistoryDirectIntegration" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryDirectNonlinear : TimeHistoryDirectIntegration
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        /// <value>The time history direct nonlinear.</value>
        protected ApiTimeHistory _apiTimeHistoryDirectNonlinear => getApiLoadCase(_apiApp).TimeHistoryDirectNonlinear;
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
                _massSource = new MassSourceHelper(_apiApp, _apiTimeHistoryDirectNonlinear, Name);
                _massSource.FillMassSource();

                return _massSource;
            }
        }
#endif
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryDirectNonlinear Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            TimeHistoryDirectNonlinear loadCase = new TimeHistoryDirectNonlinear(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectNonlinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryDirectNonlinear(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, analyzer, name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _apiInitialLoadCase = _apiTimeHistoryDirectNonlinear;
            _apiDampingProportional = _apiTimeHistoryDirectNonlinear;
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
            FillSolutionControlParameters();
            FillGeometricNonlinearity();
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
        internal static TimeHistoryDirectNonlinear Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiTimeHistory apiTimeHistoryDirectNonlinear = getApiLoadCase(app).TimeHistoryDirectNonlinear;
            apiTimeHistoryDirectNonlinear?.SetCase(uniqueName);
            return Factory(app, analyzer, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiTimeHistoryDirectNonlinear?.SetCase(Name);
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
            fillLoads(_apiTimeHistoryDirectNonlinear);
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
            fillDampingProportional(_apiTimeHistoryDirectNonlinear);
        }

        public override void SetDampingProportional()
        {
            setDampingProportional(_apiTimeHistoryDirectNonlinear);
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
        public void FillSolutionControlParameters(string name,
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
