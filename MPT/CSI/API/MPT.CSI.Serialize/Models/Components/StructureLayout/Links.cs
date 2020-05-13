// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Links.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Links.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class Links : ObjectLists<Link>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Links"/> class.
        /// </summary>
        internal Links() { }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Link fillNewItem(string uniqueName)
        {
            return Link.Factory(uniqueName);
        }
        #endregion
    }
}
