// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryDirectNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Nonlinear direct integration time history load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public class TimeHistoryDirectNonlinear : TimeHistoryDirectIntegration
    {
        #region Fields & Properties
        /// <summary>
        /// The mass source
        /// </summary>
        private MassSourceHelper _massSource;
        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public virtual MassSourceHelper MassSource
        {
            get
            {
                if (_massSource != null) return _massSource;
                _massSource = new MassSourceHelper(Name);

                return _massSource;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryDirectNonlinear Factory(
            Analyzer analyzer,
            LoadCases loadCases,
            string uniqueName)
        {
            TimeHistoryDirectNonlinear loadCase = new TimeHistoryDirectNonlinear(analyzer, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectNonlinear" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryDirectNonlinear(
            Analyzer analyzer,
            LoadCases loadCases,
            string name)
            : base(analyzer, loadCases, name)
        { }
        #endregion
    }
}
