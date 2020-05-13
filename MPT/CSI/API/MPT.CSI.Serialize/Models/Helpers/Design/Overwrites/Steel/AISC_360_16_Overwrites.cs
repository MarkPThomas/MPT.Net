// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="AISC_360_16_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    /// <summary>
    /// Class AISC_360_16_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel.SteelDesignOverwriteProperties" />
    public class AISC_360_16_Overwrites : SteelDesignOverwriteProperties
    {
        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public AISC_360_16_Preferences.FrameTypes? FrameType { get; set; }

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
        /// This is called the Overstrength Factor.
        /// This is a function of Seismic Force Resisting System.
        /// It can assume different values in two orthogonal directions.
        /// The Omega0 value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See ASCE 7-05 section 12.2.1 and Table 12.2-1 for details.
        /// </summary>
        /// <value>The omega0.</value>
        public double Omega0 { get; set; } = 0;

        /// <summary>
        /// Expected to specified Fy ratio.
        /// The ratio of the expected yield strength to the minimum specified yield strength.
        /// This ratio is used in capacity based design for special seismic cases.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The ry.</value>
        public double Ry { get; set; } = 0;


        /// <summary>
        /// Unitless factor, Cb, used in determining the allowable bending capacity.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The cb.</value>
        public double Cb { get; set; } = 0;

        #region Moment Factors: Sway/NonSway
        /// <summary>
        /// Unitless moment magnification factor for non-sway major axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The b1 major.</value>
        public double B1Major { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for non-sway minor axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The b1 minor.</value>
        public double B1Minor { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for sway major-axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The b2 major.</value>
        public double B2Major { get; set; } = 0;

        /// <summary>
        /// Unitless moment magnification factor for sway minor-axis bending moment.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The b2 minor.</value>
        public double B2Minor { get; set; } = 0;
        #endregion


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
        /// <value>The MN3.</value>
        public double Mn3 { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The MN2.</value>
        public double Mn2 { get; set; } = 0;

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

        /// <summary>
        /// The procedure used for welding.
        /// </summary>
        /// <value>The type of the HSS welding.</value>
        public AISC_360_16_Preferences.HSSWeldingTypes? HSSWeldingType { get; set; }

        /// <summary>
        /// Toggle to consider whether the reduction of HSS (box or Pipe) thickness should be considered.
        /// </summary>
        /// <value><c>true</c> if [reduce HSS thickness]; otherwise, <c>false</c>.</value>
        public bool? ReduceHSSThickness { get; set; }
    }
}
