﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="DoubleAngleSection.cs" company="">
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
    /// Class DoubleAngleSection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection{DoubleAngleSectionProperties}" />
    public class DoubleAngleSection : FrameSection<DoubleAngleSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DoubleAngleSection.</returns>
        internal static DoubleAngleSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            DoubleAngleSectionProperties properties = null)
        {
            DoubleAngleSection frameSection = new DoubleAngleSection(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAngleSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected DoubleAngleSection(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name, eFrameSectionType.DoubleAngle)
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
            _apiFrameSection.GetDoubleAngle(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var tf,
                out var tw,
                out var separation,
                out var color,
                out var notes,
                out var guid);

            FileName = fileName;
            if (!(_sectionProperties is DoubleAngleSectionProperties)) return;
            _sectionProperties = new DoubleAngleSectionProperties
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
                Separation = separation
            };
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>DoubleAngleSection.</returns>
        internal static DoubleAngleSection Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            DoubleAngleSectionProperties properties)
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
        protected override void set(string name, DoubleAngleSectionProperties properties)
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
        protected static void setApi(ApiFrameSection apiFrameSection, string name, DoubleAngleSectionProperties properties)
        {
            apiFrameSection.SetDoubleAngle(
                name,
                properties.MaterialName,
                properties.t3,
                properties.t2,
                properties.tf,
                properties.tw,
                properties.Separation,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }

        #endregion
    }
}
