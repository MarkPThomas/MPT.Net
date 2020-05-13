// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-17-2018
// ***********************************************************************
// <copyright file="StructureLoading.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model
{
    /// <summary>
    /// Class StructureLoading.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public sealed class StructureLoading : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        private readonly Analyzer _analyzer;

        /// <summary>
        /// The patterns
        /// </summary>
        private LoadPatterns _patterns;

        /// <summary>
        /// Gets the patterns.
        /// </summary>
        /// <value>The patterns.</value>
        public LoadPatterns Patterns => _patterns ?? (_patterns = new LoadPatterns(_apiApp));


        /// <summary>
        /// The cases
        /// </summary>
        private LoadCases _cases;

        /// <summary>
        /// Gets the cases.
        /// </summary>
        /// <value>The cases.</value>
        public LoadCases Cases => _cases ?? 
                                  (_cases = new LoadCases(_apiApp, _analyzer, Patterns));


        /// <summary>
        /// The combinations
        /// </summary>
        private LoadCombinations _combinations;

        /// <summary>
        /// Gets the combinations.
        /// </summary>
        /// <value>The combinations.</value>
        public LoadCombinations Combinations => _combinations ?? 
                                                (_combinations = new LoadCombinations(_apiApp, _analyzer, Cases));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureLoading" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        internal StructureLoading(ApiCSiApplication app, Analyzer analyzer) : base(app)
        {
            _analyzer = analyzer;
        }
    }
}
