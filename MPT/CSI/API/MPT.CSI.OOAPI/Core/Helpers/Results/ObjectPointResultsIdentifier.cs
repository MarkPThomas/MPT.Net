// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ObjectPointResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class ObjectPointResultsIdentifier.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Results.ObjectResultsIdentifier" />
    public class ObjectPointResultsIdentifier : ObjectResultsIdentifier
    {
        /// <summary>
        /// The point element name associated with the result.
        /// </summary>
        /// <value>The name of the point.</value>
        public string PointName { get; set; }


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
