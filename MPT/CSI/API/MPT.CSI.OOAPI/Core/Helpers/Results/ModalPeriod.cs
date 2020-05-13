// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ModalPeriod.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class ModalPeriod.
    /// </summary>
    public class ModalPeriod : ApiProperty
    {
        /// <summary>
        /// The period for the result. [s].
        /// </summary>
        /// <value>The period.</value>
        public double Period { get; set; }

        /// <summary>
        /// The cyclic frequency for the result. [1/s].
        /// </summary>
        /// <value>The frequency.</value>
        public double Frequency { get; set; }

        /// <summary>
        /// The circular frequency for the result. [rad/s].
        /// </summary>
        /// <value>The circular frequency.</value>
        public double CircularFrequency { get; set; }

        /// <summary>
        /// The eigenvalue for the specified mode for the result. [rad^2/s^2].
        /// </summary>
        /// <value>The eigenvalue.</value>
        public double Eigenvalue { get; set; }


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
