// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Materials.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class Materials.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class Materials : ObjectLists<Material>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Materials"/> class.
        /// </summary>
        internal Materials() { }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Material fillNewItem(string uniqueName)
        {
            return Material.Factory(uniqueName);
        }
        #endregion
    }
}
