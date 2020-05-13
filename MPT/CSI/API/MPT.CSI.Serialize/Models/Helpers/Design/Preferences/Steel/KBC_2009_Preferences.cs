// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="KBC_2009_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class KBC_2009_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class KBC_2009_Preferences : SteelDesignPreferenceProperties
    {
        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Special Moment Frame
            /// </summary>
            SMF = 1,

            /// <summary>
            /// Intermediate Moment Frame
            /// </summary>
            IMF = 2,

            /// <summary>
            /// Ordinary Moment Frame
            /// </summary>
            OMF = 3,

            /// <summary>
            /// Special Concentric Braced Frame
            /// </summary>
            SCBF = 4,

            /// <summary>
            /// Ordinary Concentric Braced Frame
            /// </summary>
            OCBF = 5,

            /// <summary>
            /// Base Isolated Ordinary Concentric Braced Frame
            /// </summary>
            OCBFI = 6,

            /// <summary>
            /// Eccentric Braced Frame
            /// </summary>
            EBF = 7,
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.SMF;

        /// <summary>
        /// The resistance factor for bending.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for compression.
        /// </summary>
        /// <value>The phi c.</value>
        public double PhiC { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for tension-yielding.
        /// </summary>
        /// <value>The phi ty.</value>
        public double PhiTY { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for tension-fracture.
        /// </summary>
        /// <value>The phi tf.</value>
        public double PhiTF { get; set; } = 0.75;

        /// <summary>
        /// The resistance factor for shear.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for shear of short-webbed rolled I-sections.
        /// </summary>
        /// <value>The phi v rolled.</value>
        public double PhiVRolledI { get; set; } = 1;

        /// <summary>
        /// The resistance factor for torsion.
        /// </summary>
        /// <value>The phi vt.</value>
        public double PhiVT { get; set; } = 0.9;

        /// <summary>
        /// Toggle to consider whether the seismic part of the code should be considered in design.
        /// </summary>
        /// <value><c>true</c> if [use seismic code]; otherwise, <c>false</c>.</value>
        public bool UseSeismicCode { get; set; } = true;

        /// <summary>
        /// Toggle to consider whether the special seismic load combinations should be considered in design.
        /// </summary>
        /// <value><c>true</c> if [use seismic loading]; otherwise, <c>false</c>.</value>
        public bool UseSeismicLoading { get; set; } = true;

        /// <summary>
        /// This is related to seismic design.
        /// This is a function of Occupancy Category and Sds or Sd1.
        /// See ASCE 7-05 section 11.6 for details.
        /// </summary>
        public enum SeismicDesignCategories
        {
            /// <summary>
            /// a
            /// </summary>
            A = 1,
            /// <summary>
            /// The b
            /// </summary>
            B = 2,
            /// <summary>
            /// The c
            /// </summary>
            C = 3,
            /// <summary>
            /// The d
            /// </summary>
            D = 4,
            /// <summary>
            /// The e
            /// </summary>
            E = 5,
            /// <summary>
            /// The f
            /// </summary>
            F = 6,
        }
        /// <summary>
        /// This is related to seismic design.
        /// This is a function of Occupancy Category and Sds or Sd1.
        /// See ASCE 7-05 section 11.6 for details.
        /// </summary>
        /// <value>The seismic design category.</value>
        public SeismicDesignCategories SeismicDesignCategory { get; set; } = SeismicDesignCategories.D;

        /// <summary>
        /// This is related to seismic design.
        /// This is a function of Occupancy Category.
        /// See ASCE 7-05 section 11.5.1 for details.
        /// </summary>
        /// <value>The importance factor.</value>
        public double ImportanceFactor { get; set; } = 1;

        /// <summary>
        /// This is called the redundancy factor.
        /// It is related to seismic design.
        /// This is used in the default design combinations.
        /// The program uses the same value for all directions.
        /// See ASCE 7-05 section 12.3.4 for details.  .
        /// </summary>
        /// <value>The system rho.</value>
        public double Rho { get; set; } = 1;

        /// <summary>
        /// This is called the Design Spectral Acceleration Parameter.
        /// This is related to seismic design.
        /// See ASCE 7-05 section 11.4.4 and 11.4.5 for details.
        /// </summary>
        /// <value>The system SDS.</value>
        public double Sds { get; set; } = 0.5;

        /// <summary>
        /// This is called the Response Modification Factor.
        /// This is a function of Seismic Force Resisting System.
        /// The R values can be specified in the definition of Auto-Seismic Loads for load calculation.
        /// It can assume different values for load calculation in two orthogonal directions.
        /// The R value specifid here is solely used for design.
        /// For design, the program uses the same value for all directions.
        /// See ASCE 7-05 section 12.2.1 and Table 12.2-1 for details.
        /// </summary>
        /// <value>The system r.</value>
        public double R { get; set; } = 8;

        /// <summary>
        /// This is called the Deflection Amplification Factor.
        /// This is a function of Seismic Force Resisting System.
        /// It can assume different values in two orthogonal directions.
        /// The Cd value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See ASCE 7-05 section 12.2.1 and Table 12.2-1 for details.
        /// </summary>
        /// <value>The system cd.</value>
        public double Cd { get; set; } = 5.5;

        /// <summary>
        /// This is called the System Overstrength Factor.
        /// This is a function of Seismic Force Resisting System.
        /// It can assume different values in two orthogonal directions.
        /// The Omega0 value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See ASCE 7-05 section 12.2.1 and Table 12.2-1 for details.
        /// </summary>
        /// <value>The omega0.</value>
        public double Omega0 { get; set; } = 3;

        /// <summary>
        /// Gets or sets the nl coeff.
        /// </summary>
        /// <value>The nl coeff.</value>
        public double NLCoeff { get; set; } = 0.005;

        /// <summary>
        /// Analysis methods used to check/design the steel members.
        /// </summary>
        public enum AnalysisMethods
        {
            /// <summary>
            /// The direct analysis
            /// </summary>
            [Description("Direct Analysis")]
            DirectAnalysis = 1,

            /// <summary>
            /// The effective length
            /// </summary>
            [Description("Effective Length")]
            EffectiveLength = 2,

            /// <summary>
            /// The limited1st order
            /// </summary>
            [Description("Limited 1st Order")]
            Limited1stOrder = 3,
        }
        /// <summary>
        /// Analysis method used to check/design the steel members.
        /// </summary>
        /// <value>The analysis method.</value>
        public AnalysisMethods AnalysisMethod { get; set; } = AnalysisMethods.DirectAnalysis;

        /// <summary>
        /// Indicates the second order methods used to analyze the structure.
        /// </summary>
        public enum SecondOrderMethods
        {
            /// <summary>
            /// The general2nd order
            /// </summary>
            [Description("General 2nd Order")]
            General2ndOrder = 1,

            /// <summary>
            /// The amplified1st order
            /// </summary>
            [Description("Amplified 1st Order")]
            Amplified1stOrder = 2
        }
        /// <summary>
        /// Indicates the second order method used to analyze the structure.
        /// </summary>
        /// <value>The second order method.</value>
        public SecondOrderMethods SecondOrderMethod { get; set; } = SecondOrderMethods.General2ndOrder;

        /// <summary>
        /// Indicates the stiffness reduction methods used to analyze the structure.
        /// </summary>
        public enum StiffnessReductionMethods
        {
            /// <summary>
            /// The tau b variable
            /// </summary>
            [Description("Tau-b Variable")]
            Tau_b_Variable = 1,

            /// <summary>
            /// The tau b fixed
            /// </summary>
            [Description("Tau-b Fixed")]
            Tau_b_Fixed = 2,

            /// <summary>
            /// The none
            /// </summary>
            [Description("No Modification")]
            None = 3
        }
        /// <summary>
        /// Indicates the stiffness reduction method used to analyze the structure.
        /// </summary>
        /// <value>The stiffness reduction method.</value>
        public StiffnessReductionMethods StiffnessReductionMethod { get; set; } = StiffnessReductionMethods.Tau_b_Fixed;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is doubler plate plug welded.
        /// </summary>
        /// <value><c>true</c> if this instance is doubler plate plug welded; otherwise, <c>false</c>.</value>
        public bool IsDoublerPlatePlugWelded { get; set; } = true;

        /// <summary>
        /// The procedures used for welding.
        /// </summary>
        public enum HSSWeldingTypes
        {
            /// <summary>
            /// Electric resistance welding.
            /// </summary>
            ERW = 1,

            /// <summary>
            /// Shielded-arc welding.
            /// </summary>
            SAW = 2
        }
        /// <summary>
        /// The procedure used for welding.
        /// </summary>
        /// <value>The type of the HSS welding.</value>
        public HSSWeldingTypes HSSWeldingType { get; set; } = HSSWeldingTypes.ERW;

        /// <summary>
        /// Toggle to consider whether the reduction of HSS (box or Pipe) thickness should be considered.
        /// </summary>
        /// <value><c>true</c> if [reduce HSS thickness]; otherwise, <c>false</c>.</value>
        public bool ReduceHSSThickness { get; set; } = false;

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
