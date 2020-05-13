// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="AS_3600_2009_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class AS_3600_2009_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class AS_3600_2009_Preferences : ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// The strength reduction factor for tension controlled sections.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiT { get; set; } = 0.8;

        /// <summary>
        /// The strength reduction factor for compression controlled sections.
        /// </summary>
        /// <value>The phi c tied.</value>
        public double PhiC { get; set; } = 0.6;

        /// <summary>
        /// The strength reduction factor for shear and torsion.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.7;

        /// <summary>
        /// The strength reduction factor for shear in structures that rely on special moment resisting frames or special reinforced concrete structural walls to resist earthquake effects.
        /// </summary>
        /// <value>The phi v seismic.</value>
        public double PhiVSeismic { get; set; } = 0.6;

        /// <summary>
        /// The strength reduction factor for joint shear in structures that rely on special moment resisting frames or special reinforced concrete structural walls to resist earthquake effects.
        /// </summary>
        /// <value>The phi v joint.</value>
        public double PhiVJoint { get; set; } = 0.7;

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
