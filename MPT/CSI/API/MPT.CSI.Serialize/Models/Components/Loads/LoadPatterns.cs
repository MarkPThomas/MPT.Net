// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadPatterns.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class LoadPatterns.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class LoadPatterns : ObjectLists<LoadPattern>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPatterns"/> class.
        /// </summary>
        internal LoadPatterns()
        {
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a new load pattern.
        /// </summary>
        /// <param name="uniqueName">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        public bool Add(string uniqueName,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0)
        {
            if (Contains(uniqueName)) return false;

            LoadPattern item = LoadPattern.Factory(uniqueName);
            item.Type = loadPatternType;
            item.SelfWeightMultiplier = selfWeightMultiplier;
            _items.Add(item);

            return true;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override LoadPattern fillNewItem(string uniqueName)
        {
            return LoadPattern.Factory(uniqueName);
        }
        #endregion
    }
}
