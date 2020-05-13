// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="StiffnessCoupled.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Stiffnesses
{
    /// <summary>
    /// Stiffness values for each coupled degree of freedom.
    /// </summary>
    public class StiffnessCoupled : ModelProperty
    {
        #region Properties
        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u1 u1.</value>
        public double U1U1 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u1 u2.</value>
        public double U1U2 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u2 u2.</value>
        public double U2U2 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u1 u3.</value>
        public double U1U3 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u2 u3.</value>
        public double U2U3 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/L].
        /// </summary>
        /// <value>The u3 u3.</value>
        public double U3U3 { get; set; }

        // ==============

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u1 r1.</value>
        public double U1R1 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u2 r1.</value>
        public double U2R1 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u3 r1.</value>
        public double U3R1 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r1 r1.</value>
        public double R1R1 { get; set; }


        // ==============

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u1 r2.</value>
        public double U1R2 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u2 r2.</value>
        public double U2R2 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u3 r2.</value>
        public double U3R2 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r1 r2.</value>
        public double R1R2 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r2 r2.</value>
        public double R2R2 { get; set; }


        // ==============

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u1 r3.</value>
        public double U1R3 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u2 r3.</value>
        public double U2R3 { get; set; }

        /// <summary>
        /// Coupled stiffness [F/rad].
        /// </summary>
        /// <value>The u3 r3.</value>
        public double U3R3 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r1 r3.</value>
        public double R1R3 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r2 r3.</value>
        public double R2R3 { get; set; }

        /// <summary>
        /// Coupled stiffness [FL/rad].
        /// </summary>
        /// <value>The r3 r3.</value>
        public double R3R3 { get; set; }

        /// <summary>
        /// True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.
        /// </summary>
        /// <value><c>true</c> if this instance is local coordinate system; otherwise, <c>false</c>.</value>
        public bool IsLocalCoordinateSystem { get; set; }
        // TODO: Reconcile IsLocalCoordinateSystem to CoordinateSystem
        public string CoordinateSystem { get; set; }
        #endregion

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
        public static StiffnessCoupled operator +(StiffnessCoupled a, StiffnessCoupled b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Stiffnesses must both be in local coordinate systems in order to be added together.");
            }

            StiffnessCoupled stiffness = new StiffnessCoupled()
            {
                U1U1 = a.U1U1 + b.U1U1,

                U1U2 = a.U1U2 + b.U1U2,
                U2U2 = a.U2U2 + b.U2U2,

                U1U3 = a.U1U3 + b.U1U3,
                U2U3 = a.U2U3 + b.U2U3,
                U3U3 = a.U3U3 + b.U3U3,

                U1R1 = a.U1R1 + b.U1R1,
                U2R1 = a.U2R1 + b.U2R1,
                U3R1 = a.U3R1 + b.U3R1,
                R1R1 = a.R1R1 + b.R1R1,

                U1R2 = a.U1R2 + b.U1R2,
                U2R2 = a.U2R2 + b.U2R2,
                U3R2 = a.U3R2 + b.U3R2,
                R1R2 = a.R1R2 + b.R1R2,
                R2R2 = a.R2R2 + b.R2R2,

                U1R3 = a.U1R3 + b.U1R3,
                U2R3 = a.U2R3 + b.U2R3,
                U3R3 = a.U3R3 + b.U3R3,
                R1R3 = a.R1R3 + b.R1R3,
                R2R3 = a.R2R3 + b.R2R3,
                R3R3 = a.R3R3 + b.R3R3,
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
        public static StiffnessCoupled operator -(StiffnessCoupled a, StiffnessCoupled b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Stiffnesses must both be in local coordinate systems in order to be subtracted from each other.");
            }

            StiffnessCoupled stiffness = new StiffnessCoupled()
            {
                U1U1 = a.U1U1 - b.U1U1,

                U1U2 = a.U1U2 - b.U1U2,
                U2U2 = a.U2U2 - b.U2U2,

                U1U3 = a.U1U3 - b.U1U3,
                U2U3 = a.U2U3 - b.U2U3,
                U3U3 = a.U3U3 - b.U3U3,

                U1R1 = a.U1R1 - b.U1R1,
                U2R1 = a.U2R1 - b.U2R1,
                U3R1 = a.U3R1 - b.U3R1,
                R1R1 = a.R1R1 - b.R1R1,

                U1R2 = a.U1R2 - b.U1R2,
                U2R2 = a.U2R2 - b.U2R2,
                U3R2 = a.U3R2 - b.U3R2,
                R1R2 = a.R1R2 - b.R1R2,
                R2R2 = a.R2R2 - b.R2R2,

                U1R3 = a.U1R3 - b.U1R3,
                U2R3 = a.U2R3 - b.U2R3,
                U3R3 = a.U3R3 - b.U3R3,
                R1R3 = a.R1R3 - b.R1R3,
                R2R3 = a.R2R3 - b.R2R3,
                R3R3 = a.R3R3 - b.R3R3,
            };

            return stiffness;
        }
    }
}
