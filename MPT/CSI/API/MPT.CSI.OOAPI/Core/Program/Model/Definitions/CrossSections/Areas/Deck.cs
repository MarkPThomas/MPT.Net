// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Deck.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Deck.
    /// </summary>
    /// <seealso cref="Shell{DeckProperties}" />
    public class Deck : Shell<DeckProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties.
        /// </summary>
        /// <value>The extended.</value>
        private DeckExtended _extended;
        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        /// <value>The extended.</value>
        public DeckExtended Extended
        {
            get
            {
                if (_extended == null)
                {
                    FillExtended();
                }

                return _extended;
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Deck Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            DeckProperties properties = null)
        {
            Deck areaSection = new Deck(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Deck" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected Deck(
            ApiCSiApplication app, 
            Materials.Materials materials, 
            string name) : base(app, materials, name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetDeck(Name,
                out var overallDepth,
                out var floorType,
                out var shellType,
                out var nameMaterial,
                out var color,
                out var notes,
                out var guid);

            if (!(_sectionProperties is DeckProperties)) return;
            _sectionProperties = new DeckProperties
            {
                MaterialName = nameMaterial,
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                OverallDepth = overallDepth,
                FloorType = floorType,
                ShellType = shellType
            };
        }

        /// <summary>
        /// Adds the specified area section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique area section name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>Slab.</returns>
        internal static Deck Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            DeckProperties properties)
        {
            if (properties == null) return null;
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(DeckProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, DeckProperties properties)
        {
            apiAreaSection.SetDeck(name,
                properties.OverallDepth,
                properties.FloorType,
                properties.ShellType,
                properties.MaterialName,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }

        /// <summary>
        /// Fills the extended.
        /// </summary>
        public void FillExtended()
        {
            switch (SectionProperties.FloorType)
            {
                case eDeckType.SolidSlab:
                    _extended = DeckSolidSlab.Factory(_apiApp, Name);
                    break;
                case eDeckType.Unfilled:
                    _extended = DeckUnfilled.Factory(_apiApp, Name);
                    break;
                case eDeckType.Filled:
                    _extended = DeckFilled.Factory(_apiApp, Name);
                    break;
            }
        }

        /// <summary>
        /// Sets the extended.
        /// </summary>
        /// <param name="extendedProperties">The extended properties.</param>
        public void SetExtended(DeckExtendedProperties extendedProperties)
        {
            if (extendedProperties == null) return;
            switch (extendedProperties)
            {
                case DeckSolidSlabProperties deckSolidSlabProperties:
                    _extended = DeckSolidSlab.Factory(_apiApp, Name, deckSolidSlabProperties);
                    break;
                case DeckUnfilledProperties deckUnfilledProperties:
                    _extended = DeckUnfilled.Factory(_apiApp, Name, deckUnfilledProperties);
                    break;
                case DeckFilledProperties deckFilledProperties:
                    _extended = DeckFilled.Factory(_apiApp, Name, deckFilledProperties);
                    break;
            }
        }
        #endregion
    }
}
#endif
