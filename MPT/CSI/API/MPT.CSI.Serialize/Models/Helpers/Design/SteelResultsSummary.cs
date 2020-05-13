// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="SteelResultsSummary.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a steel frame.
    /// </summary>
    /// <seealso cref="FrameResultsSummary" />
    public class SteelResultsSummary : FrameResultsSummary
    {
        /// <summary>
        /// The controlling stress or capacity ratio for the frame object.
        /// </summary>
        /// <value>The ratio.</value>
        public double Ratio { get; set; }

        /// <summary>
        /// The controlling stress or capacity ratio type for the frame object.
        /// </summary>
        /// <value>The type of the ratio.</value>
        public eRatioType RatioType { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling stress or capacity ratio occurs.
        /// </summary>
        /// <value>The name of the combo.</value>
        public string ComboName { get; set; }



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
