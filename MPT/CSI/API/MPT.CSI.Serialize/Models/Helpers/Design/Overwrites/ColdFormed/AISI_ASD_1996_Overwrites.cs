// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-09-2019
// ***********************************************************************
// <copyright file="AISI_ASD_1996_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.ColdFormed
{
    /// <summary>
    /// Class AISI_ASD_1996_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.ColdFormed.SteelColdFormedDesignOverwriteProperties" />
    public class AISI_ASD_1996_Overwrites: SteelColdFormedDesignOverwriteProperties
    {
        /// <summary>
        /// Frame types
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// The moment frame
            /// </summary>
            [Description("Moment Frame")]
            MomentFrame = 1,

            /// <summary>
            /// The braced frame
            /// </summary>
            [Description("Braced Frame")]
            BracedFrame = 2,
        }
        /// <summary>
        /// Gets or sets the type of frame.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes? FrameType { get; set; }


        #region Moment Coefficient
        /// <summary>
        /// Bending Coefficient.
        /// Unitless moment coefficient, Cb, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The cb.</value>
        public double Cb { get; set; } = 0;

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
        /// Unitless factor, Ctf for major axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The CTF major.</value>
        public double CtfMajor { get; set; } = 0;


        /// <summary>
        /// Unitless factor, Ctf for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The CTF minor.</value>
        public double CtfMinor { get; set; } = 0;
        #endregion


        #region Moment Factors: Sway/NonSway
        /// <summary>
        /// Unitless moment magnification factor for non-sway major-axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The alpha major.</value>
        public double AlphaMajor { get; set; } = 0;


        /// <summary>
        /// Unitless moment magnification factor for non-sway minor axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The alpha minor.</value>
        public double AlphaMinor { get; set; } = 0;
        #endregion

        /// <summary>
        /// Specifies whether the section is through fastened.
        /// </summary>
        /// <value><c>true</c> if this instance is through fastened to deck; otherwise, <c>false</c>.</value>
        public bool? IsThroughFastenedToDeck { get; set; }


        /// <summary>
        /// Fastener eccentricity if the section is through fastened.
        /// </summary>
        /// <value>The fastener eccentricity.</value>
        public double FastenerEccentricity { get; set; } = 0;


        /// <summary>
        /// Hole diameter at the top flange if there is any.
        /// </summary>
        /// <value>The hole diameter at top flange.</value>
        public double HoleDiameterAtTopFlange { get; set; } = 0;


        /// <summary>
        /// Hole diameter at the bottom flange if there is any.
        /// </summary>
        /// <value>The hole diameter at bottom flange.</value>
        public double HoleDiameterAtBottomFlange { get; set; } = 0;


        /// <summary>
        /// Hole diameter at the web flange if there is any.
        /// </summary>
        /// <value>The hole diameter on web.</value>
        public double HoleDiameterOnWeb { get; set; } = 0;

        #region Capacities
        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The PNC.</value>
        public double Pnc { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The PNT.</value>
        public double Pnt { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mn33Yield.</value>
        public double Mn33Yield { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mn22Yield.</value>
        public double Mn22Yield { get; set; } = 0;

        /// <summary>
        /// Nominal bending moment capacity in major axis bending for lateral-torsional buckling.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mn33LTB.</value>
        public double Mn33LTB { get; set; } = 0;

        /// <summary>
        /// Nominal bending moment capacity in minor axis bending for lateral-torsional buckling.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mn22LTB.</value>
        public double Mn22LTB { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for major direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The VN2.</value>
        public double Vn2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The VN3.</value>
        public double Vn3 { get; set; } = 0;
        #endregion
    }
}
