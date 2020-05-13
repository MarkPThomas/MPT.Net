// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckFilledProperties.cs" company="">
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
    /// Class DeckFilledProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections.DeckExtendedProperties" />
    public class DeckFilledProperties : DeckExtendedProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of deck floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public override eDeckType FloorType => eDeckType.Filled;


        /// <summary>
        /// The overall deck depth.
        /// </summary>
        /// <value>The overall depth.</value>
        public override double OverallDepth => Form.OverallDepth + SlabFill.OverallDepth;

        /// <summary>
        /// Gets or sets the form properties.
        /// </summary>
        /// <value>The form.</value>
        public DeckUnfilledProperties Form { get; set; } = new DeckUnfilledProperties();

        /// <summary>
        /// Gets or sets the fill properties.
        /// </summary>
        /// <value>The fill.</value>
        public DeckSolidSlabProperties SlabFill { get; set; } = new DeckSolidSlabProperties();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckFilledProperties" /> class.
        /// </summary>
        public DeckFilledProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckFilledProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DeckFilledProperties(Material material) : base(material) { }


        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>DeckFilledProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
