// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Stories.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Stories.
    /// </summary>
    /// <seealso cref="ObjectLists{Story}" />
    public class Stories : ObjectLists<Story>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public StoryResults Results { get; protected set; }

        /// <summary>
        /// Gets or sets the base elevation.
        /// </summary>
        /// <value>The base elevation.</value>
        public double BaseElevation { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Stories" /> class.
        /// </summary>
        internal Stories() 
        {
            Results = new StoryResults();
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Story fillNewItem(string uniqueName)
        {
            return Story.Factory(Results, uniqueName);
        }
        #endregion
    }
}