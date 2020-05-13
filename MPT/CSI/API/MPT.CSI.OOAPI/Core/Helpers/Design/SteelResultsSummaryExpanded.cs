// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="SteelResultsSummaryExpanded.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Design
{
    /// <summary>
    /// Repesents the expanded design results summary data for a steel frame.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Design.FrameResultsSummary" />
    public class SteelResultsSummaryExpanded: ApiProperty
    {
        /// <summary>
        /// Gets or sets the type of the frame.
        /// </summary>
        /// <value>The type of the frame.</value>
        public string FrameType { get; set; }

        /// <summary>
        /// Gets or sets the design section.
        /// </summary>
        /// <value>The design section.</value>
        public string DesignSection { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the PMM combo.
        /// </summary>
        /// <value>The PMM combo.</value>
        public string PMMCombo { get; set; }

        /// <summary>
        /// Gets or sets the PMM ratio.
        /// </summary>
        /// <value>The PMM ratio.</value>
        public double PMMRatio { get; set; }

        /// <summary>
        /// Gets or sets the p ratio.
        /// </summary>
        /// <value>The p ratio.</value>
        public double PRatio { get; set; }

        /// <summary>
        /// Gets or sets the m major ratio.
        /// </summary>
        /// <value>The m major ratio.</value>
        public double MMajorRatio { get; set; }

        /// <summary>
        /// Gets or sets the m minor ratio.
        /// </summary>
        /// <value>The m minor ratio.</value>
        public double MMinorRatio { get; set; }

        /// <summary>
        /// Gets or sets the v major combo.
        /// </summary>
        /// <value>The v major combo.</value>
        public string VMajorCombo { get; set; }

        /// <summary>
        /// Gets or sets the v major ratio.
        /// </summary>
        /// <value>The v major ratio.</value>
        public double VMajorRatio { get; set; }

        /// <summary>
        /// Gets or sets the v minor combo.
        /// </summary>
        /// <value>The v minor combo.</value>
        public string VMinorCombo { get; set; }

        /// <summary>
        /// Gets or sets the v minor ratio.
        /// </summary>
        /// <value>The v minor ratio.</value>
        public double VMinorRatio { get; set; }



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
