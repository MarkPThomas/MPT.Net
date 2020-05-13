// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="AreaSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class AreaSectionProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.SectionProperties" />
    public abstract class AreaSectionProperties : SectionProperties
    {
        /// <summary>
        /// Gets or sets the type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public virtual eAreaSectionType AreaType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSectionProperties" /> class.
        /// </summary>
        protected AreaSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSectionProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        protected AreaSectionProperties(Material material) : base(material)
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
