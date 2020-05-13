// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="NonlinearSettingsHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class NonlinearSettingsHelper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IFill" />
    public class NonlinearSettingsHelper<T>: IFill 
        where T : IGeometricNonlinearity, INonlinearSolutionControlParameters, ITargetForceParameters, IHingeUnloading
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        protected static T _app;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The geometric nonlinearity option selected for the load case.
        /// </summary>
        /// <value>The type of the geometric nonlinearity.</value>
        public eGeometricNonlinearity GeometricNonlinearityType { get; protected set; }

        /// <summary>
        /// LoadType of hinge unloading for the selected load case.
        /// </summary>
        /// <value>The type of the hinge unloading.</value>
        public eHingeUnloadingType HingeUnloadingType { get; protected set; }

        /// <summary>
        /// The solution control parameters
        /// </summary>
        private SolutionControlParameters _solutionControlParameters;
        /// <summary>
        /// Gets the solution control parameters.
        /// </summary>
        /// <value>The solution control parameters.</value>
        public SolutionControlParameters SolutionControlParameters => _solutionControlParameters;

        /// <summary>
        /// The target force parameters
        /// </summary>
        private TargetForceParameters _targetForceParameters;
        /// <summary>
        /// Gets the target force parameters.
        /// </summary>
        /// <value>The target force parameters.</value>
        public TargetForceParameters TargetForceParameters => _targetForceParameters;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="NonlinearSettingsHelper{T}"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="app">The application.</param>
        public NonlinearSettingsHelper(string caseName, T app) 
        {
            _app = app;
            CaseName = caseName;
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public void FillData()
        {
            FillGeometricNonlinearity();
            FillSolutionControlParameters();
            FillTargetForceParameters();
            FillHingeUnloading();
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the geometric nonlinearity option for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillGeometricNonlinearity()
        {
            if (_app == null) return;
            GeometricNonlinearityType = _app.GetGeometricNonlinearity(CaseName);
        }

        /// <summary>
        /// Sets the geometric nonlinearity option for the analysis case.
        /// </summary>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGeometricNonlinearity(eGeometricNonlinearity geometricNonlinearityType)
        {
            _app?.SetGeometricNonlinearity(CaseName, geometricNonlinearityType);
            GeometricNonlinearityType = geometricNonlinearityType;
        }



        /// <summary>
        /// Retrieves the solution control parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSolutionControlParameters()
        {
            if (_app == null) return;
            _app.GetSolutionControlParameters(CaseName,
                out var maxTotalSteps,
                out var maxNullSteps,
                out var maxConstantStiffnessIterationsPerStep,
                out var maxNewtonRaphsonIterationsPerStep,
                out var relativeIterationConvergenceTolerance,
                out var useEventStepping,
                out var relativeEventLumpingTolerance,
                out var maxNumberLineSearches,
                out var relativeLineSearchAcceptanceTolerance,
                out var lineSearchStepFactor);

            _solutionControlParameters.MaxTotalSteps = maxTotalSteps;
            _solutionControlParameters.MaxNullSteps = maxNullSteps;
            _solutionControlParameters.MaxConstantStiffnessIterationsPerStep = maxConstantStiffnessIterationsPerStep;
            _solutionControlParameters.MaxNewtonRaphsonIterationsPerStep = maxNewtonRaphsonIterationsPerStep;
            _solutionControlParameters.RelativeIterationConvergenceTolerance = relativeIterationConvergenceTolerance;
            _solutionControlParameters.UseEventStepping = useEventStepping;
            _solutionControlParameters.RelativeEventLumpingTolerance = relativeEventLumpingTolerance;
            _solutionControlParameters.MaxNumberLineSearches = maxNumberLineSearches;
            _solutionControlParameters.RelativeLineSearchAcceptanceTolerance = relativeLineSearchAcceptanceTolerance;
            _solutionControlParameters.LineSearchStepFactor = lineSearchStepFactor;
        }

        /// <summary>
        /// Sets the solution control parameters for the analysis case.
        /// </summary>
        /// <param name="solutionControlParameters">The solution control parameters.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSolutionControlParameters(SolutionControlParameters solutionControlParameters)
        {
            _app?.SetSolutionControlParameters(CaseName,
                solutionControlParameters.MaxTotalSteps,
                solutionControlParameters.MaxNullSteps,
                solutionControlParameters.MaxConstantStiffnessIterationsPerStep,
                solutionControlParameters.MaxNewtonRaphsonIterationsPerStep,
                solutionControlParameters.RelativeIterationConvergenceTolerance,
                solutionControlParameters.UseEventStepping,
                solutionControlParameters.RelativeEventLumpingTolerance,
                solutionControlParameters.MaxNumberLineSearches,
                solutionControlParameters.RelativeLineSearchAcceptanceTolerance,
                solutionControlParameters.LineSearchStepFactor);

            _solutionControlParameters = solutionControlParameters;
        }



        /// <summary>
        /// Retrieves the target force iteration parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillTargetForceParameters()
        {
            if (_app == null) return;
            _app.GetTargetForceParameters(CaseName,
                out var convergenceTolerance,
                out var maxIterations,
                out var accelerationFactor,
                out var continueIfNoConvergence);

            _targetForceParameters.ConvergenceTolerance = convergenceTolerance;
            _targetForceParameters.MaxIterations = maxIterations;
            _targetForceParameters.AccelerationFactor = accelerationFactor;
            _targetForceParameters.ContinueIfNoConvergence = continueIfNoConvergence;
        }

        /// <summary>
        /// Sets the target force iteration parameters for the analysis case.
        /// </summary>
        /// <param name="targetForceParameters">The target force parameters.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTargetForceParameters(TargetForceParameters targetForceParameters)
        {
            _app?.SetTargetForceParameters(CaseName,
                targetForceParameters.ConvergenceTolerance,
                targetForceParameters.MaxIterations,
                targetForceParameters.AccelerationFactor,
                targetForceParameters.ContinueIfNoConvergence);

            _targetForceParameters = targetForceParameters;
        }



        /// <summary>
        /// Retrieves the hinge unloading option for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillHingeUnloading()
        {
            if (_app == null) return;
            HingeUnloadingType = _app.GetHingeUnloading(CaseName);
        }

        /// <summary>
        /// Sets the hinge unloading option for the analysis case.
        /// </summary>
        /// <param name="hingeUnloadingType">LoadType of hinge unloading for the selected load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHingeUnloading(eHingeUnloadingType hingeUnloadingType)
        {
            _app?.SetHingeUnloading(CaseName, hingeUnloadingType);
            HingeUnloadingType = hingeUnloadingType;
        }
        
        #endregion
    }
}
