// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="eMaterialPropertyType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Material types available in the program.
    /// </summary>
    public enum eMaterialPropertyType
    {
        /// <summary>
        /// The steel material.
        /// </summary>
        Steel = 1,

        /// <summary>
        /// The concrete material.
        /// </summary>
        Concrete = 2,

        /// <summary>
        /// The no design material.
        /// </summary>
        [Description("No Design")] NoDesign = 3,
        
        /// <summary>
        /// The aluminum material.
        /// </summary>
        Aluminum = 4,

        /// <summary>
        /// The cold formed material.
        /// </summary>
        ColdFormed = 5,

        /// <summary>
        /// The rebar material.
        /// </summary>
        Rebar = 6,

        /// <summary>
        /// The tendon material.
        /// </summary>
        Tendon = 7,
  
        /// <summary> material.
        /// The masonry
        /// </summary>
        Masonry = 8,
    }
}
