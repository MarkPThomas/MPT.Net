// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-24-2018
// ***********************************************************************
// <copyright file="StructureAnalysis.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model
{
    /// <summary>
    /// Class StructureAnalysis.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class StructureAnalysis : CSiOoApiBaseBase
    {
        #region Fields & Properties 
        /// <summary>
        /// The analyzer
        /// </summary>
        private Analyzer _analyzer;
        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        public Analyzer Analyzer => _analyzer ?? (_analyzer = new Analyzer(_apiApp));


        /// <summary>
        /// The section cuts
        /// </summary>
        private SectionCuts _sectionCuts;
        /// <summary>
        /// Gets the section cuts.
        /// </summary>
        /// <value>The section cuts.</value>
        public SectionCuts SectionCuts => _sectionCuts ?? (_sectionCuts = new SectionCuts(_apiApp));


        /// <summary>
        /// The base reaction results
        /// </summary>
        private BaseReactionResults _baseReactionResults;
        /// <summary>
        /// Gets the base reaction results.
        /// </summary>
        /// <value>The base reaction results.</value>
        public BaseReactionResults BaseReactionResults =>
            _baseReactionResults ?? (_baseReactionResults = new BaseReactionResults(_apiApp));



        /// <summary>
        /// The misc results
        /// </summary>
        private MiscResults _miscResults;
        /// <summary>
        /// Gets the misc results.
        /// </summary>
        /// <value>The misc results.</value>
        public MiscResults MiscResults => _miscResults ?? (_miscResults = new MiscResults(_apiApp));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureAnalysis"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal StructureAnalysis(API.Core.Program.CSiApplication app) : base(app)
        {
        }
    }
}
