// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Area.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.AreaObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Area.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject2D{AreaSection}" />
    public class Area : StructureObject2D<AreaSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application area object.
        /// </summary>
        /// <value>The API area object.</value>
        protected ApiAreaObject _apiAreaObject => getApiAreaObject(_apiApp);

        /// <summary>
        /// The analysis results
        /// </summary>
        private AreaResults _analysisResults;
        /// <summary>
        /// Gets or sets the analysis results.
        /// </summary>
        /// <value>The results.</value>
        public AreaResults AnalysisResults => _analysisResults ?? (_analysisResults = new AreaResults(_apiApp, Name));

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        //private ShearWallDesignResults _shearWallDesignResults;
        //private SlabDesignResults _slabDesignResults;

        //public ShearWallDesignResults ShearWallDesignResults => _shearWallDesignResults ?? (_shearWallDesignResults = new ShearWallDesignResults(Name));

        //public SlabDesignResults ShearWallDesignResults => _slabDesignResults ?? (_slabDesignResults = new SlabDesignResults(Name));

        /// <summary>
        /// The is opening
        /// </summary>
        private bool? _isOpening;
        /// <summary>
        /// True: Specified area object is an opening.
        /// </summary>
        /// <value><c>true</c> if this instance is opening; otherwise, <c>false</c>.</value>
        public bool IsOpening
        {
            get
            {
                if (_isOpening == null)
                {
                    FillOpening();
                }

                return _isOpening ?? false;
            }
        }

        /// <summary>
        /// The design orientation
        /// </summary>
        private eAreaDesignOrientation _designOrientation;
        /// <summary>
        /// Gets or sets the design orientation.
        /// </summary>
        /// <value>The design orientation.</value>
        public eAreaDesignOrientation DesignOrientation
        {
            get
            {
                if (_designOrientation == 0)
                {
                    FillEdgeConstraint();
                }

                return _designOrientation;
            }
        }
#endif

        /// <summary>
        /// The uniform loads
        /// </summary>
        private List<AreaLoadUniform> _uniformLoads;
        /// <summary>
        /// Gets or sets the uniform loads.
        /// </summary>
        /// <value>The uniform loads.</value>
        public List<AreaLoadUniform> UniformLoads
        {
            get
            {
                if (_uniformLoads == null)
                {
                    FillLoadUniform();
                }

                return _uniformLoads;
            }
        }

        /// <summary>
        /// The uniform to frame loads
        /// </summary>
        private List<AreaLoadUniformToFrame> _uniformToFrameLoads;
        /// <summary>
        /// Gets or sets the uniform to frame loads.
        /// </summary>
        /// <value>The uniform to frame loads.</value>
        public List<AreaLoadUniformToFrame> UniformToFrameLoads
        {
            get
            {
                if (_uniformToFrameLoads == null)
                {
                    FillLoadUniformToFrame();
                }

                return _uniformToFrameLoads;
            }
        }

        /// <summary>
        /// The wind pressure loads
        /// </summary>
        private List<AreaLoadWindPressure> _windPressureLoads;
        /// <summary>
        /// Gets or sets the wind pressure loads.
        /// </summary>
        /// <value>The wind pressure loads.</value>
        public List<AreaLoadWindPressure> WindPressureLoads
        {
            get
            {
                if (_windPressureLoads == null)
                {
                    FillLoadWindPressure();
                }

                return _windPressureLoads;
            }
        }

        /// <summary>
        /// The area modifier
        /// </summary>
        private AreaModifier _areaModifier;
        /// <summary>
        /// Gets or sets the area modifier.
        /// </summary>
        /// <value>The area modifier.</value>
        public AreaModifier AreaModifier
        {
            get
            {
                if (_areaModifier == null)
                {
                    FillModifiers();
                }

                return _areaModifier;
            }
        }

        /// <summary>
        /// The has edge constraints
        /// </summary>
        private bool? _hasEdgeConstraints;
        /// <summary>
        /// True: An automatic edge constraint is generated by the program for the object in the analysis model.
        /// </summary>
        /// <value><c>true</c> if this instance has edge constraints; otherwise, <c>false</c>.</value>
        public bool HasEdgeConstraints
        {
            get
            {
                if (_hasEdgeConstraints == null)
                {
                    FillEdgeConstraint();
                }

                return _hasEdgeConstraints?? false;
            }
        }

        /// <summary>
        /// The selected edge booleans
        /// </summary>
        private List<bool> _selectedEdgeBooleans;
        /// <summary>
        /// Selected status for area object edges.
        /// True: The specified area object edge is selected;
        /// Selected(0) = Selected status for edge 1;
        /// Selected(1) = Selected status for edge 2;
        /// Selected(n) = Selected status for edge(n + 1)
        /// </summary>
        /// <value>The selected edge booleans.</value>
        public List<bool> SelectedEdgeBooleans
        {
            get
            {
                if (_selectedEdgeBooleans == null)
                {
                    FillSelectedEdge();
                }

                return _selectedEdgeBooleans;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Area.</returns>
        internal static Area Factory(ApiCSiApplication app,
            StructureComponentsProperties<AreaSection> componentsProperties, 
            string uniqueName)
        {
            Area item = new Area(app, componentsProperties, uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Area" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Area(ApiCSiApplication app,
            StructureComponentsProperties<AreaSection> componentsProperties, 
            string name) : base(app, componentsProperties, name) { }
        #endregion

        #region Static
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="areaObject">The area object.</param>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        internal static List<UniqueLabelNamePair> GetLabelNameList(ApiAreaObject areaObject)
        {
            return getLabelNameList(areaObject);
        }
#endif

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <param name="areaObject">The area object.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static List<string> GetNameList(ApiAreaObject areaObject)
        {
            return getNameList(areaObject);
        }
        #endregion

        #region Query
        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void FillPointNames()
        {
            getPoints(_apiAreaObject);
        }

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override List<string> GetPointNames()
        {
            return getPoints(_apiAreaObject);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
            getNameFromLabel(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_apiAreaObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves whether the specified area object is an opening.
        /// </summary>
        public void FillOpening()
        {
            if (_apiAreaObject == null) return;
            _apiAreaObject.GetOpening(Name, out var isOpening);
            _isOpening = isOpening;
        }

        /// <summary>
        /// Designates an area object(s) as an opening.
        /// </summary>
        /// <param name="isOpening">if set to <c>true</c> [is opening].</param>
        public void SetAsOpening(bool isOpening)
        {
            _apiAreaObject?.SetOpening(Name, isOpening);
            _isOpening = isOpening;
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_apiAreaObject);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID(string guid = "")
        {
            setGUID(_apiAreaObject, guid);
        }


        /// <summary>
        /// Retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_apiAreaObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            fillTransformationMatrix(_apiAreaObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override DirectionCosines GetTransformationMatrix(bool isGlobal)
        {
            return getTransformationMatrix(_apiAreaObject, isGlobal);
        }
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_apiAreaObject);
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLocalAxes(AngleLocalAxes angleOffset)
        {
            setLocalAxes(_apiAreaObject, angleOffset);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function gets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
            int csiPlaneVectorOption = 0;
            int[] csiPlaneVectorDirection = new int[0];

            _callCode = _sapModel.AreaObj.GetLocalAxesAdvanced(name, ref isActive, ref plane2, ref csiPlaneVectorOption, ref planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            planeVectorOption = (eReferenceVector)csiPlaneVectorOption;
            planeVectorDirection = csiPlaneVectorDirection.Cast<eReferenceVectorDirection>().ToArray();
        }


        /// <summary>
        /// Sets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
            eItemType itemType = eItemType.Object)
        {
            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planePoint), planePoint.Length);
            arraysLengthMatch(nameof(planeVectorDirection), planeVectorDirection.Length, nameof(planeReferenceVector), planeReferenceVector.Length);

            int[] csiPlaneVectorDirection = planeVectorDirection.Cast<int>().ToArray();

            _callCode = _sapModel.AreaObj.SetLocalAxesAdvanced(name, 
                            isActive, plane2, (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, 
                            ref planePoint, ref planeReferenceVector, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Creation
        /// <summary>
        /// Changes the name of an existing object.
        /// </summary>
        /// <param name="newName">The new name for the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_apiAreaObject, newName);
        }

        /// <summary>
        /// Adds a new object whose corner points are at the specified coordinates.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// At least three coordinates are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default  property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <returns>Area.</returns>
        /// <exception cref="CSiException">At least three coordinates must be provided for an area object. " +
        /// "Provided: " + coordinates.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        internal static Area AddByCoordinate(ApiCSiApplication app,
            StructureComponentsProperties<AreaSection> componentsProperties,
            List<Coordinate3DCartesian> coordinates,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            ApiAreaObject apiAreaObject = getApiAreaObject(app);

            uniqueName = apiAreaObject.AddByCoordinate(
                            coordinates.ToArray(), 
                            propertyName, 
                            uniqueName, 
                            coordinateSystem);
            return Factory(app, componentsProperties, uniqueName);
        }


        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="points">The point objects that define the corner points of the added object.
        /// At least three points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined  property.
        /// If it is Default, the program assigns a default  property to the  object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns>Area.</returns>
        /// <exception cref="CSiException">At least three points must be provided for an area object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        internal static Area AddByPoint(ApiCSiApplication app,
            StructureComponentsProperties<AreaSection> componentsProperties,
            List<Point> points,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            ApiAreaObject apiAreaObject = getApiAreaObject(app);
            string[] pointNames = points.Select(o => o.Name).ToArray();

            uniqueName = apiAreaObject.AddByPoint(
                            pointNames, 
                            propertyName, 
                            uniqueName);
            return Factory(app, componentsProperties, uniqueName);
        }


        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// At least three points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined  property.
        /// If it is Default, the program assigns a default  property to the  object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns>Area.</returns>
        /// <exception cref="CSiException">At least three points must be provided for an area object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        internal static Area AddByPoint(ApiCSiApplication app,
            StructureComponentsProperties<AreaSection> componentsProperties,
            List<string> pointNames,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            ApiAreaObject apiAreaObject = getApiAreaObject(app);

            uniqueName = apiAreaObject.AddByPoint(
                            pointNames.ToArray(), 
                            propertyName, 
                            uniqueName);
            return Factory(app, componentsProperties, uniqueName);
        }

        /// <summary>
        /// The function deletes a specified frame object.
        /// It returns an error if the specified object cannot be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void Delete()
        {
            delete(_apiAreaObject);
        }
        #endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillSelected()
        {
            getSelected(_apiAreaObject);
        }

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Select()
        {
            setSelected(isSelected: true);
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Deselect()
        {
            setSelected(isSelected: false);
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected(bool isSelected)
        {
            setSelected(_apiAreaObject, isSelected);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_apiAreaObject);
        }
#endif

        /// <summary>
        /// Returns the selected status for area object edges.
        /// True: The specified area object edge is selected;
        /// Selected(0) = Selected status for edge 1;
        /// Selected(1) = Selected status for edge 2;
        /// Selected(n) = Selected status for edge(n + 1)
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSelectedEdge()
        {
            _selectedEdgeBooleans = new List<bool>(_apiAreaObject.GetSelectedEdge(Name));
        }

        /// <summary>
        /// Determines whether the provided edge number is selected.
        /// </summary>
        /// <param name="edgeNumber">The edge number.</param>
        /// <returns><c>true</c> if [is edge selected] [the specified edge number]; otherwise, <c>false</c>.</returns>
        public bool IsEdgeSelected(int edgeNumber)
        {
            if (edgeNumber < 1 || SelectedEdgeBooleans.Count < edgeNumber) return false;
            return SelectedEdgeBooleans[edgeNumber - 1];
        }

        /// <summary>
        /// Sets the selected status for area object edges.
        /// </summary>
        /// <param name="edgeNumber">The area object edge that is to have its selected status set.</param>
        /// <param name="isEdgeSelected">True: The specified area object edge is selected.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSelectedEdge(
            int edgeNumber,
            bool isEdgeSelected)
        {
            if (edgeNumber < 1 || SelectedEdgeBooleans.Count < edgeNumber) return;
            _apiAreaObject.SetSelectedEdge(Name, edgeNumber, isEdgeSelected);
            _selectedEdgeBooleans[edgeNumber - 1] = isEdgeSelected;
        }
        #endregion

        #region Modifiers
        /// <summary>
        /// Returns the unitless modifier assignments.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModifiers()
        {
            _areaModifier = _apiAreaObject.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for area objects.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(AreaModifier modifiers)
        {
            _apiAreaObject.SetModifiers(Name, modifiers);
            _areaModifier = modifiers;
        }

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteModifiers()
        {
            deleteModifiers(_apiAreaObject);
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMass()
        {
            getMass(_apiAreaObject);
        }

        /// <summary>
        /// Assigns mass per unit area to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetMass(double value, bool replace)
        {
            setMass(_apiAreaObject, value, replace);
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteMass()
        {
            deleteMass(_apiAreaObject);
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void FillMaterialOverwriteName()
        {
            getMaterialOverwriteName(_apiAreaObject);
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override string GetMaterialOverwriteName()
        {
            return getMaterialOverwriteName(_apiAreaObject);
        }

        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddMaterialOverwrite(Material material)
        {
            setMaterialOverwrite(_apiAreaObject, material);
        }

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void RemoveMaterialOverwrite()
        {
            setMaterialOverwrite(_apiAreaObject, null);
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void FillSectionName()
        {
            getSectionName(_apiAreaObject);
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override string GetSectionName()
        {
            return getSectionName(_apiAreaObject);
        }

        // IChangeableSection
        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(AreaSection section)
        {
            if (section == null) return;
            setSection(_apiAreaObject, section);
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the thickness overwrite assignments for area elements.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="thicknessType">Indicates the thickness overwrite type.</param>
        /// <param name="thicknessPattern">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="thicknessPatternScaleFactor">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="thicknesses">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area element. [L]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetThickness(string name,
            ref eAreaThicknessType thicknessType,
            ref string thicknessPattern,
            ref double thicknessPatternScaleFactor,
            ref double[] thicknesses)
        {
            int csiThicknessType = 0;

            _callCode = _sapModel.AreaObj.GetThickness(name, ref csiThicknessType, ref thicknessPattern, ref thicknessPatternScaleFactor, ref thicknesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            thicknessType = (eAreaThicknessType) csiThicknessType;
        }

        /// <summary>
        /// Assigns the thickness overwrite assignments for area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="thicknessType">Indicates the thickness overwrite type.</param>
        /// <param name="thicknessPattern">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="thicknessPatternScaleFactor">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="thicknesses">This item applies only when <paramref name="thicknessType" /> = <see cref="eAreaThicknessType.OverwriteByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area object. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetThickness(string name,
            eAreaThicknessType thicknessType,
            string thicknessPattern,
            double thicknessPatternScaleFactor,
            double[] thicknesses,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetThickness(name, 
                            (int)thicknessType, thicknessPattern, thicknessPatternScaleFactor, ref thicknesses, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns the method to determine the notional size of an area section for the creep and shrinkage calculations.
        /// This function is currently worked for shell type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, the Value represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, the Value represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, the Value will not be used and can be set to 1.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetNotionalSize(string name,
            ref eNotionalSizeType sizeType,
            ref double value)
        {
            string csiSizeType = "";

            _callCode = _sapModel.PropArea.GetNotionalSize(name, ref csiSizeType, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            sizeType = EnumLibrary.ConvertStringToEnumByDescription<eNotionalSizeType>(csiSizeType);
        }

        /// <summary>
        /// Assigns the method to determine the notional size of an area section for the creep and shrinkage calculations.
        /// This function is currently worked for shell type area section.
        /// </summary>
        /// <param name="name">The name of an existing shell-type area section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, will not be used and can be set to 1.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNotionalSize(string name,
            eNotionalSizeType sizeType,
            double value)
        {
            string csiSizeType = EnumLibrary.GetEnumDescription(sizeType);

            _callCode = _sapModel.PropArea.SetNotionalSize(name, csiSizeType, value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns the material temperature assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="temperature">This is the material temperature value assigned to the object. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line object is uniform along the object at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line object may vary from one end to the other.
        /// The material temperature at each end of the object is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.AreaObj.GetMatTemp(name, ref temperature, ref patternName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the material temperature assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <param name="temperature">This is the material temperature value assigned to the element. [T]</param>
        /// <param name="patternName">This is blank or the name of a defined joint pattern.
        /// If it is blank, the material temperature for the line element is uniform along the element at the value specified by <paramref name="temperature" />.
        /// If <paramref name="patternName"/> is the name of a defined joint pattern, the material temperature for the line element may vary from one end to the other.
        /// The material temperature at each end of the element is equal to the specified temperature multiplied by the pattern value at the joint at the end of the line element.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.AreaObj.SetMatTemp(name, 
                            temperature, patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Area Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the joint offset assignments for area elements.
        /// </summary>
        /// <param name="name">The name of an existing area element.</param>
        /// <param name="offsetType">Indicates the joint offset type.</param>
        /// <param name="offsetPattern">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="offsetPatternScaleFactor">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="offsets">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area element. [L]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetOffsets(string name,
            ref eAreaOffsetType offsetType,
            ref string offsetPattern,
            ref double offsetPatternScaleFactor,
            ref double[] offsets)
        {

        }

        /// <summary>
        /// Assigns the joint offset assignments for area objects.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="offsetType">Indicates the joint offset type.</param>
        /// <param name="offsetPattern">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the name of the defined joint pattern that is used to calculate the thicknesses.</param>
        /// <param name="offsetPatternScaleFactor">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByJointPattern" />.
        /// It is the scale factor applied to the joint pattern when calculating the thicknesses. [L]</param>
        /// <param name="offsets">This item applies only when <paramref name="offsetType" /> = <see cref="eAreaOffsetType.OffsetByPoint" />.
        /// It is an array of thicknesses at each of the points that define the area object. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetOffsets(string name,
            eAreaOffsetType offsetType,
            string offsetPattern,
            double offsetPatternScaleFactor,
            double[] offsets,
            eItemType itemType = eItemType.Object)
        {

        }



        /// <summary>
        /// Returns automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.
        /// Mesh options <see cref="eMeshType.SpecifiedNumber" />, <see cref="eMeshType.SpecifiedMaxSize" /> and <see cref="eMeshType.PointsOnAreaEdges" /> apply to quadrilaterals and triangles only.</param>
        /// <param name="numberOfObjectsAlongPoint12">The number of objects created along the edge of the meshed object that runs from point 1 to point 2.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="numberOfObjectsAlongPoint13">The number of objects created along the edge of the meshed object that runs from point 1 to point 3.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="pointOnEdgeFromLine">True: Points on the area object edges are determined from intersections of straight line objects included in the group specified by the <paramref name="group" /> item with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromPoint">True: Point on the area object edges are determined from point objects included in the group specified by the Group item that lie on the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="extendCookieCutLines">True: All straight line objects included in the group specified by the <paramref name="group" /> item are extended to intersect the area object edges for the purpose of meshing the area object.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutLinesIntersectingEdges" />, which provides cookie cut meshing based on straight line objects included in the group specified by the <paramref name="group" /> item that intersect the area object edges.</param>
        /// <param name="rotation">An angle in degrees that the meshing lines are rotated from their default orientation. [deg]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutPoints" />, which provides cookie cut meshing based on two perpendicular lines passing through point objects included in the group specified by the <paramref name="group" /> item.</param>
        /// <param name="maxSizeGeneral">The maximum size of objects created by the General Divide Tool.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.GeneralDivideTool" />.</param>
        /// <param name="localAxesOnEdge">True: If both points along an edge of the original area object have the same local axes, the program makes the local axes for added points along the edge the same as the edge end points.</param>
        /// <param name="localAxesOnFace">True: If on the area object edges are determined from point objects included in the group specified by the <paramref name="group" /> item that lie on the area object edges.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="group">The name of a defined group.
        /// Some of the meshing options make use of point and line objects included in this group.</param>
        /// <param name="subMesh">True: After initial meshing, the program further meshes any area objects that have an edge longer than the length specified by the <paramref name="subMeshSize" /> = item.</param>
        /// <param name="subMeshSize">The maximum size of area objects to remain when the auto meshing is complete. [L]
        /// This item applies when <paramref name="subMesh" /> = True.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref bool pointOnEdgeFromLine,
            ref bool pointOnEdgeFromPoint,
            ref bool extendCookieCutLines,
            ref double rotation,
            ref double maxSizeGeneral,
            ref bool localAxesOnEdge,
            ref bool localAxesOnFace,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace,
            ref string group,
            ref bool subMesh,
            ref double subMeshSize)
        {

        }

        /// <summary>
        /// Returns automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">The number of objects created along the edge of the meshed object that runs from point 1 to point 2.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="numberOfObjectsAlongPoint13">The number of objects created along the edge of the meshed object that runs from point 1 to point 3.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">The maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.</param>
        /// <param name="pointOnEdgeFromLine">True: Points on the area object edges are determined from intersections of straight line objects included in the group specified by the <paramref name="group" /> item with the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="pointOnEdgeFromPoint">True: Point on the area object edges are determined from point objects included in the group specified by the Group item that lie on the area object edges.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.PointsOnAreaEdges" />.</param>
        /// <param name="extendCookieCutLines">True: All straight line objects included in the group specified by the <paramref name="group" /> item are extended to intersect the area object edges for the purpose of meshing the area object.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutLinesIntersectingEdges" />, which provides cookie cut meshing based on straight line objects included in the group specified by the <paramref name="group" /> item that intersect the area object edges.</param>
        /// <param name="rotation">An angle in degrees that the meshing lines are rotated from their default orientation. [deg]
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.CookieCutPoints" />, which provides cookie cut meshing based on two perpendicular lines passing through point objects included in the group specified by the <paramref name="group" /> item.</param>
        /// <param name="maxSizeGeneral">The maximum size of objects created by the General Divide Tool.
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.
        /// This item applies when <paramref name="meshType" /> = <see cref="eMeshType.GeneralDivideTool" />.</param>
        /// <param name="localAxesOnEdge">True: If both points along an edge of the original area object have the same local axes, the program makes the local axes for added points along the edge the same as the edge end points.</param>
        /// <param name="localAxesOnFace">True: If on the area object edges are determined from point objects included in the group specified by the <paramref name="group" /> item that lie on the area object edges.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="group">The name of a defined group.
        /// Some of the meshing options make use of point and line objects included in this group.</param>
        /// <param name="subMesh">True: After initial meshing, the program further meshes any area objects that have an edge longer than the length specified by the <paramref name="subMeshSize" /> = item.</param>
        /// <param name="subMeshSize">The maximum size of area objects to remain when the auto meshing is complete. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 12 inches if the database units are English or 30 centimeters if the database units are metric.
        /// This item applies when <paramref name="subMesh" /> = True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            bool pointOnEdgeFromLine = false,
            bool pointOnEdgeFromPoint = false,
            bool extendCookieCutLines = false,
            double rotation = 0,
            double maxSizeGeneral = 0,
            bool localAxesOnEdge = false,
            bool localAxesOnFace = false,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            string group = "ALL",
            bool subMesh = false,
            double subMeshSize = 0,
            eItemType itemType = eItemType.Object)
        {

        }
#endif
        #endregion

        #region Support & Connections
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_apiAreaObject);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="springName">Name of the spring.</param>
        public override void SetSpringAssignment(string springName)
        {
            setSpringAssignment(_apiAreaObject, springName);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _apiAreaObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }


        /// <summary>
        /// Returns the generated edge constraint assignments to objects.
        /// True: An automatic edge constraint is generated by the program for the object in the analysis model.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillEdgeConstraint()
        {
            _hasEdgeConstraints = _apiAreaObject.GetEdgeConstraint(Name);
        }

        /// <summary>
        /// Adds the edge constraint.
        /// </summary>
        public void AddEdgeConstraint()
        {
            setEdgeConstraint(constraintExists: true);
        }

        /// <summary>
        /// Removes the edge constraint.
        /// </summary>
        public void RemoveEdgeConstraint()
        {
            setEdgeConstraint(constraintExists: false);
        }

        /// <summary>
        /// Sets generated edge constraint assignments to objects.
        /// </summary>
        /// <param name="constraintExists">True: An automatic edge constraint is generated by the program for the object in the analysis model.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setEdgeConstraint(bool constraintExists)
        {
            _apiAreaObject.SetEdgeConstraint(Name, constraintExists);
            _hasEdgeConstraints = constraintExists;
        }
        #endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        internal override void FillPierName()
        {
            getPierName(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetPierName()
        {
            return getPierName(_apiAreaObject);
        }

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public override void AddToPier(Pier pier)
        {
            addPier(_apiAreaObject, pier);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromPier()
        {
            removePier(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        internal override void FillSpandrelName()
        {
            getSpandrelName(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetSpandrelName()
        {
            return getSpandrelName(_apiAreaObject);
        }

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public override void AddToSpandrel(Spandrel spandrel)
        {
            addSpandrel(_apiAreaObject, spandrel);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromSpandrel()
        {
            removeSpandrel(_apiAreaObject);
        }

        /// <summary>
        /// Retrieves the design orientation of an area object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDesignOrientation()
        {
            _designOrientation = _apiAreaObject.GetDesignOrientation(Name);
        }
#endif
        #endregion

        #region Loading
        // LoadTemperature
        /// <summary>
        /// Returns the temperature load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLoadTemperature()
        {
            getLoadTemperature(_apiAreaObject);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddLoadTemperature(LoadTemperature temperatureLoad)
        {
            setLoadTemperature(_apiAreaObject, temperatureLoad, replace: false);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ReplaceLoadTemperature(LoadTemperature temperatureLoad)
        {
            setLoadTemperature(_apiAreaObject, temperatureLoad, replace: true);
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteLoadTemperature(string loadPattern)
        {
            deleteLoadTemperature(_apiAreaObject, loadPattern);
            deleteLoad(loadPattern, TemperatureLoads);
        }


        // LoadUniform
        /// <summary>
        /// Returns the uniform load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadUniform()
        {
            _apiAreaObject.GetLoadUniform(Name,
                out var names,
                out var loadPatterns,
                out var coordinateSystems,
                out var directionApplied,
                out var uniformLoadValues);

            _uniformLoads = new List<AreaLoadUniform>();
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadUniform areaLoadUniform = new AreaLoadUniform
                {
                    LoadPattern = loadPatterns[i],
                    Direction = directionApplied[i],
                    Value = uniformLoadValues[i],
                    CoordinateSystem = coordinateSystems[i]
                };

                _uniformLoads.Add(areaLoadUniform);
            }
        }

        /// <summary>
        /// Assigns uniform loads to objects.
        /// </summary>
        /// <param name="uniformLoad">The load uniform.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadUniform(AreaLoadUniform uniformLoad)
        {
            setLoadUniform(uniformLoad, replace: false);
            addOrReplace(false, uniformLoad, _uniformLoads);
        }

        /// <summary>
        /// Assigns uniform loads to objects.
        /// </summary>
        /// <param name="uniformLoad">The load uniform.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadUniform(AreaLoadUniform uniformLoad)
        {
            setLoadUniform(uniformLoad, replace: true);
            addOrReplace(true, uniformLoad, _uniformLoads);
        }

        /// <summary>
        /// Assigns uniform loads to objects.
        /// </summary>
        /// <param name="uniformLoad">The load uniform.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadUniform(
            AreaLoadUniform uniformLoad,
            bool replace = true)
        {
            _apiAreaObject?.SetLoadUniform(Name,
                uniformLoad.LoadPattern,
                uniformLoad.Direction,
                uniformLoad.Value,
                uniformLoad.CoordinateSystem,
                replace);
        }

        /// <summary>
        /// Deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadUniform(string loadPattern)
        {
            _apiAreaObject.DeleteLoadUniform(Name, loadPattern);
            deleteLoad(loadPattern, UniformLoads);
        }


        // LoadUniformToFrame
        /// <summary>
        /// Returns the wind pressure load assignments to area objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadUniformToFrame()
        {
            _apiAreaObject.GetLoadUniformToFrame(Name,
                out var loadPatterns,
                out var names,
                out var values,
                out var directions,
                out var distributionTypes,
                out var coordinateSystems);

            _uniformToFrameLoads = new List<AreaLoadUniformToFrame>();
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadUniformToFrame areaLoadUniformToFrame = new AreaLoadUniformToFrame
                {
                    LoadPattern = loadPatterns[i],
                    Direction = directions[i],
                    DistributionType = distributionTypes[i],
                    Value = values[i],
                    CoordinateSystem = coordinateSystems[i]
                };

                _uniformToFrameLoads.Add(areaLoadUniformToFrame);
            }
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="uniformLoad">The uniform load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadUniformToFrame(AreaLoadUniformToFrame uniformLoad)
        {
            setLoadUniformToFrame(uniformLoad, replace: false);
            addOrReplace(false, uniformLoad, _uniformToFrameLoads);
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="uniformLoad">The uniform load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadUniformToFrame(AreaLoadUniformToFrame uniformLoad)
        {
            setLoadUniformToFrame(uniformLoad, replace: true);
            addOrReplace(true, uniformLoad, _uniformToFrameLoads);
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="uniformLoad">The uniform load.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadUniformToFrame(
            AreaLoadUniformToFrame uniformLoad,
            bool replace = true)
        {
            _apiAreaObject?.SetLoadUniformToFrame(Name,
                uniformLoad.LoadPattern,
                uniformLoad.Value,
                uniformLoad.Direction,
                uniformLoad.DistributionType,
                replace: replace,
                coordinateSystem: uniformLoad.CoordinateSystem);
        }

        /// <summary>
        /// Deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadUniformToFrame(string loadPattern)
        {
            if (_apiAreaObject == null) return;
            _apiAreaObject.DeleteLoadUniformToFrame(Name, loadPattern);
            deleteLoad(loadPattern, UniformToFrameLoads);
        }


        // LoadWindPressure
        /// <summary>
        /// Returns the wind pressure load assignments to area objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadWindPressure()
        {
            _apiAreaObject.GetLoadWindPressure(Name,
                out var names,
                out var loadPatterns,
                out var windPressureTypes,
                out var pressureCoefficients);

            _windPressureLoads = new List<AreaLoadWindPressure>();
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadWindPressure areaLoadWindPressure = new AreaLoadWindPressure
                {
                    LoadPattern = loadPatterns[i],
                    WindPressureType = windPressureTypes[i],
                    PressureCoefficient = pressureCoefficients[i]
                };

                _windPressureLoads.Add(areaLoadWindPressure);
            }
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="windPressure">The wind pressure.</param>
        /// <exception cref="CSiException">API_DEFALT_ERROR_CODE</exception>
        public void SetLoadWindPressure(AreaLoadWindPressure windPressure)
        {
            // TODO: Check if SetLoadWindPressure adds to or replaces
            _apiAreaObject?.SetLoadWindPressure(Name,
                windPressure.LoadPattern,
                windPressure.WindPressureType,
                windPressure.PressureCoefficient);

            addOrReplace(true, windPressure, _windPressureLoads);
        }

        /// <summary>
        /// Deletes the wind pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadWindPressure(string loadPattern)
        {
            if (_apiAreaObject == null) return;
            _apiAreaObject.DeleteLoadWindPressure(Name, loadPattern);
            deleteLoad(loadPattern, WindPressureLoads);
        }
        #endregion
    }
}
