// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="CoverPlatedISectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{

    /// <summary>
    /// Class CoverPlatedISectionProperties.
    /// </summary>
    /// <seealso cref="SectionProperties" />
    public class CoverPlatedISectionProperties : FrameSectionProperties
    {  
        /// <summary>
        /// The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.
        /// </summary>
        /// <value>The name of the section.</value>
        public string SectionName { get; internal set; }

        /// <summary>
        /// The name of the material property for the top cover plate.
        /// This item applies only if both the <see cref="tcTop" /> and the <see cref="bcTop" /> items are greater than 0.
        /// </summary>
        /// <value>The name material top.</value>
        public string MaterialNameTop { get; internal set; }

        /// <summary>
        /// The name of the material property for the bottom cover plate.
        /// This item applies only if both the <see cref="tcBottom" /> and the <see cref="bcBottom" /> items are greater than 0.
        /// </summary>
        /// <value>The name material bottom.</value>
        public string MaterialNameBottom { get; internal set; }



        /// <summary>
        /// The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <see cref="SectionName" /> item is used.
        /// </summary>
        /// <value>The fy top flange.</value>
        public double fyTopFlange { get; set; }

        /// <summary>
        /// The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <see cref="SectionName" /> item is used.
        /// </summary>
        /// <value>The fy web.</value>
        public double fyWeb { get; set; }

        /// <summary>
        /// The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <see cref="SectionName" /> item is used.
        /// </summary>
        /// <value>The fy bottom flange.</value>
        public double fyBottomFlange { get; set; }



        /// <summary>
        /// Maximum width. [L]
        /// </summary>
        /// <value>The b maximum.</value>
        public double bMax { get; set; }

        /// <summary>
        /// Total height. [L]
        /// </summary>
        /// <value>The h total.</value>
        public double hTotal { get; set; }


        /// <summary>
        /// The thickness of the top cover plate. [L]
        /// If the <see cref="tcTop" /> or the <see cref="bcTop" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The tcTop.</value>
        public double tcTop { get; set; }

        /// <summary>
        /// The width of the top cover plate. [L]
        /// If the <see cref="tcTop" /> or the <see cref="bcTop" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The bcTop.</value>
        public double bcTop { get; set; }


        /// <summary>
        /// The thickness of the bottom cover plate. [L]
        /// If the <see cref="tcBottom" /> or the <see cref="bcBottom" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The tc bottom.</value>
        public double tcBottom { get; set; }

        /// <summary>
        /// The width of the bottom cover plate. [L]
        /// If the <see cref="tcBottom" /> or the <see cref="bcBottom" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The bc bottom.</value>
        public double bcBottom { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="CoverPlatedISectionProperties"/> class.
        /// </summary>
        public CoverPlatedISectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CoverPlatedISectionProperties"/> class.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="materialTop">The material top.</param>
        /// <param name="materialBottom">The material bottom.</param>
        public CoverPlatedISectionProperties(ISection section, Material materialTop, Material materialBottom)
        {
            SectionName = section.Name;
            MaterialNameTop = materialTop.Name;
            MaterialNameBottom = materialBottom.Name;
        }

        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="material">The material.</param>
        public override void SetMaterial(Material material)
        {
            // Does nothing
        }


        /// <summary>
        /// Sets the top material.
        /// </summary>
        /// <param name="material">The material.</param>
        public void SetTopMaterial(Material material)
        {
            MaterialNameTop = material.Name;
        }


        /// <summary>
        /// Sets the bottom material.
        /// </summary>
        /// <param name="material">The material.</param>
        public void SetBottomMaterial(Material material)
        {
            MaterialNameBottom = material.Name;
        }


        /// <summary>
        /// Sets the section.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(ISection section)
        {
            SectionName = section.Name;
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
