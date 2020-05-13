// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="RectangleSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class RectangleSectionProperties.
    /// </summary>
    /// <seealso cref="FrameSectionProperties" />
    public class RectangleSectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// The section depth. [L].
        /// </summary>
        /// <value>The depth.</value>
        public virtual double h { get; set; }

        /// <summary>
        /// The section width. [L].
        /// </summary>
        /// <value>The width.</value>
        public virtual double b { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleSectionProperties"/> class.
        /// </summary>
        public RectangleSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public RectangleSectionProperties(Material material) : base(material) { }

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
