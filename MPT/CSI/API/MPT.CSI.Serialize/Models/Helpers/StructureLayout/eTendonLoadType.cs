﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eTendonLoadType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Tendon load types available in the program.
    /// </summary>
    public enum eTendonLoadType
    {
        /// <summary>
        /// Force.
        /// </summary>
        Force = 0,

        /// <summary>
        /// Stress.
        /// </summary>
        Stress = 1
    }
}