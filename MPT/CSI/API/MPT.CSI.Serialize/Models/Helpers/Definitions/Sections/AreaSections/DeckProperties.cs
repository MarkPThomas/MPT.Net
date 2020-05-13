// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class DeckProperties.
    /// </summary>
    /// <seealso cref="AreaSectionProperties" />
    public class DeckProperties : ShellProperties
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the type of shell.
        /// </summary>
        /// <value>The type of the shell.</value>
        public override eShellType ShellType => eShellType.Membrane;

        /// <summary>
        /// Gets or sets the type of deck floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public virtual eDeckType FloorType { get; set; }

        /// <summary>
        /// The overall deck depth. [L]
        /// </summary>
        /// <value>The overall depth.</value>
        public double OverallDepth { get; set; }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="DeckProperties" /> class.
        /// </summary>
        public DeckProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DeckProperties(Material material) : base (material) { }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
