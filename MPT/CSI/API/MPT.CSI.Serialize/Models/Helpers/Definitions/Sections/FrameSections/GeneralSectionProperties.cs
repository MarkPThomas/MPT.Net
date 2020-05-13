// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="GeneralSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class GeneralSectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class GeneralSectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public SectionResultantProperties Properties { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSectionProperties" /> class.
        /// </summary>
        public GeneralSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSectionProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public GeneralSectionProperties(Material material) : base(material)
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
