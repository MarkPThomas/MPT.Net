// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="NZS_3404_1997_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class NZS_3404_1997_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel.SteelDesignPreferenceProperties" />
    public class NZS_3404_1997_Preferences : SteelDesignPreferenceProperties
    {
        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Moment Frame
            /// </summary>
            [Description("Moment Frame")]
            MomentFrame = 1,

            /// <summary>
            /// Braced Frame
            /// </summary>
            [Description("Braced Frame")]
            BracedFrame = 2
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.MomentFrame;

        /// <summary>
        /// The resistance factor for bending.
        /// See sections 3.4 and Table 3.4 of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The phi b.</value>
        public double PhiB { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for compression.
        /// See sections 3.4 and Table 3.4 of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The phi c.</value>
        public double PhiC { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for tension-yielding.
        /// See sections 3.4 and Table 3.4 of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The phi t.</value>
        public double PhiTY { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for tension-fracture.
        /// See sections 3.4 and Table 3.4 of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The phi tf.</value>
        public double PhiTF { get; set; } = 0.9;

        /// <summary>
        /// The resistance factor for shear.
        /// See sections 3.4 and Table 3.4 of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The phi v.</value>
        public double PhiV { get; set; } = 0.9;

        /// <summary>
        /// Analysis methods used to check/design the steel members.
        /// See sections 4.1, 4.3, 4.4.1.2, and Appendix E of "NZS 3404:1997" code for details.
        /// </summary>
        public enum AnalysisMethods
        {
            /// <summary>
            /// The general2nd order
            /// </summary>
            [Description("General 2nd Order")]
            General2ndOrder = 1,

            /// <summary>
            /// The amplified1st order
            /// </summary>
            [Description("Amplified 1st Order")]
            Amplified1stOrder = 2
        }
        /// <summary>
        /// Analysis method used to check/design the steel members.
        /// See sections 4.1, 4.3, 4.4.1.2, and Appendix E of "NZS 3404:1997" code for details.
        /// </summary>
        /// <value>The analysis method.</value>
        public AnalysisMethods AnalysisMethod { get; set; } = AnalysisMethods.General2ndOrder;

        /// <summary>
        /// Indicates the residual stress level in the structural section.
        /// This affects plasticity limit and yield limit of plate element slenderness values.
        /// Eventually this can affect moment capacity and axial compression capacity through modification on Ze and Aeff.
        /// </summary>
        public enum SteelTypes
        {
            /// <summary>
            /// The hot rolled
            /// </summary>
            [Description("Hot Rolled")]
            HotRolled = 1,

            /// <summary>
            /// The hot finished
            /// </summary>
            [Description("Hot Finished")]
            HotFinished = 2,

            /// <summary>
            /// The cold formed
            /// </summary>
            [Description("Cold Formed")]
            ColdFormed = 3,

            /// <summary>
            /// The stress relieved
            /// </summary>
            [Description("Stress Relieved")]
            StressRelieved = 4,

            /// <summary>
            /// The lightly welded
            /// </summary>
            [Description("Lightly Welded")]
            LightlyWelded = 5,

            /// <summary>
            /// The heavily welded
            /// </summary>
            [Description("Heavily Welded")]
            HeavilyWelded = 6,
        }
        /// <summary>
        /// Indicates the residual stress level in the structural section.
        /// This affects plasticity limit and yield limit of plate element slenderness values.
        /// Eventually this can affect moment capacity and axial compression capacity through modification on Ze and Aeff.
        /// </summary>
        /// <value>The type of the steel.</value>
        public SteelTypes SteelType { get; set; } = SteelTypes.HotRolled;

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
