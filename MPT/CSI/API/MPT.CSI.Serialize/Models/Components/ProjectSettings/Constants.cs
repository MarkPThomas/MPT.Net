// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="Constants.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Grids;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents 'None'.
        /// </summary>
        public const string NONE = "None";

        /// <summary>
        /// Represents 'Default'.
        /// </summary>
        public const string DEFAULT = "Default";
        
        internal const string SAP2000_FILE_EXTENSION = ".$2k";

        internal const string ETABS_FILE_EXTENSION = ".$et";

        /// <summary>
        /// Gets or sets the tolerance.
        /// </summary>
        /// <value>The tolerance.</value>
        public static double Tolerance { get; set; }
        
        /// <summary>
        /// Gets or sets the coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public static string CoordinateSystem { get; set; } = CoordinateSystems.Global;
    }
}
