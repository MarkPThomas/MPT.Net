// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="eHysteresisType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Hysteresis types available in the application.
    /// </summary>
    public enum eHysteresisType
    {
        /// <summary>
        /// Elastic hysteresis type.
        /// </summary>
        Elastic = 0,

        /// <summary>
        /// Kinematic hysteresis type.
        /// </summary>
        Kinematic = 1,

        /// <summary>
        /// Takeda hysteresis type.
        /// </summary>
        Takeda = 2,

        // Below are not documented for CSI products
        /// <summary>
        /// The pivot
        /// </summary>
        Pivot = 3,

        /// <summary>
        /// The concrete
        /// </summary>
        Concrete = 4,

        /// <summary>
        /// The BRB hardening
        /// </summary>
        [Description("BRB Hardening")]
        BRBHardening = 5,

        /// <summary>
        /// The degrading
        /// </summary>
        Degrading = 6,

        /// <summary>
        /// The isotropic
        /// </summary>
        Isotropic = 7
    }
}