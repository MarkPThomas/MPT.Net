// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="AluminumProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class AluminumProperties.
    /// </summary>
    /// <seealso cref="MaterialProperties" />
    public class AluminumProperties : MaterialProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of aluminum.
        /// </summary>
        /// <value>The type of the aluminum.</value>
        public eAluminumType AluminumType { get; set; }

        /// <summary>
        /// The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.
        /// </summary>
        /// <value>The alloy.</value>
        public string Alloy { get; set; }

        /// <summary>
        /// The compressive yield strength of aluminum. [F/L^2]
        /// </summary>
        /// <value>The fcy.</value>
        public double Fcy { get; set; }

        /// <summary>
        /// The tensile yield strength of aluminum. [F/L^2]
        /// </summary>
        /// <value>The fty.</value>
        public double Fty { get; set; }

        /// <summary>
        /// The tensile ultimate strength of aluminum. [F/L^2]
        /// </summary>
        /// <value>The ftu.</value>
        public double Ftu { get; set; }

        /// <summary>
        /// The shear ultimate strength of aluminum. [F/L^2]
        /// </summary>
        /// <value>The fsu.</value>
        public double Fsu { get; set; }

        /// <summary>
        /// The stress-strain hysteresis type.
        /// </summary>
        /// <value>The type of the stress strain hysteresis.</value>
        public eHysteresisType StressStrainHysteresisType { get; set; }
        #endregion

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