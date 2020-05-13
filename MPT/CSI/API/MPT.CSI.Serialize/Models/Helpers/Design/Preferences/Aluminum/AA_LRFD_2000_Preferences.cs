// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="AA_LRFD_2000_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum
{
    /// <summary>
    /// Class AA_LRFD_2000_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum.AluminumDesignPreferenceProperties" />
    public class AA_LRFD_2000_Preferences : AluminumDesignPreferenceProperties
    {
        /// <summary>
        /// The resistance factor applicable to limit states of general yield.
        /// </summary>
        /// <value>The phi y.</value>
        public double PhiY { get; set; } = 0.95;

        /// <summary>
        /// The resistance factor applicable to limit states of beams or elements of beams.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.85;

        /// <summary>
        ///The resistance factor applicable to limit states of elements of columns.
        /// </summary>
        /// <value>The phi c.</value>
        public double PhiC { get; set; } = 0.85;

        /// <summary>
        /// The resistance factor applicable to limit states of ultimate strength.
        /// </summary>
        /// <value>The phi u.</value>
        public double PhiU { get; set; } = 0.85;

        /// <summary>
        /// The resistance factor applicable to limit states of columns.
        /// </summary>
        /// <value>The phi cc.</value>
        public double PhiCC { get; set; } = 1;

        /// <summary>
        /// The resistance factor applicable to limit states of elastic buckling of tubes.
        /// </summary>
        /// <value>The phi cp.</value>
        public double PhiCP{ get; set; } = 0.8;

        /// <summary>
        /// The resistance factor applicable to limit states of elastic shear buckling.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.8;

        /// <summary>
        /// The resistance factor applicable to limit states of inelastic shear buckling.
        /// </summary>
        /// <value>The phi vp.</value>
        public double PhiVP { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor applicable to limit states of web crippling.
        /// </summary>
        /// <value>The phi w.</value>
        public double PhiW { get; set; } = 0.9;

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
