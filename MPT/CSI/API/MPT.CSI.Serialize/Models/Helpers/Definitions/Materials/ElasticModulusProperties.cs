// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="ElasticModulusProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ElasticModulusProperties.
    /// </summary>
    public class ElasticModulusProperties : ModelProperty
    {

        /// <summary>
        /// Gets or sets the e1.
        /// </summary>
        /// <value>The e1.</value>
        public double E1 { get; set; }
        /// <summary>
        /// Gets or sets the e2.
        /// </summary>
        /// <value>The e2.</value>
        public double E2 { get; set; }
        /// <summary>
        /// Gets or sets the e3.
        /// </summary>
        /// <value>The e3.</value>
        public double E3 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public List<double> ToList()
        {
            return new List<double>(){E1, E2, E3};
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
