// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="ThermalExpansionOrthotropicCoefficients.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ThermalExpansionOrthotropicCoefficients.
    /// </summary>
    public class ThermalExpansionOrthotropicCoefficients : ApiProperty
    {
        /// <summary>
        /// Gets or sets the a1.
        /// </summary>
        /// <value>The a1.</value>
        public double A1 { get; set; }
        /// <summary>
        /// Gets or sets the a2.
        /// </summary>
        /// <value>The a2.</value>
        public double A2 { get; set; }
        /// <summary>
        /// Gets or sets the a3.
        /// </summary>
        /// <value>The a3.</value>
        public double A3 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public virtual List<double> ToList()
        {
            return new List<double>() { A1, A2, A3 };
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
