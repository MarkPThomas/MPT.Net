// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="EN_3_2005_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class EN_3_2005_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class EN_3_2005_Preferences : SteelDesignPreferenceProperties
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
            /// Ductile Capacity Medium-Moment Resisting Frame
            /// </summary>
            [Description("DCM-MRF")]
            DCM_MRF = 2,

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
            /// Ductile Capacity Medium-Concentric Braced Frame
            /// </summary>
            [Description("DCM-CBF")]
            DCM_CBF = 5,

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
            /// Ductile Capacity Medium-Eccentric Braced Frame
            /// </summary>
            [Description("DCM-EBF")]
            DCM_EBF = 8,

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
        /// Country specific implementations considering country National Annex.
        /// CEN Default is the general version without an annex.
        /// </summary>
        public enum Countries
        {
            /// <summary>
            /// The cen default
            /// </summary>
            [Description("CEN Default")]
            CENDefault = 1,

            /// <summary>
            /// The denmark
            /// </summary>
            Denmark = 2,
            /// <summary>
            /// The finland
            /// </summary>
            Finland = 3,
            /// <summary>
            /// The germany
            /// </summary>
            Germany = 4,
            /// <summary>
            /// The ireland
            /// </summary>
            Ireland = 5,
            /// <summary>
            /// The norway
            /// </summary>
            Norway = 6,
            /// <summary>
            /// The poland
            /// </summary>
            Poland = 7,
            /// <summary>
            /// The portugal
            /// </summary>
            Portugal = 8,
            /// <summary>
            /// The singapore
            /// </summary>
            Singapore = 9,
            /// <summary>
            /// The slovenia
            /// </summary>
            Slovenia = 10,
            /// <summary>
            /// The sweden
            /// </summary>
            Sweden = 11,

            /// <summary>
            /// The united kingdom
            /// </summary>
            [Description("United Kingdom")]
            UnitedKingdom = 12,

            Bulgaria = 13
        }
        /// <summary>
        /// Country specific implementation considering country National Annex.
        /// CEN Default is the general version without an annex.
        /// </summary>
        /// <value>The country.</value>
        public Countries Country { get; protected set; } = Countries.CENDefault;

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
        /// Methods for determining the interaction factors for Eurocode 3-2005.
        /// </summary>
        public enum InteractionFactorsMethods
        {
            /// <summary>
            /// The method1
            /// </summary>
            [Description("Method 1 (Annex A)")]
            Method1 = 1,

            /// <summary>
            /// The method2
            /// </summary>
            [Description("Method 2 (Annex B)")]
            Method2 = 2,
        }
        /// <summary>
        /// Method for determining the interaction factors for Eurocode 3-2005.
        /// </summary>
        /// <value>The interaction factors method.</value>
        public InteractionFactorsMethods InteractionFactorsMethod { get; set; } = InteractionFactorsMethods.Method2;

        /// <summary>
        /// The partial factor for resistance of cross-sections.
        /// </summary>
        /// <value>The gamma m0.</value>
        public double GammaM0 { get; set; } = 1;

        /// <summary>
        /// The partial factor for resistance of members to instability.
        /// </summary>
        /// <value>The gamma m1.</value>
        public double GammaM1 { get; set; } = 1;

        /// <summary>
        /// The partial factor for resistance of cross-sections to tensile fracture.
        /// </summary>
        /// <value>The gamma m2.</value>
        public double GammaM2 { get; set; } = 1.25;

        /// <summary>
        /// This is called the Response Modification Factor.
        /// This is a function of Seismic Force Resisting System.
        /// The q values can be specified in the definition of Auto-Seismic Loads for load calculation.
        /// It can assume different values for load calculation in two orthogonal directions.
        /// The q value specifid here is solely used for design.
        /// For design, the program uses the same value for all directions.
        /// See EC8 sections 6.1.2(1), 6.3.2(1), and Table 6.2 for details. 
        /// </summary>
        /// <value>The q0.</value>
        public double q { get; set; } = 4;

        /// <summary>
        /// This is called the System Overstrength Factor.
        /// Omega factor is related to seismic factored member force and member capacity.
        /// It can assume different values in two orthogonal directions.
        /// The Omega value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See EC8 sections 6.6.3(1), 6.7.4(1), 6.8.3(1), and 6.8.4(1) for details.  
        /// </summary>
        /// <value>The omega.</value>
        public double Omega { get; set; } = 1;

        /// <summary>
        /// Toggle to consider whether P-Delta analysis is done.  This affects K factor calculation.
        /// </summary>
        /// <value><c>true</c> if [consider p delta done]; otherwise, <c>false</c>.</value>
        public bool ConsiderPDeltaDone { get; set; } = false;

        /// <summary>
        /// Toggle to consider whether torsion is to be considered.
        /// If chosen "Yes", the program considers torsion only for certain sections which are I, Channel, Rectangular Hollow, Square Hollow, and Circular Hollow sections.
        /// For all other sections, torsion is not considered in design checks even if this parameter is chosen as "Yes".
        /// </summary>
        /// <value><c>true</c> if [consider torsion]; otherwise, <c>false</c>.</value>
        public bool ConsiderTorsion { get; set; } = false;

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

        /// <summary>
        /// Sets the country default values for the preferences.
        /// </summary>
        /// <param name="country">The country.</param>
        public void SetCountryDefault(Countries country)
        {
            switch (country)
            {
                case Countries.CENDefault:
                    setCENDefaults();
                    break;
                case Countries.Bulgaria:
                    setBulgariaDefaults();
                    break;
                case Countries.Denmark:
                    setDenmarkDefaults();
                    break;
                case Countries.Finland:
                    setFinlandDefaults();
                    break;
                case Countries.Germany:
                    setGermanyDefaults();
                    break;
                case Countries.Ireland:
                    setIrelandDefaults();
                    break;
                case Countries.Norway:
                    setNorwayDefaults();
                    break;
                case Countries.Poland:
                    setPolandDefaults();
                    break;
                case Countries.Portugal:
                    setPortugalDefaults();
                    break;
                case Countries.Singapore:
                    setSingaporeDefaults();
                    break;
                case Countries.Slovenia:
                    setSloveniaDefaults();
                    break;
                case Countries.Sweden:
                    setSwedenDefaults();
                    break;
                case Countries.UnitedKingdom:
                    setUnitedKingdomDefaults();
                    break;
                default:
                    // No action taken
                    break;
            }
        }

        protected void setCENDefaults()
        {
            Country = Countries.CENDefault;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setBulgariaDefaults()
        {
            Country = Countries.Bulgaria;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1.05;
            GammaM1 = 1.05;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setDenmarkDefaults()
        {
            Country = Countries.Denmark;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1.1;
            GammaM1 = 1.2;
            GammaM2 = 1.35;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setFinlandDefaults()
        {
            Country = Countries.Finland;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setGermanyDefaults()
        {
            Country = Countries.Germany;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1.1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setIrelandDefaults()
        {
            Country = Countries.Ireland;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setNorwayDefaults()
        {
            Country = Countries.Norway;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1.05;
            GammaM1 = 1.05;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setPolandDefaults()
        {
            Country = Countries.Poland;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.1;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setPortugalDefaults()
        {
            Country = Countries.Portugal;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setSingaporeDefaults()
        {
            Country = Countries.Singapore;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.1;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setSloveniaDefaults()
        {
            Country = Countries.Slovenia;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setSwedenDefaults()
        {
            Country = Countries.Sweden;
            ReliabilityClass = ReliabilityClasses.Class3;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method1;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.25;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }

        protected void setUnitedKingdomDefaults()
        {
            Country = Countries.UnitedKingdom;
            ReliabilityClass = ReliabilityClasses.Class2;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            FrameType = FrameTypes.DCH_MRF;
            InteractionFactorsMethod = InteractionFactorsMethods.Method2;
            GammaM0 = 1;
            GammaM1 = 1;
            GammaM2 = 1.1;
            q = 4;
            Omega = 1;
            ConsiderPDeltaDone = false;
            ConsiderTorsion = false;
            UseSeismicCode = true;
            UseSeismicLoading = true;
            IsDoublerPlatePlugWelded = true;
        }
    }
}
