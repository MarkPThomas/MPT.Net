// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eFrameDesignOrientation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames
{
    /// <summary>
    /// Frame orientations in the application.
    /// </summary>
    public enum eFrameDesignOrientation
    {
        /// <summary>
        /// Design frame as a column.
        /// </summary>
        Column = 1,

        /// <summary>
        /// Design frame as a beam.
        /// </summary>
        Beam = 2,

        /// <summary>
        /// Design frame as a brace.
        /// </summary>
        Brace = 3,

        /// <summary>
        /// Frame is a null element, not to be designed.
        /// </summary>
        Null = 4,

        /// <summary>
        /// Frame is of another type.
        /// </summary>
        Other = 5 
    }
}