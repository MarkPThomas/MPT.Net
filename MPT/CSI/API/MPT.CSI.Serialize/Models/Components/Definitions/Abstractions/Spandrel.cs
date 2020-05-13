// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Spandrel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Spandrel.
    /// </summary>
    /// <seealso cref="PierSpandrelBase" />
    public class Spandrel : PierSpandrelBase
    {
        #region Fields & Properties
        /// <summary>
        /// The results
        /// </summary>
        private readonly SpandrelResults _results;

        /// <summary>
        /// The story drifts
        /// </summary>
        private List<Tuple<PierSpandrelResultsIdentifier, Forces>> _forces;
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces { get; internal set; }

        /// <summary>
        /// True: Spandrel Label spans multiple story levels.
        /// </summary>
        /// <value><c>true</c> if this instance is multi story; otherwise, <c>false</c>.</value>
        public bool IsMultiStory { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Spandrel Factory(
            SpandrelResults spandrelResults, 
            string uniqueName)
        {
            Spandrel item = new Spandrel(spandrelResults, uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spandrel" /> class.
        /// </summary>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="name">The name.</param>
        protected Spandrel(
            SpandrelResults spandrelResults,
            string name) : base(name)
        {
            _results = spandrelResults;
        }
        #endregion
        
        #region CRUD
        /// <summary>
        /// Adds a new Spandrel Label.
        /// </summary>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="name">The name.</param>
        /// <param name="isMultiStory">if set to <c>true</c> [is multi story].</param>
        /// <returns>Spandrel.</returns>
        internal static Spandrel Add( 
            SpandrelResults spandrelResults, 
            string name, 
            bool isMultiStory)
        {
            return Factory(spandrelResults, name);
        }
        #endregion
    }
}