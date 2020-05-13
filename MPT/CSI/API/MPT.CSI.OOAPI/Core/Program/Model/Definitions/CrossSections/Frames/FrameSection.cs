// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="FrameSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection" />
    public abstract class FrameSection<T> : FrameSection where T : FrameSectionProperties, new()
    {

        /// <summary>
        /// The section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public T SectionProperties => (T)_sectionProperties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected FrameSection(ApiCSiApplication app,
            Materials.Materials material,
            string name,
            eFrameSectionType type = eFrameSectionType.All)
            : base(app, material, name, type)
        {
            _sectionProperties = new T();
        }


        /// <summary>
        /// Modifies a frame section property data for the section.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Set(T properties)
        {
            if (properties == null) return;
            _sectionProperties = properties;
            set(Name, properties);
        }


        /// <summary>
        /// This function initializes a frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">Name of the frame section.
        /// If not specified, the current object's name will be used.</param>
        /// <param name="properties">The properties to apply to the section.</param>
        protected abstract void set(string name, T properties);
    }

    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.CrossSection" />
    public abstract class FrameSection : CrossSection 
    {
        #region Fields & Properties

        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The frame section.</value>
        protected ApiFrameSection _apiFrameSection => getApiFrameSection(_apiApp);


        /// <summary>
        /// The section properties
        /// </summary>
        protected SectionProperties _sectionProperties;

        /// <summary>
        /// The resultant properties base.
        /// </summary>
        private SectionResultantProperties _resultantPropertiesBase;
        /// <summary>
        /// The resultant properties
        /// </summary>
        /// <value>The resultant properties.</value>
        protected SectionResultantProperties _resultantProperties
        {
            get
            {
                if (_resultantPropertiesBase == null)
                {
                    FillSectionProperties();
                }

                return _resultantPropertiesBase;
            }
        }

        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        private ColumnRebar _columnRebarBase;
        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        protected ColumnRebar _columnRebar
        {
            get
            {
                if (_columnRebarBase == null)
                {
                    fillColumnRebar();
                }

                return _columnRebarBase;
            }
        }

        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        private BeamRebar _beamRebarBase;
        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        protected BeamRebar _beamRebar
        {
            get
            {
                if (_beamRebarBase == null)
                {
                    fillBeamRebar();
                }

                return _beamRebarBase;
            }
        }

        /// <summary>
        /// The section type
        /// </summary>
        private eFrameSectionType _sectionType;
        /// <summary>
        /// Gets or sets the type of the frame.
        /// </summary>
        /// <value>The type of the frame.</value>
        public eFrameSectionType SectionType
        {
            get
            {
                if (_sectionType == 0)
                {
                    FillFrameType();
                }

                return _sectionType;
            }
        }

        /// <summary>
        /// The rebar type
        /// </summary>
        private eRebarType _rebarType;
        /// <summary>
        /// Rebar design type for the specified frame section property.
        /// This function applies only to the following section property types:<para /><see cref="eFrameSectionType.TSection" /><para /><see cref="eFrameSectionType.Angle" /><para /><see cref="eFrameSectionType.Rectangular" /><para /><see cref="eFrameSectionType.Circle" /><para />
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// </summary>
        /// <value>The type of the rebar.</value>
        public eRebarType RebarType
        {
            get
            {
                if (_rebarType == 0)
                {
                    FillRebarType();
                }

                return _rebarType;
            }
        }

        /// <summary>
        /// The modifiers
        /// </summary>
        private FrameModifier _modifiers;
        /// <summary>
        /// Gets or sets the frame modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public FrameModifier Modifiers
        {
            get
            {
                if (_modifiers == null)
                {
                    FillModifiers();
                }

                return _modifiers;
            }
        }

        /// <summary>
        /// The gross cross-sectional area. [L^2].
        /// </summary>
        /// <value>The ag.</value>
        public double Ag => _resultantProperties.Ag;

        /// <summary>
        /// The shear area for forces in the section local 2-axis direction. [L^2].
        /// </summary>
        /// <value>The as2.</value>
        public double As2 => _resultantProperties.As2;

        /// <summary>
        /// The shear area for forces in the section local 3-axis direction. [L^2].
        /// </summary>
        /// <value>The as3.</value>
        public double As3 => _resultantProperties.As3;

        /// <summary>
        /// The torsional constant. [L^4].
        /// </summary>
        /// <value>The j.</value>
        public double J => _resultantProperties.J;

        /// <summary>
        /// The moment of inertia for bending about the local 2 axis. [L^4].
        /// </summary>
        /// <value>The i22.</value>
        public double I22 => _resultantProperties.I22;

        /// <summary>
        /// The moment of inertia for bending about the local 3 axis. [L^4].
        /// </summary>
        /// <value>The i33.</value>
        public double I33 => _resultantProperties.I33;

        /// <summary>
        /// The section modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The S22.</value>
        public double S22 => _resultantProperties.S22;

        /// <summary>
        /// The section modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The S33.</value>
        public double S33 => _resultantProperties.S33;

        /// <summary>
        /// The plastic modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The Z22.</value>
        public double Z22 => _resultantProperties.Z22;

        /// <summary>
        /// The plastic modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The Z33.</value>
        public double Z33 => _resultantProperties.Z33;

        /// <summary>
        /// The radius of gyration about the local 2 axis. [L].
        /// </summary>
        /// <value>The R22.</value>
        public double r22 => _resultantProperties.r22;

        /// <summary>
        /// The radius of gyration about the local 3 axis. [L].
        /// </summary>
        /// <value>The R33.</value>
        public double r33 => _resultantProperties.r33;

        /// <summary>
        /// Gets or sets the name of the material.
        /// </summary>
        /// <value>The name of the material.</value>
        internal override string MaterialName
        {
            get => _sectionProperties.MaterialName;
            set => _sectionProperties.MaterialName = value;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="frameSection">The frame section.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>FrameSection.</returns>
        internal static FrameSection Factory(
            ApiCSiApplication app,
            Materials.Materials material,
            FrameSections frameSection,
            string uniqueName)
        {
            eFrameSectionType frameSectionType = GetType(getApiFrameSection(app), uniqueName);
            switch (frameSectionType)
            {
                case eFrameSectionType.Angle:
                    return AngleSection.Factory(app, material, uniqueName);

                case eFrameSectionType.Channel:
                    return ChannelSection.Factory(app, material, uniqueName);

                case eFrameSectionType.Circle:
                    return CircleSection.Factory(app, material, uniqueName);

                case eFrameSectionType.BuiltUpICoverPlate:
                    return CoverPlatedISection.Factory(app, material, frameSection, uniqueName);

                case eFrameSectionType.DoubleAngle:
                    return DoubleAngleSection.Factory(app, material, uniqueName);

                case eFrameSectionType.DoubleChannel:
                    return DoubleChannelSection.Factory(app, material, uniqueName);

                case eFrameSectionType.General:
                    return GeneralSection.Factory(app, material, uniqueName);

                case eFrameSectionType.ISection:
                    return ISection.Factory(app, material, uniqueName);

                case eFrameSectionType.Pipe:
                    return PipeSection.Factory(app, material, uniqueName);

                case eFrameSectionType.Rectangular:
                    return RectangleSection.Factory(app, material, uniqueName);

                case eFrameSectionType.TSection:
                    return TeeSection.Factory(app, material, uniqueName);

                case eFrameSectionType.Box:
                    return TubeSection.Factory(app, material, uniqueName);

#if BUILD_ETABS2016 || BUILD_ETABS2017
                case eFrameSectionType.ConcreteL:
                    return ConcreteLSection.Factory(app, material, uniqueName);

                case eFrameSectionType.ConcreteTee:
                    return ConcreteTeeSection.Factory(app, material, uniqueName);

                case eFrameSectionType.SteelAngle: // TODO: Check SteelAngleSection type returned
                    return SteelAngleSection.Factory(app, material, uniqueName);

                case eFrameSectionType.SteelTee: // TODO: Check SteelTeeSection type returned
                    return SteelTeeSection.Factory(app, material, uniqueName);

                case eFrameSectionType.SteelPlate:
                    return PlateSection.Factory(app, material, uniqueName);

                case eFrameSectionType.SteelRod:
                    return RodSection.Factory(app, material, uniqueName);
#else
                case eFrameSectionType.ColdC:
                    return ColdCSection.Factory(app, material, uniqueName);

                case eFrameSectionType.ColdHat:
                    return ColdHatSection.Factory(app, material, uniqueName);

                case eFrameSectionType.ColdZ:
                    return ColdZSection.Factory(app, material, uniqueName);

                case eFrameSectionType.BuiltUpIHybrid:
                    return HybridISection.Factory(app, material, uniqueName);

                case eFrameSectionType.BuiltUpUHybrid:
                    return HybridUSection.Factory(app, material, uniqueName);

                case eFrameSectionType.PreCastConcreteGirderI:
                    return PrecastISection.Factory(app, material, uniqueName);

                case eFrameSectionType.PreCastConcreteGirderU:
                    return PrecastUSection.Factory(app, material, uniqueName);
#endif
                case eFrameSectionType.All:
                case eFrameSectionType.Auto:
                case eFrameSectionType.SectionDesigner:
                case eFrameSectionType.Variable:
                case eFrameSectionType.Joist:
                case eFrameSectionType.Bridge:
                case eFrameSectionType.ColdDoubleC:
                case eFrameSectionType.ColdL:
                case eFrameSectionType.ColdDoubleL:
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
                case eFrameSectionType.FilledTube:
                case eFrameSectionType.FilledPipe:
                case eFrameSectionType.EncasedRectangle:
                case eFrameSectionType.EncasedCircle:
                case eFrameSectionType.BucklingRestrainedBrace:
                case eFrameSectionType.CoreBraceBRB:
                case eFrameSectionType.ConcreteBox:
                case eFrameSectionType.ConcretePipe:
                case eFrameSectionType.ConcreteCross:
#endif
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected FrameSection(ApiCSiApplication app,
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.All)
            : base(app, material, name)
        {
            _sectionType = type;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
#if !BUILD_ETABS2015
            FillNameInPropertyFile();
#endif
            base.FillData();
        }
        #endregion

        #region Static
        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <param name="frameSection">The frame section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiFrameSection frameSection)
        {
            return new List<string>(getNameList(frameSection));
        }

        /// <summary>
        /// Returns the names of all defined frame properties of the specified type.
        /// </summary>
        /// <param name="frameSection">The frame section.</param>
        /// <param name="frameType">Type of the frame.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiFrameSection frameSection, eFrameSectionType frameType)
        {
            return new List<string>(frameSection.GetNameList(frameType));
        }

        /// <summary>
        /// Gets the frame section type.
        /// </summary>
        /// <param name="frameSection">The frame section.</param>
        /// <param name="name">The name of the section.</param>
        /// <returns>eFrameSectionType.</returns>
        internal static eFrameSectionType GetType(ApiFrameSection frameSection, string name)
        {
            return frameSection?.GetType(name) ?? 0;
        }
        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_apiFrameSection, newName);
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void Delete()
        {
            delete(_apiFrameSection);
        }

        #endregion

        #region Methods: Section
        /// <summary>
        /// Returns properties for frame section.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void FillSectionProperties()
        {
            _apiFrameSection.GetSectionProperties(Name,
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
                out var r33);

            _resultantPropertiesBase = new SectionResultantProperties
            {
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
            };
        }

        /// <summary>
        /// Returns the property type for the specified frame section property.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void FillFrameType()
        {
            _sectionType = GetType(_apiFrameSection, Name);
        }

#if !BUILD_ETABS2015
        /// <summary>
        /// Returns the rebar design type for the specified frame section property.
        /// This function applies only to the following section property types:<para /><see cref="eFrameSectionType.TSection" /><para /><see cref="eFrameSectionType.Angle" /><para /><see cref="eFrameSectionType.Rectangular" /><para /><see cref="eFrameSectionType.Circle" /><para />
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillRebarType()
        {
            if (SectionType == eFrameSectionType.TSection ||
                SectionType == eFrameSectionType.Angle ||
                SectionType == eFrameSectionType.Rectangular ||
                SectionType == eFrameSectionType.Circle)
            {
                _rebarType = _apiFrameSection.GetRebarType(Name);
            }
        }
#endif
        #endregion

        #region Methods: Imported Section
        /// <summary>
        /// This function imports a frame section property from a property file.
        /// </summary>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="fileName">The name of the frame section property file from which to get the frame section property specified by the <paramref name="nameInFile" /> item.
        /// In most cases you can input just the name of the property file (e.g.Sections8.pro) and the program will be able to find it.
        /// In some cases you may have to input the full path to the property file.
        /// TODO: Handle this - ImportSectionProperty exception.</param>
        /// <param name="nameInFile">The name of the frame section property, inside the property file specified by the <paramref name="fileName" /> item, that is to be imported.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal void ImportSectionProperty(
            string nameMaterial,
            string fileName,
            string nameInFile,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            // TODO: Handle AddFromFile vs. SetFromFile to complete & make public
            _apiFrameSection.ImportSectionProperty(Name,
                MaterialName,
                FileName,
                NameInFile,
                Color,
                Notes,
                GUID);
        }

#if !BUILD_ETABS2015
        /// <summary>
        /// Returns the names of the section property file from which an imported frame section originated, and it also retrieves the section name used in the property file.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillNameInPropertyFile()
        {
            _apiFrameSection.GetNameInPropertyFile(Name,
                out var nameInFile,
                out var fileName,
                out var nameMaterial,
                out var frameSectionType);

            _sectionType = frameSectionType;
            FileName = fileName;
            NameInFile = nameInFile;
            MaterialName = nameMaterial;
        }
#endif
        #endregion

        #region Methods: Modifiers
        /// <summary>
        /// Returns the modifier assignment for frame properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModifiers()
        {
            _modifiers = _apiFrameSection.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for frame properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="frameModifier">The frame modifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(FrameModifier frameModifier)
        {
            _apiFrameSection.SetModifiers(Name, frameModifier);
            _modifiers = frameModifier;
        }
        #endregion

        #region Methods: Fill/Set
        /// <summary>
        /// Fills the beam rebar.
        /// </summary>
        protected void fillBeamRebar()
        {
            if (Material.Type == eMaterialPropertyType.Concrete)
            {
                _beamRebarBase = new BeamRebar(_apiApp, Name);
                _beamRebarBase.Fill();
            }
            else
            {
                _beamRebarBase = null;
            }
        }


        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        protected void setBeamRebar(BeamRebarDetailing beamRebar)
        {
            if (Material.Type == eMaterialPropertyType.Concrete)
            {
                _beamRebarBase = new BeamRebar(_apiApp, Name);
                _beamRebarBase.Set(beamRebar);
            }
            else
            {
                _beamRebarBase = null;
            }
        }



        /// <summary>
        /// Fills the column rebar.
        /// </summary>
        protected void fillColumnRebar()
        {
            if (Material.Type == eMaterialPropertyType.Concrete)
            {
                _columnRebarBase = new ColumnRebar(_apiApp, Name);
                _columnRebarBase.Fill();
            }
            else
            {
                _columnRebarBase = null;
            }
        }


        /// <summary>
        /// Sets the column rebar.
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        protected void setColumnRebar(ColumnRebarDetailing columnRebar)
        {
            if (Material.Type == eMaterialPropertyType.Concrete)
            {
                _columnRebarBase = new ColumnRebar(_apiApp, Name);
                _columnRebarBase.Set(columnRebar);
            }
            else
            {
                _columnRebarBase = null;
            }
        }
        #endregion

    }
}
