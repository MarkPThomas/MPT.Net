// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="PlateSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class PlateSection.
    /// </summary>
    /// <seealso cref="FrameSection{RectangleSectionProperties}" />
    public class PlateSection : FrameSection<RectangleSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>RectangleSection.</returns>
        internal static PlateSection Factory(
            Materials.Materials material,
            string uniqueName, 
            RectangleSectionProperties properties = null)
        {
            PlateSection frameSection = new PlateSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlateSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected PlateSection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.SteelPlate)
        {

        }
        #endregion
    }
}