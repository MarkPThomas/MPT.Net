// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-13-2017
// ***********************************************************************
// <copyright file="eMaterialComponentBehaviorType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Material component behavior types available in the applicaion.
    /// </summary>
    public enum eMaterialComponentBehaviorType
    {
        /// <summary>
        /// Material is inactive.
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// Linear material behavior.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Nonlinear material behavior.
        /// </summary>
        Nonlinear = 2
    }
}
