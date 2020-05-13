// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-24-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-24-2017
// ***********************************************************************
// <copyright file="eBracingType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames
{
    /// <summary>
    /// Bracing types available in the program.
    /// </summary>
    public enum eBracingType
    {
        /// <summary>
        /// Point bracing.
        /// </summary>
        Point = 1,

        /// <summary>
        /// Uniform bracing.
        /// </summary>
        Uniform = 2,

        /// <summary>
        /// Both point and uniform bracing.
        /// </summary>
        Both = 3
    }
}