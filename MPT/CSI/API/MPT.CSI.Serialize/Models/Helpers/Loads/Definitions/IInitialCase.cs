// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-22-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="IInitialCase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Loads.Cases;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Interface IInitialCase
    /// </summary>
    public interface IInitialCase : ILoadCase
    {
        /// <summary>
        /// Gets the initial case.
        /// </summary>
        /// <value>The initial case.</value>
        InitialCaseHelper InitialCase { get; }
    }
}
