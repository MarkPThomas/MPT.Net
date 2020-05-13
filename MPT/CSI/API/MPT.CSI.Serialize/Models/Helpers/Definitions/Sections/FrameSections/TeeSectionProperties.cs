// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="TeeSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class TeeSectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class TeeSectionProperties : FrameSectionProperties
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
        public virtual double tw { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="TeeSectionProperties"/> class.
        /// </summary>
        public TeeSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TeeSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public TeeSectionProperties(Material material) : base(material) { }

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
