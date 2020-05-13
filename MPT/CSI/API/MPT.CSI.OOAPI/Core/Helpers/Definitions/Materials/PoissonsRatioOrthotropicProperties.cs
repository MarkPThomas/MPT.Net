// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="PoissonsRatioOrthotropicProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class PoissonsRatioOrthotropicProperties.
    /// </summary>
    public class PoissonsRatioOrthotropicProperties : ApiProperty
    {
        /// <summary>
        /// Gets or sets the u12.
        /// </summary>
        /// <value>The u12.</value>
        public double U12 { get; set; }
        /// <summary>
        /// Gets or sets the u13.
        /// </summary>
        /// <value>The u13.</value>
        public double U13 { get; set; }
        /// <summary>
        /// Gets or sets the u23.
        /// </summary>
        /// <value>The u23.</value>
        public double U23 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public virtual List<double> ToList()
        {
            return new List<double>()
                { U12,
                  U13, U23
                };
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
