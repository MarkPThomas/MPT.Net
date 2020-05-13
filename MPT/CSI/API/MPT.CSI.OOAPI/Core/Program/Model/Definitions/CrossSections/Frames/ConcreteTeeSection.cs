// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="ConcreteTeeSection.cs" company="">
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
    /// Class ConcreteTeeSection.
    /// </summary>
    public class ConcreteTeeSection : FrameSection<ConcreteTeeSectionProperties>
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
        /// <returns>TeeSection.</returns>
        internal static ConcreteTeeSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            string uniqueName, 
            ConcreteTeeSectionProperties properties = null)
        {
            ConcreteTeeSection frameSection = new ConcreteTeeSection(app, material, uniqueName) { _sectionProperties = properties };
            if (properties == null)
            {
                frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteTeeSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected ConcreteTeeSection(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name, eFrameSectionType.ConcreteTee)
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
            _apiFrameSection.GetConcreteTee(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var tf,
                out var twF,
                out var twT,
                out var mirrorAbout3,
                out var color,
                out var notes,
                out var guid);
            
            FileName = fileName;
            if (!(_sectionProperties is ConcreteTeeSectionProperties)) return;
            _sectionProperties = new ConcreteTeeSectionProperties
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
                twFlange = twF,
                twTip = twT,
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
        /// <returns>TeeSection.</returns>
        internal static ConcreteTeeSection Add(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName, 
            ConcreteTeeSectionProperties properties)
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
        protected override void set(string name, ConcreteTeeSectionProperties properties)
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
        protected static void setApi(ApiFrameSection apiFrameSection, string name, ConcreteTeeSectionProperties properties)
        {
            apiFrameSection.SetConcreteTee(
                name,
                properties.MaterialName,
                properties.t3,
                properties.t2,
                properties.tf,
                properties.twFlange,
                properties.twTip,
                properties.MirrorAbout3,
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
#endif