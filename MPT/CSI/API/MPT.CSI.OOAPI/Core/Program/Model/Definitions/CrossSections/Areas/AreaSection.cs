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
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class FrameSection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas.AreaSection" />
    public abstract class AreaSection<T> : AreaSection where T : AreaSectionProperties, new()
    {
        /// <summary>
        /// The type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public eAreaSectionType AreaType { get; }

        /// <summary>
        /// The section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public T SectionProperties => (T)_sectionProperties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        protected AreaSection(ApiCSiApplication app,
            Materials.Materials material,
            string name,
            eAreaSectionType type = eAreaSectionType.All)
            : base(app, material, name)
        {
            _sectionProperties = new T();
            AreaType = type;
        }


        /// <summary>
        /// Modifies an area section property data for the section.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public virtual void Set(T properties)
        {
            if (properties == null) return;
            set(properties);
            setProperty(properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected abstract void set(T properties);

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
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas.AreaSection" />
    /// <seealso cref="CrossSection" />
    public abstract class AreaSection : CrossSection
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);

        /// <summary>
        /// The section properties
        /// </summary>
        protected SectionProperties _sectionProperties;

        /// <summary>
        /// The modifiers
        /// </summary>
        private AreaModifier _modifiers;
        /// <summary>
        /// Gets or sets the section modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public AreaModifier Modifiers
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
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>AreaSection.</returns>
        internal static AreaSection Factory(
            ApiCSiApplication app,
            Materials.Materials material, 
            string uniqueName)
        {
            eAreaSectionType areaSectionType = GetType(getApiAreaSection(app), uniqueName);
            switch (areaSectionType)
            {
                case eAreaSectionType.Shell:
#if BUILD_ETABS2016 || BUILD_ETABS2017
                    return initializeShell(app, material, uniqueName);
#else
                    return Shell.Factory(app, material, uniqueName);
                case eAreaSectionType.ASolid:
                    return ASolid.Factory(app, material, uniqueName);
                case eAreaSectionType.Plane:
                    return Plane.Factory(app, material, uniqueName);
#endif
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes the shell.
        /// The API has no method to determine the shell type in ETABS.
        /// Instead, it is assumed that it will fail &amp; throw an exception when filling a given type with an incompatible name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The shell name.</param>
        /// <returns>ShellBase.</returns>
        /// <exception cref="CSiException">No valid shell type was able to be initialied.</exception>
        protected static ShellBase initializeShell(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name)
        {
            ShellBase areaSection = initializeShell(app, material, name, (a, b, c) => Deck.Factory(app, material, name));
            if (areaSection != null) return areaSection;
            areaSection = initializeShell(app, material, name, (a, b, c) => Slab.Factory(app, material, name));
            if (areaSection != null) return areaSection;
            areaSection = initializeShell(app, material, name, (a, b, c) => Wall.Factory(app, material, name));
            if (areaSection != null) return areaSection;
            throw new CSiException("No valid shell type was able to be initialied.");
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The shell name.</param>
        /// <param name="factory">The factory to initialize the shell.</param>
        /// <returns>ShellBase.</returns>
        protected static ShellBase initializeShell(
            ApiCSiApplication app,
            Materials.Materials material,
            string name, 
            Func<ApiCSiApplication, Materials.Materials, string, ShellBase> factory) 
        {
            try
            {  
                return factory(app, material, name);
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected AreaSection(
            ApiCSiApplication app,
            Materials.Materials material,
            string name) : base(app, material, name) {}
        #endregion

        #region Static
        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <param name="areaSection">The area section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiAreaSection areaSection)
        {
            return new List<string>(getNameList(areaSection));
        }

        /// <summary>
        /// Returns the names of all defined area section properties of the specified type.
        /// </summary>
        /// <param name="areaSection">The area section.</param>
        /// <param name="areaType">Type of area section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiAreaSection areaSection, eAreaSectionType areaType)
        {
            return new List<string>(areaSection.GetNameList(areaType));
        }

        /// <summary>
        /// Gets the area section type.
        /// </summary>
        /// <param name="areaSection">The area section.</param>
        /// <param name="name">The name of the section.</param>
        /// <returns>eFrameSectionType.</returns>
        internal static eAreaSectionType GetType(ApiAreaSection areaSection, string name)
        {
            return areaSection?.GetType(name) ?? 0;
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
            changeName(_apiAreaSection, newName);
        }



        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void Delete()
        {
            delete(_apiAreaSection);
        }
        #endregion

        #region Methods: Section

        #endregion

        #region Methods: Imported Section

        #endregion

        #region Methods: Modifiers
        /// <summary>
        /// Returns the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModifiers()
        {
            _modifiers = _apiAreaSection.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="areaModifier">The area modifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(AreaModifier areaModifier)
        {
            _apiAreaSection.SetModifiers(Name, areaModifier);
            _modifiers = areaModifier;
        }
        #endregion
    }
}