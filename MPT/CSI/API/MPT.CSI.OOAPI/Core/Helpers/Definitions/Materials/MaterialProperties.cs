// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="MaterialProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MaterialProperties.
    /// </summary>
    public class MaterialProperties : ApiProperty
    {
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
