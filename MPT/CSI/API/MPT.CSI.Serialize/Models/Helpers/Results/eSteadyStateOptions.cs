// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-12-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eSteadyStateOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Steady state options available for display in the application.
    /// </summary>
    public enum eSteadyStateOptions
    {
        /// <summary>
        /// In and out of phase.
        /// </summary>
        InAndOutOfPhase = 1,

        /// <summary>
        /// The magnitude.
        /// </summary>
        Magnitude = 2,

        /// <summary>
        /// All.
        /// </summary>
        All = 3
    }
}