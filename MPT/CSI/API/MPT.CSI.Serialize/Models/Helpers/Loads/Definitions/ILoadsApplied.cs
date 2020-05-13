// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-22-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="ILoadsApplied.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Loads.Cases;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Interface ILoadsApplied
    /// </summary>
    public interface ILoadsApplied : ILoadCase
    {
        /// <summary>
        /// Gets the loads.
        /// </summary>
        /// <value>The loads.</value>
        LoadsAppliedHelper Loads { get; }
    }
}
