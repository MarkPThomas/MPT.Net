// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="eWallPropertyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Local axes available in plane elements the applicaion.
    /// </summary>
    public enum eWallPropertyType
    {
        /// <summary>
        /// The wall property is manually specified.
        /// </summary>
        Specified = 1,

        /// <summary>
        /// Property is from the auto select list.
        /// </summary>
        AutoSelectList = 2
    }
}