// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="TubeSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class TubeSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class TubeSection : FrameSection<TubeSectionProperties>
    {

        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static TubeSection Factory(
            Materials.Materials material,
            string uniqueName, 
            TubeSectionProperties properties = null)
        {
            TubeSection frameSection = new TubeSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TubeSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected TubeSection(
            Materials.Materials material,
            string name) : base(material, name, eFrameSectionType.Box)
        {

        }
        #endregion
    }
}
