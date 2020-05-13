using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    public class NTC_2018_Overwrites : SteelDesignOverwriteProperties
    {
        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public NTC_2018_Preferences.FrameTypes? FrameType { get; set; }

        /// <summary>
        /// Enum SectionClasses
        /// </summary>
        public enum SectionClasses
        {
            /// <summary>
            /// The class1
            /// </summary>
            Class1 = 1,
            /// <summary>
            /// The class2
            /// </summary>
            Class2 = 2,
            /// <summary>
            /// The class3
            /// </summary>
            Class3 = 3,
            /// <summary>
            /// The class4
            /// </summary>
            Class4 = 4
        }
        /// <summary>
        /// Gets or sets the section classe.
        /// </summary>
        /// <value>The section classe.</value>
        public SectionClasses? SectionClass { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is rolled.
        /// </summary>
        /// <value><c>true</c> if this instance is rolled; otherwise, <c>false</c>.</value>
        public bool? IsRolled { get; set; }

        /// <summary>
        /// Enum BucklingCurves
        /// </summary>
        public enum BucklingCurves
        {
            /// <summary>
            /// The a0
            /// </summary>
            a0 = 1,
            /// <summary>
            /// a
            /// </summary>
            a = 2,
            /// <summary>
            /// The b
            /// </summary>
            b = 3,
            /// <summary>
            /// The c
            /// </summary>
            c = 4,
            /// <summary>
            /// The d
            /// </summary>
            d = 5
        }
        /// <summary>
        /// Column buckling curve to be used.
        /// It determines the imperfection factors for buckling curve.
        /// If not overwritten, it is taken from the Table 6.2 of the code, "EN 1993-1-1:2005."
        /// </summary>
        /// <value>The buckling curve yy.</value>
        public BucklingCurves? BucklingCurveYY { get; set; }
        /// <summary>
        /// Column buckling curve to be used.
        /// It determines the imperfection factors for buckling curve.
        /// If not overwritten, it is taken from the Table 6.2 of the code, "EN 1993-1-1:2005."
        /// </summary>
        /// <value>The buckling curve zz.</value>
        public BucklingCurves? BucklingCurveZZ { get; set; }
        /// <summary>
        /// Buckling curve to be used for lateral-torsional buckling.
        /// The program gives one extra option "a0" following flexural buckling mode.
        /// It determines the imperfection factors for buckling curve.
        /// If not overwritten, it is taken from the Table 6.4 of the code, "EN 1993-1-1:2005."
        /// </summary>
        /// <value>The buckling curve LTB.</value>
        public BucklingCurves? BucklingCurveLTB { get; set; }


        #region Moment Coefficient
        /// <summary>
        /// Bending Coefficient.
        /// Unitless moment coefficient, C1, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The c1.</value>
        public double C1 { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, kyy, for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The kyy major.</value>
        public double kyyMajor { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, kzz for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The KZZ minor.</value>
        public double kzzMinor { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, kzy for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The kzy.</value>
        public double kzy { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, kyz for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The kyz.</value>
        public double kyz { get; set; } = 0;
        #endregion

        /// <summary>
        /// System Overstrength Factor.
        /// Omega factor is related to seismic factored member force and member capacity.
        /// It can assume different values in two orthogonal directions.
        /// The Omega value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See EC8 sections 6.6.3(1), 6.7.4(1), 6.8.3(1), and 6.8.4(1) for details.
        /// Specifying 0 means the value is program determined.
        /// Program determined value means it is taken from the preferences.
        /// </summary>
        /// <value>The omega.</value>
        public double Omega { get; set; } = 0;

        /// <summary>
        /// Material Overstrength Factor.
        /// Material overstrength factor, Gamma_ov: The ratio of the expected yield strength to the minimum specified yield strength.
        /// This ratio is used in capacity based design for special seismic cases.
        /// See EC8 sections 6.1.3(2), 6.2(3), 6.5.5(3), 6.6.3(1), 6.7.4(1), 6.8.3(1), and 6.8.4(1) for details.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The gamma ov.</value>
        public double GammaOV { get; set; } = 0;

        /// <summary>
        /// Warping constant
        /// </summary>
        /// <value>The iw.</value>
        public double Iw { get; set; } = 0;

        #region Capacities        
        /// <summary>
        /// Elastic flexural-torsional buckling force.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The NCR tf.</value>
        public double NcrTF { get; set; } = 0;

        /// <summary>
        /// Elastic torsional buckling force.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The NCR t.</value>
        public double NcrT { get; set; } = 0;

        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Nc.</value>
        public double Nc { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Nt.</value>
        public double Nt { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mc3.</value>
        public double Mc3 { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mc2.</value>
        public double Mc2 { get; set; } = 0;

        /// <summary>
        /// Buckling Resistance Moment.
        /// Allowable critical moment capacity for major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mb.</value>
        public double Mb { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for major direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The V2.</value>
        public double V2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The V3.</value>
        public double V3 { get; set; } = 0;
        #endregion
    }
}
