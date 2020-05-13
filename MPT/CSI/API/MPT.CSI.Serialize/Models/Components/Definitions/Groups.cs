// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Groups.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions
{
    /// <summary>
    /// Class Groups.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class Groups : ObjectLists<Group>
    {
        #region Fields & Properties
        /// <summary>
        /// The objects
        /// </summary>
        private readonly StructureObjects _objects;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Groups" /> class.
        /// </summary>
        /// <param name="objects">The objects.</param>
        internal Groups(StructureObjects objects) 
        {
            _objects = objects;
        }
        #endregion

        #region Add/Remove        
        /// <summary>
        /// Adds a group to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(string uniqueName,
            GroupProperties properties)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Group.Factory(_objects, uniqueName, properties));
            return true;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Group fillNewItem(string uniqueName)
        {
            return Group.Factory(_objects, uniqueName);
        }
        #endregion
    }
}
