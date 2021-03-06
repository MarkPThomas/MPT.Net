﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ShellStress.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Represents stresses in a shell.
    /// </summary>
    public class ShellStress : ModelProperty
    {
        /// <summary>
        /// The area element internal stresses, at the top of the area element, at the specified point element location. [F/L^2].
        /// </summary>
        /// <value>The stress top.</value>
        public Stress StressTop { get; set; }

        /// <summary>
        /// The area element internal stresses, at the bottom of the area element, at the specified point element location. [F/L^2].
        /// </summary>
        /// <value>The stress bottom.</value>
        public Stress StressBottom { get; set; }

        /// <summary>
        /// The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].
        /// </summary>
        /// <value>The S13 average.</value>
        public double S13Avg { get; set; }

        /// <summary>
        /// The area element average S13 out-of-plane shear stress at the specified point element location, for the specified layer and layer integration point. [F/L^2].
        /// </summary>
        /// <value>The S23 average.</value>
        public double S23Avg { get; set; }

        /// <summary>
        /// The area element maximum average out-of-plane shear stress for the specified layer and layer integration point.
        /// It is equal to the square root of the sum of the squares of <see cref="S13Avg" /> and <see cref="S23Avg" /> [F/L^2].
        /// </summary>
        /// <value>The s maximum average.</value>
        public double SMaxAvg { get; set; }

        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of <see cref="SMaxAvg" />. [deg].
        /// </summary>
        /// <value>The s angle average.</value>
        public double SAngleAvg { get; set; }


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
