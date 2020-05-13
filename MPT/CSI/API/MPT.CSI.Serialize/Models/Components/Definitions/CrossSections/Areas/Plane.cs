// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Plane.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Plane.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public class Plane : AreaSection<PlaneProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Plane Factory(
            Materials.Materials material,
            string uniqueName,
            PlaneProperties properties = null)
        {
            Plane areaSection = new Plane(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Plane(
            Materials.Materials material,
            string name) : base(material, name, eAreaSectionType.Plane)
        {

        }
        #endregion
    }
}
