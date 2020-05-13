// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="INonlinearSolutionControlParameters.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case uses solution control parameters for nonlinear analysis.
    /// </summary>
    public interface INonlinearSolutionControlParameters
    {
        /// <summary>
        /// Returns the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        void GetSolutionControlParameters(string name,
            out int maxTotalSteps,
            out int maxNullSteps,
            out int maxConstantStiffnessIterationsPerStep,
            out int maxNewtonRaphsonIterationsPerStep,
            out double relativeIterationConvergenceTolerance,
            out bool useEventStepping,
            out double relativeEventLumpingTolerance,
            out int maxNumberLineSearches,
            out double relativeLineSearchAcceptanceTolerance,
            out double lineSearchStepFactor);

        /// <summary>
        /// Sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        void SetSolutionControlParameters(string name,
            int maxTotalSteps,
            int maxNullSteps,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor);
    }
}