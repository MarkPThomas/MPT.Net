// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-17-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-17-2017
// ***********************************************************************
// <copyright file="ePrismaticType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Prismatic section types available in the application.
    /// </summary>
    public enum ePrismaticType
    {
        /// <summary>
        /// Variable (relative length) type over the segment.
        /// </summary>
        Variable = 1,

        /// <summary>
        /// Absolute length type over the segment.
        /// </summary>
        Absolute = 2
    }
}