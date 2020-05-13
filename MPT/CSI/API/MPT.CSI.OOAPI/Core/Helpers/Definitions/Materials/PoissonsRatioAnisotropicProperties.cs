// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="PoissonsRatioAnisotropicProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class PoissonsRatioAnisotropicProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.PoissonsRatioOrthotropicProperties" />
    public class PoissonsRatioAnisotropicProperties : PoissonsRatioOrthotropicProperties
    {
        /// <summary>
        /// Gets or sets the u14.
        /// </summary>
        /// <value>The u14.</value>
        public double U14 { get; set; }
        /// <summary>
        /// Gets or sets the u24.
        /// </summary>
        /// <value>The u24.</value>
        public double U24 { get; set; }
        /// <summary>
        /// Gets or sets the u34.
        /// </summary>
        /// <value>The u34.</value>
        public double U34 { get; set; }

        /// <summary>
        /// Gets or sets the u15.
        /// </summary>
        /// <value>The u15.</value>
        public double U15 { get; set; }
        /// <summary>
        /// Gets or sets the u25.
        /// </summary>
        /// <value>The u25.</value>
        public double U25 { get; set; }
        /// <summary>
        /// Gets or sets the u35.
        /// </summary>
        /// <value>The u35.</value>
        public double U35 { get; set; }
        /// <summary>
        /// Gets or sets the u45.
        /// </summary>
        /// <value>The u45.</value>
        public double U45 { get; set; }

        /// <summary>
        /// Gets or sets the u16.
        /// </summary>
        /// <value>The u16.</value>
        public double U16 { get; set; }
        /// <summary>
        /// Gets or sets the u26.
        /// </summary>
        /// <value>The u26.</value>
        public double U26 { get; set; }
        /// <summary>
        /// Gets or sets the u36.
        /// </summary>
        /// <value>The u36.</value>
        public double U36 { get; set; }
        /// <summary>
        /// Gets or sets the u46.
        /// </summary>
        /// <value>The u46.</value>
        public double U46 { get; set; }
        /// <summary>
        /// Gets or sets the u56.
        /// </summary>
        /// <value>The u56.</value>
        public double U56 { get; set; }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public override List<double> ToList()
        {
            return new List<double>(base.ToList())
            {
                U14, U24, U34,
                U15, U25, U35, U45,
                U16, U26, U36, U46, U56
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
