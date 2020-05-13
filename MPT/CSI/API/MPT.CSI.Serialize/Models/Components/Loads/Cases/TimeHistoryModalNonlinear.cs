// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryModalNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Nonlinear modal time history load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public class TimeHistoryModalNonlinear : TimeHistoryModal
    {
        #region Fields & Properties        
        /// <summary>
        /// Gets the type of time history motion.
        /// </summary>
        /// <value>The type of the motion.</value>
        public override eMotionType MotionType => eMotionType.Transient;

        /// <summary>
        /// The nonlinear parameters.
        /// </summary>
        protected NonlinearModalTimeSettingsHelper _nonlinearParameters;
        /// <summary>
        /// Gets the nonlinear parameters.
        /// </summary>
        /// <value>The nonlinear parameters.</value>
        public virtual NonlinearModalTimeSettingsHelper NonlinearParameters
        {
            get
            {
                if (_nonlinearParameters != null) return _nonlinearParameters;
                _nonlinearParameters = new NonlinearModalTimeSettingsHelper(Name);

                return _nonlinearParameters;
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
        internal static TimeHistoryModalNonlinear Factory(
            Analyzer analyzer,
            LoadCases loadCases,
            string uniqueName)
        {
            TimeHistoryModalNonlinear loadCase = new TimeHistoryModalNonlinear(analyzer, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModalNonlinear" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryModalNonlinear(
            Analyzer analyzer,
            LoadCases loadCases,
            string name)
            : base(analyzer, loadCases, name)
        { }
        #endregion
    }
}
