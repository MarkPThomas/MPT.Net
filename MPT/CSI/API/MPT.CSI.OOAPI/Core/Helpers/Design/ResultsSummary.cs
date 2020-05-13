// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ResultsSummary.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data.
    /// </summary>
    public class ResultsSummary: ApiProperty
    {
        /// <summary>
        /// The frame object name for which results are obtained..
        /// </summary>
        /// <value>The name of the frame.</value>
        public string FrameName { get; set; }

        /// <summary>
        /// The design error messages for the object, if any.
        /// </summary>
        /// <value>The error summary.</value>
        public string ErrorSummary { get; set; }

        /// <summary>
        /// The design warning messages for the object, if any.
        /// </summary>
        /// <value>The warning summary.</value>
        public string WarningSummary { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
