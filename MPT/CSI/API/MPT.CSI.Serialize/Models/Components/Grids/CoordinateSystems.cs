// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="CoordinateSystems.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers;

namespace MPT.CSI.Serialize.Models.Components.Grids
{
    /// <summary>
    /// Represents the coordinate systems in the application.
    /// </summary>
    public class CoordinateSystems 
    {
        #region Fields        
        /// <summary>
        /// The global coordinate system name.
        /// </summary>
        public const string Global = "GLOBAL";

        /// <summary>
        /// The local coordinate system name.
        /// </summary>
        public const string Local = "LOCAL";

        /// <summary>
        /// User-defined coordinate system names.
        /// </summary>
        /// <value>The user.</value>
        public List<string> UserCoordinateSystemNames { get; } = new List<string>();

        /// <summary>
        /// Gets the default global coordinate system.
        /// </summary>
        /// <value>The global coordinate system.</value>
        public CoordinateSystem GlobalCoordinateSystem { get; internal set; } = new CoordinateSystem(Global);

        /// <summary>
        /// User-defined coordinate systems.
        /// </summary>
        /// <value>The user.</value>
        public List<CoordinateSystem> UserCoordinateSystems { get; } = new List<CoordinateSystem>();
        #endregion
    }
}
