// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="TendonProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class TendonProperties.
    /// </summary>
    /// <seealso cref="MetalProperties" />
    public class TendonMaterialProperties : MetalProperties
    {
        #region Fields & Properties
        /// <summary>
        /// LoadType of stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eTendonStressStrainCurveType StressStrainCurveType { get; set; }
        #endregion

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
