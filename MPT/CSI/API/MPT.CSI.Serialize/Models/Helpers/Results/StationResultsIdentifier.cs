// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StationResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class StationResultsIdentifier.
    /// </summary>
    /// <seealso cref="ObjectResultsIdentifier" />
    public class StationResultsIdentifier : ObjectResultsIdentifier
    {
        /// <summary>
        /// The distance measured from the I-end of the line object to the result location.
        /// </summary>
        /// <value>The object station.</value>
        public double ObjectStation { get; set; }

        /// <summary>
        /// The distance measured from the I-end of the line element to the result location.
        /// </summary>
        /// <value>The element station.</value>
        public double ElementStation { get; set; }


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
