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

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Components.Loads;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureLoading.
    /// </summary>
    public sealed class StructureLoading
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        private readonly Analyzer _analyzer;

        /// <summary>
        /// The functions
        /// </summary>
        private Functions _functions;

        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <value>The functions.</value>
        public Functions Functions => _functions ?? (_functions = new Functions());

        /// <summary>
        /// The patterns
        /// </summary>
        private LoadPatterns _patterns;

        /// <summary>
        /// Gets the patterns.
        /// </summary>
        /// <value>The patterns.</value>
        public LoadPatterns Patterns => _patterns ?? (_patterns = new LoadPatterns());


        /// <summary>
        /// The cases
        /// </summary>
        private LoadCases _cases;

        /// <summary>
        /// Gets the cases.
        /// </summary>
        /// <value>The cases.</value>
        public LoadCases Cases => _cases ?? 
                                  (_cases = new LoadCases(_analyzer, Patterns));


        /// <summary>
        /// The combinations
        /// </summary>
        private LoadCombinations _combinations;

        /// <summary>
        /// Gets the combinations.
        /// </summary>
        /// <value>The combinations.</value>
        public LoadCombinations Combinations => _combinations ?? 
                                                (_combinations = new LoadCombinations(_analyzer, Cases));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureLoading" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        internal StructureLoading(Analyzer analyzer)
        {
            _analyzer = analyzer;
        }
    }
}
