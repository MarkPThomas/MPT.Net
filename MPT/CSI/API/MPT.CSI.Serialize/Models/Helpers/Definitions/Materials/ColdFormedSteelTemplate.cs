// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-14-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="ColdFormedSteelTemplate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ColdFormedSteelTemplate.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Definitions.Materials.MaterialTemplate" />
    public class ColdFormedSteelTemplate : MaterialTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColdFormedSteelTemplate"/> class.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="standard">The standard.</param>
        /// <param name="grade">The grade.</param>
        public ColdFormedSteelTemplate(
            eMaterialRegion region, 
            string standard, 
            string grade) : base(eMaterialPropertyType.ColdFormed, region, standard, grade)
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