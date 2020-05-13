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
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Base class for all analysis results.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public abstract class AnalysisResults : CSiOoApiBaseBase
    {
        /// <summary>
        /// Gets the analysis results API object.
        /// </summary>
        /// <value>The analysis results.</value>
        protected Results _apiAnalysisResults => _apiApp?.Model?.Results?.Results;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResults"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        protected AnalysisResults(ApiCSiApplication app) : base(app) { }

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
