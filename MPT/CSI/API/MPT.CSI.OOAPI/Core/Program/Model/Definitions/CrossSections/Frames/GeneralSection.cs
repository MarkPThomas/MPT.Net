// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="GeneralSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class GeneralSection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection{GeneralSectionProperties}" />
    public class GeneralSection : FrameSection<GeneralSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>GeneralSection.</returns>
        internal static GeneralSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            GeneralSectionProperties properties = null)
        {
            GeneralSection frameSection = new GeneralSection(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected GeneralSection(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name, eFrameSectionType.General)
        {
        }
        #endregion

        #region Methods: Get/Set
        /// <summary>
        /// Returns frame section property data for the section.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Fill()
        {
            _apiFrameSection.GetGeneral(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var Ag,
                out var As2,
                out var As3,
                out var J,
                out var I22,
                out var I33,
                out var S22,
                out var S33,
                out var Z22,
                out var Z33,
                out var r22,
                out var r33,
                out var color,
                out var notes,
                out var guid);

            FileName = fileName;
            if (!(_sectionProperties is GeneralSectionProperties)) return;
            _sectionProperties = new GeneralSectionProperties
            {
                MaterialName = nameMaterial,
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                Properties =
                {
                    t3 = t3,
                    t2 = t2,
                    Ag = Ag,
                    As2 = As2,
                    As3 = As3,
                    J = J,
                    I22 = I22,
                    I33 = I33,
                    S22 = S22,
                    S33 = S33,
                    Z22 = Z22,
                    Z33 = Z33,
                    r22 = r22,
                    r33 = r33
                }
            };
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>GeneralSection.</returns>
        internal static GeneralSection Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            GeneralSectionProperties properties)
        {
            setApi(getApiFrameSection(app), uniqueName, properties);
            return Factory(app, material, uniqueName, properties);
        }

        /// <summary>
        /// This function initializes a frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">Name of the frame section.
        /// If not specified, the current object's name will be used.</param>
        /// <param name="properties">The properties to apply to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected override void set(string name, GeneralSectionProperties properties)
        {
            setApi(_apiFrameSection, name, properties);
        }

        /// <summary>
        /// This function initializes a frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="apiFrameSection">The API frame section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected static void setApi(ApiFrameSection apiFrameSection, string name, GeneralSectionProperties properties)
        {
            apiFrameSection.SetGeneral(
                name,
                properties.MaterialName,
                properties.Properties.t3,
                properties.Properties.t2,
                properties.Properties.Ag,
                properties.Properties.As2,
                properties.Properties.As3,
                properties.Properties.J,
                properties.Properties.I22,
                properties.Properties.I33,
                properties.Properties.S22,
                properties.Properties.S33,
                properties.Properties.Z22,
                properties.Properties.Z33,
                properties.Properties.r22,
                properties.Properties.r33,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }
        #endregion
    }
}
