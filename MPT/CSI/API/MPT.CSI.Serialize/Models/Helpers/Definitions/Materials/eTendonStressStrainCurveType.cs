// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="eTendonStressStrainCurveType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Stress-strain curve types available for tendons in the application.
    /// </summary>
    public enum eTendonStressStrainCurveType
    {
        /// <summary>
        /// UserCoordinateSystemNames defined.
        /// </summary>
        [Description("User Defined")]
        UserDefined = 0,

        /// <summary>
        /// Parametric – 250 ksi strand.
        /// </summary>
        [Description("250 ksi")]
        Parametric250ksiStrand = 1,

        /// <summary>
        /// Parametric – 270 ksi strand.
        /// </summary>
        [Description("270 ksi")]
        Parametric270ksiStrand = 2
    }
}