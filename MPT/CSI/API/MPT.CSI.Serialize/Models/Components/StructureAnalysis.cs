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

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Definitions;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureAnalysis.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class StructureAnalysis
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
        public Analyzer Analyzer => _analyzer ?? (_analyzer = new Analyzer());


        /// <summary>
        /// The section cuts
        /// </summary>
        private SectionCuts _sectionCuts;
        /// <summary>
        /// Gets the section cuts.
        /// </summary>
        /// <value>The section cuts.</value>
        public SectionCuts SectionCuts => _sectionCuts ?? (_sectionCuts = new SectionCuts());


        /// <summary>
        /// The base reaction results
        /// </summary>
        private BaseReactionResults _baseReactionResults;
        /// <summary>
        /// Gets the base reaction results.
        /// </summary>
        /// <value>The base reaction results.</value>
        public BaseReactionResults BaseReactionResults =>
            _baseReactionResults ?? (_baseReactionResults = new BaseReactionResults());



        /// <summary>
        /// The misc results
        /// </summary>
        private MiscResults _miscResults;
        /// <summary>
        /// Gets the misc results.
        /// </summary>
        /// <value>The misc results.</value>
        public MiscResults MiscResults => _miscResults ?? (_miscResults = new MiscResults());
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureAnalysis"/> class.
        /// </summary>
        internal StructureAnalysis()
        {
        }
    }
}
