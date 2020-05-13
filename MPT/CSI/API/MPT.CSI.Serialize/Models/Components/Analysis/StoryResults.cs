// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="StoryResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class StoryResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class StoryResults : AnalysisResults
    {
        #region Fields & Properties

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
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryResults"/> class.
        /// </summary>
        internal StoryResults() { }

        #endregion

        #region Fill
        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            _storyDrifts.Clear();
        }
        #endregion
    }
}