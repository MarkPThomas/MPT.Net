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
using MPT.CSI.Serialize.Models.Helpers.Definitions.Masses;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class AssembledJointMass.
    /// </summary>
    public class AssembledJointMass : ModelProperty
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
        public MassProperties Mass { get; set; }
        
        /// <summary>
        /// The mass source name associated with the result.
        /// </summary>
        /// <value>The mass source names.</value>
        public string MassSourceNames { get; set; }

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
