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
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class DeckSolidSlab.
    /// </summary>
    /// <seealso cref="DeckExtended{DeckSolidSlabProperties}" />
    public class DeckSolidSlab : DeckExtended<DeckSolidSlabProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static DeckSolidSlab Factory(ApiCSiApplication app, string uniqueName, DeckSolidSlabProperties properties = null)
        {
            DeckSolidSlab areaSection = new DeckSolidSlab(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeckSolidSlab" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DeckSolidSlab(ApiCSiApplication app, string name) : base(app, name)
        {
        }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetDeckSolidSlab(Name,
                out var slabDepth,
                out var shearStudDiameter,
                out var shearStudHeight,
                out var shearStudFu);
            
            if (!(_properties is DeckSolidSlabProperties properties)) return;
            properties.SlabDepth = slabDepth;
            properties.ShearStudDiameter = shearStudDiameter;
            properties.ShearStudHeight = shearStudHeight;
            properties.ShearStudFu = shearStudFu;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DeckSolidSlab.</returns>
        internal static DeckSolidSlab Add(ApiCSiApplication app, string uniqueName, DeckSolidSlabProperties properties)
        {
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, uniqueName, properties);
        }

        /// <summary>
        /// This function initializes an area section property.
        /// If this function is called for an existing area section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the section.</param>
        public override void Set(DeckSolidSlabProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
            set(properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, DeckSolidSlabProperties properties)
        {
            apiAreaSection.SetDeckSolidSlab(
                name,
                properties.SlabDepth,
                properties.ShearStudDiameter,
                properties.ShearStudHeight,
                properties.ShearStudFu);
        }
        #endregion
    }
}
#endif
