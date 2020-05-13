// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="SteelAngleSectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class SteelAngleSectionProperties.
    /// </summary>
    /// <seealso cref="AngleSectionProperties" />
    public class SteelAngleSectionProperties : AngleSectionProperties
    {
        /// <summary>
        /// The fillet radius. [L]
        /// </summary>
        /// <value>The r.</value>
        public double r { get; set; }

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
        /// Initializes a new instance of the <see cref="SteelAngleSectionProperties"/> class.
        /// </summary>
        public SteelAngleSectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SteelAngleSectionProperties"/> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public SteelAngleSectionProperties(Material material) : base(material) { }


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
