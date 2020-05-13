// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-17-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-17-2019
// ***********************************************************************
// <copyright file="eModalLoadItemType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// LoadItemType for modal load participation.
    /// </summary>
    public enum eModalLoadItemType
    {
        /// <summary>
        /// The load pattern
        /// </summary>
        [Description("Load Pattern")]
        LoadPattern = 1,

        /// <summary>
        /// The acceleration
        /// </summary>
        Acceleration = 2,

        /// <summary>
        /// The link
        /// </summary>
        Link = 3,

        /// <summary>
        /// The panel zone
        /// </summary>
        [Description("Panel Zone")]
        PanelZone = 4
    }
}
