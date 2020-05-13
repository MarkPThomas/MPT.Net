// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-17-2018
// ***********************************************************************
// <copyright file="StructureComponents.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model
{
    /// <summary>
    /// Class StructureComponents.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class StructureComponents : CSiOoApiBaseBase
    {
        #region Fields & Properties

        /// <summary>
        /// The materials
        /// </summary>
        private Materials _materials;
        /// <summary>
        /// Gets the materials.
        /// </summary>
        /// <value>The materials.</value>
        public Materials Materials => _materials ?? (_materials = new Materials(_apiApp));

        /// <summary>
        /// The frame sections
        /// </summary>
        private FrameSections _frameSections;
        /// <summary>
        /// Gets the frame sections.
        /// </summary>
        /// <value>The frame sections.</value>
        public FrameSections FrameSections => _frameSections ?? (_frameSections = new FrameSections(_apiApp, Materials));

        /// <summary>
        /// The area sections
        /// </summary>
        private AreaSections _areaSections;
        /// <summary>
        /// Gets the area sections.
        /// </summary>
        /// <value>The area sections.</value>
        public AreaSections AreaSections => _areaSections ?? (_areaSections = new AreaSections(_apiApp, Materials));

        /// <summary>
        /// The piers
        /// </summary>
        private Piers _piers;
        /// <summary>
        /// Gets the piers.
        /// </summary>
        /// <value>The piers.</value>
        public Piers Piers => _piers ?? (_piers = new Piers(_apiApp));

        /// <summary>
        /// The spandrels
        /// </summary>
        private Spandrels _spandrels;
        /// <summary>
        /// Gets the spandrels.
        /// </summary>
        /// <value>The spandrels.</value>
        public Spandrels Spandrels => _spandrels ?? (_spandrels = new Spandrels(_apiApp));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureComponents"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal StructureComponents(ApiCSiApplication app) : base(app)
        {
        }
    }
}
