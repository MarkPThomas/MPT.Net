// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ConcreteResultsSummaryColumn.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a concrete column.
    /// </summary>
    /// <seealso cref="FrameResultsSummary" />
    public class ConcreteResultsSummaryColumn : FrameResultsSummary
    {
        /// <summary>
        /// The design option for each frame object.
        /// </summary>
        /// <value>My option.</value>
        public eDesignType DesignOption { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling PMM ratio or rebar area occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The PMM combo.</value>
        public string PMMCombo { get; set; }

        /// <summary>
        /// This is an array that includes the total longitudinal rebar area required for the axial force plus biaxial moment (PMM) design at the specified location. [L^2]
        /// </summary>
        /// <value>The PMM area.</value>
        public double PMMArea { get; set; }

        /// <summary>
        /// This is an array that includes the axial force plus biaxial moment (PMM) stress ratio at the specified location.
        /// Item applies only when <see cref="DesignOption" /> = <see cref="eDesignType.Check" />.
        /// </summary>
        /// <value>The PMM ratio.</value>
        public double PMMRatio { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling major shear occurs.
        /// </summary>
        /// <value>The v major combo.</value>
        public string VMajorCombo { get; set; }

        /// <summary>
        /// The required area of transverse shear reinforcing per unit length along the frame object for major shear at the specified location. [L^2/L]
        /// </summary>
        /// <value>The av major.</value>
        public double AVMajor { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling minor shear occurs.
        /// </summary>
        /// <value>The v minor combo.</value>
        public string VMinorCombo { get; set; }

        /// <summary>
        /// The required area of transverse shear reinforcing per unit length along the frame object for minor shear at the specified location. [L^2/L]
        /// </summary>
        /// <value>The av minor.</value>
        public double AVMinor { get; set; }



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
