// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="Stiffness.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Stiffnesses
{
    /// <summary>
    /// Stiffness values for each decoupled degree of freedom.
    /// </summary>
    public class Stiffness : ModelProperty
    {
        /// <summary>
        /// Translational stiffness along the 1-axis [F/L].
        /// </summary>
        /// <value>The u1.</value>
        public double U1 { get; set; }

        /// <summary>
        /// Translational stiffness along the 2-axis [F/L].
        /// </summary>
        /// <value>The u2.</value>
        public double U2 { get; set; }


        /// <summary>
        /// Translational stiffness along the 3-axis [F/L].
        /// </summary>
        /// <value>The u3.</value>
        public double U3 { get; set; }


        /// <summary>
        /// Rotational stiffness along the 1-axis [FL/rad].
        /// </summary>
        /// <value>The r1.</value>
        public double R1 { get; set; }

        /// <summary>
        /// Rotational stiffness along the 2-axis [FL/rad].
        /// </summary>
        /// <value>The r2.</value>
        public double R2 { get; set; }

        /// <summary>
        /// Rotational stiffness along the 3-axis [FL/rad].
        /// </summary>
        /// <value>The r3.</value>
        public double R3 { get; set; }

        /// <summary>
        /// True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.
        /// </summary>
        /// <value><c>true</c> if this instance is local coordinate system; otherwise, <c>false</c>.</value>
        public bool IsLocalCoordinateSystem { get; set; }
        // TODO: Reconcile IsLocalCoordinateSystem to CoordinateSystem
        public string CoordinateSystem { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="ArgumentException">Stiffnesses must both be in local coordinate systems in order to be added together.</exception>
        public static Stiffness operator +(Stiffness a, Stiffness b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Stiffnesses must both be in local coordinate systems in order to be added together.");
            }

            Stiffness stiffness = new Stiffness()
            {
                U1 = a.U1 + b.U1,
                U2 = a.U2 + b.U2,
                U3 = a.U3 + b.U3,
                R1 = a.R1 + b.R1,
                R2 = a.R2 + b.R2,
                R3 = a.R3 + b.R3
            };

            return stiffness;
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="ArgumentException">Stiffnesses must both be in local coordinate systems in order to be subtracted from each other.</exception>
        public static Stiffness operator -(Stiffness a, Stiffness b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Stiffnesses must both be in local coordinate systems in order to be subtracted from each other.");
            }

            Stiffness stiffness = new Stiffness()
            {
                U1 = a.U1 - b.U1,
                U2 = a.U2 - b.U2,
                U3 = a.U3 - b.U3,
                R1 = a.R1 - b.R1,
                R2 = a.R2 - b.R2,
                R3 = a.R3 - b.R3
            };

            return stiffness;
        }
    }
}
