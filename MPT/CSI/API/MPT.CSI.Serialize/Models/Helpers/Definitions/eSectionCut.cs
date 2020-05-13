// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-31-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-31-2019
// ***********************************************************************
// <copyright file="eSectionCut.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions
{
    /// <summary>
    /// Enum eSectionCut
    /// </summary>
    public enum eSectionCut
    {
        /// <summary>
        /// Defined by group.
        /// </summary>
        Group = 1,

        /// <summary>
        /// Defined by quarilateral plane.
        /// </summary>
        [Description("Quadrilateral")]
        Quad = 2
    }
}
