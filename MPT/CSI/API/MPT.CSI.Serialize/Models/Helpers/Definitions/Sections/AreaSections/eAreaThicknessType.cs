﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-16-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-16-2017
// ***********************************************************************
// <copyright file="eAreaThicknessType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Thickness types avilable for aera elements in the application.
    /// </summary>
    public enum eAreaThicknessType
    {
        /// <summary>
        /// No thickness overwrite.
        /// </summary>
        NoOverwrites = 0,

        /// <summary>
        /// UserCoordinateSystemNames defined thickness overwrites specified by joint patte.
        /// </summary>
        OverwriteByJointPattern = 1,

        /// <summary>
        /// UserCoordinateSystemNames defined thickness overwrites specified by point.
        /// </summary>
        OverwriteByPoint = 2
    }
}
