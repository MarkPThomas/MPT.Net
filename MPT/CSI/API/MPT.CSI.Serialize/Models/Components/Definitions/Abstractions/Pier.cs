// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="Pier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Pier.
    /// </summary>
    /// <seealso cref="PierSpandrelBase" />
    public class Pier : PierSpandrelBase
    {
        #region Fields & Properties

        /// <summary>
        /// The results
        /// </summary>
        private readonly PierResults _results;
        
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces { get; internal set; }

        /// <summary>
        /// Gets or sets the piers by story.
        /// </summary>
        /// <value>The piers by story.</value>
        public List<PierProperties> PiersByStory { get; protected set; } = new List<PierProperties>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="pierResults">The pier results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Pier Factory(
            PierResults pierResults, 
            string uniqueName)
        {
            Pier item = new Pier(pierResults, uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pier" /> class.
        /// </summary>
        /// <param name="pierResults">The pier results.</param>
        /// <param name="name">The name.</param>
        protected Pier(
            PierResults pierResults,
            string name) : base(name)
        {
            _results = pierResults;
        }
        #endregion
    }
}