// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="JointResponseSpectrum.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class JointResponseSpectrum.
    /// </summary>
    public class JointResponseSpectrum : ApiProperty
    {
        /// <summary>
        /// The critical damping ratio.
        /// </summary>
        /// <value>The damping.</value>
        public double Damping { get; set; }

        /// <summary>
        /// The percent spectrum widening.
        /// </summary>
        /// <value>The percent spectrum widening.</value>
        public double PercentSpectrumWidening { get; set; }

        /// <summary>
        /// The period or frequency, as defined in each named set. [s or 1/s].
        /// </summary>
        /// <value>The abscissa values.</value>
        public double AbscissaValues { get; set; }

        /// <summary>
        /// The response quantity, as defined in each named set.
        /// The possible response quantities are spectral displacement [L], spectral velocity [L/s], pseudo spectral velocity [L/s], spectral acceleration [L/s2], or pseudo spectral acceleration [L/s2].
        /// </summary>
        /// <value>The ordinate values.</value>
        public double OrdinateValues { get; set; }


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
