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
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Analysis
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
                }

                return _results;
            }
        }
        #endregion

        #region Fill
        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Results?.Clear();
        }
        #endregion
    }
}
