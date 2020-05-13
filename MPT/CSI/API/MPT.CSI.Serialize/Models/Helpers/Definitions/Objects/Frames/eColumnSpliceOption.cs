// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eColumnSpliceOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames
{
    /// <summary>
    /// Column splice options available in the application.
    /// </summary>
    public enum eColumnSpliceOption
    {
        /// <summary>
        /// From story data (default).
        /// </summary>
        FromStoryData = 1,

        /// <summary>
        /// The no splice.
        /// </summary>
        NoSplice = 2,

        /// <summary>
        /// The splice at height above story at bottom of the column.
        /// </summary>
        SpliceAtHeightAboveStoryAtBottomOfColumn = 3
    }
}