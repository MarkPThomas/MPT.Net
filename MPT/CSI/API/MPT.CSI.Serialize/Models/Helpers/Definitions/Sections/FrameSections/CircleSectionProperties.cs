// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="CircleSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class CircleSectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class CircleSectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// The section diameter. [L].
        /// </summary>
        /// <value>The t2.</value>
        public double D { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="CircleSectionProperties"/> class.
        /// </summary>
        public CircleSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public CircleSectionProperties(Material material) : base(material)
        {
        }

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
