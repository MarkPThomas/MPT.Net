// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="IS_800_2007_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class IS_800_2007_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class IS_800_2007_Preferences : SteelDesignPreferenceProperties
    {
        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Special Moment Frame
            /// </summary>
            SMF = 1,

            /// <summary>
            /// Ordinary Moment Frame
            /// </summary>
            OMF = 3,

            /// <summary>
            /// Special Concentric Braced Frame
            /// </summary>
            SCBF = 4,

            /// <summary>
            /// Ordinary Concentric Braced Frame
            /// </summary>
            OCBF = 5,

            /// <summary>
            /// Eccentric Braced Frame
            /// </summary>
            EBF = 7,

            /// <summary>
            /// Secondary frame
            /// </summary>
            Secondary = 8
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.SMF;

        /// <summary>
        /// The partial factor for resistance of cross-sections.
        /// </summary>
        /// <value>The gamma m0.</value>
        public double GammaM0 { get; set; } = 1;

        /// <summary>
        /// The partial factor for resistance of members to instability.
        /// </summary>
        /// <value>The gamma m1.</value>
        public double GammaM1 { get; set; } = 1;

        /// <summary>
        /// The partial factor for resistance of cross-sections to tensile fracture8.
        /// </summary>
        /// <value>The gamma m2.</value>
        public double GammaM2 { get; set; } = 1.25;

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
