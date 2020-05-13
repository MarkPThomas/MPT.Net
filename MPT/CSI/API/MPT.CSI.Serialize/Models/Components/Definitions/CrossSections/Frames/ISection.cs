// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="ISection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class ISection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class ISection : FrameSection<ISectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>ISection.</returns>
        internal static ISection Factory(
            Materials.Materials material,
            string uniqueName, 
            ISectionProperties properties = null)
        {
            ISection frameSection = new ISection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ISection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected ISection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.ISection)
        {

        }
        #endregion
    }
}
