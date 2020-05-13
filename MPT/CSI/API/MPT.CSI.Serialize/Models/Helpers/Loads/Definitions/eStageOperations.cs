// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="eStageOperations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Stage operations available in the application.
    /// </summary>
    public enum eStageOperations
    {
        /// <summary>
        /// Add structure.
        /// </summary>
        [Description("Add Structure")]
        AddStructure = 1,

        /// <summary>
        /// Remove structure.
        /// </summary>
        [Description("Remove Structure")]
        RemoveStructure = 2,

        /// <summary>
        /// Load objects if new.
        /// </summary>
        [Description("Load Objects If Added")]
        LoadObjectsIfNew = 3,

        /// <summary>
        /// Load objects.
        /// </summary>
        [Description("Load Objects")]
        LoadObjects = 4,

        /// <summary>
        /// Change section properties.
        /// </summary>
        [Description("Change Section")]
        ChangeSectionProperties = 5,

        /// <summary>
        /// Change section property modifiers.
        /// </summary>
        [Description("Change Section Property Modifiers")]
        ChangeSectionPropertyModifiers = 6,

        /// <summary>
        /// Change releases.
        /// </summary>
        [Description("Change Releases")]
        ChangeReleases = 7,

        /// <summary>
        /// Change section properties and age.
        /// </summary>
        [Description("Change Section and Age")]
        ChangeSectionPropertiesAndAge = 11,

        /// <summary>
        /// Adds a guide structure.
        /// </summary>
        [Description("Add Guide Structure")]
        AddGuideStructure = 12
    }
}