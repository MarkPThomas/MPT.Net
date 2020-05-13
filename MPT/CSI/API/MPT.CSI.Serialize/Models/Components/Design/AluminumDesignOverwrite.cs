// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-21-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-09-2019
// ***********************************************************************
// <copyright file="AluminumDesignOverwrite.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class AluminumDesignOverwrite.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Design.DesignOverwrites" />
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.IFrameDesign" />
    public class AluminumDesignOverwrite : DesignOverwrites, IFrameDesign
    {
        /// <summary>
        /// The design section for the selected frame objects.When this overwrite is applied, any previous auto select section assigned to the frame object is removed.
        /// Program determined/null value means it is taken from the analysis section.
        /// </summary>
        /// <value>The design section.</value>
        public virtual FrameSection DesignSection { get; set; }

        /// <summary>
        /// Yield stress.
        /// Material yield strength used in the design/check.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken from the material property assigned to the frame object.
        /// </summary>
        /// <value>The fy.</value>
        public double Fy { get; set; } = 0;

        /// <summary>
        /// Reduced Live Load Factor.
        /// The live load reduction factor.A reducible live load is multiplied by this factor to obtain the reduced live load for the frame object.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The RLLF.</value>
        public double RLLF { get; set; } = 0;

        #region Moments & Buckling
        #region Unbraced Lengths
        /// <summary>
        /// Unbraced length ratio, major.
        /// Unbraced length factor for buckling about the frame object major axis.
        /// This item is specified as a fraction of the frame object length.Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.For unsymmetrical sections(e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The xl major.</value>
        public double XLMajor { get; set; } = 0;

        /// <summary>
        /// Unbraced length ratio, minor.
        /// Unbraced length factor for buckling about the frame object minor axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.For unsymmetrical sections(e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The xl minor.</value>
        public double XLMinor { get; set; } = 0;

        /// <summary>
        /// Unbraced length ratio, LTB.
        /// Unbraced length factor for lateral-torsional buckling for the frame object.This item is specified as a fraction of the frame object length.
        /// Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The XLLTB.</value>
        public double XLLTB { get; set; } = 0;
        #endregion

        #region Effective Lengths        
        /// <summary>
        /// Effective length factor for buckling about the frame object major axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// Specifying 0 means the value is program determined.
        /// For beam design, this major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The XK major.</value>
        public double XKMajor { get; set; } = 0;

        /// <summary>
        /// Effective length factor for buckling about the frame object minor axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.Specifying 0 means the value is program determined.
        /// For beam design, this factor is always taken as 1 regardless of what may be specified in the overwrites.
        /// This factor is also used for determining the effective length for lateral-torsional buckling.
        /// For symmetrical sections minor bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The XK minor.</value>
        public double XKMinor { get; set; } = 0;
        #endregion

        #region Moment Coefficients
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
        /// Unitless factor, Cb, used in determining the allowable bending capacity.
        /// It captures the effect of non-uniform moment distribution along the length.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The cb.</value>
        public double Cb { get; set; } = 0;
        #endregion
        #endregion
    }
}
