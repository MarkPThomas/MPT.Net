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
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class StoryResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
    public class StoryResults : AnalysisResults
    {
        /// <summary>
        /// Gets or sets the name of the story.
        /// </summary>
        /// <value>The name of the story.</value>
        public string StoryName { get; protected set; }

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> StoryDrifts { get; protected set; }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StoryResults(string name)
        {
            StoryName = name;
        }

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
#if BUILD_ETABS2016 || BUILD_ETABS2017
            FillJointDrifts();
#endif
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
#if BUILD_ETABS2016 || BUILD_ETABS2017
            StoryDrifts?.Clear();
#endif
        }


#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Fills the joint drifts.
        /// </summary>
        public void FillJointDrifts()
        {
            StoryDrifts = new List<Tuple<LabelNameResultsIdentifier, StoryDrifts>>();
            if (Registry.StoryDrifts.Count == 0)
            {
                Registry.StoryDrifts = GetStoryDrifts();
            }

            foreach (var result in Registry.StoryDrifts)
            {
                if (result.Item1.StoryName != StoryName) continue;
                StoryDrifts.Add(new Tuple<LabelNameResultsIdentifier, StoryDrifts>(result.Item1, result.Item2));
            }
        }
#endif


#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the story drifts.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;LabelNameResultsIdentifier, StoryDrifts&gt;&gt;.</returns>
        public static List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> GetStoryDrifts()
        {
            _analysisResults.StoryDrifts(
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
#endif

    }
}
