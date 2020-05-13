// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="TS_500_2000_R2018_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete
{
    /// <summary>
    /// Class TS_500_2000_R2018_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete.ConcreteDesignOverwriteProperties" />
    public class TS_500_2000_R2018_Overwrites : ConcreteDesignOverwriteProperties
    {
        /// <summary>
        /// Frame Types used for ductility considerations in seismic design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// The high ductile
            /// </summary>
            [Description("High Ductile")]
            HighDuctile = 1,

            /// <summary>
            /// The nominal ductile
            /// </summary>
            [Description("Nominal Ductile")]
            NominalDuctile = 2,

            /// <summary>
            /// The ordinary
            /// </summary>
            Ordinary = 3,

            /// <summary>
            /// The non sway
            /// </summary>
            NonSway = 4
        }

        /// <summary>
        /// This item is used for ductility considerations in seismic design.
        /// Program determined value means that it defaults to the highest ductility requirement.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes? FrameType { get; set; }

        #region Moment Coefficients

        /// <summary>
        /// Moment Coefficient, major.
        /// Unitless factor, Cm for major axis bending, used in determining the interaction ratio.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections(e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The cm major.</value>
        public double CmMajor { get; set; } = 0;

        /// <summary>
        /// Moment Coefficient, minor.
        /// Unitless factor, Cm for minor axis bending, used in determining the interaction ratio.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections(e.g., angles)minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The cm minor.</value>
        public double CmMinor { get; set; } = 0;

        #endregion

        #region Moment Factors: Sway/NonSway

        /// <summary>
        /// Nonsway Moment Factor.
        /// Unitless moment magnification factor for non-sway major-axis bending moment.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Bns major.</value>
        public double BnsMajor { get; set; } = 0;

        /// <summary>
        /// Nonsway Moment Factor.
        /// Unitless moment magnification factor for non-sway minor-axis bending moment.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Bns minor.</value>
        public double BnsMinor { get; set; } = 0;

        /// <summary>
        /// Sway Moment Factor.
        /// Unitless moment magnification factor for sway major-axis bending moment.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// </summary>
        /// <value>The Bs major.</value>
        public double BsMajor { get; set; } = 0;

        /// <summary>
        /// Sway Moment Factor.
        /// Unitless moment magnification factor for sway minor-axis bending moment.
        /// This item only applies to frame objects with column-type current design sections.
        /// Program determined value means it is calculated for each element for each load combination uniquely.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken as 1 because it is assumed that P-Delta effects were specified to be included in the analysis, and thus no further magnification is required.
        /// </summary>
        /// <value>The Bs minor.</value>
        public double BsMinor { get; set; } = 0;

        #endregion
    }
}
