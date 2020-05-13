// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ColdFormedSteelProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ColdFormedSteelProperties.
    /// </summary>
    /// <seealso cref="MaterialProperties" />
    public class ColdFormedSteelProperties : MaterialProperties
    {
        #region Fields & Properties

        /// <summary>
        /// The minimum yield stress. [F/L^2]
        /// </summary>
        /// <value>The fy.</value>
        public double Fy { get; set; }


        /// <summary>
        /// The minimum tensile stress. [F/L^2]
        /// </summary>
        /// <value>The fu.</value>
        public double Fu { get; set; }

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