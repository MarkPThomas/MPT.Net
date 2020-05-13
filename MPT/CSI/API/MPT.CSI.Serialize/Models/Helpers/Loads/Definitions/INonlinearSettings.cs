// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-22-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="INonlinearSettings.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Loads.Cases;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Interface INonlinearSettings
    /// </summary>
    public interface INonlinearSettings : ILoadCase
    {
        /// <summary>
        /// Gets the nonlinear settings.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        NonlinearSettingsHelper NonlinearSettings { get; }
    }
}
