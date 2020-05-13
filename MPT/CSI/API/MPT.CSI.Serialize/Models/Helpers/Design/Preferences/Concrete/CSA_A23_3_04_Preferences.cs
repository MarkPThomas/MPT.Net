// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="CSA_A23_3_04_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class CSA_A23_3_04_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class CSA_A23_3_04_Preferences : ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// The strength reduction factor for steel.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiS { get; set; } = 0.85;

        /// <summary>
        /// The strength reduction factor for concrete.
        /// </summary>
        /// <value>The phi c tied.</value>
        public double PhiC { get; set; } = 0.65;

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
