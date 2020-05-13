// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="GeneralizedDisplacementResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class GeneralizedDisplacementResultsIdentifier.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Results.StepResultsIdentifier" />
    public class GeneralizedDisplacementResultsIdentifier : StepResultsIdentifier
    {
        /// <summary>
        /// The generalized displacement name associated with the result.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }


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
