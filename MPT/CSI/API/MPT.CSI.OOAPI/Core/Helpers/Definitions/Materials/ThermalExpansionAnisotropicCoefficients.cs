// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="ThermalExpansionAnisotropicCoefficients.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ThermalExpansionAnisotropicCoefficients.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.ThermalExpansionOrthotropicCoefficients" />
    public class ThermalExpansionAnisotropicCoefficients : ThermalExpansionOrthotropicCoefficients
    {
        /// <summary>
        /// Gets or sets the a12.
        /// </summary>
        /// <value>The a12.</value>
        public double A12 { get; set; }
        /// <summary>
        /// Gets or sets the a13.
        /// </summary>
        /// <value>The a13.</value>
        public double A13 { get; set; }
        /// <summary>
        /// Gets or sets the a23.
        /// </summary>
        /// <value>The a23.</value>
        public double A23 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public override List<double> ToList()
        {
            return new List<double>(base.ToList()) { A12, A13, A23 };
        }

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
