﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLoadCaseSubTypeModal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Load case subtypes available in the program for modal load cases.
    /// </summary>
    public enum eLoadCaseSubTypeModal
    {
        /// <summary>
        /// Eigen modes.
        /// </summary>
        Eigen = 1,

        /// <summary>
        /// Ritz modes.
        /// </summary>
        Ritz = 2
    }
}
