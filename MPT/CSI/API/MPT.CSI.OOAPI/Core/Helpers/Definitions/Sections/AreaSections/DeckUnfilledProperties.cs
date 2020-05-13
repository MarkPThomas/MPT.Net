// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckUnfilledProperties.cs" company="">
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
    /// Class DeckUnfilledProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections.DeckProperties" />
    public class DeckUnfilledProperties : DeckExtendedProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of deck floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public override eDeckType FloorType => eDeckType.Unfilled;
        

        /// <summary>
        /// Gets or sets the rib depth. [L]
        /// </summary>
        /// <value>The rib depth.</value>
        public double RibDepth { get; set; }


        /// <summary>
        /// Gets or sets the rib width top. [L]
        /// </summary>
        /// <value>The rib width top.</value>
        public double RibWidthTop { get; set; }


        /// <summary>
        /// Gets or sets the rib width bottom. [L]
        /// </summary>
        /// <value>The rib width bottom.</value>
        public double RibWidthBottom { get; set; }


        /// <summary>
        /// Gets or sets the rib spacing. [L]
        /// </summary>
        /// <value>The rib spacing.</value>
        public double RibSpacing { get; set; }


        /// <summary>
        /// Gets or sets the shear thickness. [L]
        /// </summary>
        /// <value>The shear thickness.</value>
        public double ShearThickness { get; set; }


        /// <summary>
        /// Gets or sets the unit weight. [F/L^3]
        /// </summary>
        /// <value>The unit weight.</value>
        public double UnitWeight { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckUnfilledProperties" /> class.
        /// </summary>
        public DeckUnfilledProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckUnfilledProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DeckUnfilledProperties(Material material) : base (material) { }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>DeckUnfilledProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
