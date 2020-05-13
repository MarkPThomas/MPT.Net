// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eCableGeometryDefinition.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Cable definition used in the program for defining cable geometry.
    /// </summary>
    public enum eCableGeometryDefinition
    {
        /// <summary>
        /// Minimum tension at I-End.
        /// </summary>
        [Description("Minimum Tension At I-End")]
        MinTensionIEnd = 1,

        /// <summary>
        /// Minimum tension at J-End.
        /// </summary>
        [Description("Minimum Tension At J-End")]
        MinTensionJEnd = 2,

        /// <summary>
        /// Tension at I-End.
        /// </summary>
        [Description("Tension At I-End")]
        TensionIEnd = 3,

        /// <summary>
        /// Tension at J-End.
        /// </summary>
        [Description("Tension At J-End")]
        TensionJEnd = 4,

        /// <summary>
        /// Horizontal tension component.
        /// </summary>
        [Description("Horizontal Tension Component")]
        HorizontalTensionComponent = 5,

        /// <summary>
        /// Maximum vertical sag.
        /// </summary>
        [Description("Maximum Vertical Sag")]
        MaximumVerticalSag = 6,

        /// <summary>
        /// Low-point vertical sag.
        /// </summary>
        [Description("Low-Point Vertical Sag")]
        LowPointVerticalSag = 7,

        /// <summary>
        /// Undeformed length.
        /// </summary>
        [Description("Undeformed Length")]
        UndeformedLength = 8,

        /// <summary>
        /// Relative undeformed length.
        /// </summary>
        [Description("Undeformed Relative Length")]
        RelativeUndeformedLength = 9
    }
}