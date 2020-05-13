// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="ChannelSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class ChannelSectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class ChannelSectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// The section depth. [L].
        /// </summary>
        /// <value>The t3.</value>
        public virtual double t3 { get; set; }

        /// <summary>
        /// The section/flange width. [L].
        /// </summary>
        /// <value>The t2.</value>
        public virtual double t2 { get; set; }

        /// <summary>
        /// The flange thickness. [L].
        /// </summary>
        /// <value>The tf.</value>
        public double tf { get; set; }

        /// <summary>
        /// The web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; set; }

        /// <summary>
        /// Gets or sets the shear center offset v2. [L].
        /// </summary>
        /// <value>The shear center offset v2.</value>
        public double ShearCenterOffsetV2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelSectionProperties"/> class.
        /// </summary>
        public ChannelSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ChannelSectionProperties(Material material) : base (material) { }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
