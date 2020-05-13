// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ConcreteResultsSummaryBeam.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a concrete beam.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Design.FrameResultsSummary" />
    public class ConcreteResultsSummaryBeam : FrameResultsSummary
    {
        /// <summary>
        /// The name of the design combination for which the controlling top longitudinal rebar area for flexure occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The top combo.</value>
        public string TopCombo { get; set; }

        /// <summary>
        /// The total top longitudinal rebar area required for the flexure at the specified location.
        /// It does not include the area of steel required for torsion. [L^2]
        /// </summary>
        /// <value>The top area.</value>
        public double TopArea { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling bottom longitudinal rebar area for flexure occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The bottom combo.</value>
        public string BottomCombo { get; set; }

        /// <summary>
        /// The total bottom longitudinal rebar area required for the flexure at the specified location.
        /// It does not include the area of steel required for torsion. [L^2]
        /// </summary>
        /// <value>The bottom area.</value>
        public double BottomArea { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling shear occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The v major combo.</value>
        public string VMajorCombo { get; set; }

        /// <summary>
        /// The required area of transverse shear reinforcing per unit length along the frame object for shear at the specified location. [L^2/L]
        /// </summary>
        /// <value>The v major area.</value>
        public double VMajorArea { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling longitudinal rebar area for torsion occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The tl combo.</value>
        public string TLCombo { get; set; }

        /// <summary>
        /// The total longitudinal rebar area required for torsion. [L^2]
        /// </summary>
        /// <value>The tl area.</value>
        public double TLArea { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling transverse reinforcing for torsion occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific, multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The tt combo.</value>
        public string TTCombo { get; set; }

        /// <summary>
        /// The required area of transverse torsional shear reinforcing per unit length along the frame object for torsion at the specified location. [L^2/L]
        /// </summary>
        /// <value>The tt area.</value>
        public double TTArea { get; set; }



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
