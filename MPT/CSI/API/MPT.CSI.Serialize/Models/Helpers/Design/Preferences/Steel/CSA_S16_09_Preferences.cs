// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="CSA_S16_09_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class CSA_S16_09_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class CSA_S16_09_Preferences : SteelDesignPreferenceProperties
    {
        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type LD MRF")]
            Type_LD_MRF = 1,

            /// <summary>
            /// Medium Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type MD MRF")]
            Type_MD_MRF = 2,

            /// <summary>
            /// Ductile Moment Resisting Frame
            /// </summary>
            [Description("Type D MRF")]
            Type_D_MRF = 3,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type LD CBF (V)")]
            Type_LD_CBF_V = 4,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type LD CBF (TC)")]
            Type_LD_CBF_TC = 5,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type LD CBF (TO)")]
            Type_LD_CBF_TO = 6,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type LD CBF (OT)")]
            Type_LD_CBF_OT = 7,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type MD CBF (V)")]
            Type_MD_CBF_V = 8,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type M CBF (TC)")]
            Type_MD_CBF_TC = 9,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type MD CBF (TO)")]
            Type_MD_CBF_TO = 10,

            /// <summary>
            /// Low Ductility Moment Resisting Frame
            /// </summary>
            [Description("Type MD CBF (OT)")]
            Type_MD_CBF_OT = 11,

            /// <summary>
            /// Eccentric Braced Frame
            /// </summary>
            EBF = 12,

            /// <summary>
            /// Cantilever Column
            /// </summary>
            [Description("Cantilever Column")]
            CantileverColumn = 13,

            /// <summary>
            /// Conventional Moment Frame
            /// </summary>
            [Description("Conventional MF")]
            ConventionalMF = 14,

            /// <summary>
            /// Conventional Braced Frame
            /// </summary>
            [Description("Conventional BF")]
            ConventionalBF = 15,
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.Type_D_MRF;

        /// <summary>
        /// Toggle to consider whether the Class 4 sections should be handled by modifying the geometry or by modifying fy.
        /// </summary>
        public enum SlenderSectionModifications
        {
            /// <summary>
            /// The geometry
            /// </summary>
            [Description("Modify Geometry")]
            Geometry = 1,

            /// <summary>
            /// The fy
            /// </summary>
            [Description("Modify Fy")]
            Fy = 2
        }
        /// <summary>
        /// Toggle to consider whether the Class 4 sections should be handled by modifying the geometry or by modifying fy.
        /// </summary>
        /// <value>The slender section modification.</value>
        public SlenderSectionModifications SlenderSectionModification { get; set; } = SlenderSectionModifications.Geometry;

        /// <summary>
        /// The resistance factor for bending.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for compression.
        /// </summary>
        /// <value>The phi c.</value>
        public double PhiC { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for tension.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiT { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for shear.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.9;

        /// <summary>
        /// This is called the Short Period Spectral Acceleration Ratio.
        /// This is Sa(T=0.2sec) multiplied by IE=e and Fa.
        /// It can assume different values in two orthogonal directions.
        /// The value specified here is solely used for design.
        /// The program uses the same value for all directions.
        /// See CSA S16-09/CSA S16-01/S16S1-05 section 27.1.1, NBCC section 4.1.8.2, Table 4.1.8.9, Table 4.1.8.5, and Table 4.1.8.4B for details.
        /// </summary>
        /// <value>The spectral acceleration ratio.</value>
        public double SpectralAccelerationRatio { get; set; } = 0.35;

        /// <summary>
        /// This is called the Ductility Related Force Modification Factor.
        /// This reflects the capability of a structure to dissipate energy through inelastic behaviour.
        /// This is a function of Seismic Force Resisting System.
        /// It can assume different values in two orthogonal directions.
        /// The program uses the same value for all directions.
        /// The value specified here is solely used for design.
        /// See CSA S16-09/CSA S16-01/S16S1-05 section 27.1.1, NBCC sections 4.1.8.2 and 4.1.8.9, and NBCC Table 4.1.8.9 for details.
        /// </summary>
        /// <value>The rd.</value>
        public double Rd { get; set; } = 5;

        /// <summary>
        /// This is called the Overstrength Related Force Modification Factor.
        /// This accounts for the dependable portion of reserve strength in a structure.
        /// This is a function of Seismic Force Resisting System.
        /// It can assume different values in two orthogonal directions.
        /// The program uses the same value for all directions.
        /// The value specified here is solely used for design.
        /// See CSA S16-09/CSA, S16-01/S16S1-05 section 27.1.1, NBCC sections 4.1.8.2 and 4.1.8.9, and NBCC Table 4.1.8.9 for details.
        /// </summary>
        /// <value>The fa.</value>
        public double Fa { get; set; } = 1.5;

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
    }
}
