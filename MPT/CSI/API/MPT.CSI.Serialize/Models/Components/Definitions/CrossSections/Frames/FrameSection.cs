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

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="FrameSection" />
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
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected FrameSection(
            Materials.Materials material,
            string name,
            eFrameSectionType type = eFrameSectionType.All)
            : base(material, name, type)
        {
            _sectionProperties = new T();
        }


        /// <summary>
        /// Modifies a frame section property data for the section.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void Set(T properties)
        {
            if (properties == null) return;
            _sectionProperties = properties;
        }
    }

    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <seealso cref="CrossSection" />
    public abstract class FrameSection : CrossSection 
    {
        #region Fields & Properties
        /// <summary>
        /// The section properties
        /// </summary>
        protected SectionProperties _sectionProperties;

        /// <summary>
        /// The resultant properties base.
        /// </summary>
        protected SectionResultantProperties _resultantPropertiesBase;

        /// <summary>
        /// The resultant properties
        /// </summary>
        /// <value>The resultant properties.</value>
        protected virtual SectionResultantProperties _resultantProperties => _resultantPropertiesBase ??
                                                                             (_resultantPropertiesBase =
                                                                                 new SectionResultantProperties());

        protected ColumnRebar _columnRebarBase;
        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        protected virtual ColumnRebar _columnRebar => _columnRebarBase ?? (_columnRebarBase = new ColumnRebar(Name));

        protected BeamRebar _beamRebarBase;
        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        protected virtual BeamRebar _beamRebar => _beamRebarBase ?? (_beamRebarBase = new BeamRebar(Name));

        /// <summary>
        /// The section type
        /// </summary>
        protected eFrameSectionType _sectionType;
        /// <summary>
        /// Gets or sets the type of the frame.
        /// </summary>
        /// <value>The type of the frame.</value>
        public virtual eFrameSectionType SectionType
        {
            get => _sectionType;
        }


        /// <summary>
        /// Rebar design type for the specified frame section property.
        /// This function applies only to the following section property types:<para /><see cref="eFrameSectionType.TSection" /><para /><see cref="eFrameSectionType.Angle" /><para /><see cref="eFrameSectionType.Rectangular" /><para /><see cref="eFrameSectionType.Circle" /><para />
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// </summary>
        /// <value>The type of the rebar.</value>
        public virtual eRebarType RebarType { get; internal set; }
        
        /// <summary>
        /// Gets or sets the frame modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public virtual FrameModifier Modifiers { get; internal set; } = new FrameModifier();

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
        /// The moment of inertia for bending about the local 2-3 axis. [L^4].
        /// </summary>
        /// <value>The i23.</value>
        public double I23 => _resultantProperties.I23;

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
        /// <param name="material">The material.</param>
        /// <param name="frameSection">The frame section.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>FrameSection.</returns>
        internal static FrameSection Factory(
            Materials.Materials material,
            FrameSections frameSection,
            string uniqueName,
            eFrameSectionType frameSectionType)
        {
            switch (frameSectionType)
            {
                case eFrameSectionType.Angle:
                    return AngleSection.Factory(material, uniqueName);

                case eFrameSectionType.Channel:
                    return ChannelSection.Factory(material, uniqueName);

                case eFrameSectionType.Circle:
                    return CircleSection.Factory(material, uniqueName);

                case eFrameSectionType.BuiltUpICoverPlate:
                    return CoverPlatedISection.Factory(material, frameSection, uniqueName);

                case eFrameSectionType.DoubleAngle:
                    return DoubleAngleSection.Factory(material, uniqueName);

                case eFrameSectionType.DoubleChannel:
                    return DoubleChannelSection.Factory(material, uniqueName);

                case eFrameSectionType.General:
                    return GeneralSection.Factory(material, uniqueName);

                case eFrameSectionType.ISection:
                    return ISection.Factory(material, uniqueName);

                case eFrameSectionType.Pipe:
                    return PipeSection.Factory(material, uniqueName);

                case eFrameSectionType.Rectangular:
                    return RectangleSection.Factory(material, uniqueName);

                case eFrameSectionType.TSection:
                    return TeeSection.Factory(material, uniqueName);

                case eFrameSectionType.Box:
                    return TubeSection.Factory(material, uniqueName);

                case eFrameSectionType.ConcreteL:
                    return ConcreteLSection.Factory(material, uniqueName);

                case eFrameSectionType.ConcreteTee:
                    return ConcreteTeeSection.Factory(material, uniqueName);

                case eFrameSectionType.SteelAngle: // TODO: Check SteelAngleSection type returned
                    return SteelAngleSection.Factory(material, uniqueName);

                case eFrameSectionType.SteelTee: // TODO: Check SteelTeeSection type returned
                    return SteelTeeSection.Factory(material, uniqueName);

                case eFrameSectionType.SteelPlate:
                    return PlateSection.Factory(material, uniqueName);

                case eFrameSectionType.SteelRod:
                    return RodSection.Factory(material, uniqueName);

                case eFrameSectionType.ColdC:
                    return ColdCSection.Factory(material, uniqueName);

                case eFrameSectionType.ColdHat:
                    return ColdHatSection.Factory(material, uniqueName);

                case eFrameSectionType.ColdZ:
                    return ColdZSection.Factory(material, uniqueName);

                case eFrameSectionType.BuiltUpIHybrid:
                    return HybridISection.Factory(material, uniqueName);

                case eFrameSectionType.BuiltUpUHybrid:
                    return HybridUSection.Factory(material, uniqueName);

                case eFrameSectionType.PreCastConcreteGirderI:
                    return PrecastISection.Factory(material, uniqueName);

                case eFrameSectionType.PreCastConcreteGirderU:
                    return PrecastUSection.Factory(material, uniqueName);

                case eFrameSectionType.All:
                case eFrameSectionType.Auto:
                case eFrameSectionType.SectionDesigner:
                case eFrameSectionType.Variable:
                case eFrameSectionType.Joist:
                case eFrameSectionType.Bridge:
                case eFrameSectionType.ColdDoubleC:
                case eFrameSectionType.ColdL:
                case eFrameSectionType.ColdDoubleL:
                case eFrameSectionType.FilledTube:
                case eFrameSectionType.FilledPipe:
                case eFrameSectionType.EncasedRectangle:
                case eFrameSectionType.EncasedCircle:
                case eFrameSectionType.BucklingRestrainedBrace:
                case eFrameSectionType.CoreBraceBRB:
                case eFrameSectionType.ConcreteBox:
                case eFrameSectionType.ConcretePipe:
                case eFrameSectionType.ConcreteCross:
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected FrameSection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.All)
            : base(material, name)
        {
            _sectionType = type;
        }
        #endregion




        #region Methods: Fill/Set        
        /// <summary>
        /// Sets the section properties.
        /// </summary>
        /// <param name="resultantProperties">The resultant properties.</param>
        internal void SetSectionProperties(SectionResultantProperties resultantProperties)
        {
            _resultantPropertiesBase = resultantProperties;
        }

        /// <summary>
        /// Sets the type of the section.
        /// </summary>
        /// <param name="sectionType">Type of the section.</param>
        internal void SetSectionType(eFrameSectionType sectionType)
        {
            _sectionType = sectionType;
        }

        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        protected void setBeamRebar(BeamRebarDetailing beamRebar)
        {
            _beamRebarBase = Material.Type == eMaterialPropertyType.Concrete ? 
                new BeamRebar(Name) {Detailing = beamRebar} : 
                null;
        }

        /// <summary>
        /// Sets the column rebar.
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        protected void setColumnRebar(ColumnRebarDetailing columnRebar)
        {
            _columnRebarBase = Material.Type == eMaterialPropertyType.Concrete ? 
                new ColumnRebar(Name) { Detailing = columnRebar } : 
                null;
        }
        #endregion

    }
}
