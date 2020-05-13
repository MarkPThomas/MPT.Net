// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-22-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="INonlinearLoadApplication.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Interface INonlinearLoadApplication
    /// </summary>
    public interface INonlinearLoadApplication : ILoadCase
    {
        /// <summary>
        /// Gets or sets the load application.
        /// </summary>
        /// <value>The load application.</value>
        LoadApplication LoadApplication { get; set; }
    }
}
