// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="Areas.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Areas.
    /// </summary>
    /// <seealso cref="ObjectLists2D{T,TU}" />
    /// <seealso cref="ObjectLists{T}" />
    public class Areas : ObjectLists2D<Area, AreaSection>
    {
        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="Areas" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        internal Areas(StructureComponentsProperties<AreaSection> componentsProperties) : base(componentsProperties)
        {
        }
        #endregion
        
        #region Add/Remove
        /// <summary>
        /// Adds a new object whose corner points are at the specified coordinates.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// At least three coordinates are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(List<Coordinate3DCartesian> coordinates,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Area.AddByCoordinate(
                ComponentsProperties, 
                coordinates, 
                propertyName, 
                uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="points">The point objects that define the corner points of the added object.
        /// At least three points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(List<Point> points,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Area.AddByPoint(
                ComponentsProperties, 
                points, 
                propertyName, 
                uniqueName));
            return true;
        }
        #endregion


        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Area fillNewItem(string uniqueName)
        {
            return Area.Factory(ComponentsProperties, uniqueName);
        }
        #endregion
    }
}
