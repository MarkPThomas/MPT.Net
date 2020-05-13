// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="eLoadComboType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Load combination types available in the application.
    /// </summary>
    public enum eLoadComboType
    {
        /// <summary>
        /// Linear Additive.
        /// </summary>
        [Description("Linear Add")]
        LinearAdditive = 0,

        /// <summary>
        /// Envelope.
        /// </summary>
        Envelope = 1,

        /// <summary>
        /// Absolute additive.
        /// </summary>
        AbsoluteAdditive = 2,

        /// <summary>
        /// SRSS.
        /// </summary>
        SRSS = 3,

        /// <summary>
        /// Range additive.
        /// </summary>
        [Description("Range Add")]
        RangeAdditive = 4
    }
}
