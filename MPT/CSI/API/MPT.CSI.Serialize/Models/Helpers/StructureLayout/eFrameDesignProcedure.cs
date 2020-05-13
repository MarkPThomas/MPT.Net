// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-28-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-28-2019
// ***********************************************************************
// <copyright file="eFrameDesignProcedure.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Enum eFrameDesignProcedure
    /// </summary>
    public enum eFrameDesignProcedure
    {
        /// <summary>
        /// Frame code design is automatically determined based on the material assigned to the frame.
        /// </summary>
        [Description("From Material")]
        FromMaterial = 1,

        /// <summary>
        /// The frame will not be designed.
        /// </summary>
        [Description("No Design")]
        NoDesign = 2
    }
}
