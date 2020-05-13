// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="ShearModulusProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ShearModulusProperties.
    /// </summary>
    public class ShearModulusProperties : ApiProperty
    {

        /// <summary>
        /// Gets or sets the G12.
        /// </summary>
        /// <value>The G12.</value>
        public double G12 { get; set; }
        /// <summary>
        /// Gets or sets the G13.
        /// </summary>
        /// <value>The G13.</value>
        public double G13 { get; set; }
        /// <summary>
        /// Gets or sets the G23.
        /// </summary>
        /// <value>The G23.</value>
        public double G23 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public List<double> ToList()
        {
            return new List<double>() { G12, G13, G23 };
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
