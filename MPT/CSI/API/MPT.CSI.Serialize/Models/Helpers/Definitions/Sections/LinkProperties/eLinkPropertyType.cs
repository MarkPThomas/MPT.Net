// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eLinkPropertyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.LinkProperties
{
    /// <summary>
    /// Link types available in the application.
    /// </summary>
    public enum eLinkPropertyType
    {
        /// <summary>
        /// Linear link.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Damper link.
        /// </summary>
        Damper = 2,

        /// <summary>
        /// Gap link.
        /// </summary>
        Gap = 3,

        /// <summary>
        /// Hook link.
        /// </summary>
        Hook = 4,

        /// <summary>
        /// Plastic Wen link.
        /// </summary>
        [Description("Plastic (Wen)")]
        PlasticWen = 5,

        /// <summary>
        /// Rubber isolator link.
        /// </summary>
        [Description("Rubber Isolator")]
        IsolatorRubber = 6,

        /// <summary>
        /// Friction isolator link.
        /// </summary>
        [Description("Friction Isolator")]
        IsolatorFriction = 7,

        /// <summary>
        /// Multi linear elastic link.
        /// </summary>
        [Description("MultiLinear Elastic")]
        MultiLinearElastic = 8,

        /// <summary>
        /// Multi linear plastic link.
        /// </summary>
        [Description("MultiLinear Plastic")]
        MultiLinearPlastic = 9,

        /// <summary>
        /// Tension/Compression friction isolator link.
        /// </summary>
        [Description("T/C Friction Isolator")]
        IsolatorTCFriction = 10,

        // Not documented - may not match CSi programs
        [Description("Damper - Exponential")]
        DamperExponential = 11,


        [Description("Damper - Bilinear")]
        DamperBilinear = 12,


        [Description("Damper - Friction Spring")]
        DamperFrictionSpring = 13,


        [Description("High Damping Rubber Isolator")]
        IsolatorRubberHighDamping = 14,
    }
}
