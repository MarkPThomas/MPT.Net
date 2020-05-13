// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="LoadPattern.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Represents a load pattern.
    /// </summary>
    /// <seealso cref="UniqueName" />
    public class LoadPattern : UniqueName
    {
        #region Fields & Properties
        
        /// <summary>
        /// Load Pattern type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadPatternType Type { get; internal set; }
        
        /// <summary>
        /// Gets or sets the self weight multiplier.
        /// </summary>
        /// <value>The self weight multiplier.</value>
        public double SelfWeightMultiplier { get; internal set; }
        
        /// <summary>
        /// Gets or sets the automatic load pattern.
        /// </summary>
        /// <value>The automatic load pattern.</value>
        public AutoLoadPattern AutoLoadPattern { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPattern" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected LoadPattern(string name) : base(name) { }
        
        /// <summary>
        /// Returns a new load pattern class.
        /// </summary>
        /// <param name="uniqueName">Unique load pattern name.</param>
        /// <returns>Steel.</returns>
        internal static LoadPattern Factory(string uniqueName)
        {
            LoadPattern loadPattern = new LoadPattern(uniqueName);

            return loadPattern;
        }
        #endregion
    }
}
