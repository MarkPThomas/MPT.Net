// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-09-2019
// ***********************************************************************
// <copyright file="AA_LRFD_2000_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Aluminum
{
    /// <summary>
    /// Class AA_LRFD_2000_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Aluminum.AluminumDesignOverwriteProperties" />
    public class AA_LRFD_2000_Overwrites: AluminumDesignOverwriteProperties
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

        // Note: These do not correspond to settings in the program.
        /// <summary>
        /// Gets or sets the k1 comp.
        /// </summary>
        /// <value>The k1 comp.</value>
        public double K1Comp { get; set; } = 0;
        /// <summary>
        /// Gets or sets the k2 comp.
        /// </summary>
        /// <value>The k2 comp.</value>
        public double K2Comp { get; set; } = 0;
        /// <summary>
        /// Gets or sets the k1 bend.
        /// </summary>
        /// <value>The k1 bend.</value>
        public double K1Bend { get; set; } = 0;
        /// <summary>
        /// Gets or sets the k2 bend.
        /// </summary>
        /// <value>The k2 bend.</value>
        public double K2Bend { get; set; } = 0;
        /// <summary>
        /// Gets or sets the kt.
        /// </summary>
        /// <value>The kt.</value>
        public double KT { get; set; } = 0;
        /// <summary>
        /// Gets or sets the c1.
        /// </summary>
        /// <value>The c1.</value>
        public double C1 { get; set; } = 0;
        /// <summary>
        /// Gets or sets the c2.
        /// </summary>
        /// <value>The c2.</value>
        public double C2 { get; set; } = 0;

        #region Capacities
        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Fa.</value>
        public double Fa { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Ft.</value>
        public double Ft { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in major axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Fb3.</value>
        public double Fb3 { get; set; } = 0;

        /// <summary>
        /// Allowable bending moment capacity in minor axis bending.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Fb2.</value>
        public double Fb2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for major direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Fs2.</value>
        public double Fs2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Fs3.</value>
        public double Fs3 { get; set; } = 0;
        #endregion
    }
}
