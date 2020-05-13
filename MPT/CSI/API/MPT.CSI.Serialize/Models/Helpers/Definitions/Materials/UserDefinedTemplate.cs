// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="UserDefinedProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class UserDefinedProperties.
    /// </summary>
    /// <seealso cref="MaterialTemplate" />
    public class UserDefinedTemplate : MaterialTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDefinedTemplate" /> class.
        /// </summary>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="region">The region.</param>
        /// <param name="standard">The material standard.</param>
        /// <param name="grade">The material grade.</param>
        public UserDefinedTemplate(
            eMaterialPropertyType materialType,
            eMaterialRegion region,
            string standard,
            string grade) : base(materialType, region, standard, grade)
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
