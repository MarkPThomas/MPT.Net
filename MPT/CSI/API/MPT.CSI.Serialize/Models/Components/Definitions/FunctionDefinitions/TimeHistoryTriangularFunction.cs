// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="TimeHistoryTriangularFunction.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class TimeHistoryTriangularFunction.
    /// </summary>
    /// <seealso cref="TimeHistoryFunction" />
    public class TimeHistoryTriangularFunction : TimeHistoryFunction
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <value>The period.</value>
        public double Period { get; internal set; }

        /// <summary>
        /// Gets the amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        public double Amplitude { get; internal set; }

        /// <summary>
        /// Gets the number of cycles.
        /// </summary>
        /// <value>The number of cycles.</value>
        public int NumberOfCycles { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>TimeHistoryTriangularFunction.</returns>
        internal static TimeHistoryTriangularFunction Factory(string uniqueName)
        {
            return new TimeHistoryTriangularFunction(uniqueName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryTriangularFunction"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal TimeHistoryTriangularFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
