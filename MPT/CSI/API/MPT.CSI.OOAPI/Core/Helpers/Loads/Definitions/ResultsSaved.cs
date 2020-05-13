// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-20-2018
// ***********************************************************************
// <copyright file="ResultsSaved.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data relating to saved results for multi-step load cases.
    /// </summary>
    public class ResultsSaved : ApiProperty
    {
        /// <summary>
        /// Gets or sets the save multiple steps.
        /// </summary>
        /// <value>The save multiple steps.</value>
        public bool SaveMultipleSteps { get; set; }

        /// <summary>
        /// Gets or sets the minimum saved states.
        /// </summary>
        /// <value>The minimum saved states.</value>
        public int MinSavedStates { get; set; }

        /// <summary>
        /// Gets or sets the maximum saved states.
        /// </summary>
        /// <value>The maximum saved states.</value>
        public int MaxSavedStates { get; set; }

        /// <summary>
        /// Gets or sets the save positive displacement increments only.
        /// </summary>
        /// <value>The save positive displacement increments only.</value>
        public bool SavePositiveDisplacementIncrementsOnly { get; set; }


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
