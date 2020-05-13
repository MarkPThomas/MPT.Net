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
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    /// <summary>
    /// Class AreaSections.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class AreaSections : ObjectLists<AreaSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;

        /// <summary>
        /// Gets or sets the type of the area section type to be used for generic fill operations.
        /// </summary>
        /// <value>The type of the area section.</value>
        public static eAreaSectionType AreaSectionType { get; set; }
        #endregion
        
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaSections" /> class.
        /// </summary>
        /// <param name="materials">The materials.</param>
        internal AreaSections(Materials.Materials materials) 
        {
            _materials = materials; 
        }
        #endregion
        
        #region Add/Remove
        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddShell(string uniqueName, ShellProperties properties)
        {
            return add(uniqueName, properties, Shell.Factory);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddPlane(string uniqueName, PlaneProperties properties)
        {
            return add(uniqueName, properties, Plane.Factory);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddASolid(string uniqueName, ASolidProperties properties)
        {
            return add(uniqueName, properties, ASolid.Factory);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddDeck(string uniqueName, DeckProperties properties)
        {
            return add(uniqueName, properties, Deck.Factory);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddSlab(string uniqueName, SlabProperties properties)
        {
            return add(uniqueName, properties, Slab.Factory);
        }

        /// <summary>
        /// Adds an area section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique area.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddWall(string uniqueName, WallProperties properties)
        {
            return add(uniqueName, properties, Wall.Factory);
        }

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
            Func<Materials.Materials, string, T, AreaSection> factory) where T: AreaSectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(factory(_materials, uniqueName, properties));
            return true;
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
            return AreaSection.Factory(_materials, uniqueName, AreaSectionType); 
        }
        #endregion
    }
}
