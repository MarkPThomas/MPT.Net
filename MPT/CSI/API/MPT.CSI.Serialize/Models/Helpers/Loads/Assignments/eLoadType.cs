// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eLoadType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Loads.Assignments
{
    /// <summary>
    /// The type of load assignable in the application.
    /// </summary>
    public enum eLoadType
    {
        /// <summary>
        /// Load, (i.e. force).
        /// </summary>
        Load = 1,

        /// <summary>
        /// Acceleration.
        /// </summary>
        Accel = 2
    }
}