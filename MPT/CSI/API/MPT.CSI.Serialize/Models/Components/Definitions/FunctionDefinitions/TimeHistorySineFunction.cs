﻿// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="TimeHistorySineFunction.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class TimeHistorySineFunction.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions.TimeHistoryFunction" />
    public class TimeHistorySineFunction : TimeHistoryFunction
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

        /// <summary>
        /// Gets the steps per cycle.
        /// </summary>
        /// <value>The steps per cycle.</value>
        public double StepsPerCycle { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>TimeHistorySineFunction.</returns>
        internal static TimeHistorySineFunction Factory(string uniqueName)
        {
            return new TimeHistorySineFunction(uniqueName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistorySineFunction"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal TimeHistorySineFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
