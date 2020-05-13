// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Shell.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <seealso cref="Areas.Shell{ShellProperties}" />
    public class Shell : Shell<ShellProperties>
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
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Shell Factory(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName, 
            ShellProperties properties = null)
        {
            Shell areaSection = new Shell(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellBase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Shell(
            ApiCSiApplication app, 
            Materials.Materials material,
            string name) : base(app, material, name)
        {
        }
#endregion

#region Fill/Set

        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetShell(Name,
                out var shellType,
                out var includeDrillingDof,
                out var nameMaterial,
                out var materialAngle,
                out var membraneThickness,
                out var bendingThickness,
                out var color,
                out var notes,
                out var guid);

            if (!(_sectionProperties is ShellProperties properties)) return;
    // TODO: SAP2000 - Use object initializer for Shell fill
            properties.MaterialName = nameMaterial;
            properties.ShellType = shellType;
            properties.IncludeDrillingDOF = includeDrillingDof;
            properties.MaterialAngle = materialAngle;
            properties.MembraneThickness = membraneThickness;
            properties.BendingThickness = bendingThickness;
            properties.GeneralData.Color = color;
            properties.GeneralData.Notes = notes;
            properties.GeneralData.GUID = guid;
        }

        /// <summary>
        /// Adds the specified area section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Unique area section name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>Shell.</returns>
        internal static Shell Add(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName, 
            ShellProperties properties)
        { 
            if (properties == null) return null;
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(ShellProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, ShellProperties properties)
        {
            _apiAreaSection.SetShell(name,
                properties.ShellType,
                properties.IncludeDrillingDOF,
                properties.MaterialName,
                properties.MaterialAngle,
                properties.MembraneThickness,
                properties.BendingThickness,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);

        }

#endregion
    }
}
#endif