// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="SteelAngleSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class SteelAngleSection.
    /// </summary>
    public class SteelAngleSection : FrameSection<SteelAngleSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static SteelAngleSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            SteelAngleSectionProperties properties = null)
        {
            SteelAngleSection frameSection = new SteelAngleSection(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SteelAngleSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected SteelAngleSection(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name, eFrameSectionType.Angle)
        {

        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns frame section property data for the section.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Fill()
        {
            _apiFrameSection.GetSteelAngle(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var tf,
                out var tw,
                out var r,
                out var mirrorAbout2,
                out var mirrorAbout3,
                out var color,
                out var notes,
                out var guid);

            FileName = fileName;
            if (!(_sectionProperties is SteelAngleSectionProperties)) return;
            _sectionProperties = new SteelAngleSectionProperties
            {
                MaterialName = nameMaterial,
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                t3 = t3,
                t2 = t2,
                tf = tf,
                tw = tw,
                r = r,
                MirrorAbout2 = mirrorAbout2,
                MirrorAbout3 = mirrorAbout3
            };
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static SteelAngleSection Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            SteelAngleSectionProperties properties)
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
        protected override void set(string name, SteelAngleSectionProperties properties)
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
        protected static void setApi(ApiFrameSection apiFrameSection, string name, SteelAngleSectionProperties properties)
        {
            apiFrameSection.SetSteelAngle(
                name,
                properties.MaterialName,
                properties.t3,
                properties.t2,
                properties.tf,
                properties.tw,
                properties.r,
                properties.MirrorAbout2,
                properties.MirrorAbout3,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }

        #endregion
    }
}
#endif