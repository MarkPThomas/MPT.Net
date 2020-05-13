// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="PierResults.cs" company="">
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
    /// Class PierResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class PierResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the forces.
        /// </summary>
        /// <value>The forces.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces { get; protected set; }
        #endregion


        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="PierResults" /> class.
        /// </summary>
        internal PierResults() { }
        #endregion

        #region Fill
        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Forces?.Clear();
        }
        #endregion
    }
}