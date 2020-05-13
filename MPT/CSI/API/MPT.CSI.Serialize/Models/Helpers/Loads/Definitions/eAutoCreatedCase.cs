// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-10-2017
// ***********************************************************************
// <copyright file="eAutoCreatedCase.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Values indicating if the load case has been automatically created.
    /// </summary>
    public enum eAutoCreatedCase
    {
        /// <summary>
        /// Not automatically created.
        /// </summary>
        NotAutomaticallyCreated = 0,

        /// <summary>
        /// Automatically created by the bridge construction scheduler.
        /// </summary>
        AutomaticallyCreated = 1
    }
}
