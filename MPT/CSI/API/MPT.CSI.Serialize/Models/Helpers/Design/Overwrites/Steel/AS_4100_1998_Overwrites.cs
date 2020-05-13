using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    public class AS_4100_1998_Overwrites : SteelDesignOverwriteProperties
    { /// <summary>
      /// Type of frame used for ductility considerations in the design.
      /// </summary>
      /// <value>The type of the frame.</value>
        public AS_4100_1998_Preferences.FrameTypes? FrameType { get; set; }

        /// <summary>
        /// Indicates the residual stress level in the structural section.
        /// This affects plasticity limit and yield limit of plate element slenderness values.
        /// Eventually this can affect moment capacity and axial compression capacity through modification on Ze and Aeff.
        /// </summary>
        /// <value>The type of the steel.</value>
        public AS_4100_1998_Preferences.SteelTypes? SteelType { get; set; }

        /// <summary>
        /// Moment Coefficient, major.
        /// Unitless factor, Cm for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections(e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The cm major.</value>
        public double CmMajor { get; set; } = 0;

        /// <summary>
        /// Moment Coefficient, minor.
        /// Unitless factor, Cm for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections(e.g., angles)minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The cm minor.</value>
        public double CmMinor { get; set; } = 0;


        /// <summary>
        /// Twist Restraint Factor.
        /// Twist restraint effective length factor for members subjected to flexure.
        /// It is unitless.
        /// It affects lateral-torsional buckling capacity, Mb by affecting Mo and alphaID_s.
        /// Its value depends on lateral or rotational restraints and section slenderness b/t or d/t.
        /// Specifying 0 means the value is program default which is 1.
        /// See section 5.6.3 and Table 5.6.3(1) of "AS 4100:1998/NZS 3404:1997" code for details.  
        /// </summary>
        /// <value>The kt LTB.</value>
        public double ktLTB { get; set; } = 0;


        /// <summary>
        /// Lateral Rotation Restraint Factor.
        /// Lateral rotation restraint effective length factor for members subjected to flexure.
        /// It is unitless.
        /// It affects lateral-torsional buckling capacity, Mb by affecting Mo and alphaID_s.
        /// Its value depends on lateral or rotational restraints.
        /// Specifying 0 means the value is program default.
        /// See section 5.6.3 and Table 5.6.3(3) of "AS 4100:1998/NZS 3404:1997" code for details. 
        /// </summary>
        /// <value>The kr LTB.</value>
        public double krLTB { get; set; } = 0;


        /// <summary>
        /// Load Height Factor for LTB.
        /// Load height effective length factor for members subjected to flexure.
        /// It is unitless.
        /// It affects lateral-torsional buckling capacity, Mb by affecting Mo and alphaID_s.
        /// Its value depends on lateral or rotational restraints and load height position.
        /// Specifying 0 means the value is program default.
        /// See section 5.6.3 and Table 5.6.3(2) of "AS 4100:1998/NZS 3404:1997" code for details.  
        /// </summary>
        /// <value>The kl LTB.</value>
        public double klLTB { get; set; } = 0;


        /// <summary>
        /// Moment Modification Factor.
        /// Moment modification factor for distribution of bending moments in a member subjected to flexure.
        /// It is unitless.
        /// It captures the effect of non-uniform moment distribution along the length.Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// See sections 5.6.1.1 and 5.6.2 and Tables 5.6.1 and 5.6.2 of "AS 4100:1998/NZS 3404:1997" code for details. 
        /// </summary>
        /// <value>The alpham.</value>
        public double Alpha_m { get; set; } = 0;


        /// <summary>
        /// Slenderness Reduction Factor.
        /// Slenderness reduction factor for a member subjected to flexure.
        /// It is unitless.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// See section 5.6.1.1 "AS 4100:1998/NZS 3404:1997" code for details.  
        /// </summary>
        /// <value>The alphas.</value>
        public double Alpha_s { get; set; } = 0;


        /// <summary>
        /// Form Factor.
        /// Form factor for members subjected to axial compression.
        /// It is unitless.
        /// Its value is 1 for compact sections and less than one for non-compact and slender sections.
        /// Specifying 0 means the value is program determined.
        /// See sections 6.2.1, 6.3.3, 8.3.2, 8.4.2.2, and 8.4.4.1 of "AS 4100:1998/NZS 3404:1997" code for details.  
        /// </summary>
        /// <value>The kf.</value>
        public double Kf { get; set; } = 0;


        /// <summary>
        /// Axial Capacity Correction Factor.
        /// Correction factor for distribution of forces for members subjected to axial tension.
        /// It is unitless.
        /// Its value depends on the connections and can be less than or equal to one for any sections.
        /// Specifying 0 means the value is program default which is 1.
        /// See sections 7.2, 7.3.1, and 7.3.2 and Table 7.3.2 of "AS 4100:1998/NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The kt axial.</value>
        public double KtAxial { get; set; } = 0;

        #region Moment Factors: Sway/NonSway

        /// <summary>
        /// Gets or sets the database major.
        /// </summary>
        /// <value>The database major.</value>
        public double DbMajor { get; set; } = 0;


        /// <summary>
        /// Gets or sets the database minor.
        /// </summary>
        /// <value>The database minor.</value>
        public double DbMinor { get; set; } = 0;


        /// <summary>
        /// Gets or sets the ds major.
        /// </summary>
        /// <value>The ds major.</value>
        public double DsMajor { get; set; } = 0;


        /// <summary>
        /// Gets or sets the ds minor.
        /// </summary>
        /// <value>The ds minor.</value>
        public double DsMinor { get; set; } = 0;
        #endregion

        #region Capacities
        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Ns.</value>
        public double Ns { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The NT.</value>
        public double Nt { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The MS3.</value>
        public double Ms3 { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The MS2.</value>
        public double Ms2 { get; set; } = 0;

        /// <summary>
        /// Allowable critical moment capacity for major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The mb.</value>
        public double Mb { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for major direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Vv2.</value>
        public double Vv2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Vv3.</value>
        public double Vv3 { get; set; } = 0;
        #endregion
    }
}
