// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckSolidSlabProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class DeckSolidSlabProperties.
    /// </summary>
    public class DeckSolidSlabProperties : DeckExtendedProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of deck floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public override eDeckType FloorType => eDeckType.SolidSlab;

        /// <summary>
        /// Gets or sets the slab depth. [L]
        /// </summary>
        /// <value>The slab depth.</value>
        public double SlabDepth { get; set; }


        /// <summary>
        /// Gets or sets the shear stud diameter. [L]
        /// </summary>
        /// <value>The shear stud diameter.</value>
        public double ShearStudDiameter { get; set; }


        /// <summary>
        /// Gets or sets the height of the shear stud. [L]
        /// </summary>
        /// <value>The height of the shear stud.</value>
        public double ShearStudHeight { get; set; }


        /// <summary>
        /// The shear stud tensile strength, Fu. [F/L^2]
        /// </summary>
        /// <value>The shear stud fu.</value>
        public double ShearStudFu { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckSolidSlabProperties" /> class.
        /// </summary>
        public DeckSolidSlabProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckSolidSlabProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DeckSolidSlabProperties(Material material) : base(material) { }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>DeckSolidSlabProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
