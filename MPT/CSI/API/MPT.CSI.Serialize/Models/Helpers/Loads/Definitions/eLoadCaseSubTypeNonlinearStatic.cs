// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLoadCaseSubTypeNonlinearStatic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Load case subtypes available in the program for nonlinear static load cases.
    /// </summary>
    public enum eLoadCaseSubTypeNonlinearStatic
    {
        /// <summary>
        /// Nonlinear static.
        /// </summary>
        Nonlinear = 1,

        /// <summary>
        /// Nonlinear static staged construction.
        /// </summary>
        NonlinearStagedConstruction = 2
    }
}