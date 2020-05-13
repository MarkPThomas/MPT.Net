// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Slab.cs" company="">
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
    /// Class Slab.
    /// </summary>
    /// <seealso cref="Shell{SlabProperties}" />
    public class Slab : Shell<SlabProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties.
        /// </summary>
        /// <value>The extended.</value>
        private SlabExtended _extended;
        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        /// <value>The extended.</value>
        public SlabExtended Extended {
            get
            {
                if (_extended == null)
                {
                    FillExtended();
                }

                return _extended;
            }
        }

        /// <summary>
        /// Gets the shell layers.
        /// </summary>
        /// <value>The layers.</value>
        public ShellLayered Layers => getShellLayered();
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
        internal static Slab Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            SlabProperties properties = null)
        {
            Slab areaSection = new Slab(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Slab" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected Slab(
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
            _apiAreaSection.GetSlab(Name,
                out var slabThickness,
                out var floorType,
                out var shellType,
                out var nameMaterial,
                out var color,
                out var notes,
                out var guid);
            
            if (!(_sectionProperties is SlabProperties)) return;
            _sectionProperties = new SlabProperties
            {
                MaterialName = nameMaterial,
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                SlabThickness = slabThickness,
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
        internal static Slab Add(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            SlabProperties properties)
        {
            if (properties == null) return null;
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(SlabProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, SlabProperties properties)
        {
            apiAreaSection.SetSlab(name,
                properties.SlabThickness,
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
            if (SectionProperties.ShellType == eShellType.ShellLayered)
            {
                _extended = null;
                return;
            }

            switch (SectionProperties.FloorType)
            {
                case eSlabType.Ribbed:
                    _extended = SlabRibbed.Factory(_apiApp, Name);
                    break;
                case eSlabType.Waffle:
                    _extended = SlabWaffle.Factory(_apiApp, Name);
                    break;
                case eSlabType.Slab:
                case eSlabType.Drop:
                    _extended = null;
                    break;
            }
        }

        /// <summary>
        /// Sets the extended.
        /// </summary>
        /// <param name="extendedProperties">The extended properties.</param>
        public void SetExtended(SlabExtendedProperties extendedProperties)
        {
            switch (extendedProperties)
            {
                case SlabRibbedProperties slabRibbedProperties:
                    _extended = SlabRibbed.Factory(_apiApp, Name, slabRibbedProperties);
                    _layerProperties = null;
                    break;
                case SlabWaffleProperties slabWaffleProperties:
                    _extended = SlabWaffle.Factory(_apiApp, Name, slabWaffleProperties);
                    _layerProperties = null;
                    break;
                default:
                    _extended = null;
                    break;
            }
        }
        #endregion
    }
}
#endif
