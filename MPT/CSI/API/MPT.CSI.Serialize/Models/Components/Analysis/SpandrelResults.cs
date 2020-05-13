// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SpandrelResults.cs" company="">
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
    /// Class SpandrelResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class SpandrelResults : AnalysisResults
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
        /// Initializes a new instance of the <see cref="SpandrelResults" /> class.
        /// </summary>
        internal SpandrelResults()  { }
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