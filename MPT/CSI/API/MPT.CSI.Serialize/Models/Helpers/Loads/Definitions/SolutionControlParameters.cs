// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-20-2018
// ***********************************************************************
// <copyright file="SolutionControlParameters.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data related to nonlienar analysis solution parameters.
    /// </summary>
    public class SolutionControlParameters : ModelProperty
    {
        /// <summary>
        /// The maximum total steps per stage.
        /// </summary>
        /// <value>The maximum total steps.</value>
        public int MaxTotalSteps { get; set; }

        /// <summary>
        /// The maximum null (zero) steps per stage.
        /// </summary>
        /// <value>The maximum null steps.</value>
        public int MaxNullSteps { get; set; }

        public bool UseIteration { get; set; }

        /// <summary>
        /// The maximum constant stiffness iterations per step.
        /// </summary>
        /// <value>The maximum constant stiffness iterations per step.</value>
        public int MaxConstantStiffnessIterationsPerStep { get; set; }

        /// <summary>
        /// The maximum Newton-Raphson iterations per step.
        /// </summary>
        /// <value>The maximum newton raphson iterations per step.</value>
        public int MaxNewtonRaphsonIterationsPerStep { get; set; }

        /// <summary>
        /// The relative iteration convergence tolerance.
        /// </summary>
        /// <value>The relative iteration convergence tolerance.</value>
        public double RelativeIterationConvergenceTolerance { get; set; }

        /// <summary>
        /// True: Event-to-event stepping is used.
        /// </summary>
        /// <value><c>true</c> if [use event stepping]; otherwise, <c>false</c>.</value>
        public bool UseEventStepping { get; set; }

        /// <summary>
        /// The relative event lumping tolerance.
        /// </summary>
        /// <value>The relative event lumping tolerance.</value>
        public double RelativeEventLumpingTolerance { get; set; }

        public int MaxNumberEventsPerStep { get; set; }

        /// <summary>
        /// The maximum number of line-searches per iteration.
        /// </summary>
        /// <value>The maximum number line searches.</value>
        public int MaxNumberLineSearches { get; set; }

        public bool UseLineSearch { get; set; }

        /// <summary>
        /// The relative line-search acceptance tolerance.
        /// </summary>
        /// <value>The relative line search acceptance tolerance.</value>
        public double RelativeLineSearchAcceptanceTolerance { get; set; }

        /// <summary>
        /// The line-search step factor.
        /// </summary>
        /// <value>The line search step factor.</value>
        public double LineSearchStepFactor { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
