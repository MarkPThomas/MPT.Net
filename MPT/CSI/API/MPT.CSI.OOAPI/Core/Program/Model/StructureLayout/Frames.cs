﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Frames.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.FrameObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Frames.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists2D{Frame, FrameSection}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Frame}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Frames : ObjectLists2D<Frame, FrameSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The frame object.</value>
        private ApiFrameObject _apiFrameObject => getApiFrameObject(_apiApp);
        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="Frames" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        internal Frames(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties) : base(app, componentsProperties)
        {
        }
        #endregion

        #region Fill      
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Frame fillNewItem(string uniqueName)
        {
            return Frame.Factory(_apiApp, ComponentsProperties, uniqueName);
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
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// Two coordinates are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(Tuple<Coordinate3DCartesian, Coordinate3DCartesian> coordinates,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Frame.AddByCoordinate(
                _apiApp, 
                ComponentsProperties, 
                coordinates, 
                propertyName, 
                uniqueName, 
                coordinateSystem));
            return true;
        }

        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="points">The point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(Tuple<Point,Point> points,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Frame.AddByPoint(
                _apiApp, 
                ComponentsProperties, 
                points, 
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
        /// <param name="pointNames">The point names that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool Add(Tuple<string, string> pointNames,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Frame.AddByPoint(
                _apiApp, 
                ComponentsProperties, 
                pointNames, 
                propertyName, 
                uniqueName));
            return true;
        }
        #endregion

        #region Query
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        public List<UniqueLabelNamePair> GetLabelNameList()
        {
            return Frame.GetLabelNameList(_apiFrameObject);
        }
#endif

        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return Frame.GetNameList(_apiFrameObject);
        }
        #endregion
    }
}
