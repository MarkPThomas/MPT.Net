// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="eLinkDirection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.LinkProperties
{
    /// <summary>
    /// Directions available for links in the application.
    /// </summary>
    public enum eLinkDirection
    {
        /// <summary>
        /// Both directions (linear).
        /// </summary>
        None = 0,

        /// <summary>
        /// Compression direction only.
        /// </summary>
        Compression = 1,

        /// <summary>
        /// Tension direction only.
        /// </summary>
        Tension = 2,
        
    }
}