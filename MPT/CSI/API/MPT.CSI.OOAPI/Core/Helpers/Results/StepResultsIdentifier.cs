// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StepResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class StepResultsIdentifier.
    /// </summary>
    /// <seealso cref="ResultsIdentifier" />
    public class StepResultsIdentifier : ResultsIdentifier
    {
        /// <summary>
        /// The step type, if any, for the result.
        /// </summary>
        /// <value>The type.</value>
        public string StepType { get; set; }

        /// <summary>
        /// The step number, if any, for the result.
        /// </summary>
        /// <value>The numbers.</value>
        public double StepNumber{ get; set; }

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
