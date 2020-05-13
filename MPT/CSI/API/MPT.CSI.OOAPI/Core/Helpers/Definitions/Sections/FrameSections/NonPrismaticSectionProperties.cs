// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="NonPrismaticSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class NonPrismaticSectionProperties.
    /// </summary>
    public class NonPrismaticSectionProperties : ApiProperty
    {   // TODO: Finish NonPrismaticSectionProperties
        // List of below:

        /// <summary>
        /// Gets or sets name of the section at the start of the segment.
        /// </summary>
        /// <value>The start name of the section.</value>
        public string StartSectionName { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the section at the end of the segment.
        /// </summary>
        /// <value>The end name of the section.</value>
        public string EndSectionName { get; internal set; }

        /// <summary>
        /// Gets or sets the length of the segment. [L]
        /// </summary>
        /// <value>The length.</value>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the prismatic type of the segment.
        /// </summary>
        /// <value>The type of the prismatic.</value>
        public ePrismaticType PrismaticType { get; set; }

        /// <summary>
        /// The variation type for EI33 in the segment.
        /// </summary>
        /// <value>The e i33.</value>
        public ePrismaticInertiaType EI33 { get; set; }

        /// <summary>
        /// The variation type for EI22 in the segment.
        /// </summary>
        /// <value>The e i22.</value>
        public ePrismaticInertiaType EI22 { get; set; }



        /// <summary>
        /// The misc properties such as color, notes, etc.
        /// </summary>
        /// <value>The misc properties.</value>
        public GeneralData GeneralData { get; set; }

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
