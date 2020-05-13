// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="StructureGroupings.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureGroupings.
    /// </summary>
    public class StructureGroupings
    {
        #region Fields & Properties

        /// <summary>
        /// The objects
        /// </summary>
        private readonly StructureObjects _objects;

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


        /// <summary>
        /// The diaphragms
        /// </summary>
        private Diaphragms _diaphragms;

        /// <summary>
        /// Gets the diaphragms.
        /// </summary>
        /// <value>The diaphragms.</value>
        public Diaphragms Diaphragms => _diaphragms ?? (_diaphragms = new Diaphragms());


        /// <summary>
        /// The stories
        /// </summary>
        private Stories _stories;

        /// <summary>
        /// Gets the stories.
        /// </summary>
        /// <value>The stories.</value>
        public Stories Stories => _stories ?? (_stories = new Stories());


        /// <summary>
        /// The groups
        /// </summary>
        private Groups _groups;

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public Groups Groups => _groups ?? (_groups = new Groups(_objects));


        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureGroupings" /> class.
        /// </summary>
        /// <param name="objects">The objects.</param>
        internal StructureGroupings(StructureObjects objects)
        {
            _objects = objects;
        }
        #endregion
    }
}
