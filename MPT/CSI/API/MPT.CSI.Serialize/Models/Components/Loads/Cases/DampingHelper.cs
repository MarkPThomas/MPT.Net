// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="DampingHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class DampingHelper.
    /// </summary>
    public class DampingHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// Hysteretic damping type for the load case.
        /// </summary>
        /// <value>The type of the damping.</value>
        public eDampingType DampingType { get; internal set; }

        /// <summary>
        /// Constant modal damping for all modes (0 &lt;= damping &lt; 1) assigned to the load case.
        /// </summary>
        /// <value>The damping constant.</value>
        public double DampingConstant { get; internal set; }

        /// <summary>
        /// Gets or sets the interpolated damping.
        /// </summary>
        /// <value>The damping interpolated.</value>
        public List<DampingInterpolated> DampingInterpolated { get; internal set; }

        /// <summary>
        /// Gets or sets the proportional damping.
        /// </summary>
        /// <value>The damping proportional.</value>
        public DampingProportional DampingProportional { get; internal set; }

        /// <summary>
        /// The damping overwrites of mode #, overwrite.
        /// </summary>
        /// <value>The damping overwrites.</value>
        public List<DampingOverride> DampingOverrides { get; internal set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="DampingHelper"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        internal DampingHelper(string caseName)
        {
            CaseName = caseName;
        }
        #endregion
    }
}
