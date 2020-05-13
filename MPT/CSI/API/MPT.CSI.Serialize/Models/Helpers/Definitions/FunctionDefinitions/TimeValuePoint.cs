// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="TimeValuePoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class TimeValuePoint.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.FunctionPoint" />
    public class TimeValuePoint : FunctionPoint
    {
        /// <summary>
        /// Gets the time. [sec]
        /// </summary>
        /// <value>The time.</value>
        public double Time => X;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public double Value => Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeValuePoint"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="value">The value.</param>
        public TimeValuePoint(double time = 0, double value = 0) : base(time, value) { }
    }
}
