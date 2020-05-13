// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryDirectLinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Linear direct integration time history load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public class TimeHistoryDirectLinear : TimeHistoryDirectIntegration
    {
        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryDirectLinear Factory(
            Analyzer analyzer,
            LoadCases loadCases, string uniqueName)
        {
            TimeHistoryDirectLinear loadCase = new TimeHistoryDirectLinear(analyzer, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectLinear" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryDirectLinear(
            Analyzer analyzer,
            LoadCases loadCases,
            string name) 
            : base(analyzer, loadCases, name)
        { }
        #endregion
    }
}
