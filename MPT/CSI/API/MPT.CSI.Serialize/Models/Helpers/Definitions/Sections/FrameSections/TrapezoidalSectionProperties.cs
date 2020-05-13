// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-30-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-30-2019
// ***********************************************************************
// <copyright file="TrapezoidalSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class TrapezoidalSectionProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections.FrameSectionProperties" />
    public class TrapezoidalSectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// The section depth. [L].
        /// </summary>
        /// <value>The t3.</value>
        public virtual double t3 { get; set; }

        /// <summary>
        /// The top flange width. [L].
        /// </summary>
        /// <value>The t2.</value>
        public virtual double t2 { get; set; }

        /// <summary>
        /// The bottom flange width. [L].
        /// </summary>
        /// <value>The T2B.</value>
        public double t2b { get; set; }
    }
}
