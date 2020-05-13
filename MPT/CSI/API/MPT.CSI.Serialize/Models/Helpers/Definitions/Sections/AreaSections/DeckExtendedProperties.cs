// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="DeckExtendedProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class DeckExtendedProperties.
    /// </summary>
    /// <seealso cref="ModelProperty" />
    public class DeckExtendedProperties : ModelProperty
    {
        #region Fields & Properties
        /// <summary>
        /// The type of deck floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public virtual eDeckType FloorType { get; set; }

        /// <summary>
        /// The overall deck depth. [L]
        /// </summary>
        /// <value>The overall depth.</value>
        public virtual double OverallDepth { get; set; }

        /// <summary>
        /// The material name.
        /// </summary>
        /// <value>The material.</value>
        public string MaterialName { get; internal set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckExtendedProperties" /> class.
        /// </summary>
        public DeckExtendedProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckExtendedProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public DeckExtendedProperties(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="material">The material.</param>
        public void SetMaterial(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>DeckSolidSlabProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
