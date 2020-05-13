// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ConcreteResultsSummaryJoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a concrete joint.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Design.ResultsSummary" />
    public class ConcreteResultsSummaryJoint : ResultsSummary
    {
        /// <summary>
        /// The name of the design combination for which the controlling joint shear ratio associated with the column major axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The LCJS ratio major.</value>
        public string JointShearRatioMajorCombo { get; set; }

        /// <summary>
        /// The joint shear ratio associated with the column major axis.
        /// This is the joint shear divided by the joint shear capacity.
        /// </summary>
        /// <value>The js ratio major.</value>
        public double JointShearRatioMajor { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling joint shear ratio associated with the column minor axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The LCJS ratio minor.</value>
        public string JointShearRatioMinorCombo { get; set; }

        /// <summary>
        /// The joint shear ratio associated with the column minor axis.
        /// This is the joint shear divided by the joint shear capacity.
        /// </summary>
        /// <value>The js ratio minor.</value>
        public double JointShearRatioMinor { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling beam/column capacity ratio associated with the column major axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The LCBCC ratio major.</value>
        public string BCCRatioMajorCombo { get; set; }

        /// <summary>
        /// The beam/column capacity ratio associated with the column major axis.
        /// </summary>
        /// <value>The BCC ratio major.</value>
        public double BCCRatioMajor { get; set; }

        /// <summary>
        /// The name of the design combination for which the controlling beam/column capacity ratio associated with the column minor axis occurs.
        /// A combination name followed by (Sp) indicates that the design loads were obtained by applying special, code-specific multipliers to all or part of the specified design load combination, or that the design was based on the capacity of other objects (or other design locations for the same object).
        /// </summary>
        /// <value>The LCBCC ratio minor.</value>
        public string BCCRatioMinorCombo { get; set; }

        /// <summary>
        /// The beam/column capacity ratio associated with the column minor axis.
        /// </summary>
        /// <value>The BCC ratio minor.</value>
        public double BCCRatioMinor { get; set; }



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
