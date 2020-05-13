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

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Stiffness, weight, and mass modifiers for areas.
    /// The default value for all modifiers is one.
    /// </summary>
    public class AreaModifier // Notes: This is a class and not a struct because default values must be set to 1.
    {
        /// <summary>
        /// Membrane stiffness modifier along the 1-1 local plane.
        /// </summary>
        /// <value>The membrane F11.</value>
        public double MembraneF11 { get; set; } = 1;

        /// <summary>
        /// Membrane stiffness modifier along the 2-2 local plane.
        /// </summary>
        /// <value>The membrane F22.</value>
        public double MembraneF22 { get; set; } = 1;

        /// <summary>
        /// Membrane stiffness modifier along the 1-2 local plane.
        /// </summary>
        /// <value>The membrane F12.</value>
        public double MembraneF12 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 1-1 local plane.
        /// </summary>
        /// <value>The bending M11.</value>
        public double BendingM11 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 2-2 local plane.
        /// </summary>
        /// <value>The bending M22.</value>
        public double BendingM22 { get; set; } = 1;

        /// <summary>
        /// Bending stiffness modifier along the 1-2 local plane.
        /// </summary>
        /// <value>The bending M12.</value>
        public double BendingM12 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 1-3 local plane.
        /// </summary>
        /// <value>The shear V13.</value>
        public double ShearV13 { get; set; } = 1;

        /// <summary>
        /// Shear stiffness modifier along the 2-3 local plane.
        /// </summary>
        /// <value>The shear V23.</value>
        public double ShearV23 { get; set; } = 1;

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
