// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="eConcreteStressStrainCurveType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Stress-strain curve types available for concrete in the application.
    /// </summary>
    public enum eConcreteStressStrainCurveType
    {
        /// <summary>
        /// UserCoordinateSystemNames defined.
        /// </summary>
        [Description("User Defined")]
        UserDefined = 0,

        /// <summary>
        /// Parametric – Simple.
        /// </summary>
        [Description("Simple")]
        ParametricSimple = 1,

        /// <summary>
        /// Parametric – Mander.
        /// </summary>
        [Description("Mander")]
        ParametricMander = 2
    }
}