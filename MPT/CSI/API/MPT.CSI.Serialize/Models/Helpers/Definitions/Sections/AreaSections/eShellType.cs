// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eShellType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Shell types available in the applicaion.
    /// </summary>
    public enum eShellType
    {
        /// <summary>
        /// Shell-thin section.
        /// </summary>
        ShellThin = 1,

        /// <summary>
        /// Shell-thick section.
        /// </summary>
        ShellThick = 2,
        
        /// <summary>
        /// Plate-thin section.
        /// </summary>
        PlateThin = 3,

        /// <summary>
        /// Plate-thick section.
        /// </summary>
        PlateThick = 4,

        /// <summary>
        /// Membrane section.
        /// </summary>
        Membrane = 5,

        /// <summary>
        /// Shell layered/nonlinear section.
        /// </summary>
        ShellLayered = 6
    }
}
