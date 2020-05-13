// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="DampingOverride.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents damping overrides.
    /// </summary>
    public class DampingOverride : ModelProperty
    {
        /// <summary>
        /// The mode number.
        /// </summary>
        /// <value>The mode number.</value>
        public int ModeNumber { get; set; }

        /// <summary>
        /// The damping for the specified mode (0 &lt;= <see cref="Damping" /> &lt; 1).
        /// </summary>
        /// <value>The damping.</value>
        public double Damping { get; set; }

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
