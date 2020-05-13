// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="DirectionCombination.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents the direction combination.
    /// </summary>
    public class DirectionCombination : ModelProperty
    {
        /// <summary>
        /// The directional combination option.
        /// </summary>
        /// <value>The directional combination.</value>
        public eDirectionalCombination DirectionalCombination { get; set; }

        /// <summary>
        /// The abslute value scale factor.
        /// This item applies only when <see cref="DirectionalCombination" /> = <see cref="eDirectionalCombination.ABS" />
        /// </summary>
        /// <value>The scale factor.</value>
        public double ScaleFactor { get; set; }


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
