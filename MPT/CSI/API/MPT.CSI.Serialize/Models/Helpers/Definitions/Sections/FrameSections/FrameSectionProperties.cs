// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="FrameSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class FrameSectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public abstract class FrameSectionProperties : SectionProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSectionProperties" /> class.
        /// </summary>
        protected FrameSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSectionProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        protected FrameSectionProperties(Material material) : base(material) { }

    }
}
