// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="MexicanRCDF2004Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class MexicanRCDF2004Preferences.
    /// </summary>
    /// <seealso cref="ConcreteDesignPreferencesProperties" />
    public class RCDF_2004_Preferences: ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// The strength reduction factor for bending.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.9;

        /// <summary>
        /// The strength reduction factor for tension.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiT { get; set; } = 0.8;

        /// <summary>
        /// The strength reduction factor for axial compression when the member has tie reinforcement.
        /// </summary>
        /// <value>The phi c tied.</value>
        public double PhiCTied { get; set; } = 0.7;

        /// <summary>
        /// The strength reduction factor for axial compression when the member has spiral reinforcement.
        /// </summary>
        /// <value>The phi c spiral.</value>
        public double PhiCSpiral { get; set; } = 0.8;

        /// <summary>
        /// The strength reduction factor for shear.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.8;

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
