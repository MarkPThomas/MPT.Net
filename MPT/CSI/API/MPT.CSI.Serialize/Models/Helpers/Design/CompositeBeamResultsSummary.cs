// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="CompositeBeamResultsSummary.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Repesents the basic design results summary data for a composite beam.
    /// </summary>
    public class CompositeBeamResultsSummary : ModelProperty
    {
        /// <summary>
        /// The design section.
        /// </summary>
        /// <value>The design section.</value>
        public string DesignSection { get; set; }

        /// <summary>
        /// The beam steel yield strength, Fy. [F/L^2].
        /// </summary>
        /// <value>The beam fy.</value>
        public double BeamFy { get; set; }

        /// <summary>
        /// The stud diameter. [L].
        /// </summary>
        /// <value>The stud diameter.</value>
        public double StudDiameter { get; set; }

        /// <summary>
        /// The stud layout.
        /// </summary>
        /// <value>The stud layout.</value>
        public string StudLayout { get; set; }

        /// <summary>
        /// True: The is beam shored.
        /// </summary>
        /// <value><c>true</c> if this instance is beam shored; otherwise, <c>false</c>.</value>
        public bool IsBeamShored { get; set; }

        /// <summary>
        /// The beam cambers. [L]
        /// </summary>
        /// <value>The beam cambers.</value>
        public double BeamCambers { get; set; }

        /// <summary>
        /// The pass fail design status.
        /// </summary>
        /// <value>The pass fail.</value>
        public string PassFail { get; set; }

        /// <summary>
        /// The left support reactions.
        /// </summary>
        /// <value>The reaction left.</value>
        public double ReactionLeft { get; set; }

        /// <summary>
        /// The right support reactions.
        /// </summary>
        /// <value>The reaction right.</value>
        public double ReactionRight { get; set; }

        /// <summary>
        /// The maximum negative moment.
        /// </summary>
        /// <value>The m maximum negative.</value>
        public double MMaxNegative { get; set; }

        /// <summary>
        /// The maximum positive moment.
        /// </summary>
        /// <value>The m maximum positive.</value>
        public double MMaxPositive { get; set; }

        /// <summary>
        /// The percent composite connection (PCC).
        /// </summary>
        /// <value>The percent composite connection.</value>
        public double PercentCompositeConnection { get; set; }

        /// <summary>
        /// The overall ratio.
        /// </summary>
        /// <value>The overall ratio.</value>
        public double OverallRatio { get; set; }

        /// <summary>
        /// The stud ratio.
        /// </summary>
        /// <value>The stud ratio.</value>
        public double StudRatio { get; set; }

        /// <summary>
        /// The strength ratio considering PM (Axial &amp; Bending).
        /// </summary>
        /// <value>The strength ratio pm.</value>
        public double StrengthRatioPM { get; set; }

        /// <summary>
        /// The construction ratio considering PM (Axial &amp; Bending).
        /// </summary>
        /// <value>The construction ratio pm.</value>
        public double ConstructionRatioPM { get; set; }

        /// <summary>
        /// The strength shear ratio.
        /// </summary>
        /// <value>The strength shear ratio.</value>
        public double StrengthShearRatio { get; set; }

        /// <summary>
        /// The construction shear ratio.
        /// </summary>
        /// <value>The construction shear ratio.</value>
        public double ConstructionShearRatio { get; set; }

        /// <summary>
        /// The deflection ratio post-concrete, DL (Dead Load).
        /// </summary>
        /// <value>The deflection ratio post concrete dl.</value>
        public double DeflectionRatioPostConcreteDL { get; set; }

        /// <summary>
        /// The deflection ratio, SDL (Specified Dead Load).
        /// </summary>
        /// <value>The deflection ratio SDL.</value>
        public double DeflectionRatioSDL { get; set; }

        /// <summary>
        /// The deflection ratio, LL (Live Load).
        /// </summary>
        /// <value>The deflection ratio ll.</value>
        public double DeflectionRatioLL { get; set; }

        /// <summary>
        /// The deflection ratio from total camber.
        /// </summary>
        /// <value>The deflection ratio total camber.</value>
        public double DeflectionRatioTotalCamber { get; set; }

        /// <summary>
        /// The walking frequency ratio.
        /// </summary>
        /// <value>The frequency ratio.</value>
        public double FrequencyRatio { get; set; }

        /// <summary>
        /// The M damping ratio.
        /// </summary>
        /// <value>The m damping ratio.</value>
        public double MDampingRatio { get; set; }



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
