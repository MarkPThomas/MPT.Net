// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="PeriodAccelerationPoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class PeriodAccelerationPoint.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.FunctionPoint" />
    public class PeriodAccelerationPoint : FunctionPoint
    {
        /// <summary>
        /// Gets the period. [sec]
        /// </summary>
        /// <value>The period.</value>
        public double Period => X;

        /// <summary>
        /// Gets the acceleration. [L/sec^2]
        /// </summary>
        /// <value>The acceleration.</value>
        public double Acceleration => Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodAccelerationPoint"/> class.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <param name="acceleration">The acceleration.</param>
        public PeriodAccelerationPoint(double period = 0, double acceleration = 0) : base(period, acceleration) { }
    }
}
