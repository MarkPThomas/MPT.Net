// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="FrameResultsSummary.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a frame.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Design.ResultsSummary" />
    public class FrameResultsSummary : ResultsSummary
    {
        /// <summary>
        /// The distance from the I-end of the frame object to the location where the controlling stress or capacity ratio occurs. [L]
        /// </summary>
        /// <value>The location.</value>
        public double Location { get; set; }
        
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
