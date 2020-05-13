// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePlaneType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Plane types available in the application.
    /// </summary>
    public enum ePlaneType
    {
        /// <summary>
        /// Plane-stress.
        /// </summary>
        PlaneStress = 1,

        /// <summary>
        /// Plane-strain.
        /// </summary>
        PlaneStrain = 2
    }
}
