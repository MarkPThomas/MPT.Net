// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Points.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Points.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class Points : ObjectLists<Point>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the object with the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Point this[Coordinate3DCartesian coordinate]
        {
            get
            {
                foreach (var item in _items)
                {
                    if (Math.Abs(item.X - coordinate.X) < Constants.Tolerance &&
                        Math.Abs(item.Y - coordinate.Y) < Constants.Tolerance &&
                        Math.Abs(item.Z - coordinate.Z) < Constants.Tolerance)
                    {
                        return item;
                    }
                }
                throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Add/Remove        
        /// <summary>
        /// Adds a point object to the model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="uniqueName">Unique name of the point.
        /// If not supplied, the program will generate one automatically.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(
            Coordinate3DCartesian coordinate,
            string uniqueName = "")
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Point.AddByCoordinate(coordinate, uniqueName));
            return true;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Point fillNewItem(string uniqueName)
        {
            return Point.Factory(uniqueName);
        }
        #endregion
    }
}
