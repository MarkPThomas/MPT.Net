// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ASolid.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ASolid.
    /// </summary>
    /// <seealso cref="AreaSection{ASolidProperties}" />
    public class ASolid : AreaSection<ASolidProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ASolid Factory(
            Materials.Materials material,
            string uniqueName, 
            ASolidProperties properties = null)
        {
            ASolid areaSection = new ASolid(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASolid" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ASolid(
            Materials.Materials material,
            string name) : base(material, name, eAreaSectionType.ASolid)
        {

        }
        #endregion
    }
}
