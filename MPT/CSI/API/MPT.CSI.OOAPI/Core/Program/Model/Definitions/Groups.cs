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
using System.Collections.Generic;
using MPT.CSI.OOAPI.Core.Helpers.Definitions;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiGroups = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Groups;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Class Groups.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Group}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Groups : ObjectLists<Group>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The groups.</value>
        private ApiGroups _apiGroups => getApiGroups(_apiApp);

        /// <summary>
        /// The objects
        /// </summary>
        private readonly StructureObjects _objects;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Groups" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="objects">The objects.</param>
        internal Groups(ApiCSiApplication app, StructureObjects objects) : base(app)
        {
            _objects = objects;
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
            return Group.Factory(_apiApp, _objects, uniqueName);
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
            _items.Add(Group.AddGroup(_apiApp, _objects, uniqueName, properties));
            return true;
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return Group.GetNameList(_apiGroups);
        }
        #endregion
    }
}
