// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="TimeHistoryUserFunction.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class TimeHistoryUserFunction.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions.TimeHistoryFunction" />
    public class TimeHistoryUserFunction : TimeHistoryFunction, IFunctionCurve<TimeValuePoint>
    {

        #region Fields & Properties
        /// <summary>
        /// Gets the function curve.
        /// </summary>
        /// <value>The function curve.</value>
        public FunctionCurve<TimeValuePoint> FunctionCurve { get; } = new FunctionCurve<TimeValuePoint>();

        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>TimeHistoryUserFunction.</returns>
        internal static TimeHistoryUserFunction Factory(string uniqueName)
        {
            return new TimeHistoryUserFunction(uniqueName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryUserFunction"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TimeHistoryUserFunction(string name) : base(name)
        {
        }
        #endregion
    }
}

