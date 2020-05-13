// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ASolidProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class ASolidProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections.AreaSectionProperties" />
    public class ASolidProperties : AreaSectionProperties
    {
#region Fields & Properties
        /// <summary>
        /// The type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public override eAreaSectionType AreaType => eAreaSectionType.ASolid;

        /// <summary>
        /// The material angle. [deg]
        /// </summary>
        /// <value>The material angle.</value>
        public double MaterialAngle { get; set; }

        /// <summary>
        /// The arc angle through which the area object is passed to define the asolid element. [deg]
        /// </summary>
        /// <value>The arc angle.</value>
        public double ArcAngle { get; set; }

        /// <summary>
        /// True: Incompatible bending modes are included in the stiffness formulation.
        /// In general, incompatible modes significantly improve the bending behavior of the object.
        /// </summary>
        /// <value><c>true</c> if [incompatible bending modes]; otherwise, <c>false</c>.</value>
        public bool IncompatibleBendingModes { get; set; }

        /// <summary>
        /// The area object is rotated about the Z-axis in this coordinate system to define the asolid element.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }
#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ASolidProperties" /> class.
        /// </summary>
        public ASolidProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ASolidProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ASolidProperties(Material material) : base(material)
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
#endif
