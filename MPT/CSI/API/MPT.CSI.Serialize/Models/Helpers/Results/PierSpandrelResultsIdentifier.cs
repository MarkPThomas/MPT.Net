// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="PierSpandrelResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class PierSpandrelResultsIdentifier.
    /// </summary>
    /// <seealso cref="ResultsIdentifier" />
    public class PierSpandrelResultsIdentifier : ResultsIdentifier
    {
        /// <summary>
        /// The name of the pier (P) or spandrel (S).
        /// </summary>
        /// <value>The name.</value>
        public string PierSpandrelName { get; set; }

        /// <summary>
        /// The location on the pier/spandrel of the result being reported.
        /// </summary>
        /// <value>The location.</value>
        public eLocationVertical Location { get; set; }

        /// <summary>
        /// The name of the story for the results.
        /// </summary>
        /// <value>The name of the story.</value>
        public string StoryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pier. Otherwise it is a spandrel.
        /// </summary>
        /// <value><c>true</c> if this instance is pier; otherwise, <c>false</c>.</value>
        public bool IsPier { get; set; }


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
