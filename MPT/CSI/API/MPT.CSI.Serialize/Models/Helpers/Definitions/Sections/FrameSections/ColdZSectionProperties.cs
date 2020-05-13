// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-18-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-30-2019
// ***********************************************************************
// <copyright file="ColdZSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class ColdZSectionProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections.FrameSectionProperties" />
    public class ColdZSectionProperties : FrameSectionProperties
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
        /// The web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; set; }

        /// <summary>
        /// The radius at the corners. [L].
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// The lip depth. [L].
        /// </summary>
        /// <value>The lip depth.</value>
        public double LipDepth { get; set; }

        /// <summary>
        /// The lip angle. [deg].
        /// </summary>
        /// <value>The lip angle.</value>
        public double LipAngle { get; set; }
    }
}
