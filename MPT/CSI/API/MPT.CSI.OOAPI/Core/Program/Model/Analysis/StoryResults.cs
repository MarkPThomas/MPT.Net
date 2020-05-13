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

#if BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class StoryResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
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
                    FillJointDrifts();
                }

                return _storyDrifts;
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryResults"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal StoryResults(ApiCSiApplication app) : base(app) { }

        #endregion

        #region Fill

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillJointDrifts();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            _storyDrifts.Clear();
        }

        /// <summary>
        /// Fills the joint drifts.
        /// </summary>
        public void FillJointDrifts()
        {
            _storyDrifts = GetStoryDrifts(_apiAnalysisResults);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the story drifts.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;LabelNameResultsIdentifier, StoryDrifts&gt;&gt;.</returns>
        public static List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> GetStoryDrifts(IResults app)
        {
            app.StoryDrifts(
                out var storyNames,
                out var labels,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var directions,
                out var drifts,
                out var displacementsX,
                out var displacementsY,
                out var displacementsZ);

            List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> jointDrifts = new List<Tuple<LabelNameResultsIdentifier, StoryDrifts>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                LabelNameResultsIdentifier identifier =
                    new LabelNameResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        StoryName = storyNames[i],
                        Label = labels[i]
                    };

                StoryDrifts storyDrifts = new StoryDrifts
                {
                    Direction = directions[i],
                    Drift = drifts[i],
                    DisplacementX = displacementsX[i],
                    DisplacementY = displacementsY[i],
                    DisplacementZ = displacementsZ[i]
                };

                jointDrifts.Add(new Tuple<LabelNameResultsIdentifier, StoryDrifts>(identifier, storyDrifts));
            }

            return jointDrifts;
        }
        #endregion
    }
}
#endif