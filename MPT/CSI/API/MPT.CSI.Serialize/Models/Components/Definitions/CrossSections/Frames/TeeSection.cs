// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="TeeSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class TeeSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class TeeSection : FrameSection<TeeSectionProperties>,
        IBeamRebar
    {
        #region Fields & Properties
        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        public BeamRebar BeamRebar => _beamRebar;
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TeeSection.</returns>
        internal static TeeSection Factory(
            Materials.Materials material,
            string uniqueName, 
            TeeSectionProperties properties = null)
        {
            TeeSection frameSection = new TeeSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeeSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected TeeSection(
            Materials.Materials material, 
            string name) : base(material, name, eFrameSectionType.TSection)
        {

        }
        #endregion

        #region Rebar
        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        public void SetBeamRebar(BeamRebarDetailing beamRebar)
        {
            setBeamRebar(beamRebar);
        }
        #endregion
    }
}
