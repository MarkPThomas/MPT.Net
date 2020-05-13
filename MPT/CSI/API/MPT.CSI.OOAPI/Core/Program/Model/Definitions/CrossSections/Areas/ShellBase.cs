// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Shell.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas.ShellBase" />
    public abstract class Shell<T> : ShellBase where T : ShellProperties, new()
    {
        /// <summary>
        /// The section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public new T SectionProperties => (T)_sectionProperties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell{T}" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected Shell(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name)
        {
            _sectionProperties = new T();
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
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
    }


    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public abstract class ShellBase : AreaSection<ShellProperties>
    {
        #region Fields & Properties

        /// <summary>
        /// The design properties
        /// </summary>
        private ShellDesign _designProperties;

        /// <summary>
        /// Gets the design properties.
        /// </summary>
        /// <value>The design properties.</value>
        public ShellDesign DesignProperties => _designProperties ?? (_designProperties = ShellDesign.Factory(_apiApp, Name));

        /// <summary>
        /// Gets or sets the shell layer properties.
        /// </summary>
        /// <value>The layer properties.</value>
        protected ShellLayered _layerProperties;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellBase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected ShellBase(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name) : base(app, material, name, eAreaSectionType.Shell) { }
        #endregion

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected override void set(ShellProperties properties)
        {

        }
        #region Protected


        /// <summary>
        /// Gets the shell layered.
        /// </summary>
        /// <returns>ShellLayered.</returns>
        protected ShellLayered getShellLayered()
        {
            if (SectionProperties.ShellType != eShellType.ShellLayered) return null;
            return _layerProperties ?? (_layerProperties = ShellLayered.Factory(_apiApp, Name));
        }
        #endregion
    }
}