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

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ShellBase" />
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
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected Shell(
            Materials.Materials material, 
            string name) : base(material, name)
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
            setProperty(properties);
        }
    }


    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <seealso cref="Areas.AreaSection{T}" />
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
        public ShellDesign DesignProperties => _designProperties ?? (_designProperties = ShellDesign.Factory(Name));

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
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected ShellBase(
            Materials.Materials material, 
            string name) : base(material, name, eAreaSectionType.Shell) { }
        #endregion
        
        #region Protected


        /// <summary>
        /// Gets the shell layered.
        /// </summary>
        /// <returns>ShellLayered.</returns>
        protected ShellLayered getShellLayered()
        {
            if (SectionProperties.ShellType != eShellType.ShellLayered) return null;
            return _layerProperties ?? (_layerProperties = ShellLayered.Factory(Name));
        }
        #endregion
    }
}