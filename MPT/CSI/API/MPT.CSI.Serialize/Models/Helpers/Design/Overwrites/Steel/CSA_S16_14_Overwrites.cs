// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="CSA_S16_14_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    /// <summary>
    /// Class CSA_S16_14_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel.SteelDesignOverwriteProperties" />
    public class CSA_S16_14_Overwrites : SteelDesignOverwriteProperties
    {
        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public CSA_S16_14_Preferences.FrameTypes? FrameType { get; set; }

        /// <summary>
        /// Moment Coefficient, major.
        /// Unitless factor, Omega1 for major axis bending, used in determining the interaction ratio for major axis bending.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections(e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The Omega1 major.</value>
        public double Omega1Major { get; set; } = 0;

        /// <summary>
        /// Moment Coefficient, minor.
        /// Unitless factor, Omega1 for minor axis bending, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections(e.g., angles)minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The Omega1 minor.</value>
        public double Omega1Minor { get; set; } = 0;

        /// <summary>
        /// Bending Coefficient.
        /// Unitless factor, Omega2, used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The omega2.</value>
        public double Omega2 { get; set; } = 0;

        #region Moment Factors: Sway/NonSway
        /// <summary>
        /// Unitless moment magnification factor for non-sway major axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The u1 major.</value>
        public double U1Major { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for non-sway minor axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The u1 minor.</value>
        public double U1Minor { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for sway major-axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The u2 major.</value>
        public double U2Major { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for sway minor-axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The u2 minor.</value>
        public double U2Minor { get; set; } = 0;
        #endregion

        /// <summary>
        /// Expected to specified Fy ratio.
        /// The ratio of the expected yield strength to the minimum specified yield strength.
        /// This ratio is used in capacity based design for special seismic cases.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The ry.</value>
        public double Ry { get; set; } = 0;

        /// <summary>
        /// Parameter for Compression Resistance.
        /// Unitless factor used in the calculation of compression resistance.
        /// Its value should be picked up for the compression curve.
        /// Typical values are 2.24 (SSRC Curve 1), 1.34 (SSRC Curve 2), and 1.00 (SSRC Curve 3).
        /// Lower values are conservative.
        /// Its effect is minimal if lambda is close to zero or very large (lambda &gt; 2.0).
        /// See CSA-S16-01/S16S1-05 section 13.3.1 and related commentary.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The n power.</value>
        public double NPower { get; set; } = 0;

        #region Capacities
        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Cr.</value>
        public double Cr { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Tr.</value>
        public double Tr { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mr3.</value>
        public double Mr3 { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Mr2.</value>
        public double Mr2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for major direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Vr2.</value>
        public double Vr2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Vr3.</value>
        public double Vr3 { get; set; } = 0;
        #endregion
    }
}
