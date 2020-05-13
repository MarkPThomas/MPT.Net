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
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
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
        protected T _apiApp;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The geometric nonlinearity type
        /// </summary>
        private eGeometricNonlinearity _geometricNonlinearityType;
        /// <summary>
        /// The geometric nonlinearity option selected for the load case.
        /// </summary>
        /// <value>The type of the geometric nonlinearity.</value>
        public eGeometricNonlinearity GeometricNonlinearityType
        {
            get
            {
                if (_geometricNonlinearityType == 0)
                {
                    FillGeometricNonlinearity();
                }

                return _geometricNonlinearityType;
            }
        }

        /// <summary>
        /// The hinge unloading type
        /// </summary>
        private eHingeUnloadingType _hingeUnloadingType;
        /// <summary>
        /// LoadType of hinge unloading for the selected load case.
        /// </summary>
        /// <value>The type of the hinge unloading.</value>
        public eHingeUnloadingType HingeUnloadingType
        {
            get
            {
                if (_hingeUnloadingType == 0)
                {
                    FillHingeUnloading();
                }

                return _hingeUnloadingType;
            }
        }

        /// <summary>
        /// The solution control parameters
        /// </summary>
        private SolutionControlParameters _solutionControlParameters;
        /// <summary>
        /// Gets the solution control parameters.
        /// </summary>
        /// <value>The solution control parameters.</value>
        public SolutionControlParameters SolutionControlParameters
        {
            get
            {
                if (_solutionControlParameters == null)
                {
                    FillSolutionControlParameters();
                }

                return _solutionControlParameters;
            }
        }

        /// <summary>
        /// The target force parameters
        /// </summary>
        private TargetForceParameters _targetForceParameters;
        /// <summary>
        /// Gets the target force parameters.
        /// </summary>
        /// <value>The target force parameters.</value>
        public TargetForceParameters TargetForceParameters
        {
            get
            {
                if (_targetForceParameters == null)
                {
                    FillTargetForceParameters();
                }

                return _targetForceParameters;
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="NonlinearSettingsHelper{T}"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="apiApp">The application.</param>
        internal NonlinearSettingsHelper(T apiApp, string caseName) 
        {
            _apiApp = apiApp;
            CaseName = caseName;
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public void FillData()
        {
            FillGeometricNonlinearity();
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
            if (_apiApp == null) return;
            _geometricNonlinearityType = _apiApp.GetGeometricNonlinearity(CaseName);
        }

        /// <summary>
        /// Sets the geometric nonlinearity option for the analysis case.
        /// </summary>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGeometricNonlinearity(eGeometricNonlinearity geometricNonlinearityType)
        {
            _apiApp?.SetGeometricNonlinearity(CaseName, geometricNonlinearityType);
            _geometricNonlinearityType = geometricNonlinearityType;
        }



        /// <summary>
        /// Retrieves the solution control parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSolutionControlParameters()
        {
            if (_apiApp == null) return;
            _apiApp.GetSolutionControlParameters(CaseName,
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

            _solutionControlParameters = new SolutionControlParameters
            {
                MaxTotalSteps = maxTotalSteps,
                MaxNullSteps = maxNullSteps,
                MaxConstantStiffnessIterationsPerStep = maxConstantStiffnessIterationsPerStep,
                MaxNewtonRaphsonIterationsPerStep = maxNewtonRaphsonIterationsPerStep,
                RelativeIterationConvergenceTolerance = relativeIterationConvergenceTolerance,
                UseEventStepping = useEventStepping,
                RelativeEventLumpingTolerance = relativeEventLumpingTolerance,
                MaxNumberLineSearches = maxNumberLineSearches,
                RelativeLineSearchAcceptanceTolerance = relativeLineSearchAcceptanceTolerance,
                LineSearchStepFactor = lineSearchStepFactor
            };
        }

        /// <summary>
        /// Sets the solution control parameters for the analysis case.
        /// </summary>
        /// <param name="solutionControlParameters">The solution control parameters.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSolutionControlParameters(SolutionControlParameters solutionControlParameters)
        {
            _apiApp?.SetSolutionControlParameters(CaseName,
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
            if (_apiApp == null) return;
            _apiApp.GetTargetForceParameters(CaseName,
                out var convergenceTolerance,
                out var maxIterations,
                out var accelerationFactor,
                out var continueIfNoConvergence);

            _targetForceParameters = new TargetForceParameters
            {
                ConvergenceTolerance = convergenceTolerance,
                MaxIterations = maxIterations,
                AccelerationFactor = accelerationFactor,
                ContinueIfNoConvergence = continueIfNoConvergence
            };
        }

        /// <summary>
        /// Sets the target force iteration parameters for the analysis case.
        /// </summary>
        /// <param name="targetForceParameters">The target force parameters.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTargetForceParameters(TargetForceParameters targetForceParameters)
        {
            _apiApp?.SetTargetForceParameters(CaseName,
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
            if (_apiApp == null) return;
            _hingeUnloadingType = _apiApp.GetHingeUnloading(CaseName);
        }

        /// <summary>
        /// Sets the hinge unloading option for the analysis case.
        /// </summary>
        /// <param name="hingeUnloadingType">LoadType of hinge unloading for the selected load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHingeUnloading(eHingeUnloadingType hingeUnloadingType)
        {
            _apiApp?.SetHingeUnloading(CaseName, hingeUnloadingType);
            _hingeUnloadingType = hingeUnloadingType;
        }
        
        #endregion
    }
}
