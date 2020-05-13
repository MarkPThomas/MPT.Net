// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="WallProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class WallProperties.
    /// </summary>
    /// <seealso cref="AreaSectionProperties" />
    public class WallProperties : ShellProperties
    {
        #region Fields & Properties

        /// <summary>
        /// Gets the type of wall.
        /// </summary>
        /// <value>The type of the wall.</value>
        public eWallPropertyType WallType => eWallPropertyType.Specified;


        /// <summary>
        /// Gets or sets the thickness. [L]
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WallProperties" /> class.
        /// </summary>
        public WallProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="WallProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public WallProperties(Material material) : base(material) { }

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
