// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-10-2017
// ***********************************************************************
// <copyright file="eSpecificationSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Specification sources in the application.
    /// </summary>
    public enum eSpecificationSource
    {
        /// <summary>
        /// Program determined.
        /// </summary>
        ProgramDetermined = 0,

        /// <summary>
        /// UserCoordinateSystemNames specified.
        /// </summary>
        UserSpecified = 1
    }
}
