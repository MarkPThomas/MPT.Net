// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IAddableCoordinate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Methods for adding points by providing a coordinate.
    /// </summary>
    public interface IAddableCoordinate
    {
        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        string AddByCoordinate(Coordinate3DCartesian coordinate,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0);

        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        string AddByCoordinate(Coordinate3DCylindrical coordinate,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0);

        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="coordinate">Coordinate for the point object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        string AddByCoordinate(Coordinate3DSpherical coordinate,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0);
    }
}
#endif