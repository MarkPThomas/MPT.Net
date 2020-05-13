// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Piers.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Piers.
    /// </summary>
    /// <seealso cref="ObjectLists{Pier}" />
    public class Piers : ObjectLists<Pier>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public PierResults Results { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="API.Core.Program.ModelBehavior.Definition.LoadCombinations" /> class.
        /// </summary>
        internal Piers()
        {
            Results = new PierResults();
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Pier fillNewItem(string uniqueName)
        {
            return Pier.Factory(Results, uniqueName);
        }
        #endregion
    }
}