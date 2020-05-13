// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="DoubleAngleSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class DoubleAngleSectionProperties.
    /// </summary>
    /// <seealso cref="AngleSectionProperties" />
    public class DoubleAngleSectionProperties : AngleSectionProperties
    {

        /// <summary>
        /// The back-to-back distance between the sections. [L].
        /// </summary>
        /// <value>The separation.</value>
        public double Separation { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAngleSectionProperties"/> class.
        /// </summary>
        public DoubleAngleSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAngleSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DoubleAngleSectionProperties(Material material) : base(material)
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
