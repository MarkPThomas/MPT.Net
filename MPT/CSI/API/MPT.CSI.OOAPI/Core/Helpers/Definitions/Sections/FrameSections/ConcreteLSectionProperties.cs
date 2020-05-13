// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="ConcreteLSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections
{

    /// <summary>
    /// Class ConcreteLSectionProperties.
    /// </summary>
    /// <seealso cref="TeeSectionProperties" />
    public class ConcreteLSectionProperties : TeeSectionProperties
    {
        /// <summary>
        /// The average web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public override double tw => (twCorner + twTip) / 2;

        /// <summary>
        /// The vertical leg thickness at the corner. [L].
        /// </summary>
        /// <value>The twFlange.</value>
        public double twCorner { get; set; }

        /// <summary>
        /// The vertical leg thickness at the tip. [L].
        /// </summary>
        /// <value>The twT.</value>
        public double twTip { get; set; }

        /// <summary>
        /// True: The section is mirrored about the local 2-axis.
        /// </summary>
        /// <value><c>true</c> if [mirror about2]; otherwise, <c>false</c>.</value>
        public bool MirrorAbout2 { get; set; }

        /// <summary>
        /// True: The section is mirrored about the local 3-axis.
        /// </summary>
        /// <value><c>true</c> if [mirror about3]; otherwise, <c>false</c>.</value>
        public bool MirrorAbout3 { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteLSectionProperties"/> class.
        /// </summary>
        public ConcreteLSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteLSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ConcreteLSectionProperties(Material material) : base(material) { }


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
