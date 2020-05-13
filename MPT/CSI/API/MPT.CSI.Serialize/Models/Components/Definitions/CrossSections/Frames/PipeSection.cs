// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="PipeSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class PipeSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class PipeSection : FrameSection<PipeSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>PipeSection.</returns>
        internal static PipeSection Factory(
            Materials.Materials material,
            string uniqueName, 
            PipeSectionProperties properties = null)
        {
            PipeSection frameSection = new PipeSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipeSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected PipeSection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.Pipe)
        {

        }
        #endregion
    }
}
