// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="StageResultsSaved.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data relating to saved results for staged construction load cases.
    /// </summary>
    public class StageResultsSaved : ModelProperty
    {
        /// <summary>
        /// The results saved option for the load case.
        /// </summary>
        /// <value>The stage saved option.</value>
        public eStageSavedOption StageSavedOption { get; set; }

        /// <summary>
        /// The minimum number of steps for application of instantaneous load. <para />
        /// This item applies only when <see cref="StageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.
        /// </summary>
        /// <value>The minimum steps for instantanous load.</value>
        public int MinStepsForInstantanousLoad { get; set; }

        /// <summary>
        /// The minimum number of steps for analysis of time dependent items. <para />
        /// This item applies only when <see cref="StageSavedOption" /> = <see cref="eStageSavedOption.TwoOrMoreTimesPerStage" />.
        /// </summary>
        /// <value>The minimum steps for time dependent items.</value>
        public int MinStepsForTimeDependentItems { get; set; }

        /// <summary>
        /// The load case has time dependent material.
        /// </summary>
        /// <value><c>true</c> if this instance has time dependent material; otherwise, <c>false</c>.</value>
        public bool HasTimeDependentMaterial { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
