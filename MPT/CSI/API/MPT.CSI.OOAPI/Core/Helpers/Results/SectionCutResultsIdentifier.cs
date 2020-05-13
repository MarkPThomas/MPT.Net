// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="SectionCutResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class SectionCutResultsIdentifier.
    /// </summary>
    /// <seealso cref="StepResultsIdentifier" />
    public class SectionCutResultsIdentifier : StepResultsIdentifier
    {
        /// <summary>
        /// Gets or sets the name of the section cut.
        /// </summary>
        /// <value>The name of the section cut.</value>
        public string SectionCutName { get; set; }


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
