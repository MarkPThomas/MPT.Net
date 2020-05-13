// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="PipeSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class PipeSectionProperties.
    /// </summary>
    /// <seealso cref="CircleSectionProperties" />
    public class PipeSectionProperties : CircleSectionProperties
    {

        /// <summary>
        /// The wall thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Serialize.Models.Helpers.Definitions.Sections.FrameSections.CircleSectionProperties" /> class.
        /// </summary>
        /// <returns>AngleSectionProperties.</returns>
        public PipeSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PipeSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public PipeSectionProperties(Material material) : base(material) { }

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
