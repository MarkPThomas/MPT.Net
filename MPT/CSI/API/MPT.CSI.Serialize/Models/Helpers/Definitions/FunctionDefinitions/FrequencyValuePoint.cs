// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="FrequencyValuePoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class FrequencyValuePoint.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.FunctionPoint" />
    public class FrequencyValuePoint : FunctionPoint
    {
        /// <summary>
        /// Gets the frequency. [1/sec]
        /// </summary>
        /// <value>The frequency.</value>
        public double Frequency => X;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public double Value => Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyValuePoint"/> class.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="value">The value.</param>
        public FrequencyValuePoint(double frequency = 0, double value = 0) : base(frequency, value) { }
    }
}
