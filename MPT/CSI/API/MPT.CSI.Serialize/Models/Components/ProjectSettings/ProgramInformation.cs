// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-21-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="ProgramInformation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    /// <summary>
    /// Class ProgramInformation.
    /// </summary>
    public class ProgramInformation
    {
        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <value>The name of the program.</value>
        public string ProgramName { get; internal set; }

        /// <summary>
        /// The program version name that is externally displayed to the user.
        /// </summary>
        /// <value>The name of the version.</value>
        public string VersionName { get; internal set; }

        /// <summary>
        /// The program version number that is used internally by the program and not displayed to the user.
        /// </summary>
        /// <value>The version number.</value>
        public double VersionNumber { get; internal set; }
    }
}
