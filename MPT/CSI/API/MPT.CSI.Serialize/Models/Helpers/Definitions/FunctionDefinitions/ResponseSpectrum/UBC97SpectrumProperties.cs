// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="UBC97SpectrumProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum
{
    /// <summary>
    /// Class UBC97SpectrumProperties.
    /// </summary>
    /// <seealso cref="ResponseSpectrumProperties" />
    public class UBC97SpectrumProperties : ResponseSpectrumProperties
    {
        /// <summary>
        /// Gets or sets the seismic coefficient Ca.
        /// </summary>
        /// <value>The ca.</value>
        public double Ca { get; set; }

        /// <summary>
        /// Gets or sets the seismic coefficient Cv.
        /// </summary>
        /// <value>The cv.</value>
        public double Cv { get; set; }

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
