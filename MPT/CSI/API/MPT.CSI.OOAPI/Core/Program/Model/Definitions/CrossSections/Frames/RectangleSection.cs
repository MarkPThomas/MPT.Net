// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="RectangleSection.cs" company="">
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
    /// Class RectangleSection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection{RectangleSectionProperties}" />
    public class RectangleSection : FrameSection<RectangleSectionProperties>
    {
        #region Fields & Properties        

        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        public ColumnRebar ColumnRebar => _columnRebar;

        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        public BeamRebar BeamRebar => _beamRebar;
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>RectangleSection.</returns>
        internal static RectangleSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            RectangleSectionProperties properties = null)
        {
            RectangleSection frameSection = new RectangleSection(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected RectangleSection(
            ApiCSiApplication app,
            Materials.Materials material,
            string name) : base(app, material, name, eFrameSectionType.Rectangular)
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
            _apiFrameSection.GetRectangle(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var color,
                out var notes,
                out var guid);

            FileName = fileName;
            if (!(_sectionProperties is RectangleSectionProperties)) return;
            _sectionProperties = new RectangleSectionProperties
            {
                MaterialName = nameMaterial,
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                h = t3,
                b = t2
            };
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>RectangleSection.</returns>
        internal static RectangleSection Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            RectangleSectionProperties properties)
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
        protected override void set(string name, RectangleSectionProperties properties)
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
        protected static void setApi(ApiFrameSection apiFrameSection, string name, RectangleSectionProperties properties)
        {
            apiFrameSection.SetRectangle(
                name,
                properties.MaterialName,
                properties.h,
                properties.b,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }
        #endregion

        #region Rebar
        /// <summary>
        /// Fills the beam rebar.
        /// </summary>
        public void FillBeamRebar()
        {
            fillBeamRebar();
        }

        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        public void SetBeamRebar(BeamRebarDetailing beamRebar)
        {
            setBeamRebar(beamRebar);
        }


        /// <summary>
        /// Fills the column rebar.
        /// </summary>
        public void FillColumnRebar()
        {
            fillColumnRebar();
        }

        /// <summary>
        /// Sets the column rebar.
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        public void SetColumnRebar(ColumnRebarDetailing columnRebar)
        {
            setColumnRebar(columnRebar);
        }
        #endregion
    }
}
