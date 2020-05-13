// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="eMaterialRegion.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Enum eMaterialRegion
    /// </summary>
    public enum eMaterialRegion
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The user
        /// </summary>
        User = 1,

        /// <summary>
        /// The united states
        /// </summary>
        [Description("United States")] UnitedStates = 2,

        /// <summary>
        /// The europe
        /// </summary>
        Europe = 3,

        /// <summary>
        /// The italy
        /// </summary>
        Italy = 4,

        /// <summary>
        /// The spain
        /// </summary>
        Spain = 5,

        /// <summary>
        /// The india
        /// </summary>
        India = 5,

        /// <summary>
        /// The new zealand
        /// </summary>
        [Description("New Zealand")] NewZealand = 5,

        /// <summary>
        /// The vietnam
        /// </summary>
        Vietnam = 5,

        /// <summary>
        /// The russia
        /// </summary>
        Russia = 5,

        /// <summary>
        /// The china
        /// </summary>
        China = 5,
    }
}
