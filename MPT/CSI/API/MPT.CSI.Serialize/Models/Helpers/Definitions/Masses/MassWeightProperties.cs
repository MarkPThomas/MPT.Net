// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="MassWeightProperties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Masses
{
    /// <summary>
    /// Mass by weight values for each degree of freedom.
    /// </summary>
    public class MassWeightProperties : ModelProperty
    {
        /// <summary>
        /// Translational mass along the 1-axis [F].
        /// </summary>
        /// <value>The u1.</value>
        public double U1 { get; set; }

        /// <summary>
        /// Translational mass along the 2-axis [F].
        /// </summary>
        /// <value>The u2.</value>
        public double U2 { get; set; }


        /// <summary>
        /// Translational mass along the 3-axis [F].
        /// </summary>
        /// <value>The u3.</value>
        public double U3 { get; set; }


        /// <summary>
        /// Rotational mass about the 1-axis [F*L^2].
        /// </summary>
        /// <value>The r1.</value>
        public double R1 { get; set; }

        /// <summary>
        /// Rotational mass about the 2-axis [F*L^2].
        /// </summary>
        /// <value>The r2.</value>
        public double R2 { get; set; }

        /// <summary>
        /// Rotational mass about the 3-axis [F*L^2].
        /// </summary>
        /// <value>The r3.</value>
        public double R3 { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
