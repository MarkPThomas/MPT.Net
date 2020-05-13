// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Wall.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Wall.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public class Wall : Shell<WallProperties>
    {
        #region Fields & Properties        
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
        internal static Wall Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            WallProperties properties = null)
        {
            Wall areaSection = new Wall(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Wall" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected Wall(
            ApiCSiApplication app,
            Materials.Materials material,
            string name) : base(app, material, name) { }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        /// <exception cref="CSiException">Wall type is 'auto select list'.</exception>
        public override void Fill()
        {
            _apiAreaSection.GetWall(Name,
                out var thickness,
                out var walltype,
                out var shellType,
                out var nameMaterial,
                out var color,
                out var notes,
                out var guid);

            if (walltype == eWallPropertyType.AutoSelectList) throw new CSiException("Wall type is 'auto select list'.");

            if (!(_sectionProperties is WallProperties properties)) return;
            properties.Thickness = thickness;
            properties.ShellType = shellType;
            properties.MaterialName = nameMaterial;
            properties.GeneralData.Color = color;
            properties.GeneralData.Notes = notes;
            properties.GeneralData.GUID = guid;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>Wall.</returns>
        internal static Wall Add(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            WallProperties properties)
        {
            if (properties == null) return null;
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }


        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(WallProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, WallProperties properties)
        {
            apiAreaSection.SetWall(name,
                properties.Thickness,
                properties.WallType,
                properties.ShellType,
                properties.MaterialName,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }
        #endregion
    }
}
#endif
