// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-07-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="NTC_2018_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class NTC_2018_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class NTC_2018_Preferences : SteelDesignPreferenceProperties
    {
        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Ductile Capacity High-Moment Resisting Frame
            /// </summary>
            [Description("DCH-MRF")]
            DCH_MRF = 1,

            /// <summary>
            /// Ductile Capacity Low-Moment Resisting Frame
            /// </summary>
            [Description("DCL-MRF")]
            DCL_MRF = 3,

            /// <summary>
            /// Ductile Capacity High-Concentric Braced Frame
            /// </summary>
            [Description("DCH-CBF")]
            DCH_CBF = 4,

            /// <summary>
            /// Ductile Capacity Low-Concentric Braced Frame
            /// </summary>
            [Description("DCL-CBF")]
            DCL_CBF = 6,

            /// <summary>
            /// Ductile Capacity High-Eccentric Braced Frame
            /// </summary>
            [Description("DCH-EBF")]
            DCH_EBF = 7,

            /// <summary>
            /// Ductile Capacity Low-Eccentric Braced Frame
            /// </summary>
            [Description("DCL-EBF")]
            DCL_EBF = 9,

            /// <summary>
            /// Inverted Pendulum
            /// </summary>
            InvPendulum = 10,

            /// <summary>
            /// Non Dissipative
            /// </summary>
            [Description("Non Dissipative")]
            NonDissipative = 11,
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.DCH_MRF;

        /// <summary>
        /// Reliabilty classes which define a scale factor for load combinations.
        /// </summary>
        public enum ReliabilityClasses
        {
            /// <summary>
            /// The class1
            /// </summary>
            [Description("Class 1")]
            Class1,

            /// <summary>
            /// The class2
            /// </summary>
            [Description("Class 2")]
            Class2,

            /// <summary>
            /// The class3
            /// </summary>
            [Description("Class 3")]
            Class3
        }
        /// <summary>
        /// Reliabilty class which defines a scale factor for load combinations.
        /// This is currently only used for Denmark, Finland, and Sweden.
        /// </summary>
        /// <value>The reliability class.</value>
        public ReliabilityClasses ReliabilityClass { get; set; } = ReliabilityClasses.Class2;

        /// <summary>
        /// Design code combinations to consider.
        /// </summary>
        public enum DesignCombinationEquations
        {

            /// <summary>
            /// The eq 6 10
            /// </summary>
            [Description("Eq. 6.10")]
            Eq_6_10 = 1,

            /// <summary>
            /// The maximum 6 10a 6 10B
            /// </summary>
            [Description("Max of Eq. 6.10a/6.10b")]
            Max_6_10a_6_10b = 2
        }
        /// <summary>
        /// Design code combinations to consider.
        /// These are either generated from Eq. 6.10 or from both Eqs. 6.10a and 6.10b.
        /// </summary>
        /// <value>The design combination equation.</value>
        public DesignCombinationEquations DesignCombinationEquation { get; set; } = DesignCombinationEquations.Eq_6_10;

        /// <summary>
        /// Methods for determining the interaction factors for "Italian NTC 2008".
        /// </summary>
        public enum InteractionFactorsMethods
        {
            /// <summary>
            /// Method A based on equation C4.2.32.
            /// </summary>
            [Description("Method A")]
            MethodA = 1,

            /// <summary>
            /// Method B based on equation C4.2.37.
            /// </summary>
            [Description("Method B")]
            MethodB = 2,

            /// <summary>
            /// The both
            /// </summary>
            Both = 3
        }
        /// <summary>
        /// Method for determining the interaction factors for "Italian NTC 2008".
        /// </summary>
        /// <value>The interaction factors method.</value>
        public InteractionFactorsMethods InteractionFactorsMethod { get; set; } = InteractionFactorsMethods.MethodB;

        /// <summary>
        /// The partial factor for resistance of cross-sections for Italian NTC 2008.
        /// </summary>
        /// <value>The gamma m0.</value>
        public double GammaM0 { get; set; } = 1.05;

        /// <summary>
        /// The partial factor for resistance of members to instability for Italian NTC 2008.
        /// </summary>
        /// <value>The gamma m1.</value>
        public double GammaM1 { get; set; } = 1.05;

        /// <summary>
        /// The partial factor for resistance of cross-sections to tensile fracture for Italian NTC 2008.
        /// </summary>
        /// <value>The gamma m2.</value>
        public double GammaM2 { get; set; } = 1.25;

        /// <summary>
        /// This is called the Response Modification Factor.
        /// This is a function of Seismic Force Resisting System.
        /// The q0 values can be specified in the definition of Auto-Seismic Loads for load calculation.
        /// It can assume different values for load calculation in two orthogonal directions.
        /// The q0 value specifid here is solely used for design.
        /// For design, the program uses the same value for all directions.
        /// See NTC2008 section 7.5.2.2 and Table 7.5.II for details.
        /// </summary>
        /// <value>The q0.</value>
        public double q0 { get; set; } = 4;

        /// <summary>
        /// This is called the System Overstrength Factor.
        /// Omega factor is related to seismic factored member force and member capacity.
        /// It can assume different values in two orthogonal directions.
        /// The Omega value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See NTC2008 section 7.5.5, 7.5.6, and 7.5.6 for details.
        /// </summary>
        /// <value>The omega.</value>
        public double Omega { get; set; } = 1;

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
        /// Gets or sets a value indicating whether this instance is doubler plate plug welded.
        /// </summary>
        /// <value><c>true</c> if this instance is doubler plate plug welded; otherwise, <c>false</c>.</value>
        public bool IsDoublerPlatePlugWelded { get; set; } = true;

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
