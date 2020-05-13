// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="eBubbleLocation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Possible locations for the bubble label associated with a gridline.
    /// </summary>
    public enum eBubbleLocation
    {
        /// <summary>
        /// The start of the gridline.
        /// </summary>
        Start = 1,
        /// <summary>
        /// The end of the gridline.
        /// </summary>
        End = 2
    }
}
