// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="AISI_ASD_1996_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed
{
    /// <summary>
    /// Class AISI_ASD_1996_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed.ColdFormedDesignPreferenceProperties" />
    public class AISI_ASD_1996_Preferences : ColdFormedDesignPreferenceProperties
    {

        /// <summary>
        /// The factor of safety Omega for bending of sections with stiffened or partially stiffened compression flange.
        /// </summary>
        /// <value>The omega bending stiffened.</value>
        public double OmegaBendingStiffened { get; set; } = 1.67;


        /// <summary>
        /// The factor of safety Omega for bending of sections with unstiffened compression flange.
        /// </summary>
        /// <value>The omega bending unstiffened.</value>
        public double OmegaBendingUnstiffened { get; set; } = 1.67;


        /// <summary>
        /// The factor of safety Omega for bending of sections under lateral-torsional buckling mode.
        /// </summary>
        /// <value>The omega bending LTB.</value>
        public double OmegaBendingLTB { get; set; } = 1.67;


        /// <summary>
        /// The factor of safety Omega for shear of sections with slender web.
        /// </summary>
        /// <value>The omega shear slender.</value>
        public double OmegaShearSlender { get; set; } = 1.67;


        /// <summary>
        /// The factor of safety Omega for shear of sections with nonslender web.
        /// </summary>
        /// <value>The omega shear nonslender.</value>
        public double OmegaShearNonslender { get; set; } = 1.5;


        /// <summary>
        /// The factor of safety Omega for tension.
        /// </summary>
        /// <value>The omega axial tension.</value>
        public double OmegaAxialTension { get; set; } = 1.67;


        /// <summary>
        /// The factor of safety Omega for compression.
        /// </summary>
        /// <value>The omega axial compression.</value>
        public double OmegaAxialCompression { get; set; } = 1.8;

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
