// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="DoubleChannelSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class DoubleChannelSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class DoubleChannelSection : FrameSection<DoubleChannelSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DoubleChannelSection.</returns>
        internal static DoubleChannelSection Factory(
            Materials.Materials material,
            string uniqueName, 
            DoubleChannelSectionProperties properties = null)
        {
            DoubleChannelSection frameSection = new DoubleChannelSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleChannelSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected DoubleChannelSection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.DoubleChannel)
        {

        }
        #endregion
    }
}
