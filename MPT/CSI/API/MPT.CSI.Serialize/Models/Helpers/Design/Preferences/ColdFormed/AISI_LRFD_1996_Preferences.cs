// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="AISI_LRFD_1996_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed
{
    /// <summary>
    /// Class AISI_LRFD_1996_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed.ColdFormedDesignPreferenceProperties" />
    public class AISI_LRFD_1996_Preferences: ColdFormedDesignPreferenceProperties
    {
        /// <summary>
        /// The resistance factor Phi for bending of sections with stiffened or partially stiffened compression flange.
        /// </summary>
        /// <value>The phi bending stiffened.</value>
        public double PhiBendingStiffened { get; set; } = 0.95;

        /// <summary>
        /// The resistance factor Phi for bending of sections with unstiffened compression flange.
        /// </summary>
        /// <value>The phi bending unstiffened.</value>
        public double PhiBendingUnstiffened { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor Phi for bending of sections under lateral-torsional buckling mode.
        /// </summary>
        /// <value>The phi bending LTB.</value>
        public double PhiBendingLTB { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor Phi for shear of sections with slender web.
        /// </summary>
        /// <value>The phi shear slender.</value>
        public double PhiShearSlender { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor Phi for shear of sections with nonslender web.
        /// </summary>
        /// <value>The phi shear nonslender.</value>
        public double PhiShearNonslender { get; set; } = 1;

        /// <summary>
        /// The resistance factor Phi for tension.
        /// </summary>
        /// <value>The phi axial tension.</value>
        public double PhiAxialTension { get; set; } = 0.95;

        /// <summary>
        /// The resistance factor Phi for compression.
        /// </summary>
        /// <value>The phi axial compression.</value>
        public double PhiAxialCompression { get; set; } = 0.85;

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
