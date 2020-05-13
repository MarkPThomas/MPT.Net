// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="SlabRibbedProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class SlabRibbedProperties.
    /// </summary>
    /// <seealso cref="SlabExtendedProperties" />
    public class SlabRibbedProperties : SlabExtendedProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of slab floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public override eSlabType FloorType => eSlabType.Ribbed;

        /// <summary>
        /// Gets or sets the rib spacing. [L]
        /// </summary>
        /// <value>The rib spacing.</value>
        public double RibSpacing { get; set; }

        /// <summary>
        /// The local axis that the ribs are parallel to.
        /// </summary>
        /// <value>The ribs parallel to axis.</value>
        public eLocalAxisPlane RibsParallelToAxis { get; set; }
        #endregion
        

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
