// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="SlabRibbed.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class SlabRibbed.
    /// </summary>
    /// <seealso cref="SlabExtended{T}" />
    public class SlabRibbed : SlabExtended<SlabRibbedProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static SlabRibbed Factory(
            string uniqueName, 
            SlabRibbedProperties properties = null)
        {
            SlabRibbed areaSection = new SlabRibbed(uniqueName) { _properties = properties };

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SlabRibbed"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected SlabRibbed(string name) : base(name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(SlabRibbedProperties properties)
        {
            set(properties);
        }
        #endregion
    }
}
