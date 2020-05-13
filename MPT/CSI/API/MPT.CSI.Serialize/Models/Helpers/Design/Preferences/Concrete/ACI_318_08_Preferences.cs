// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="ACI_318_08_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class ACI_318_08_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class ACI_318_08_Preferences: ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// Enum SeismicCategories
        /// </summary>
        public enum SeismicCategories
        {
            /// <summary>
            /// a
            /// </summary>
            A = 1,
            /// <summary>
            /// The b
            /// </summary>
            B = 2,
            /// <summary>
            /// The c
            /// </summary>
            C = 3,
            /// <summary>
            /// The d
            /// </summary>
            D = 4,
            /// <summary>
            /// The e
            /// </summary>
            E = 5,
            /// <summary>
            /// The f
            /// </summary>
            F = 6
        }

        /// <summary>
        /// Gets or sets the seismic category.
        /// </summary>
        /// <value>The seismic category.</value>
        public SeismicCategories SeismicCategory { get; set; } = SeismicCategories.D;

        /// <summary>
        /// This is called the System Rho.
        /// The System Rho value specified here is solely used for design.
        /// The factor accounts the redundancy factor to modify load combinations involving seismic case.
        /// </summary>
        /// <value>The rho.</value>
        public double Rho { get; set; } = 1;

        /// <summary>
        /// This is called the System Sds.
        /// The System Sds value specified here is solely used for design.
        /// The factor accounts the Sds factor to modify load combinations involving seismic case.
        /// </summary>
        /// <value>The SDS.</value>
        public double Sds { get; set; } = 0.5;

        /// <summary>
        /// The strength reduction factor for tension controlled sections.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiT { get; set; } = 0.9;

        /// <summary>
        /// The strength reduction factor for compression controlled sections with tied reinforcement.
        /// </summary>
        /// <value>The phi c tied.</value>
        public double PhiCTied { get; set; } = 0.65;

        /// <summary>
        /// The strength reduction factor for compression controlled sections with spiral reinforcement.
        /// </summary>
        /// <value>The phi c spiral.</value>
        public double PhiCSpiral { get; set; } = 0.75;

        /// <summary>
        /// The strength reduction factor for shear and torsion.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.75;

        /// <summary>
        /// The strength reduction factor for shear in structures that rely on special moment resisting frames or special reinforced concrete structural walls to resist earthquake effects.
        /// </summary>
        /// <value>The phi v seismic.</value>
        public double PhiVSeismic { get; set; } = 0.6;

        /// <summary>
        /// The strength reduction factor for joint shear in structures that rely on special moment resisting frames or special reinforced concrete structural walls to resist earthquake effects.
        /// </summary>
        /// <value>The phi v joint.</value>
        public double PhiVJoint { get; set; } = 0.85;


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
