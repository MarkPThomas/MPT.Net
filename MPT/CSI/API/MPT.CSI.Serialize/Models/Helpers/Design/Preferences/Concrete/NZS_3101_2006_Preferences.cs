// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="NZS_3101_2006_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class NZS_3101_2006_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class NZS_3101_2006_Preferences : ConcreteDesignPreferencesProperties
    {
        // TABLE:  "PREFERENCES - CONCRETE DESIGN - NZS 3101-06"
        // Omega=1   PhiZero=1.47058823529412   Rm=1   Rv=1

        /// <summary>
        /// The strength reduction factor for bending.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.85;

        /// <summary>
        /// The strength reduction factor for tension.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiT { get; set; } = 0.85;

        /// <summary>
        /// The strength reduction factor for axial compression.
        /// </summary>
        /// <value>The phi c tied.</value>
        public double PhiC { get; set; } = 0.85;

        /// <summary>
        /// The strength reduction factor for shear.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.75;

        /// <summary>
        /// Amplification factor.
        /// </summary>
        /// <value>The omega.</value>
        public double Omega { get; set; } = 1;

        /// <summary>
        /// Gets or sets the phi zero.
        /// </summary>
        /// <value>The phi zero.</value>
        public double PhiZero { get; set; } = 1.47058823529412;

        /// <summary>
        /// Rm factor to be used in shear design.
        /// </summary>
        /// <value>The rm.</value>
        public double Rm { get; set; } = 1;

        /// <summary>
        /// Rv factor to be used in shear design.
        /// </summary>
        /// <value>The rv.</value>
        public double Rv { get; set; } = 1;

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
