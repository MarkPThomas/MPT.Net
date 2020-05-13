// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="ISectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class ISectionProperties.
    /// </summary>
    /// <seealso cref="FrameSectionProperties" />
    public class ISectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// The section depth. [L].
        /// </summary>
        /// <value>The t3.</value>
        public virtual double t3 { get; set; }

        /// <summary>
        /// The top flange width. [L].
        /// </summary>
        /// <value>The t2.</value>
        public virtual double t2 { get; set; }

        /// <summary>
        /// The flange thickness. [L].
        /// </summary>
        /// <value>The tf.</value>
        public double tf { get; set; }

        /// <summary>
        /// The web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; set; }

        /// <summary>
        /// The bottom flange width. [L].
        /// </summary>
        /// <value>The T2B.</value>
        public double t2b { get; set; }

        /// <summary>
        /// The bottom flange thickness. [L].
        /// </summary>
        /// <value>The TFB.</value>
        public double tfb { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="ISectionProperties"/> class.
        /// </summary>
        public ISectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ISectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ISectionProperties(Material material) : base(material) { }

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
