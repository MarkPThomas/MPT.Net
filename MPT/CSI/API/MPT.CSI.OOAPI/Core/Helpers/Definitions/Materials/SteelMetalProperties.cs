// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="SteelMetalProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class SteelMetalProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MetalProperties" />
    public class SteelMetalProperties : MetalProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The expected yield stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fye { get; set; }

        /// <summary>
        /// The expected tensile stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fue { get; set; }

        /// <summary>
        /// The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at hardening.</value>
        public double StrainAtHardening { get; set; }
        #endregion

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
