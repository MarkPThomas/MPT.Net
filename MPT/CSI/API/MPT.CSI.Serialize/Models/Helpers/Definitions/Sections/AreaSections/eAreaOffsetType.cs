// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-16-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-16-2017
// ***********************************************************************
// <copyright file="eAreaOffsetType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Joint offset types avilable for aera elements in the application.
    /// </summary>
    public enum eAreaOffsetType
    {
        /// <summary>
        /// No joint offsets.
        /// </summary>
        NoOffsets = 0,

        /// <summary>
        /// UserCoordinateSystemNames defined joint offsets specified by joint patte.
        /// </summary>
        OffsetByJointPattern = 1,

        /// <summary>
        /// UserCoordinateSystemNames defined joint offsets specified by point.
        /// </summary>
        OffsetByPoint = 2
    }
}
