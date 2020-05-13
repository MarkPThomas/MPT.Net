// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="DoubleAngleSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class DoubleAngleSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class DoubleAngleSection : FrameSection<DoubleAngleSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DoubleAngleSection.</returns>
        internal static DoubleAngleSection Factory(
            Materials.Materials material,
            string uniqueName, 
            DoubleAngleSectionProperties properties = null)
        {
            DoubleAngleSection frameSection = new DoubleAngleSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAngleSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected DoubleAngleSection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.DoubleAngle)
        {

        }
        #endregion
    }
}
