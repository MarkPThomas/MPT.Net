// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ShellForce.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Represents forces in a shell.
    /// </summary>
    public class ShellForce : ApiProperty
    {
        /// <summary>
        /// The area element internal F11 membrane direct force per length reported in the area element local coordinate system. [F/L].
        /// </summary>
        /// <value>The F11.</value>
        public double F11 { get; set; }

        /// <summary>
        /// The area element internal F22 membrane direct force per length reported in the area element local coordinate system. [F/L].
        /// </summary>
        /// <value>The F22.</value>
        public double F22 { get; set; }

        /// <summary>
        /// The area element internal F12 membrane shear force per length reported in the area element local coordinate system. [F/L].
        /// </summary>
        /// <value>The F12.</value>
        public double F12 { get; set; }

        /// <summary>
        /// The maximum principal membrane force per length. [F/L].
        /// </summary>
        /// <value>The f maximum.</value>
        public double FMax { get; set; }

        /// <summary>
        /// The minimum principal membrane force per length. [F/L].
        /// </summary>
        /// <value>The f minimum.</value>
        public double FMin { get; set; }

        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal membrane force. [deg].
        /// </summary>
        /// <value>The f angle.</value>
        public double FAngle { get; set; }

        /// <summary>
        /// The area element internal Von Mises membrane force per length. [F/L].
        /// </summary>
        /// <value>The FVM.</value>
        public double FVM { get; set; }

        /// <summary>
        /// The area element internal M11 plate bending moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].
        /// </summary>
        /// <value>The M11.</value>
        public double M11 { get; set; }

        /// <summary>
        /// The area element internal M22 plate bending moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].
        /// </summary>
        /// <value>The M22.</value>
        public double M22 { get; set; }

        /// <summary>
        /// The area element internal M12 plate twisting moment per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].
        /// </summary>
        /// <value>The M12.</value>
        public double M12 { get; set; }

        /// <summary>
        /// The maximum principal plate moment per length.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].
        /// </summary>
        /// <value>The m maximum.</value>
        public double MMax { get; set; }

        /// <summary>
        /// The minimum principal plate moment per length.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F*L/L].
        /// </summary>
        /// <value>The m minimum.</value>
        public double MMin { get; set; }

        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal plate moment.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [deg].
        /// </summary>
        /// <value>The m angle.</value>
        public double MAngle { get; set; }

        /// <summary>
        /// The area element internal V13 plate transverse shear force per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].
        /// </summary>
        /// <value>The V13.</value>
        public double V13 { get; set; }

        /// <summary>
        /// The area element internal V23 plate transverse shear force per length reported in the area element local coordinate system.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].
        /// </summary>
        /// <value>The V23.</value>
        public double V23 { get; set; }

        /// <summary>
        /// The maximum plate transverse shear force.  It is equal to the square root of the sum of the squares of V13 and V23.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [F/L].
        /// </summary>
        /// <value>The v maximum.</value>
        public double VMax { get; set; }

        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of Vmax.
        /// This item is only reported for area elements with properties that allow plate bending behavior. [deg].
        /// </summary>
        /// <value>The v angle.</value>
        public double VAngle { get; set; }


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
