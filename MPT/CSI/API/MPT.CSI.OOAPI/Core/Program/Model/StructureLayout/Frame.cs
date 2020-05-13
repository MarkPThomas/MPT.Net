// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Frame.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments;
using MPT.CSI.OOAPI.Core.Helpers.StructureLayout;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.Design;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using FrameSection = MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.FrameObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Frame.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject2D{FrameSection}" />
    public class Frame : StructureObject2D<FrameSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application frame object.
        /// </summary>
        /// <value>The frame object.</value>
        private ApiFrameObject _apiFrameObject => getApiFrameObject(_apiApp);

        /// <summary>
        /// The analysis results
        /// </summary>
        private FrameResults _analysisResults;
        /// <summary>
        /// Analysis results.
        /// </summary>
        /// <value>The results.</value>
        public FrameResults AnalysisResults => _analysisResults ?? (_analysisResults = new FrameResults(_apiApp, Name));

        // TODO: Make object that only contains design results
        /// <summary>
        /// The steel design results
        /// </summary>
        private SteelDesignResults _steelDesignResults;
        /// <summary>
        /// Steel design results.
        /// </summary>
        /// <value>The steel design results.</value>
        public SteelDesignResults SteelDesignResults => _steelDesignResults ?? 
                                                        (_steelDesignResults = new SteelDesignResults(_apiApp, Name));


        /// <summary>
        /// The concrete design results
        /// </summary>
        private ConcreteDesignResults _concreteDesignResults;
        /// <summary>
        /// Concrete design results.
        /// </summary>
        /// <value>The concrete design results.</value>
        public ConcreteDesignResults ConcreteDesignResults => _concreteDesignResults ?? 
                                                              (_concreteDesignResults = new ConcreteDesignResults(_apiApp, Name));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        private AluminumDesignResults _aluminumDesignResults; 

        /// <summary>
        /// Aluminum beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public AluminumDesignResults AluminumDesignResults => _aluminumDesignResults ?? (_aluminumDesignResults = new AluminumDesignResults(Name));

        
        private SteelColdFormedDesignResults _steelColdFormedDesignResults;
        /// <summary>
        /// Cold-formed steel beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public SteelColdFormedDesignResults SteelColdFormedDesignResults => _steelColdFormedDesignResults ?? (_steelColdFormedDesignResults = new SteelColdFormedDesignResults(Name));
#else        
        /// <summary>
        /// The composite beam design results
        /// </summary>
        private CompositeBeamDesignResults _compositeBeamDesignResults;
        /// <summary>
        /// Composite beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public CompositeBeamDesignResults CompositeBeamDesignResults => 
                                        _compositeBeamDesignResults ?? 
                                        (_compositeBeamDesignResults = new CompositeBeamDesignResults(_apiApp, Name));
#endif
        /// <summary>
        /// The frame modifier
        /// </summary>
        private FrameModifier _frameModifier;
        /// <summary>
        /// Gets or sets the frame modifier.
        /// </summary>
        /// <value>The frame modifier.</value>
        public FrameModifier FrameModifier
        {
            get
            {
                if (_frameModifier == null)
                {
                    FillModifiers();
                }

                return _frameModifier;
            }
        }

        /// <summary>
        /// The lateral bracing
        /// </summary>
        private List<FrameLateralBracing> _lateralBracing;
        /// <summary>
        /// Gets or sets the lateral bracing.
        /// </summary>
        /// <value>The lateral bracing.</value>
        public ReadOnlyCollection<FrameLateralBracing> LateralBracing
        {
            get
            {
                if (_lateralBracing == null)
                {
                    FillLateralBracing();
                }

                return new ReadOnlyCollection<FrameLateralBracing>(_lateralBracing);
            }
        }

        /// <summary>
        /// The hinges
        /// </summary>
        private List<FrameHinge> _hinges;
        /// <summary>
        /// Gets or sets the hinges.
        /// </summary>
        /// <value>The hinges.</value>
        public ReadOnlyCollection<FrameHinge> Hinges
        {
            get
            {
                if (_hinges == null)
                {
                    FillHingeAssigns();
                }

                return new ReadOnlyCollection<FrameHinge>(_hinges);
            }
        }

        /// <summary>
        /// The insertion point
        /// </summary>
        private FrameInsertionPoint _insertionPoint;
        /// <summary>
        /// Gets or sets the insertion point.
        /// </summary>
        /// <value>The insertion point.</value>
        public FrameInsertionPoint InsertionPoint
        {
            get
            {
                if (_insertionPoint == null)
                {
                    FillInsertionPoint();
                }

                return _insertionPoint;
            }
        }

        /// <summary>
        /// The end length offsets
        /// </summary>
        private FrameEndLengthOffset _endLengthOffsets;
        /// <summary>
        /// Gets or sets the end length offsets.
        /// </summary>
        /// <value>The end length offsets.</value>
        public FrameEndLengthOffset EndLengthOffsets
        {
            get
            {
                if (_endLengthOffsets == null)
                {
                    FillEndLengthOffset();
                }

                return _endLengthOffsets;
            }
        }

        /// <summary>
        /// The output stations
        /// </summary>
        private FrameOutputStation _outputStations;
        /// <summary>
        /// Gets or sets the output stations.
        /// </summary>
        /// <value>The output stations.</value>
        public FrameOutputStation OutputStations
        {
            get
            {
                if (_outputStations == null)
                {
                    FillOutputStations();
                }

                return _outputStations;
            }
        }

        /// <summary>
        /// Gets or sets the point i.
        /// </summary>
        /// <value>The point i.</value>
        public Point PointI => (Points.Count > 0) ? Points[0] : null;

        /// <summary>
        /// Gets or sets the point j.
        /// </summary>
        /// <value>The point j.</value>
        public Point PointJ => (Points.Count > 1) ? Points[1] : null;

        /// <summary>
        /// Gets the length of the frame.
        /// </summary>
        /// <value>The length.</value>
        public double Length => getLength(PointI, PointJ);

        /// <summary>
        /// The frame release i
        /// </summary>
        private Release _frameReleaseI;
        /// <summary>
        /// Gets or sets the frame release i.
        /// </summary>
        /// <value>The frame release i.</value>
        public Release FrameReleaseI
        {
            get
            {
                if (_frameReleaseI == null)
                {
                    FillReleases();
                }

                return _frameReleaseI;
            }
        }

        /// <summary>
        /// The frame release j
        /// </summary>
        private Release _frameReleaseJ;
        /// <summary>
        /// Gets or sets the frame release j.
        /// </summary>
        /// <value>The frame release j.</value>
        public Release FrameReleaseJ
        {
            get
            {
                if (_frameReleaseJ == null)
                {
                    FillReleases();
                }

                return _frameReleaseJ;
            }
        }
        
        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        internal string DesignSectionName { get; set; }
        /// <summary>
        /// The cross section
        /// </summary>
        private FrameSection _designSection;
        /// <summary>
        /// Gets or sets the cross section.
        /// </summary>
        /// <value>The cross section.</value>
        public FrameSection DesignSection 
        {
            get
            {
                if (_designSection == null && !string.IsNullOrEmpty(DesignSectionName))
                {
                    _designSection = _crossSections.FillItem(DesignSectionName);
                }

                return _designSection ?? CrossSection;
            }
        }
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// The frame support i
        /// </summary>
        private FrameSupport _frameSupportI;
        /// <summary>
        /// Gets or sets the frame support i.
        /// </summary>
        /// <value>The frame support i.</value>
        public FrameSupport FrameSupportI
        {
            get
            {
                if (_frameSupportI == null)
                {
                    FillSupports();
                }

                return _frameSupportI;
            }
        }

        /// <summary>
        /// The frame support j
        /// </summary>
        private FrameSupport _frameSupportJ;
        /// <summary>
        /// Gets or sets the frame support j.
        /// </summary>
        /// <value>The frame support j.</value>
        public FrameSupport FrameSupportJ
        {
            get
            {
                if (_frameSupportJ == null)
                {
                    FillSupports();
                }

                return _frameSupportJ;
            }
        }

        /// <summary>
        /// The design orientation
        /// </summary>
        private eFrameDesignOrientation _designOrientation;
        /// <summary>
        /// Gets or sets the design orientation.
        /// </summary>
        /// <value>The design orientation.</value>
        public eFrameDesignOrientation DesignOrientation
        {
            get
            {
                if (_designOrientation == 0)
                {
                    FillDesignOrientation();
                }

                return _designOrientation;
            }
        }
#else
        private FrameAutoMesh _autoMesh;
        public FrameAutoMesh AutoMesh
        {
            get
            {
                if (_autoMesh == null)
                {
                    FillAutoMesh();
                }

                return _autoMesh;
            }
        }

        private FrameFireProofing _fireProofing;
        public FrameFireProofing FireProofing
        {
            get
            {
                if (_fireProofing == null)
                {
                    FillFireProofing();
                }

                return _fireProofing;
            }
        }
#endif
        /// <summary>
        /// The distributed loads
        /// </summary>
        private List<FrameLoadDistributed> _distributedLoads;
        /// <summary>
        /// The distributed loads assigned to the frame.
        /// </summary>
        /// <value>The distributed loads.</value>
        public ReadOnlyCollection<FrameLoadDistributed> DistributedLoads
        {
            get
            {
                if (_distributedLoads == null)
                {
                    FillLoadDistributed();
                }

                return new ReadOnlyCollection<FrameLoadDistributed>(_distributedLoads);
            }
        }

        /// <summary>
        /// The point loads
        /// </summary>
        private List<FrameLoadPoint> _pointLoads;
        /// <summary>
        /// The point loads assigned to the frame.
        /// </summary>
        /// <value>The point loads.</value>
        public ReadOnlyCollection<FrameLoadPoint> PointLoads
        {
            get
            {
                if (_pointLoads == null)
                {
                    FillLoadPoint();
                }

                return new ReadOnlyCollection<FrameLoadPoint>(_pointLoads);
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Frame Factory(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties,
            string uniqueName)
        {
            Frame item = new Frame(app, 
                                componentsProperties, 
                                uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Frame(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties,
            string name) : base(app,
                                componentsProperties,
                                name) { }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Fills the design results.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool FillDesignResults()
        {
            eMaterialPropertyType currentMaterialUsed = MaterialUsed.Type;
            switch (currentMaterialUsed)
            {
                case eMaterialPropertyType.Steel:
                    if (SteelDesignResults.ResultsAreAvailable)
                    {
                        SteelDesignResults.FillDesignResults();
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
                        CompositeBeamDesignResults.FillDesignResults();
#endif
                        return true;
                    }
                    break;
                case eMaterialPropertyType.Concrete:
                    if (ConcreteDesignResults.ResultsAreAvailable)
                    {
                        ConcreteDesignResults.FillDesignResults();
                        return true;
                    }
                    break;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                case eMaterialPropertyType.Aluminum:
                    if (AluminumDesignResults.ResultsAreAvailable)
                    {
                        AluminumDesignResults.FillDesignResults();
                        return true;
                    }
                    break;
                case eMaterialPropertyType.ColdFormed:
                    if (SteelColdFormedDesignResults.ResultsAreAvailable)
                    {
                        SteelColdFormedDesignResults.FillDesignResults();
                        return true;
                    }
                    break;
#endif
            }
            return false;
        }
        #endregion

        #region Static
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="frameObject">The frame object.</param>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        internal static List<UniqueLabelNamePair> GetLabelNameList(ApiFrameObject frameObject)
        {
            return getLabelNameList(frameObject);
        }
#endif

        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <param name="frameObject">The frame object.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static List<string> GetNameList(ApiFrameObject frameObject)
        {
            return getNameList(frameObject);
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
            getPoints(_apiFrameObject);
        }

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override List<string> GetPointNames()
        {
            return getPoints(_apiFrameObject);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
            getNameFromLabel(_apiFrameObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_apiFrameObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_apiFrameObject);
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_apiFrameObject);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID(string guid = "")
        {
            setGUID(_apiFrameObject, guid);
        }


        /// <summary>
        /// Retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_apiFrameObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            fillTransformationMatrix(_apiFrameObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override DirectionCosines GetTransformationMatrix(bool isGlobal)
        {
            return getTransformationMatrix(_apiFrameObject, isGlobal);
        }
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_apiFrameObject);
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLocalAxes(AngleLocalAxes angleOffset)
        {
            setLocalAxes(_apiFrameObject, angleOffset);
        }
        #endregion

        #region Creation
        /// <summary>
        /// Changes the name of an existing object.
        /// </summary>
        /// <param name="newName">The new name for the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_apiFrameObject, newName);
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
        /// Two coordinates are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is <see cref="Constants.DEFAULT" />, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException">Two coordinates must be provided for a frame object. " +
        /// "Provided: " + coordinates.Length
        /// or <see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static Frame AddByCoordinate(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties,
            Tuple<Coordinate3DCartesian, Coordinate3DCartesian> coordinates,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            ApiFrameObject apiFrameObject = getApiFrameObject(app);
            Coordinate3DCartesian[] coordinatesArray = new Coordinate3DCartesian[2];
            coordinatesArray[0] = coordinates.Item1;
            coordinatesArray[1] = coordinates.Item2;

            uniqueName = apiFrameObject.AddByCoordinate(
                            coordinatesArray, 
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
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is <see cref="Constants.DEFAULT" />, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns>Frame.</returns>
        /// <exception cref="CSiException">Two points must be provided for a frame object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        internal static Frame AddByPoint(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties,
            Tuple<Point, Point> points,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            ApiFrameObject apiFrameObject = getApiFrameObject(app);
            string[] pointNames = new string[2];
            pointNames[0] = points.Item1.Name;
            pointNames[1] = points.Item2.Name;

            uniqueName = apiFrameObject.AddByPoint(
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
        /// <param name="pointNames">The point names that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is <see cref="Constants.DEFAULT" />, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns>Frame.</returns>
        /// <exception cref="CSiException">Two points must be provided for a frame object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        internal static Frame AddByPoint(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> componentsProperties,
            Tuple<string, string> pointNames,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            ApiFrameObject apiFrameObject = getApiFrameObject(app);
            string[] pointNamesArray = new string[2];
            pointNamesArray[0] = pointNames.Item1;
            pointNamesArray[1] = pointNames.Item2;

            uniqueName = apiFrameObject.AddByPoint(
                            pointNamesArray, 
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
            delete(_apiFrameObject);
        }
        #endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillSelected()
        {
            getSelected(_apiFrameObject);
        }

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Select()
        {
            setSelected(true);
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Deselect()
        {
            setSelected(false);
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected(bool isSelected)
        {
            setSelected(_apiFrameObject, isSelected);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_apiFrameObject?.Model.ObjectModel.FrameObject);
        }
#endif
        #endregion

        #region Modifiers
        /// <summary>
        /// Returns the unitless modifier assignments.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModifiers()
        {
            _frameModifier = _apiFrameObject.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for frame objects.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(FrameModifier modifiers)
        {
            _apiFrameObject.SetModifiers(Name, modifiers);
            _frameModifier = modifiers;
        }

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteModifiers()
        {
            deleteModifiers(_apiFrameObject);
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMass()
        {
            getMass(_apiFrameObject);
        }

        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetMass(double value, bool replace)
        {
            setMass(_apiFrameObject, value, replace);
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteMass()
        {
            deleteMass(_apiFrameObject);
        }



        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void FillMaterialOverwriteName()
        {
            getMaterialOverwriteName(_apiFrameObject);
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
            return getMaterialOverwriteName(_apiFrameObject);
        }

        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddMaterialOverwrite(Material material)
        {
            setMaterialOverwrite(_apiFrameObject, material);
        }

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void RemoveMaterialOverwrite()
        {
            setMaterialOverwrite(_apiFrameObject, null);
        }


        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal override void FillSectionName()
        {
            getSectionName(_apiFrameObject);
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override string GetSectionName()
        {
            return getSectionName(_apiFrameObject);
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(FrameSection section)
        {
            if (section == null) return;
            setSection(_apiFrameObject, section);
        }



        /// <summary>
        /// Returns frame object insertion point assignments.
        /// The assignments include the cardinal point and end joint offsets.
        /// </summary>
        public void FillInsertionPoint()
        {
            _apiFrameObject.GetInsertionPoint(Name,
                out var offsetDistancesI,
                out var offsetDistancesJ,
                out var cardinalInsertionPoint,
                out var isMirroredLocal2,
                out var isStiffnessTransformed,
                out var coordinateSystem);

            _insertionPoint = new FrameInsertionPoint()
            {
                OffsetDistancesI = offsetDistancesI,
                OffsetDistancesJ = offsetDistancesJ,
                CardinalPoint = cardinalInsertionPoint,
                IsMirroredLocal2 = isMirroredLocal2,
                IsStiffnessTransformed = isStiffnessTransformed,
                CoordinateSystem = coordinateSystem
            };
        }

        /// <summary>
        /// Assigns frame object insertion point data.
        /// The assignments are reported as end joint offsets.
        /// </summary>
        /// <param name="insertionPoint">The insertion point.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetInsertionPoint(FrameInsertionPoint insertionPoint)
        {
            _apiFrameObject.SetInsertionPoint(Name,
                insertionPoint.OffsetDistancesI,
                insertionPoint.OffsetDistancesJ,
                insertionPoint.CardinalPoint,
                insertionPoint.IsMirroredLocal2,
                insertionPoint.IsStiffnessTransformed,
                insertionPoint.CoordinateSystem);

            _insertionPoint = insertionPoint;
        }
        #endregion

        #region Frame Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function gets the fireproofing assignment to an existing frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="type">Type of fireproofing assigned.</param>
        /// <param name="thickness">When <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.SprayedOnUserPerimeterDefine" /> this is the thickness of the sprayed on fireproofing.
        /// When <paramref name="type" /> = <see cref="eFireProofing.ConcreteEncased" /> this is the concrete cover dimension. [L]</param>
        /// <param name="perimeter">This item applies only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnUserPerimeterDefine" />.
        /// It is the length of fireproofing applied measured around the perimeter of the frame object cross-section. [L]</param>
        /// <param name="density">This is the weight per unit volume of the fireproofing material. [F/L^3]</param>
        /// <param name="appliedToTopFlange">True: Fireproofing is assumed to be applied to the top flange of the section.
        /// False: Program assumes no fireproofing is applied to the section top flange.
        /// This flag applies for I, channel and double channel sections only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.ConcreteEncased" />.</param>
        /// <param name="includeInSelfWeight">True: Fireproofing is included in the structure self weight.</param>
        /// <param name="includeInGravityLoads">True: Fireproofing is included gravity loads applied in the X, Y and Z directions</param>
        /// <param name="includedLoadPattern">This item is either None or the name of an existing load pattern.
        /// If it is the name of a load pattern then the weight of the fireproofing is applied as a distributed load in the global Z direction in the load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillFireProofing()
        {
            _apiFrameObject
            FireProofing = new FrameFireProofing()
            {
            };
        }

        /// <summary>
        /// Sets the fireproofing assignments to existing frame objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetFireProofing(FrameFireProofing fireProofing)
        {
            _apiFrameObject
            FireProofing = fireProofing;
        }

        /// <summary>
        /// Deletes the fireproofing assignments to the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteFireProofing()
        {
            _apiFrameObject
        }
#endif

        /// <summary>
        /// Returns the lateral bracing location assignments for frame objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLateralBracing()
        {
            _apiFrameObject.GetLateralBracing(Name,
                out var frameNames,
                out var bracingTypes,
                out var bracingLocations,
                out var relativeDistanceStartBracing,
                out var relativeDistanceEndBracing,
                out var actualDistanceStartBracing,
                out var actualDistanceEndBracing);

            _lateralBracing = new List<FrameLateralBracing>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                FrameLateralBracing lateralBracing = new FrameLateralBracing(Length)
                {
                    BracingType = bracingTypes[i],
                    BracingCrossSectionLocation = bracingLocations[i],
                    BracingLocations =
                    {
                        RelativeDistanceStart = relativeDistanceStartBracing[i],
                        RelativeDistanceEnd = relativeDistanceEndBracing[i],
                        ActualDistanceStart = actualDistanceStartBracing[i],
                        ActualDistanceEnd = actualDistanceEndBracing[i]
                    },
                };

                _lateralBracing.Add(lateralBracing);
            }
        }

        /// <summary>
        /// Assigns a lateral bracing location to frame objects.
        /// </summary>
        /// <param name="lateralBracing">The lateral bracing.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLateralBracing(FrameLateralBracing lateralBracing)
        {
            // TODO: Check if SetLateralBracing adds to or replaces lateral bracing.
            lateralBracing.BracingLocations.SetLength(Length);
            _apiFrameObject.SetLateralBracing(Name,
                lateralBracing.BracingType,
                lateralBracing.BracingCrossSectionLocation,
                lateralBracing.BracingLocations.DistanceStart,
                lateralBracing.BracingLocations.DistanceEnd,
                lateralBracing.BracingLocations.UseRelativeDistance);

            if (_lateralBracing == null) _lateralBracing = new List<FrameLateralBracing>();
            _lateralBracing.Add(lateralBracing);
        }

        /// <summary>
        /// Deletes the lateral bracing assignments to the specified objects.
        /// </summary>
        /// <param name="bracingType">Indicates the bracing type to be deleted.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLateralBracing(eBracingType bracingType)
        {
            _apiFrameObject.DeleteLateralBracing(Name, bracingType);
            _lateralBracing?.RemoveAll(x => x.BracingType == bracingType);
        }

        /// <summary>
        /// Returns the frame object end offsets along the 1-axis of the element.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillEndLengthOffset()
        {
            _apiFrameObject.GetEndLengthOffset(Name,
                out var autoOffset,
                out var lengthIEnd,
                out var lengthJEnd,
                out var rigidZoneFactor);

            _endLengthOffsets = new FrameEndLengthOffset()
            {
                AutoOffset = autoOffset,
                LengthIEnd = lengthIEnd,
                LengthJEnd = lengthJEnd,
                RigidZoneFactor = rigidZoneFactor
            };
        }

        /// <summary>
        /// Assigns the line element end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="endLengthOffset">The end length offset.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetEndLengthOffset(FrameEndLengthOffset endLengthOffset)
        {
            _apiFrameObject.SetEndLengthOffset(Name,
                endLengthOffset.AutoOffset,
                endLengthOffset.LengthIEnd,
                endLengthOffset.LengthJEnd,
                endLengthOffset.RigidZoneFactor);

            _endLengthOffsets = endLengthOffset;
        }




        /// <summary>
        /// This function reports the hinge assignments for a specified frame object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillHingeAssigns()
        {
            _apiFrameObject.GetHingeAssigns(Name,
                out var hingeNumbers,
                out var generatedPropertyNames,
                out var hingeTypes,
                out var hingeBehaviors,
                out var sources,
                out var relativeDistances);

            _hinges = new List<FrameHinge>();
            for (int i = 0; i < hingeNumbers.Length; i++)
            {
                FrameHinge hinge = new FrameHinge()
                {
                    HingeNumber = hingeNumbers[i],
                    GeneratedPropertyName = generatedPropertyNames[i],
                    HingeType = hingeTypes[i],
                    HingeBehavior = hingeBehaviors[i],
                    Source = sources[i],
                    RelativeDistance = relativeDistances[i]
                };
                _hinges.Add(hinge);
            }
        }



        /// <summary>
        /// Returns object output station data.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillOutputStations()
        {
            _apiFrameObject.GetOutputStations(Name,
                out var outputStationType,
                out var maxStationSpacing,
                out var minStationNumber,
                out var noOutputAndDesignAtElementEnds,
                out var noOutputAndDesignAtPointLoads);

            _outputStations = new FrameOutputStation()
            {
                OutputStationType = outputStationType,
                MaxStationSpacing = maxStationSpacing,
                MinStationNumber = minStationNumber,
                NoOutputAndDesignAtElementEnds = noOutputAndDesignAtElementEnds,
                NoOutputAndDesignAtPointLoads = noOutputAndDesignAtPointLoads,
            };
        }

        /// <summary>
        /// Assigns object output station data.
        /// </summary>
        /// <param name="outputStation">The output station.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetOutputStations(FrameOutputStation outputStation)
        {
            _apiFrameObject.SetOutputStations(Name,
                outputStation.OutputStationType,
                outputStation.MaxStationSpacing,
                outputStation.MinStationNumber,
                outputStation.NoOutputAndDesignAtElementEnds,
                outputStation.NoOutputAndDesignAtPointLoads);

            _outputStations = outputStation;
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns  the automatic meshing assignments to frame objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillAutoMesh()
        {
            _apiFrameObject

            AutoMesh = new FrameAutoMesh()
            {
            };
        }

        /// <summary>
        /// Sets automatic meshing assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isAutoMeshed">.</param>
        /// <param name="isAutoMeshedAtPoints"></param>
        /// <param name="isAutoMeshedAtLines"></param>
        /// <param name="minElementNumber"></param>
        /// <param name="autoMeshMaxLength"></param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoMesh(FrameAutoMesh autoMesh)
        {
            _apiFrameObject
            AutoMesh = autoMesh;
        }
#endif
        #endregion

        #region Support & Connections
        /// <summary>
        /// Returns the release assignments for a frame end release.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillReleases()
        {
            _apiFrameObject.GetReleases(Name,
                out var iEndRelease,
                out var jEndRelease,
                out var iEndFixity,
                out var jEndFixity);

            _frameReleaseI = new Release()
            {
                EndRelease = iEndRelease,
                EndFixity = iEndFixity
            };

            _frameReleaseJ = new Release()
            {
                EndRelease = jEndRelease,
                EndFixity = jEndFixity
            };
        }

        /// <summary>
        /// This function defines a named frame end release.
        /// </summary>
        /// <param name="frameReleaseI">The frame release i.</param>
        /// <param name="frameReleaseJ">The frame release j.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetReleases(Release frameReleaseI, Release frameReleaseJ)
        {
            _apiFrameObject.SetReleases(Name,
                frameReleaseI.EndRelease,
                frameReleaseJ.EndRelease,
                frameReleaseI.EndFixity,
                frameReleaseJ.EndFixity);

            _frameReleaseI = frameReleaseI;
            _frameReleaseJ = frameReleaseJ;
        }

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves support data for a given frame beam object.
        /// </summary>
        public void FillSupports()
        {
            _apiFrameObject.GetSupports(Name,
                out var supportNameI,
                out var supportTypeI,
                out var supportNameJ,
                out var supportTypeJ);

            _frameSupportI = new FrameSupport()
            {
                SupportName = supportNameI,
                SupportType = supportTypeI
            };

            _frameSupportJ = new FrameSupport()
            {
                SupportName = supportNameJ,
                SupportType = supportTypeJ
            };
        }

        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_apiFrameObject);
        }
        

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="springName">Name of the spring.</param>
        public override void SetSpringAssignment(string springName)
        {
            setSpringAssignment(_apiFrameObject, springName);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _apiFrameObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }


        #endregion

        #region Design
        /// <summary>
        /// Modifies the design section for the frame object.
        /// </summary>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal void SetDesignSection(string nameSection)
        {
            DesignSectionName = nameSection;
        }

        /// <summary>
        /// Removes the design section for the specified frame object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        internal void RemoveDesignSection()
        {
            DesignSectionName = string.Empty;
            _designSection = null;
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the design orientation of a frame object.
        /// </summary>
        public void FillDesignOrientation()
        {
            _designOrientation = _apiFrameObject.GetDesignOrientation(Name);
        }


        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        internal override void FillPierName()
        {
            getPierName(_apiFrameObject);
        }

        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetPierName()
        {
            return getPierName(_apiFrameObject);
        }

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public override void AddToPier(Pier pier)
        {
            addPier(_apiFrameObject, pier);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromPier()
        {
            removePier(_apiFrameObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        internal override void FillSpandrelName()
        {
            getSpandrelName(_apiFrameObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetSpandrelName()
        {
            return getSpandrelName(_apiFrameObject);
        }

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public override void AddToSpandrel(Spandrel spandrel)
        {
            addSpandrel(_apiFrameObject, spandrel);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromSpandrel()
        {
            removeSpandrel(_apiFrameObject);
        }
#endif
        #endregion

        #region Loading
        // LoadTemperature
        /// <summary>
        /// Returns the temperature load assignments to elements.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLoadTemperature()
        {
            getLoadTemperature(_apiFrameObject);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddLoadTemperature(LoadTemperature temperatureLoad)
        {
           setLoadTemperature(_apiFrameObject, temperatureLoad, replace: false);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ReplaceLoadTemperature(LoadTemperature temperatureLoad)
        {
            setLoadTemperature(_apiFrameObject, temperatureLoad, replace: true);
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteLoadTemperature(string loadPattern)
        {
            deleteLoadTemperature(_apiFrameObject, loadPattern);
            deleteLoad(loadPattern, TemperatureLoads);
        }

        // LoadDistributed
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadDistributed()
        {
            _apiFrameObject.GetLoadDistributed(Name, 
                out var names,
                out var loadPatterns,
                out var forceTypes,
                out var loadDirections,
                out var startLoadValues,
                out var endLoadValues,
                out var absoluteDistanceStartFromI,
                out var absoluteDistanceEndFromI,
                out var relativeDistanceStartFromI,
                out var relativeDistanceEndFromI,
                out var coordinateSystems);

            _distributedLoads = new List<FrameLoadDistributed>();
            for (int i = 0; i < names.Length; i++)
            {
                FrameLoadDistributed distributedLoad = new FrameLoadDistributed
                {
                    LoadPattern = loadPatterns[i],
                    ForceType = forceTypes[i],
                    LoadDirection = loadDirections[i],
                    StartLoadValue = startLoadValues[i],
                    EndLoadValue = endLoadValues[i],
                    DistanceFromI =
                    {
                        ActualDistanceStart = absoluteDistanceStartFromI[i],
                        RelativeDistanceStart = relativeDistanceStartFromI[i],
                        ActualDistanceEnd = absoluteDistanceEndFromI[i],
                        RelativeDistanceEnd = relativeDistanceEndFromI[i],
                    },
                    CoordinateSystem = coordinateSystems[i],
                };

                _distributedLoads.Add(distributedLoad);
            }
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="distributedLoad">The distributed load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadDistributed(FrameLoadDistributed distributedLoad)
        {
            distributedLoad.DistanceFromI.SetLength(Length);
            setLoadDistributed(distributedLoad, replace: false);
            addOrReplace(false, distributedLoad, _distributedLoads);
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="distributedLoad">The distributed load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadDistributed(FrameLoadDistributed distributedLoad)
        {
            distributedLoad.DistanceFromI.SetLength(Length);
            setLoadDistributed(distributedLoad, replace: true);
            addOrReplace(true, distributedLoad, _distributedLoads);
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="distributedLoad">The distributed load.</param>
        /// <param name="replace">True: All previous distributed loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadDistributed(
            FrameLoadDistributed distributedLoad,
            bool replace = true)
        {
            _apiFrameObject.SetLoadDistributed(Name,
                distributedLoad.LoadPattern,
                distributedLoad.ForceType,
                distributedLoad.LoadDirection,
                distributedLoad.StartLoadValue,
                distributedLoad.EndLoadValue,
                distributedLoad.DistanceFromI.DistanceStart,
                distributedLoad.DistanceFromI.DistanceEnd,
                distributedLoad.DistanceFromI.UseRelativeDistance,
                distributedLoad.CoordinateSystem,
                replace);
        }

        /// <summary>
        /// Deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDistributed(string loadPattern)
        {
            _apiFrameObject.DeleteLoadDistributed(Name, loadPattern);
            deleteLoad(loadPattern, _distributedLoads);
        }

        // LoadPoint
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadPoint()
        {
            _apiFrameObject.GetLoadPoint(Name, 
                out var names,
                out var loadPatterns,
                out var forceTypes,
                out var loadDirections,
                out var pointLoadValues,
                out var absoluteDistanceFromI,
                out var relativeDistanceFromI,
                out var coordinateSystems);

            _pointLoads = new List<FrameLoadPoint>();
            for (int i = 0; i < names.Length; i++)
            {
                FrameLoadPoint pointLoad = new FrameLoadPoint
                {
                    LoadPattern = loadPatterns[i],
                    ForceType = forceTypes[i],
                    LoadDirection = loadDirections[i],
                    PointLoadValue = pointLoadValues[i],
                    DistanceFromI =
                    {
                        ActualDistance = absoluteDistanceFromI[i],
                        RelativeDistance = relativeDistanceFromI[i],
                    },
                    CoordinateSystem = coordinateSystems[i],
                };

                _pointLoads.Add(pointLoad);
            }
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="pointLoad">The point load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadPoint(FrameLoadPoint pointLoad)
        {
            pointLoad.DistanceFromI.SetLength(Length);
            setLoadPoint(pointLoad, replace: false);
            addOrReplace(true, pointLoad, _pointLoads);
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="pointLoad">The point load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadPoint(FrameLoadPoint pointLoad)
        {
            pointLoad.DistanceFromI.SetLength(Length);
            setLoadPoint(pointLoad, replace: true);
            addOrReplace(true, pointLoad, _pointLoads);
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="pointLoad">The point load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadPoint(
            FrameLoadPoint pointLoad,
            bool replace = true)
        {
            _apiFrameObject.SetLoadPoint(Name,
                pointLoad.LoadPattern,
                pointLoad.ForceType,
                pointLoad.LoadDirection,
                pointLoad.PointLoadValue,
                pointLoad.DistanceFromI.Distance,
                pointLoad.DistanceFromI.UseRelativeDistance,
                pointLoad.CoordinateSystem,
                replace);
        }

        /// <summary>
        /// Deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadPoint(string loadPattern)
        {
            _apiFrameObject.DeleteLoadPoint(Name, loadPattern);
            deleteLoad(loadPattern, _pointLoads);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <param name="pointI">The point i.</param>
        /// <param name="pointJ">The point j.</param>
        /// <returns>System.Double.</returns>
        protected double getLength(Point pointI, Point pointJ)
        {
            double deltaX = pointI.X - pointJ.X;
            double deltaY = pointI.Y - pointJ.Y;
            double deltaZ = pointI.Z - pointJ.Z;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        /// <summary>
        /// Updates all values related to frame length.
        /// </summary>
        protected void updateFrameLength()
        {
            // TODO: Tie this in with adding nodes, moving nodes.
            foreach (var frameLateralBracing in LateralBracing)
            {
                frameLateralBracing.BracingLocations.SetLength(Length);
            }

            foreach (var pointLoad in PointLoads)
            {
                pointLoad.DistanceFromI.SetLength(Length);
            }

            foreach (var distributedLoad in DistributedLoads)
            {
                distributedLoad.DistanceFromI.SetLength(Length);
            }
        }
        
        #endregion
    }
}
