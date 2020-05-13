// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="SectionResultantProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class SectionResultantProperties.
    /// </summary>
    public class SectionResultantProperties : ModelProperty
    {
        /// <summary>
        /// The section overall depth. [L].
        /// </summary>
        /// <value>The t3.</value>
        public virtual double t3 { get; set; }

        /// <summary>
        /// The section overall width. [L].
        /// </summary>
        /// <value>The t2.</value>
        public virtual double t2 { get; set; }

        /// <summary>
        /// The gross cross-sectional area. [L^2].
        /// </summary>
        /// <value>The ag.</value>
        public double Ag { get; set; }

        /// <summary>
        /// The shear area for forces in the section local 2-axis direction. [L^2].
        /// </summary>
        /// <value>The as2.</value>
        public double As2 { get; set; }

        /// <summary>
        /// The shear area for forces in the section local 3-axis direction. [L^2].
        /// </summary>
        /// <value>The as3.</value>
        public double As3 { get; set; }

        /// <summary>
        /// The torsional constant. [L^4].
        /// </summary>
        /// <value>The j.</value>
        public double J { get; set; }

        /// <summary>
        /// The moment of inertia for bending about the local 2 axis. [L^4].
        /// </summary>
        /// <value>The i22.</value>
        public double I22 { get; set; }

        /// <summary>
        /// The moment of inertia for bending about the local 2-3 axis. [L^4].
        /// </summary>
        /// <value>The i23.</value>
        public double I23 { get; set; }

        /// <summary>
        /// The moment of inertia for bending about the local 3 axis. [L^4].
        /// </summary>
        /// <value>The i33.</value>
        public double I33 { get; set; }

        /// <summary>
        /// The section modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The S22.</value>
        public double S22 { get; set; }

        /// <summary>
        /// The section modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The S33.</value>
        public double S33 { get; set; }

        /// <summary>
        /// The plastic modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The Z22.</value>
        public double Z22 { get; set; }

        /// <summary>
        /// The plastic modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The Z33.</value>
        public double Z33 { get; set; }

        /// <summary>
        /// The radius of gyration about the local 2 axis. [L].
        /// </summary>
        /// <value>The R22.</value>
        public double r22 { get; set; }

        /// <summary>
        /// The radius of gyration about the local 3 axis. [L].
        /// </summary>
        /// <value>The R33.</value>
        public double r33 { get; set; }

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
