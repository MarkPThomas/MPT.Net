// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="AngleSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class AngleSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class AngleSection : FrameSection<AngleSectionProperties>,
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
        /// <returns>AngleSection.</returns>
        internal static AngleSection Factory(
            Materials.Materials material,
            string uniqueName, 
            AngleSectionProperties properties = null)
        {
            AngleSection frameSection = new AngleSection(material, uniqueName) {_sectionProperties = properties};

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected AngleSection(
            Materials.Materials material,
            string name) : base(material, name, eFrameSectionType.Angle)
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
