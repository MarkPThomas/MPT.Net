using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    public class IS_800_2007_Overwrites : SteelDesignOverwriteProperties
    {
        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public IS_800_2007_Preferences.FrameTypes? FrameType { get; set; }
        
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
        /// Unitless moment coefficient, Cmz, for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The K major.</value>
        public double KMajor { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, CMy for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The K minor.</value>
        public double KMinor { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, Kz for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The kzy.</value>
        public double kzy { get; set; } = 0;

        /// <summary>
        /// Unitless moment coefficient, Ky for minor axis bending, used in determining the interaction ratio.
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

        #region Capacities        
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
