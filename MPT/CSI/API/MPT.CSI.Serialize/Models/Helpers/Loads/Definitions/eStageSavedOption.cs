// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eStageSavedOption.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Options for saving stage results available in the application.
    /// </summary>
    public enum eStageSavedOption
    {
        /// <summary>
        /// End of final stage.
        /// </summary>
        [Description("End of Final Stage")]
        EndOfFinalStage = 0,

        /// <summary>
        /// End of each stage.
        /// </summary>
        [Description("End of Each Stage")]
        EndOfEachStage = 1,

        /// <summary>
        /// Start and end of each stage.
        /// </summary>
        [Description("Start and End of Each Stage")]
        StartAndEndOfEachStage = 2,

        /// <summary>
        /// Two or more times in each stage.
        /// </summary>
        [Description("Two or More Times In Each Stage")]
        TwoOrMoreTimesPerStage = 3
    }
}