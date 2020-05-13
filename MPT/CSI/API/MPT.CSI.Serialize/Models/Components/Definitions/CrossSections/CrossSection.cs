// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="CrossSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    // IListableNames - static
    /// <summary>
    /// Class CrossSection.
    /// </summary>
    /// <seealso cref="ObjectProperties" />
    public abstract class CrossSection : ObjectProperties
    {
        #region Fields & Properties        
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; set; }

        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;

        /// <summary>
        /// If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.
        /// </summary>
        /// <value>The name of the file.</value>
        public virtual string FileName { get; internal set; }

        /// <summary>
        /// If the section property was imported from a property file, this is the name of the specified frame section property in the frame section property file.
        /// If the section property was not imported, this item is blank.
        /// </summary>
        /// <value>The name of the file.</value>
        public virtual string NameInFile { get; internal set; }

        /// <summary>
        /// The name of the material.
        /// </summary>
        /// <value>The name of the material.</value>
        internal virtual string MaterialName { get; set; }
        /// <summary>
        /// The material
        /// </summary>
        protected Material _material;
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public virtual Material Material => _material ??
                                    (_material = null);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSection" /> class.
        /// </summary>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected CrossSection(Materials.Materials materials, string name) : base(name)
        {
            _materials = materials;
        }
        #endregion
    }
}
