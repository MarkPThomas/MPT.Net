// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="AnalysisResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Base class for all analysis results.
    /// </summary>
    public abstract class AnalysisResults
    {
        /// <summary>
        /// Gets the analysis results API object.
        /// </summary>
        /// <value>The analysis results.</value>
        protected static Results _analysisResults => Registry.AnalysisResults.Results;



        /// <summary>
        /// Fills the results.
        /// </summary>
        public abstract void FillResults();

        /// <summary>
        /// Empties the results.
        /// </summary>
        public abstract void EmptyResults();
    }
}
