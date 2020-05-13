// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Modifier.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Stiffness, weight, and mass modifiers for frames.
    /// The default value for all modifiers is one.
    /// </summary>
    public class FrameModifier // Notes: This is a class and not a struct because default values must be set to 1.
    {
        /// <summary>
        /// Cross-sectional area modifier.
        /// </summary>
        /// <value></value>
        public double CrossSectionalArea { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 2 local axis.
        /// </summary>
        /// <value>The shear V2.</value>
        public double ShearV2 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 3 local axis.
        /// </summary>
        /// <value>The shear V3.</value>
        public double ShearV3 { get; set; } = 1;

        /// <summary>
        /// Torsion stiffness modifier.
        /// </summary>
        /// <value>The torsion stiffness modifier.</value>
        public double Torsion { get; set; } = 1;
        
        /// <summary>
        /// Bending stiffness modifier along the 2 local axis.
        /// </summary>
        /// <value>The bending M2.</value>
        public double BendingM2 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 3 local axis.
        /// </summary>
        /// <value>The bending M3.</value>
        public double BendingM3 { get; set; } = 1;
        

        /// <summary>
        /// Mass modifier.
        /// </summary>
        /// <value>The mass modifier.</value>
        public double MassModifier { get; set; } = 1;

        /// <summary>
        /// Weight modifier.
        /// </summary>
        /// <value>The weight modifier.</value>
        public double WeightModifier { get; set; } = 1;
    }
}
