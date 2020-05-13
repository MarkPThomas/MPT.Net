// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eShellSteelLayoutOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Shell steel layout options available in the applicaion.
    /// </summary>
    public enum eShellSteelLayoutOption
    {
        /// <summary>
        /// Default steel layout.
        /// </summary>
        Default = 0,

        /// <summary>
        /// One layer of steel.
        /// </summary>
        [Description("One Layer")]
        OneLayer = 1,

        /// <summary>
        /// Two layers of steel.
        /// </summary>
        [Description("Two Layers")]
        TwoLayers = 2
    }
}
