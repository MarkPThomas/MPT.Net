// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="EN_2_2004_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class EN_2_2004_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class EN_2_2004_Preferences : ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// Country specific implementations considering country National Annex.
        /// CEN Default is the general version without an annex.
        /// </summary>
        public enum Countries
        {
            [Description("CEN Default")]
            CENDefault = 1,

            Denmark = 2,
            Finland = 3,
            Germany = 4,
            Ireland = 5,
            Norway = 6,
            Poland = 7,
            Portugal = 8,
            Singapore = 9,
            Slovenia = 10,
            Sweden = 11,

            [Description("United Kingdom")]
            UnitedKingdom = 12,


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
        /// Second-order methods to be used for calculating second-order moments.
        /// </summary>
        public enum SecondOrderMethods
        {
            /// <summary>
            /// The nominal stiffness
            /// </summary>
            [Description("Nominal Stiffness")]
            NominalStiffness = 1,

            /// <summary>
            /// The nominal curvature
            /// </summary>
            [Description("Nominal Curvature")]
            NominalCurvature = 2,

            /// <summary>
            /// The none
            /// </summary>
            None = 3
        }

        /// <summary>
        /// Second-order method to be used.
        /// If None is chosen, no second-order moments are computed.
        /// </summary>
        /// <value>The second order method.</value>
        public SecondOrderMethods SecondOrderMethod { get; set; } = SecondOrderMethods.NominalCurvature;


        /// <summary>
        /// Theta0 is the inclination angle for imperfections, defined as a ratio.
        /// </summary>
        /// <value>The theta0.</value>
        public double Theta0 { get; set; } = 0.005;

        /// <summary>
        /// GammaS is the partial safety factor for steel.
        /// </summary>
        /// <value>The gamma s.</value>
        public double GammaS { get; set; } = 1.15;

        /// <summary>
        /// GammaC is the partial safety factor for concrete.
        /// </summary>
        /// <value>The gamma c.</value>
        public double GammaC { get; set; } = 1.5;

        /// <summary>
        /// AlphaCC is the material coefficient taking account of long term effects on the concrete compressive strength and unfavourable effects from the load application.
        /// </summary>
        /// <value>The alpha cc.</value>
        public double AlphaCC { get; set; } = 1;

        /// <summary>
        /// AlphaCT is the material coefficient taking account of long term effects on the concrete tensile strength and unfavourable effects from the load application.
        /// </summary>
        /// <value>The alpha ct.</value>
        public double AlphaCT { get; set; } = 1;

        /// <summary>
        /// AlphaLCC is the lightweight material coefficient taking account of long term effects on the concrete compressive strength and unfavourable effects from the load application.
        /// </summary>
        /// <value>The alpha LCC.</value>
        public double AlphaLCC { get; set; } = 0.85;

        /// <summary>
        /// AlphaLCT is the lightweight material coefficient taking account of long term effects on the concrete tensile strength and unfavourable effects from the load application.
        /// </summary>
        /// <value>The alpha LCT.</value>
        public double AlphaLCT { get; set; } = 0.85;

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
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setDenmarkDefaults()
        {
            Country = Countries.Denmark;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalStiffness;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            Theta0 = 0.005;
            GammaS = 1.2;
            GammaC = 1.45;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 1;
            AlphaLCT = 1;
        }

        protected void setFinlandDefaults()
        {
            Country = Countries.Finland;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setGermanyDefaults()
        {
            Country = Countries.Germany;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalStiffness;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 0.75;
            AlphaLCC = 0.85;
            AlphaLCT = 0.75;
        }

        protected void setIrelandDefaults()
        {
            Country = Countries.Ireland;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setNorwayDefaults()
        {
            Country = Countries.Norway;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 0.85;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setPolandDefaults()
        {
            Country = Countries.Poland;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalStiffness;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.4;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setPortugalDefaults()
        {
            Country = Countries.Portugal;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setSingaporeDefaults()
        {
            Country = Countries.Singapore;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setSloveniaDefaults()
        {
            Country = Countries.Slovenia;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }

        protected void setSwedenDefaults()
        {
            Country = Countries.Sweden;
            ReliabilityClass = ReliabilityClasses.Class3;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Max_6_10a_6_10b;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 1;
            AlphaCT = 1;
            AlphaLCC = 1;
            AlphaLCT = 1;
        }

        protected void setUnitedKingdomDefaults()
        {
            Country = Countries.UnitedKingdom;
            ReliabilityClass = ReliabilityClasses.Class2;
            SecondOrderMethod = SecondOrderMethods.NominalCurvature;
            DesignCombinationEquation = DesignCombinationEquations.Eq_6_10;
            Theta0 = 0.005;
            GammaS = 1.15;
            GammaC = 1.5;
            AlphaCC = 0.85;
            AlphaCT = 1;
            AlphaLCC = 0.85;
            AlphaLCT = 0.85;
        }
    }
}
