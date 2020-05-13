// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="SlabExtendedProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class SlabExtendedProperties.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public abstract class SlabExtendedProperties : ModelProperty
    {
        /// <summary>
        /// The type of slab floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public virtual eSlabType FloorType { get; set; }

        /// <summary>
        /// Gets or sets the slab thickness. [L]
        /// </summary>
        /// <value>The slab thickness.</value>
        public double SlabThickness { get; set; }

        /// <summary>
        /// Gets or sets the overall depth. [L]
        /// </summary>
        /// <value>The overall depth.</value>
        public double OverallDepth { get; set; }


        /// <summary>
        /// Gets or sets the stem width top. [L]
        /// </summary>
        /// <value>The stem width top.</value>
        public double StemWidthTop { get; set; }

        /// <summary>
        /// Gets or sets the stem with bottom. [L]
        /// </summary>
        /// <value>The stem with bottom.</value>
        public double StemWidthBottom { get; set; }

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
