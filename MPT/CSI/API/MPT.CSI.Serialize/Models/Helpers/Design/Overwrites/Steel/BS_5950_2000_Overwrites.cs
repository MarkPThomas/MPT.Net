// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-09-2019
// ***********************************************************************
// <copyright file="BS_5950_2000_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    /// <summary>
    /// Class BS_5950_2000_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel.SteelDesignOverwriteProperties" />
    public class BS_5950_2000_Overwrites : SteelDesignOverwriteProperties
    {
        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public BS_5950_2000_Preferences.FrameTypes? FrameType { get; set; }

        #region Moment Factors: Sway/NonSway
        /// <summary>
        /// Unitless factor for major axis bending used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The m major.</value>
        public double MMajor { get; set; } = 0;

        /// <summary>
        /// Unitless factor for minor axis bending used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The m minor.</value>
        public double MMinor { get; set; } = 0;

        /// <summary>
        /// Unitless factor used in determining the interaction ratio.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The mlt.</value>
        public double MLT { get; set; } = 0;
        #endregion


        #region Capacities
        /// <summary>
        /// Allowable axial compressive capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Pc.</value>
        public double Pc { get; set; } = 0;

        /// <summary>
        /// Allowable axial tensile capacity.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Pt.</value>
        public double Pt { get; set; } = 0;

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
        /// <value>The Pv2.</value>
        public double Pv2 { get; set; } = 0;

        /// <summary>
        /// Allowable shear capacity force for minor direction shear.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The Pv3.</value>
        public double Pv3 { get; set; } = 0;
        #endregion
    }
}
