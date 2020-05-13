// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eDeckType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Deck types available in the applicaion.
    /// </summary>
    public enum eDeckType
    {
        /// <summary>
        /// Filled deck.
        /// </summary>
        Filled = 1,

        /// <summary>
        /// Unfilled deck.
        /// </summary>
        Unfilled = 2,

        /// <summary>
        /// Solid slab deck.
        /// </summary>
        SolidSlab = 3
    }
}