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

using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections
{
    // IListableNames - static
    /// <summary>
    /// Class CrossSection.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.DefinitionElement" />
    public abstract class CrossSection : DefinitionElement
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;

        /// <summary>
        /// If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; protected set; }

        /// <summary>
        /// If the section property was imported from a property file, this is the name of the specified frame section property in the frame section property file.
        /// If the section property was not imported, this item is blank.
        /// </summary>
        /// <value>The name of the file.</value>
        public string NameInFile { get; protected set; }

        /// <summary>
        /// The name of the material.
        /// </summary>
        /// <value>The name of the material.</value>
        internal virtual string MaterialName { get; set; }
        /// <summary>
        /// The material
        /// </summary>
        private Material _material;
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public Material Material => _material ??
                                    (_material = _materials.FillItem(MaterialName));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSection" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected CrossSection(ApiCSiApplication app, Materials.Materials materials, string name) : base(app, name)
        {
            _materials = materials;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            Fill();
        }
        #endregion

        #region Methods: Fill/Set
        /// <summary>
        /// Returns area section property data for the section.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void Fill();
        #endregion
    }
}
