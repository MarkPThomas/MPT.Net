// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="SlabWaffleProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class SlabWaffleProperties.
    /// </summary>
    /// <seealso cref="SlabExtendedProperties" />
    public class SlabWaffleProperties : SlabExtendedProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of slab floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public override eSlabType FloorType => eSlabType.Waffle;

        /// <summary>
        /// The rib spacing parallel to the local 1-axis.
        /// </summary>
        /// <value>The rib spacing local1.</value>
        public double RibSpacingLocal1 { get; set; }

        /// <summary>
        /// The rib spacing parallel to the local 2-axis.
        /// </summary>
        /// <value>The rib spacing local2.</value>
        public double RibSpacingLocal2 { get; set; }
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
