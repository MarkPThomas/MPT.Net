// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="JointLabelNameResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class JointLabelNameResultsIdentifier.
    /// </summary>
    /// <seealso cref="StepResultsIdentifier" />
    public class JointLabelNameResultsIdentifier : LabelNameResultsIdentifier
    {
        /// <summary>
        /// The joint name.
        /// </summary>
        /// <value>The type.</value>
        public string JointName { get; set; }


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
