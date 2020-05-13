// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="RebarProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class RebarProperties.
    /// </summary>
    /// <seealso cref="MaterialTemplate" />
    public class RebarTemplate : MaterialTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RebarTemplate" /> class.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="standard">The standard.</param>
        /// <param name="grade">The grade.</param>
        public RebarTemplate(
            eMaterialRegion region,
            string standard,
            string grade) : base(eMaterialPropertyType.Rebar, region, standard, grade)
        {
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
