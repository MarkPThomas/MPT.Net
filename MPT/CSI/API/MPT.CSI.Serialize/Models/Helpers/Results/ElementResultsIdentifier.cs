// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="ElementResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class ElementResultsIdentifier.
    /// </summary>
    /// <seealso cref="StepResultsIdentifier" />
    public class ElementResultsIdentifier : StepResultsIdentifier
    {

        /// <summary>
        /// The element associated with the result.
        /// </summary>
        /// <value>The name of the element.</value>
        public string ElementName { get; set; }

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
