// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-27-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="IBeamRebar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Interface IBeamRebar
    /// </summary>
    public interface IBeamRebar
    {
        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        BeamRebar BeamRebar { get; }

        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        void SetBeamRebar(BeamRebarDetailing beamRebar);
    }
}
