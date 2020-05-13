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
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class BaseReactionResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class BaseReactionResults : AnalysisResults
    {
        #region Fields & Properties

        /// <summary>
        /// The base reaction coordinate
        /// </summary>
        private Coordinate3DCartesian? _baseReactionCoordinate;
        /// <summary>
        /// Gets or sets the base reaction coordinate.
        /// </summary>
        /// <value>The base reaction coordinate.</value>
        public Coordinate3DCartesian BaseReactionCoordinate
        {
            get
            {
                if (_baseReactionCoordinate == null)
                {
                    FillResults();
                }

                return (Coordinate3DCartesian)_baseReactionCoordinate;
            }
        }

        /// <summary>
        /// The results
        /// </summary>
        private List<Tuple<StepResultsIdentifier, BaseReactions>> _results;
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public List<Tuple<StepResultsIdentifier, BaseReactions>> Results
        {
            get
            {
                if (_results == null)
                {
                    FillResults();
                }

                return _results;
            }
        }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReactionResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        public BaseReactionResults(ApiCSiApplication app) : base(app) { }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            _results = BaseReactionWithCentroid(_apiAnalysisResults, out var baseReactionCoordinate);      
            _baseReactionCoordinate = baseReactionCoordinate;
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Results?.Clear();
        }
        #endregion

        #region Static
        /// <summary>
        /// Fills the results.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="baseReactionCoordinate">The base reaction coordinate.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, BaseReactions&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, BaseReactions>> BaseReactionWithCentroid(
            IResults app,
            out Coordinate3DCartesian baseReactionCoordinate)
        {
            app.BaseReactionWithCentroid(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var reactions,
                out baseReactionCoordinate,
                out var centroidFxCoordinates,
                out var centroidFyCoordinates,
                out var centroidFzCoordinates);

            var results = new List<Tuple<StepResultsIdentifier, BaseReactions>>();
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

                results.Add(new Tuple<StepResultsIdentifier, BaseReactions>(identifier, baseReactions));
            }

            return results;
        }
        #endregion
    }
}
