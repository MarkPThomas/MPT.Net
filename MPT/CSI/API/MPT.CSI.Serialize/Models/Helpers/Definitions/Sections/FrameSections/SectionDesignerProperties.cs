// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-24-2018
// ***********************************************************************
// <copyright file="SectionDesignerProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class SectionDesignerProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class SectionDesignerProperties : SectionProperties
    {   // TODO: Finish SectionDesignerProperties
        /// <summary>
        /// Gets or sets the type of the design.
        /// </summary>
        /// <value>The type of the design.</value>
        public eSectionDesignerDesignOption DesignType { get; set; }

        // List of below.
        /// <summary>
        /// Gets the name of the shape.
        /// </summary>
        /// <value>The name of the shape.</value>
        public string[] ShapeName { get; internal set; }
        /// <summary>
        /// Gets the type of the section.
        /// </summary>
        /// <value>The type of the section.</value>
        public eSectionDesignerSectionType[] SectionType { get; internal set; }

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
