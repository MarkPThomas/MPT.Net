// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="BaseReactionResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Results;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class BaseReactionResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class BaseReactionResults : AnalysisResults
    {
        /// <summary>
        /// Gets or sets the base reaction coordinate.
        /// </summary>
        /// <value>The base reaction coordinate.</value>
        public Coordinate3DCartesian BaseReactionCoordinate { get; protected set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public List<Tuple<StepResultsIdentifier, BaseReactions>> Results { get; protected set; }
        

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            _analysisResults.BaseReactionWithCentroid(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var reactions,
                out var baseReactionCoordinate,
                out var centroidFxCoordinates,
                out var centroidFyCoordinates,
                out var centroidFzCoordinates);

            BaseReactionCoordinate = baseReactionCoordinate;

            Results = new List<Tuple<StepResultsIdentifier, BaseReactions>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                StepResultsIdentifier identifier = new StepResultsIdentifier()
                {
                    LoadCase = loadCases[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                };

                BaseReactions baseReactions = new BaseReactions()
                {
                    Reactions = reactions[i],
                    CentroidFxCoordinates = centroidFxCoordinates[i],
                    CentroidFyCoordinates = centroidFyCoordinates[i],
                    CentroidFzCoordinates = centroidFzCoordinates[i],
                };

                Results.Add(new Tuple<StepResultsIdentifier, BaseReactions>(identifier, baseReactions));
            }
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Results?.Clear();
        }


    }
}
