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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets or sets the coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public static string CoordinateSystem { get; set; } = CoordinateSystems.Global;

        /// <summary>
        /// Represents 'None'.
        /// </summary>
        public const string NONE = "None";

        /// <summary>
        /// Represents 'Default'.
        /// </summary>
        public const string DEFAULT = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether [fill all properties].
        /// </summary>
        /// <value><c>true</c> if [fill all properties]; otherwise, <c>false</c>.</value>
        public static bool FillAllProperties { get; set; } = true;

        /// <summary>
        /// Gets or sets the tolerance.
        /// </summary>
        /// <value>The tolerance.</value>
        public static double Tolerance { get; set; }
    }
}
