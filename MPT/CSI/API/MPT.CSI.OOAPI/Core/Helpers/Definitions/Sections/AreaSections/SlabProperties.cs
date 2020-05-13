// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="SlabProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class SlabProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections.ShellProperties" />
    public class SlabProperties : ShellProperties
    {
        #region Fields & Properties

        /// <summary>
        /// Gets or sets the type of slab floor.
        /// </summary>
        /// <value>The type of the floor.</value>
        public virtual eSlabType FloorType { get; set; }

        /// <summary>
        /// Gets or sets the slab thickness. [L]
        /// </summary>
        /// <value>The slab thickness.</value>
        public double SlabThickness { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabProperties" /> class.
        /// </summary>
        public SlabProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SlabProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public SlabProperties(Material material) : base(material) { }

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
