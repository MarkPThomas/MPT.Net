// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-27-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="eNotionalSizeType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Time dependent notional size calculation options.
    /// </summary>
    public enum eNotionalSizeType
    {
        /// <summary>
        /// The automatic
        /// </summary>
        Auto = 1,
        /// <summary>
        /// The user
        /// </summary>
        User = 2,
        /// <summary>
        /// The none
        /// </summary>
        None = 3
    }
}
