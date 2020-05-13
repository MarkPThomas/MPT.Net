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

using System.Collections.Generic;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiMaterial = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.MaterialProperties;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class Materials.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Material}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Materials : ObjectLists<Material>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The material properties.</value>
        protected ApiMaterial _apiMaterialProperties => getApiMaterialProperties(_apiApp);
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Materials"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal Materials(ApiCSiApplication app) : base(app) { }
        #endregion

        #region Fill     
        /// <summary>
        /// Gets the item from the list, or fills it from the application if it doesn't yet exist.
        /// This item is further constrained to be a sub-type of the list type.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        public T FillItem<T>(string uniqueName) where T : Material
        {
            return (T)FillItem(uniqueName);
        }

        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Material fillNewItem(string uniqueName)
        {
            return Material.Factory(_apiApp, uniqueName);
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a material to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(MaterialTemplate material)
        {
            if (Contains(material.Name)) return false;
            _items.Add(Material.AddMaterial(_apiApp, material));
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
            return Material.GetNameList(_apiMaterialProperties);
        }
        #endregion
    }
}
