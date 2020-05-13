// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="BaseReactions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Represents base reaction result data.
    /// </summary>
    public class BaseReactions : ApiProperty
    {
        /// <summary>
        /// The base reaction force and moments in/about the global X, Y and Z directions.
        /// </summary>
        /// <value>The reactions.</value>
        public Reactions Reactions { get; set; }

        /// <summary>
        /// These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global X-direction translational reaction forces. [L].
        /// </summary>
        /// <value>The centroid fx coordinates.</value>
        public Coordinate3DCartesian CentroidFxCoordinates { get; set; }

        /// <summary>
        /// These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global Y-direction translational reaction forces. [L].
        /// </summary>
        /// <value>The centroid fy coordinates.</value>
        public Coordinate3DCartesian CentroidFyCoordinates { get; set; }

        /// <summary>
        /// These are arrays of the global X, Y and Z coordinates, respectively, of the centroid of all global Z-direction translational reaction forces. [L].
        /// </summary>
        /// <value>The centroid fz coordinates.</value>
        public Coordinate3DCartesian CentroidFzCoordinates { get; set; }

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
