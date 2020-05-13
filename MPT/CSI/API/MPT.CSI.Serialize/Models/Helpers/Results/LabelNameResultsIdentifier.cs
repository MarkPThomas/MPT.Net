// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="LabelNameResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class LabelNameResultsIdentifier.
    /// </summary>
    /// <seealso cref="StepResultsIdentifier" />
    public class LabelNameResultsIdentifier : StepResultsIdentifier
    {
        /// <summary>
        /// The story name.
        /// </summary>
        /// <value>The type.</value>
        public string StoryName { get; set; }

        /// <summary>
        /// The object label.
        /// </summary>
        /// <value>The type.</value>
        public string Label { get; set; }


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
