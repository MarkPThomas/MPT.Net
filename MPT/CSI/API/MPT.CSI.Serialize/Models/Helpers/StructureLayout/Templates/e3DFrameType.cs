// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="e3DFrameType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout.Templates
{
    /// <summary>
    /// 3D frame template types available in the application.
    /// </summary>
    public enum e3DFrameType
    {
        /// <summary>
        /// Open frame.
        /// </summary>
        OpenFrame = 0,

        /// <summary>
        /// Perimeter frame.
        /// </summary>
        PerimeterFrame = 1,

        /// <summary>
        /// Beam-slab.
        /// </summary>
        BeamSlab = 2,

        /// <summary>
        /// Flat plate..
        /// </summary>
        FlatPlate = 3,
    }
}