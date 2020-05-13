// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckSolidSlab.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class DeckSolidSlab.
    /// </summary>
    /// <seealso cref="DeckExtended{T}" />
    public class DeckSolidSlab : DeckExtended<DeckSolidSlabProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static DeckSolidSlab Factory(
            string uniqueName, 
            DeckSolidSlabProperties properties = null)
        {
            DeckSolidSlab areaSection = new DeckSolidSlab(uniqueName) { _properties = properties };

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeckSolidSlab" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected DeckSolidSlab(string name) : base(name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// This function initializes an area section property.
        /// If this function is called for an existing area section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the section.</param>
        public override void Set(DeckSolidSlabProperties properties)
        {
            set(properties);
        }
        #endregion
    }
}
