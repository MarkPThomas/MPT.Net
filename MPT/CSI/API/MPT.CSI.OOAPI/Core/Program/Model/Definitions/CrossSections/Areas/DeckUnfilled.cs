// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckUnfilled.cs" company="">
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
    /// Class DeckUnfilled.
    /// </summary>
    /// <seealso cref="DeckExtended{DeckUnfilledProperties}" />
    public class DeckUnfilled : DeckExtended<DeckUnfilledProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static DeckUnfilled Factory(ApiCSiApplication app, string uniqueName, DeckUnfilledProperties properties = null)
        {
            DeckUnfilled areaSection = new DeckUnfilled(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeckUnfilled" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DeckUnfilled(ApiCSiApplication app, string name) : base(app, name) { }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetDeckUnfilled(Name,
                out var ribDepth,
                out var ribWidthTop,
                out var ribWidthBottom,
                out var ribSpacing,
                out var shearThickness,
                out var unitWeight);
            
            if (!(_properties is DeckUnfilledProperties properties)) return;
            properties.RibDepth = ribDepth;
            properties.RibWidthTop = ribWidthTop;
            properties.RibWidthBottom = ribWidthBottom;
            properties.RibSpacing = ribSpacing;
            properties.ShearThickness = shearThickness;
            properties.UnitWeight = unitWeight;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DeckUnfilled.</returns>
        internal static DeckUnfilled Add(ApiCSiApplication app, string uniqueName, DeckUnfilledProperties properties)
        {
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, uniqueName, properties);
        }

        /// <summary>
        /// This function initializes an area section property.
        /// If this function is called for an existing area section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the section.</param>
        public override void Set(DeckUnfilledProperties properties)
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
        protected static void setApi(ApiAreaSection apiAreaSection, string name, DeckUnfilledProperties properties)
        {
            apiAreaSection.SetDeckUnfilled(
                name,
                properties.RibDepth,
                properties.RibWidthTop,
                properties.RibWidthBottom,
                properties.RibSpacing,
                properties.ShearThickness,
                properties.UnitWeight);
        }
        #endregion
    }
}
#endif
