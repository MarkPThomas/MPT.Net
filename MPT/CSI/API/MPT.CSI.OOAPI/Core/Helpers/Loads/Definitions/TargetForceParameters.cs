using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data related to nonlinear analysis using target force.
    /// </summary>
    public class TargetForceParameters : ApiProperty
    {
        /// <summary>
        /// The relative convergence tolerance for target force iteration.
        /// </summary>
        /// <value>The convergence tolerance.</value>
        public double ConvergenceTolerance { get; set; }

        /// <summary>
        /// The maximum iterations per stage for target force iteration.
        /// </summary>
        /// <value>The maximum iterations.</value>
        public int MaxIterations { get; set; }

        /// <summary>
        /// The acceleration factor.
        /// </summary>
        /// <value>The acceleration factor.</value>
        public double AccelerationFactor { get; set; }

        /// <summary>
        /// True: Analysis is continued when there is no convergence in the target force iteration.
        /// </summary>
        /// <value>The continue if no convergence.</value>
        public bool ContinueIfNoConvergence { get; set; }


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
