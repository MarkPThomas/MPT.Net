// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MasonryProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MasonryProperties.
    /// </summary>
    /// <seealso cref="MaterialTemplate" />
    public class MasonryTemplate : MaterialTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasonryTemplate" /> class.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="standard">The standard.</param>
        /// <param name="grade">The grade.</param>
        public MasonryTemplate(
            eMaterialRegion region,
            string standard,
            string grade) : base(eMaterialPropertyType.Masonry, region, standard, grade)
        {
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}