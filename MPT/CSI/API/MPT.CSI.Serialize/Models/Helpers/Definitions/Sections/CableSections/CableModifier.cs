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

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.CableSections
{
    /// <summary>
    /// Stiffness, weight, and mass modifiers for cables.
    /// The default value for all modifiers is one.
    /// </summary>
    public class CableModifier 
    {
        /// <summary>
        /// Cross-sectional area modifier.
        /// </summary>
        /// <value></value>
        public double CrossSectionalArea { get; set; } = 1;
        
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
