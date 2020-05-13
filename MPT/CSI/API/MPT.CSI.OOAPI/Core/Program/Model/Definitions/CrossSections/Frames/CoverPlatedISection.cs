// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="CoverPlatedISection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class CoverPlatedISection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection{CoverPlatedISectionProperties}" />
    public class CoverPlatedISection : FrameSection<CoverPlatedISectionProperties>
    {   // TODO: Refactor CoverPlatedISectionProperties as a composite section to remove base material property
        #region Fields & Properties

        /// <summary>
        /// The frame sectionses
        /// </summary>
        private readonly FrameSections _frameSectionses;

        /// <summary>
        /// The existing I-type frame section property that is used for the I-section portion of the coverplated I section.
        /// </summary>
        /// <value>The section.</value>
        internal string SectionName
        {
            get => SectionProperties.SectionName;
            set => SectionProperties.SectionName = value;
        }
        /// <summary>
        /// The section
        /// </summary>
        private FrameSection _section;
        /// <summary>
        /// The existing I-type frame section property that is used for the I-section portion of the coverplated I section.
        /// </summary>
        /// <value>The section.</value>
        /// <exception cref="CSiException">Section name for CoverPlatedISection is not of an ISection!</exception>
        public ISection Section
        {
            get
            {
                if (_section != null) return (ISection) _section;

                _section = _frameSectionses.FillItem(SectionName);
                if (_section is ISection section) return section;

                _section = null;
                throw new CSiException("Section name for CoverPlatedISection is not of an ISection!");
            }
        }

        /// <summary>
        /// The material property for the top cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcTop" /> and the <see cref="CoverPlatedISectionProperties.bcTop" /> items are greater than 0.
        /// </summary>
        /// <value>The material top.</value>
        internal string MaterialNameTop
        {
            get => SectionProperties.MaterialNameTop;
            set => SectionProperties.MaterialNameTop = value;
        }
        /// <summary>
        /// The material
        /// </summary>
        private Material _materialTop;
        /// <summary>
        /// The material property for the top cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcTop" /> and the <see cref="CoverPlatedISectionProperties.bcTop" /> items are greater than 0.
        /// </summary>
        /// <value>The material top.</value>
        public Material MaterialTop => _materialTop ??
                                       (_materialTop = _materials.FillItem(MaterialNameTop));

        /// <summary>
        /// The material property for the bottom cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcBottom" /> and the <see cref="CoverPlatedISectionProperties.bcBottom" /> items are greater than 0.
        /// </summary>
        /// <value>The material bottom.</value>
        internal string MaterialNameBottom
        {
            get => SectionProperties.MaterialNameBottom;
            set => SectionProperties.MaterialNameBottom = value;
        }
        /// <summary>
        /// The material
        /// </summary>
        private Material _materialBottom;
        /// <summary>
        /// The material property for the bottom cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcBottom" /> and the <see cref="CoverPlatedISectionProperties.bcBottom" /> items are greater than 0.
        /// </summary>
        /// <value>The material bottom.</value>
        public Material MaterialBottom => _materialBottom ??
                                          (_materialBottom = _materials.FillItem(MaterialNameBottom));
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="frameSections">The frame sections.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TeeSection.</returns>
        internal static CoverPlatedISection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            FrameSections frameSections,
            string uniqueName, 
            CoverPlatedISectionProperties properties = null)
        {
            CoverPlatedISection frameSection = new CoverPlatedISection(app, material, frameSections, uniqueName)
                                                    { _sectionProperties = properties };
            if (properties == null)
            {
               frameSection.FillData();
            }

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverPlatedISection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="frameSections">The frame sections.</param>
        /// <param name="name">The name.</param>
        protected CoverPlatedISection(
            ApiCSiApplication app,
            Materials.Materials material,
            FrameSections frameSections,
            string name) : base(app, material, name, eFrameSectionType.BuiltUpICoverPlate)
        {
            _frameSectionses = frameSections;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns frame section property data for the section.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Fill()
        {
            _apiFrameSection.GetCoverPlatedI(Name,
                out var sectionName,
                out var fyTopFlange,
                out var fyWeb,
                out var fyBottomFlange,
                out var tcTop,
                out var bcTop,
                out var materialTop,
                out var tcBottom,
                out var bcBottom,
                out var materialBottom,
                out var color,
                out var notes,
                out var guid);
            
            if (!(_sectionProperties is CoverPlatedISectionProperties)) return;
            _sectionProperties = new CoverPlatedISectionProperties
            {
                GeneralData =
                {
                    Color = color,
                    Notes = notes,
                    GUID = guid
                },
                SectionName = sectionName,
                fyTopFlange = fyTopFlange,
                fyWeb = fyWeb,
                fyBottomFlange = fyBottomFlange,
                tcTop = tcTop,
                bcTop = bcTop,
                MaterialNameTop = materialTop,
                tcBottom = tcBottom,
                bcBottom = bcBottom,
                MaterialNameBottom = materialBottom
            };
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="frameSections">The frame sections.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>ISection.</returns>
        internal static CoverPlatedISection Add(
            ApiCSiApplication app,
            Materials.Materials material,
            FrameSections frameSections,
            string uniqueName, 
            CoverPlatedISectionProperties properties)
        {
            setApi(getApiFrameSection(app), uniqueName, properties);
            return Factory(app, material, frameSections, uniqueName, properties);
        }

        /// <summary>
        /// This function initializes a frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">Name of the frame section.
        /// If not specified, the current object's name will be used.</param>
        /// <param name="properties">The properties to apply to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected override void set(string name, CoverPlatedISectionProperties properties)
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
        protected static void setApi(ApiFrameSection apiFrameSection, string name, CoverPlatedISectionProperties properties)
        {
            apiFrameSection.SetCoverPlatedI(
                name,
                properties.SectionName,
                properties.fyTopFlange,
                properties.fyWeb,
                properties.fyBottomFlange,
                properties.tcTop,
                properties.bcTop,
                properties.MaterialNameTop,
                properties.tcBottom,
                properties.bcBottom,
                properties.MaterialNameBottom,
                properties.GeneralData.Color,
                properties.GeneralData.Notes,
                properties.GeneralData.GUID);
        }
        #endregion
    }
}
