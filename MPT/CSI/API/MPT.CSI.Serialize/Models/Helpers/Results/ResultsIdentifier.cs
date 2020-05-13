// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="ResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class ResultsIdentifier.
    /// </summary>
    public class ResultsIdentifier : ModelProperty
    {
        /// <summary>
        /// The name of the analysis case or load combination associated with the result.
        /// </summary>
        /// <value>The load case.</value>
        public string LoadCase { get; set; }


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
