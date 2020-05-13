// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="eRatioType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// The controlling stress or capacity ratio type for each frame object.
    /// </summary>
    public enum eRatioType
    {
        /// <summary>
        /// The error
        /// </summary>
        Error = 0,

        /// <summary>
        /// PMM ratio.
        /// </summary>
        PMM = 1,

        /// <summary>
        /// Major shear ratio.
        /// </summary>
        MajorShear = 2,

        /// <summary>
        /// Minor shear ratio.
        /// </summary>
        MinorShear = 3,

        /// <summary>
        /// The major beam-column capacity ratio.
        /// </summary>
        MajorBCCRatio = 4,

        /// <summary>
        /// The minor beam-column capacity ratio.
        /// </summary>
        MinorBCCRatio = 5,

        /// <summary>
        /// Other ratio.
        /// </summary>
        Other = 6
    }
}
