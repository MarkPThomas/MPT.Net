﻿// ***********************************************************************
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
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Stiffness values for each coupled degree of freedom.
    /// </summary>
    public class StiffnessCoupled
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
        #endregion


        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 21 entries.
        /// </summary>
        /// <param name="stiffnesses">1x21 matrix of stiffness values of the corresponding coupled degree of freedom:
        /// Value(0) = <see cref="U1U1" /> [F/L];
        /// Value(1) = <see cref="U1U2" /> [F/L];
        /// Value(2) = <see cref="U2U2" /> [F/L];
        /// Value(3) = <see cref="U1U3" /> [F/L];
        /// Value(4) = <see cref="U2U3" /> [F/L];
        /// Value(5) = <see cref="U3U3" /> [F/L];
        /// Value(6) = <see cref="U1R1" /> [F/rad];
        /// Value(7) = <see cref="U2R1" /> [F/rad];
        /// Value(8) = <see cref="U3R1" /> [F/rad];
        /// Value(9) = <see cref="R1R1" /> [FL/rad];
        /// Value(10) = <see cref="U1R2" /> [F/rad];
        /// Value(11) = <see cref="U2R2" /> [F/rad];
        /// Value(12) = <see cref="U3R2" /> [F/rad];
        /// Value(13) = <see cref="R1R2" /> [FL/rad];
        /// Value(14) = <see cref="R2R2" /> [FL/rad];
        /// Value(15) = <see cref="U1R3" /> [F/rad];
        /// Value(16) = <see cref="U2R3" /> [F/rad];
        /// Value(17) = <see cref="U3R3" /> [F/rad];
        /// Value(18) = <see cref="R1R3" /> [FL/rad];
        /// Value(19) = <see cref="R2R3" /> [FL/rad];
        /// Value(20) = <see cref="R3R3" /> [FL/rad];</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + stiffnesses.Length + " elements when 21 elements was expected.</exception>
        public void FromArray(double[] stiffnesses)
        {
            if (stiffnesses.Length != 21) { throw new CSiException("Array has " + stiffnesses.Length + " elements when 21 elements was expected."); }
            U1U1 = stiffnesses[0];

            U1U2 = stiffnesses[1];
            U2U2 = stiffnesses[2];

            U1U3 = stiffnesses[3];
            U2U3 = stiffnesses[4];
            U3U3 = stiffnesses[5];

            U1R1 = stiffnesses[6];
            U2R1 = stiffnesses[7];
            U3R1 = stiffnesses[8];
            R1R1 = stiffnesses[9];

            U1R2 = stiffnesses[10];
            U2R2 = stiffnesses[11];
            U3R2 = stiffnesses[12];
            R1R2 = stiffnesses[13];
            R1R2 = stiffnesses[14];

            U1R3 = stiffnesses[15];
            U2R3 = stiffnesses[16];
            U3R3 = stiffnesses[17];
            R1R3 = stiffnesses[18];
            R1R3 = stiffnesses[19];
            R3R3 = stiffnesses[20];
        }

        /// <summary>
        /// Return a 1x21 matrix of stiffness values of the corresponding coupled degree of freedom:
        /// Value(0) = <see cref="U1U1" /> [F/L];
        /// Value(1) = <see cref="U1U2" /> [F/L];
        /// Value(2) = <see cref="U2U2" /> [F/L];
        /// Value(3) = <see cref="U1U3" /> [F/L];
        /// Value(4) = <see cref="U2U3" /> [F/L];
        /// Value(5) = <see cref="U3U3" /> [F/L];
        /// Value(6) = <see cref="U1R1" /> [F/rad];
        /// Value(7) = <see cref="U2R1" /> [F/rad];
        /// Value(8) = <see cref="U3R1" /> [F/rad];
        /// Value(9) = <see cref="R1R1" /> [FL/rad];
        /// Value(10) = <see cref="U1R2" /> [F/rad];
        /// Value(11) = <see cref="U2R2" /> [F/rad];
        /// Value(12) = <see cref="U3R2" /> [F/rad];
        /// Value(13) = <see cref="R1R2" /> [FL/rad];
        /// Value(14) = <see cref="R2R2" /> [FL/rad];
        /// Value(15) = <see cref="U1R3" /> [F/rad];
        /// Value(16) = <see cref="U2R3" /> [F/rad];
        /// Value(17) = <see cref="U3R3" /> [F/rad];
        /// Value(18) = <see cref="R1R3" /> [FL/rad];
        /// Value(19) = <see cref="R2R3" /> [FL/rad];
        /// Value(20) = <see cref="R3R3" /> [FL/rad];
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] stiffnesses =
            {
                U1U1,
                U1U2, U2U2,
                U1U3, U2U3, U3U3, 
                U1R1, U2R1, U3R1, R1R1,
                U1R2, U2R2, U3R2, R1R2, R2R2,
                U1R3, U2R3, U3R3, R1R3, R2R3, R3R3
            };

            return stiffnesses;
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
