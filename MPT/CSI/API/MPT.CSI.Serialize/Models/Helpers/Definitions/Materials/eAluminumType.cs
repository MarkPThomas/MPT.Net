// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eAluminumType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Types of aluminum.
    /// </summary>
    public enum eAluminumType
    {
        /// <summary>
        /// Wrought aluminum.
        /// </summary>
        Wrought = 1,

        /// <summary>
        /// Cast-mold aluminum.
        /// </summary>
        [Description("Cast-Mold")]
        CastMold = 2,

        /// <summary>
        /// Cast-sand aluminum.
        /// </summary>
        [Description("Cast-Sand")]
        CastSand = 3
    }
}