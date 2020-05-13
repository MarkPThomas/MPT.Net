// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Story.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Story.
    /// </summary>
    /// <seealso cref="IUniqueName" />
    public class Story : 
        IUniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The results
        /// </summary>
        private readonly StoryResults _results;

        /// <summary>
        /// The story drifts
        /// </summary>
        private List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> _storyDrifts;
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> StoryDrifts
        {
            get
            {
                if (_storyDrifts == null)
                {
                   
                }

                return _storyDrifts;
            }
        }

        /// <summary>
        /// The story name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// The GUID (Global Unique ID)
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; protected set; }

        /// <summary>
        /// The elevation of the story. [L]
        /// </summary>
        /// <value>The elevation.</value>
        public double Elevation { get; protected set; }

        /// <summary>
        /// The height of the story. [L]
        /// </summary>
        /// <value>The height.</value>
        public double Height { get; protected set; }

        /// <summary>
        /// True: Story is a master story. Other stories will be influenced by this story.
        /// </summary>
        /// <value><c>true</c> if this instance is master story; otherwise, <c>false</c>.</value>
        public bool IsMasterStory { get; protected set; }

        /// <summary>
        /// If <see cref="IsMasterStory" /> = False, then this is the master story that this story is similar to.
        /// </summary>
        /// <value>The similar to story.</value>
        public string SimilarToStory { get; protected set; }

        /// <summary>
        /// True: The story has a splice height.
        /// </summary>
        /// <value><c>true</c> if [splice above]; otherwise, <c>false</c>.</value>
        public bool SpliceAbove { get; protected set; }

        /// <summary>
        /// The story splice height. [L]
        /// </summary>
        /// <value>The height of the splice.</value>
        public double SpliceHeight { get; protected set; }

        /// <summary>
        /// The display color for the story specified as an Integer.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; protected set; }
        #endregion

        #region Initialization 
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="storyName">Name of the unique.</param>
        /// <returns>Diaphragm.</returns>
        internal static Story Factory(StoryResults results, string storyName)
        {
            Story item = new Story(results, storyName);

            return item;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Story" /> class.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="name">The name.</param>
        protected Story(StoryResults results, string name) 
        {
            _results = results;
            Name = name;
        }
        #endregion
    }
}