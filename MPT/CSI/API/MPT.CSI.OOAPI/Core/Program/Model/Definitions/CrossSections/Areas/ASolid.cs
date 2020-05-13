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
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ASolid.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.AreaSection{ASolidProperties}" />
    public class ASolid : AreaSection<ASolidProperties>
    {
#region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ASolid Factory(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName, 
            ASolidProperties properties = null)
        {
            ASolid areaSection = new ASolid(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASolid" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ASolid(
            ApiCSiApplication app, 
            Materials.Materials material,
            string name) : base(app, material, name, eAreaSectionType.ASolid)
        {

        }
#endregion

#region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetASolid(Name,
                out var nameMaterial,
                out var materialAngle,
                out var arcAngle,
                out var incompatibleBendingModes,
                out var coordinateSystem,
                out var color,
                out var notes,
                out var guid);
            
            if (!(_sectionProperties is ASolidProperties properties)) return;
    // TODO: SAP2000 - Use object initializer for ASolid fill
            properties.MaterialName = nameMaterial;
            properties.MaterialAngle = materialAngle;
            properties.ArcAngle = arcAngle;
            properties.IncompatibleBendingModes = incompatibleBendingModes;
            properties.CoordinateSystem = coordinateSystem;
            properties.GeneralData.Color = color;
            properties.GeneralData.Notes = notes;
            properties.GeneralData.GUID = guid;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>ASolid.</returns>
        internal static ASolid Add(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName, 
            ASolidProperties properties)
        {
            if (properties == null) return null;
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(ASolidProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, ASolidProperties properties)
        {
            apiAreaSection.SetASolid(
                name,
                properties.MaterialName,
                properties.MaterialAngle,
                properties.ArcAngle,
                properties.IncompatibleBendingModes,
                properties.CoordinateSystem,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }
#endregion
    }
}
#endif
