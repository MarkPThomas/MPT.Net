// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="eSlabType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Slab types available in the applicaion.
    /// </summary>
    public enum eSlabType
    {
        /// <summary>
        /// Slab.
        /// </summary>
        Slab = 0,

        /// <summary>
        /// Drop slab.
        /// </summary>
        Drop = 1,

        // Do not use
        //// <summary>
        //// Stiff slab.
        //// </summary>
        // Stiff = 2,

        /// <summary>
        /// Ribbed slab.
        /// </summary>
        Ribbed = 3,

        /// <summary>
        /// Waffle slab.
        /// </summary>
        Waffle = 4


    }
}