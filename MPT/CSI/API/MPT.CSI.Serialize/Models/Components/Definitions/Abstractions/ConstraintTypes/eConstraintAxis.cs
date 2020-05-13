// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="eConstraintAxis.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Enum eConstraintAxis
    /// </summary>
    public enum eConstraintAxis
    {
        /// <summary>
        /// Axis is determined automatically.
        /// </summary>
        Auto = 1,

        /// <summary>
        /// Constraint is about the x-axis.
        /// </summary>
        X = 2,

        /// <summary>
        /// Constraint is about the y-axis.
        /// </summary>
        Y = 3,

        /// <summary>
        /// Constraint is about the z-axis.
        /// </summary>
        Z = 4
    }
}
