// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ModalLoadParticipationRatio.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class ModalLoadParticipationRatio.
    /// </summary>
    public class ModalLoadParticipationRatio : ApiProperty
    {
        /// <summary>
        /// The percent static load participation ratio for the result.
        /// </summary>
        /// <value>The percent static load participation ratio.</value>
        public double PercentStaticLoadParticipationRatio { get; set; }

        /// <summary>
        /// The percent dynamic load participation ratio for the result.
        /// </summary>
        /// <value>The percent dynamic load participation ratio.</value>
        public double PercentDynamicLoadParticipationRatio { get; set; }


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
