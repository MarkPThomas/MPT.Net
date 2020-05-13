// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-28-2019
// ***********************************************************************
// <copyright file="StructureComponents.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Rebar;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureComponents.
    /// </summary>
    public class StructureComponents 
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
        public Materials Materials => _materials ?? (_materials = new Materials());

        /// <summary>
        /// The frame sections
        /// </summary>
        private FrameSections _frameSections;
        /// <summary>
        /// Gets the frame sections.
        /// </summary>
        /// <value>The frame sections.</value>
        public FrameSections FrameSections => _frameSections ?? (_frameSections = new FrameSections(Materials));

        /// <summary>
        /// The area sections
        /// </summary>
        private AreaSections _areaSections;
        /// <summary>
        /// Gets the area sections.
        /// </summary>
        /// <value>The area sections.</value>
        public AreaSections AreaSections => _areaSections ?? (_areaSections = new AreaSections(Materials));


        /// <summary>
        /// The solid properties
        /// </summary>
        private SolidProperties _solidProperties;
        /// <summary>
        /// Gets the solid properties.
        /// </summary>
        /// <value>The solid properties.</value>
        public SolidProperties SolidProperties => _solidProperties ?? (_solidProperties = new SolidProperties(Materials));

        /// <summary>
        /// The cable properties
        /// </summary>
        private CableProperties _cableProperties;
        /// <summary>
        /// Gets the cable properties.
        /// </summary>
        /// <value>The cable properties.</value>
        public CableProperties CableProperties => _cableProperties ?? (_cableProperties = new CableProperties());

        /// <summary>
        /// The link properties
        /// </summary>
        private LinkProperties _linkProperties;
        /// <summary>
        /// Gets the link properties.
        /// </summary>
        /// <value>The link properties.</value>
        public LinkProperties LinkProperties => _linkProperties ?? (_linkProperties = new LinkProperties());

        /// <summary>
        /// The tendon properties
        /// </summary>
        private TendonProperties _tendonProperties;
        /// <summary>
        /// Gets the tendon properties.
        /// </summary>
        /// <value>The tendon properties.</value>
        public TendonProperties TendonProperties => _tendonProperties ?? (_tendonProperties = new TendonProperties());

        /// <summary>
        /// The area sections
        /// </summary>
        private RebarSizes _rebarSizes;
        /// <summary>
        /// Gets the area sections.
        /// </summary>
        /// <value>The area sections.</value>
        public RebarSizes RebarSizes => _rebarSizes ?? (_rebarSizes = new RebarSizes());

        /// <summary>
        /// The piers
        /// </summary>
        private Piers _piers;
        /// <summary>
        /// Gets the piers.
        /// </summary>
        /// <value>The piers.</value>
        public Piers Piers => _piers ?? (_piers = new Piers());

        /// <summary>
        /// The spandrels
        /// </summary>
        private Spandrels _spandrels;
        /// <summary>
        /// Gets the spandrels.
        /// </summary>
        /// <value>The spandrels.</value>
        public Spandrels Spandrels => _spandrels ?? (_spandrels = new Spandrels());
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureComponents" /> class.
        /// </summary>
        internal StructureComponents() 
        {
        }
    }
}
