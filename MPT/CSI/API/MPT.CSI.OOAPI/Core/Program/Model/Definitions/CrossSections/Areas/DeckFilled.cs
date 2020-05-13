// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="DeckFilled.cs" company="">
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
    /// Class DeckFilled.
    /// </summary>
    /// <seealso cref="DeckExtended{DeckFilledProperties}" />
    public class DeckFilled : DeckExtended<DeckFilledProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static DeckFilled Factory(ApiCSiApplication app, string uniqueName, DeckFilledProperties properties = null)
        {
            DeckFilled areaSection = new DeckFilled(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DeckFilled"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DeckFilled(ApiCSiApplication app, string name) : base(app, name)
        {

        }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetDeckFilled(Name,
                out var slabDepth,
                out var shearStudDiameter,
                out var shearStudHeight,
                out var shearStudFu,
                out var ribDepth,
                out var ribWidthTop,
                out var ribWidthBottom,
                out var ribSpacing,
                out var shearThickness,
                out var unitWeight);

            if (!(_properties is DeckFilledProperties properties)) return;
            properties.SlabFill.SlabDepth = slabDepth;
            properties.SlabFill.ShearStudDiameter = shearStudDiameter;
            properties.SlabFill.ShearStudHeight = shearStudHeight;
            properties.SlabFill.ShearStudFu = shearStudFu;
            properties.Form.RibDepth = ribDepth;
            properties.Form.RibWidthTop = ribWidthTop;
            properties.Form.RibWidthBottom = ribWidthBottom;
            properties.Form.RibSpacing = ribSpacing;
            properties.Form.ShearThickness = shearThickness;
            properties.Form.UnitWeight = unitWeight;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DeckFilled.</returns>
        internal static DeckFilled Add(ApiCSiApplication app, string uniqueName, DeckFilledProperties properties)
        {
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, uniqueName, properties);
        }

        /// <summary>
        /// This function initializes an area section property.
        /// If this function is called for an existing area section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the section.</param>
        public override void Set(DeckFilledProperties properties)
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
        protected static void setApi(ApiAreaSection apiAreaSection, string name, DeckFilledProperties properties)
        {
            apiAreaSection.SetDeckFilled(
                name,
                properties.SlabFill.SlabDepth,
                properties.SlabFill.ShearStudDiameter,
                properties.SlabFill.ShearStudHeight,
                properties.SlabFill.ShearStudFu,
                properties.Form.RibDepth,
                properties.Form.RibWidthTop,
                properties.Form.RibWidthBottom,
                properties.Form.RibSpacing,
                properties.Form.ShearThickness,
                properties.Form.UnitWeight);
        }
        #endregion
    }
}
#endif
