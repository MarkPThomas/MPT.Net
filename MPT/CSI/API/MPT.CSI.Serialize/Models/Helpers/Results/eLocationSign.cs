// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLocationSign.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Location reference by sign.
    /// </summary>
    public enum eLocationSign
    {
        /// <summary>
        /// Positive 3-axis side of quadrilateral.
        /// </summary>
        Positive3 = 1,

        /// <summary>
        /// Negative 3-axis side of quadrilateral.
        /// </summary>
        Negative3 = 2
    }
}