// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="AreaSections.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections
{
    /// <summary>
    /// Class AreaSections.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{AreaSection}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class AreaSections : ObjectLists<AreaSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSections" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="materials">The materials.</param>
        internal AreaSections(ApiCSiApplication app, Materials.Materials materials) : base(app)
        {
            _materials = materials; 
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override AreaSection fillNewItem(string uniqueName)
        {
            return AreaSection.Factory(_apiApp, _materials, uniqueName);
        }
        #endregion

        #region Add/Remove
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddShell(string uniqueName, ShellProperties properties)
        {
            return add(uniqueName, properties, Shell.Add);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddPlane(string uniqueName, PlaneProperties properties)
        {
            return add(uniqueName, properties, Plane.Add);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddASolid(string uniqueName, ASolidProperties properties)
        {
            return add(uniqueName, properties, ASolid.Add);
        }
#else

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddDeck(string uniqueName, DeckProperties properties)
        {
            return add(uniqueName, properties, Deck.Add);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddSlab(string uniqueName, SlabProperties properties)
        {
            return add(uniqueName, properties, Slab.Add);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddWall(string uniqueName, WallProperties properties)
        {
            return add(uniqueName, properties, Wall.Add);
        }
#endif

        /// <summary>
        /// Adds a new frame section to the pplication.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="factory">The factory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add<T>(
            string uniqueName,
            T properties,
            Func<ApiCSiApplication, Materials.Materials, string, T, AreaSection> factory) where T: AreaSectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(factory(_apiApp, _materials, uniqueName, properties));
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
            return AreaSection.GetNameList(_apiAreaSection);
        }
        #endregion
    }
}
