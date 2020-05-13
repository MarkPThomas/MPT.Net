// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="AssembledJointMass.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class AssembledJointMass.
    /// </summary>
    public class AssembledJointMass : ApiProperty
    {
        /// <summary>
        /// The point element name associated with the result.
        /// </summary>
        /// <value>The name of the point element.</value>
        public string PointElementName {get; set; }

        /// <summary>
        /// The mass along/about the point element local 1, 2 and 3 axes directions, for the result.
        /// </summary>
        /// <value>The mass.</value>
        public Mass Mass { get; set; }



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The mass source name associated with the result.
        /// </summary>
        /// <value>The mass source names.</value>
        public string MassSourceNames { get; set; }
#endif

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
