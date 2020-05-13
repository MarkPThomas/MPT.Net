// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="AreaSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas.AreaSection" />
    /// <seealso cref="AreaSection" />
    public abstract class AreaSection<T> : AreaSection where T : AreaSectionProperties, new()
    {
        /// <summary>
        /// The section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public T SectionProperties => (T)_sectionProperties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected AreaSection(
            Materials.Materials material,
            string name,
            eAreaSectionType type = eAreaSectionType.All)
            : base(material, name, type)
        {
            _sectionProperties = new T();
        }


        /// <summary>
        /// Modifies an area section property data for the section.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public virtual void Set(T properties)
        {
            if (properties == null) return;
            setProperty(properties);
        }

        /// <summary>
        /// This function initializes an area section property.
        /// If this function is called for an existing area section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the section.</param>
        protected virtual void setProperty(T properties)
        {
            _sectionProperties = properties;
        }
    }

    /// <summary>
    /// Class AreaSection.
    /// </summary>
    /// <seealso cref="AreaSection" />
    /// <seealso cref="Serialize.Models.Components.Definitions.CrossSections.CrossSection" />
    public abstract class AreaSection : CrossSection
    {
        #region Fields & Properties
        /// <summary>
        /// The type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public virtual eAreaSectionType AreaType { get; internal set; }

        /// <summary>
        /// The section properties
        /// </summary>
        protected SectionProperties _sectionProperties;

        /// <summary>
        /// Gets or sets the section modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public virtual AreaModifier Modifiers { get; internal set; }
        
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
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="areaSectionType">Type of the area section.</param>
        /// <returns>AreaSection.</returns>
        internal static AreaSection Factory(
            Materials.Materials material, 
            string uniqueName,
            eAreaSectionType areaSectionType)
        {
            switch (areaSectionType)
            {
                case eAreaSectionType.Shell:
                    return initializeShell(material, uniqueName);
                case eAreaSectionType.ASolid:
                    return ASolid.Factory(material, uniqueName);
                case eAreaSectionType.Plane:
                    return Plane.Factory(material, uniqueName);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes the shell.
        /// The API has no method to determine the shell type in ETABS.
        /// Instead, it is assumed that it will fail &amp; throw an exception when filling a given type with an incompatible name.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The shell name.</param>
        /// <returns>ShellBase.</returns>
        protected static ShellBase initializeShell(
            Materials.Materials material, 
            string name)
        {
            ShellBase areaSection = initializeShell(material, name, (a, b) => Deck.Factory(material, name));
            if (areaSection != null) return areaSection;
            areaSection = initializeShell(material, name, (a, b) => Slab.Factory(material, name));
            if (areaSection != null) return areaSection;
            areaSection = initializeShell(material, name, (a, b) => Wall.Factory(material, name));
            if (areaSection != null) return areaSection;
            areaSection = Shell.Factory(material, name);
            return areaSection;
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The shell name.</param>
        /// <param name="factory">The factory to initialize the shell.</param>
        /// <returns>ShellBase.</returns>
        protected static ShellBase initializeShell(
            Materials.Materials material,
            string name, 
            Func<Materials.Materials, string, ShellBase> factory) 
        {
            return factory(material, name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected AreaSection(
            Materials.Materials material,
            string name,
            eAreaSectionType type = eAreaSectionType.All) : base(material, name)
        {
            AreaType = type;
        }
        #endregion
    }
}