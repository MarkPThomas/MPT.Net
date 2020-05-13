// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="PlaneProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class PlaneProperties.
    /// </summary>
    /// <seealso cref="AreaSectionProperties" />
    public class PlaneProperties : AreaSectionProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public override eAreaSectionType AreaType => eAreaSectionType.Plane;

        /// <summary>
        /// Gets or sets the type of plane.
        /// </summary>
        /// <value>The type of the plane.</value>
        public ePlaneType PlaneType { get; set; }

        /// <summary>
        /// The material angle. [deg]
        /// </summary>
        /// <value>The material angle.</value>
        public double MaterialAngle { get; set; }

        /// <summary>
        /// Gets or sets the thickness. [L]
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }

        /// <summary>
        /// True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.
        /// </summary>
        /// <value><c>true</c> if [incompatible modes]; otherwise, <c>false</c>.</value>
        public bool IncompatibleModes { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaneProperties" /> class.
        /// </summary>
        public PlaneProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaneProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public PlaneProperties(Material material) : base(material)
        {

        }

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