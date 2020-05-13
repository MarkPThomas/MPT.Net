// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="NTC_2008_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class NTC_2008_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class NTC_2008_Preferences : ConcreteDesignPreferencesProperties
    {
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
        public SecondOrderMethods SecondOrderMethod { get; set; } = SecondOrderMethods.NominalStiffness;

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
        public double AlphaCC { get; set; } = 0.85;

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
    }
}
