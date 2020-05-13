// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using eObjectType = MPT.CSI.API.Core.Program.ModelBehavior.eObjectType;
using MassObject = MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses.Mass;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;


namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Node.
    /// </summary>
    /// <seealso cref="StructureObject" />
    public class Point : StructureObject
    {
        #region Fields & Properties
        /// <summary>
        /// The API application point object.
        /// </summary>
        /// <value>The point object.</value>
        protected ApiPointObject _apiPointObject => getApiPointObject(_apiApp);

        /// <summary>
        /// The results
        /// </summary>
        private JointResults _results;
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public JointResults Results => _results?? (_results = new JointResults(_apiApp, Name));

        /// <summary>
        /// The coordinates.
        /// </summary>
        protected Coordinate3DCartesian _coordinates;
        /// <summary>
        /// The x-axis coordinate.
        /// </summary>
        /// <value>The x.</value>
        public double X => _coordinates.X;

        /// <summary>
        /// The y-axis coordinate.
        /// </summary>
        /// <value>The y.</value>
        public double Y => _coordinates.Y;

        /// <summary>
        /// The z-axis coordinate.
        /// </summary>
        /// <value>The z.</value>
        public double Z => _coordinates.Z;

        /// <summary>
        /// The degrees of freedom
        /// </summary>
        private DegreesOfFreedomLocal? _degreesOfFreedom;
        /// <summary>
        /// These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.
        /// </summary>
        /// <value>The degrees of freedom.</value>
        public DegreesOfFreedomLocal DegreesOfFreedom
        {
            get
            {
                if (_degreesOfFreedom == null)
                {
                    FillRestraint();
                }

                return _degreesOfFreedom ?? new DegreesOfFreedomLocal();
            }
        }

        /// <summary>
        /// The panel zone
        /// </summary>
        private PanelZone _panelZone;
        /// <summary>
        /// The panel zone associated with the node.
        /// </summary>
        /// <value>The panel zone.</value>
        public PanelZone PanelZone => _panelZone ?? (_panelZone = PanelZone.Factory(_apiApp, Name));


        /// <summary>
        /// The stiffness
        /// </summary>
        private Stiffness _stiffness;
        /// <summary>
        /// Spring stiffness values for each decoupled degree of freedom.
        /// </summary>
        /// <value>The stiffness.</value>
        public Stiffness Stiffness
        {
            get
            {
                if (_stiffness == null)
                {
                     FillSpring();
                }

                return _stiffness;
            }
        }


        /// <summary>
        /// The stiffness coupled
        /// </summary>
        private StiffnessCoupled _stiffnessCoupled;
        /// <summary>
        /// Spring stiffness values for each coupled degree of freedom.
        /// </summary>
        /// <value>The stiffness coupled.</value>
        public StiffnessCoupled StiffnessCoupled
        {
            get
            {
                if (_stiffnessCoupled == null)
                {
                    FillSpringCoupled();
                }

                return _stiffnessCoupled;
            }
        }


        /// <summary>
        /// The is spring coupled
        /// </summary>
        private bool? _isSpringCoupled;
        /// <summary>
        /// Indicates if the spring assignments to a point object are coupled;
        /// that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <value><c>true</c> if this instance is spring coupled; otherwise, <c>false</c>.</value>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool IsSpringCoupled
        {
            get
            {
                if (_isSpringCoupled == null)
                {
                    FillIsSpringCoupled();
                }

                return _isSpringCoupled ?? false;
            }
        }


        /// <summary>
        /// The mass
        /// </summary>
        private MassObject _mass;
        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        /// <value>The mass.</value>
        public MassObject Mass
        {
            get
            {
                if (_mass == null)
                {
                    FillMass();
                }

                return _mass;
            }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// The diaphragm
        /// </summary>
        private Diaphragm _diaphragm;
        /// <summary>
        /// Gets or sets the diaphragm.
        /// </summary>
        /// <value>The diaphragm.</value>
        public Diaphragm Diaphragm
        {
            get
            {
                if (_diaphragm == null)
                {
                    FillDiaphragm();
                }

                return _diaphragm;
            }
        }
#else
            FillConstraint();
#endif

        /// <summary>
        /// The loads
        /// </summary>
        private List<NodeLoad> _loads;
        /// <summary>
        /// Gets or sets the loads.
        /// </summary>
        /// <value>The loads.</value>
        public List<NodeLoad> Loads
        {
            get
            {
                if (_loads == null)
                {
                    FillLoadForce();
                }

                return _loads;
            }
        }

        /// <summary>
        /// The displacements
        /// </summary>
        private List<NodeLoadDisplacement> _displacements;
        /// <summary>
        /// Gets or sets the displacements.
        /// </summary>
        /// <value>The displacements.</value>
        public List<NodeLoadDisplacement> Displacements
        {
            get
            {
                if (_displacements == null)
                {
                    FillLoadDisplacement();
                }

                return _displacements;
            }
        }


        /// <summary>
        /// The is special point
        /// </summary>
        private bool? _isSpecialPoint;
        /// <summary>
        /// True: This instance is a special point.
        /// </summary>
        /// <value><c>true</c> if this instance is special point; otherwise, <c>false</c>.</value>
        public bool IsSpecialPoint
        {
            get
            {
                if (_isSpecialPoint == null)
                {
                    FillIsSpecialPoint();
                }

                return _isSpecialPoint ?? false;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        internal static Point Factory(ApiCSiApplication app, string uniqueName)
        {
            Point item = new Point(app, uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Point(ApiCSiApplication app, string name) : base(app, name) { }


        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {  
            base.FillData();
            FillCoordinates();
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillMergeNumber();
            FillPatternValue();
#endif
        }
        #endregion

        #region Static
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="pointObject">The point object.</param>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        internal static List<UniqueLabelNamePair> GetLabelNameList(ApiPointObject pointObject)
        {
            return getLabelNameList(pointObject);
        }
#endif

        // IListableNames
        /// <summary>
        /// Returns the names of all defined point properties.
        /// </summary>
        /// <param name="pointObject">The point object.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static List<string> GetNameList(ApiPointObject pointObject)
        {
            return getNameList(pointObject);
        }
        #endregion

        #region Query

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
           getNameFromLabel(_apiPointObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_apiPointObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_apiPointObject);
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified point object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_apiPointObject);
        }

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID(string guid = "")
        {
            setGUID(_apiPointObject, guid);
        }


        /// <summary>
        /// Retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_apiPointObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            fillTransformationMatrix(_apiPointObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override DirectionCosines GetTransformationMatrix(bool isGlobal)
        {
            return getTransformationMatrix(_apiPointObject, isGlobal);
        }

        // , bool isGlobal

        /// <summary>
        /// Returns the x, y and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillCoordinates()
        {
            if (_apiPointObject == null) return;
            _coordinates = GetCoordinate();
        }

        /// <summary>
        /// Returns the x, y and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DCartesian.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCartesian GetCoordinate()
        {
            return _apiPointObject?.GetCoordinate(Name, Constants.CoordinateSystem) ?? new Coordinate3DCartesian();
        }

        /// <summary>
        /// Returns the r, Theta and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DCylindrical.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCylindrical GetCoordinateCylindrical()
        {
            return _apiPointObject?.GetCoordinateCylindrical(Name, Constants.CoordinateSystem) ?? new Coordinate3DCylindrical();
        }

        /// <summary>
        /// Returns the r, a and b coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DSpherical.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DSpherical GetCoordinateSpherical()
        {
            return _apiPointObject?.GetCoordinateSpherical(Name, Constants.CoordinateSystem) ?? new Coordinate3DSpherical();
        }

        /// <summary>
        /// Returns a list of objects and corresponding point numbers connected to a specified point object.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;eObjectType, System.String, System.Int32&gt;&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public List<Tuple<eObjectType, string, int>> GetConnectivity()
        {
            if (_apiPointObject == null) return new List<Tuple<eObjectType, string, int>>();
            _apiPointObject.GetConnectivity(Name, out var objectTypes, out var objectNames, out var pointNumbers);

            return objectTypes.Select((t, i) => new Tuple<eObjectType, string, int>(t, objectNames[i], pointNumbers[i])).ToList();
        }

        /// <summary>
        /// Returns the total number of objects (line, area, solid and link) that connect to the specified point object.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int NumberOfObjectsConnected()
        {
            return _apiPointObject?.GetCommonTo(Name) ?? 0;
        }
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_apiPointObject);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset)
        {
            _apiPointObject
        }


        /// <summary>
        /// Gets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
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
        public void FillLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
            _apiPointObject
        }

        /// <summary>
        /// Sets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object or group depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
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
        public void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            eReferenceVectorDirection[] axisVectorDirection,
            string[] axisPoint,
            double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector)
        {
            _apiPointObject
        }
#endif
        #endregion

        #region Creation
        /// <summary>
        /// Changes the name of an existing point object.
        /// </summary>
        /// <param name="newName">The new name for the point object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_apiPointObject, newName);
        }


        /// <summary>
        /// Deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        internal override void Delete()
        {
            delete(_apiPointObject);
        }


        /// <summary>
        /// Adds a point object to the model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="uniqueName">Unique name of the point.
        /// If not supplied, the program will generate one automatically.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all point objects have a merge number of zero.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <returns>Point.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static Point AddByCoordinate(ApiCSiApplication app, 
            Coordinate3DCartesian coordinate,
            string uniqueName = "",
            bool mergeOff = false,
            int mergeNumber = 0,
            string coordinateSystem = CoordinateSystems.Global)
        {
            ApiPointObject apiPointObject = getApiPointObject(app);
            uniqueName = apiPointObject?.AddByCoordinate(
                        coordinate,
                        uniqueName,
                        coordinateSystem,
                        mergeOff,
                        mergeNumber);
            return Factory(app, uniqueName);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// </summary>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal void AddByCoordinateCylindrical(
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _apiPointObject
        }

        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// </summary>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all pointobjects have a merge number of zero.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal void AddByCoordinateSpherical(
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _apiPointObject
        }
#endif
        #endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillSelected()
        {
            getSelected(_apiPointObject);
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
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected(bool isSelected)
        {
            setSelected(_apiPointObject, isSelected);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_apiPointObject);
        }
#endif
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Retrieves the point mass assignment values for a point object. The masses are always returned in the point local coordinate system.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMass()
        {
            _mass = new MassObject(_apiPointObject.GetMass(Name));
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddMass(MassObject mass)
        {
            setMass(mass, replace: false);
            _mass += mass;
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceMass(MassObject mass)
        {
            setMass(mass, replace: true);
            _mass = mass;
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setMass(MassObject mass, bool replace)
        {
            _apiPointObject?.SetMass(Name, mass.GetMass(), mass.IsLocalCoordinateSystem, replace);
        }



        /// <summary>
        /// Assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="material">The material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddMassByVolume(MassObject mass, Material material)
        {
            setMassByVolume(mass, material, replace: false);
            _mass += mass;
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="material">The material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceMassByVolume(MassObject mass, Material material)
        {
            setMassByVolume(mass, material, replace: true);
            _mass = mass;
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="material">The material.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setMassByVolume(
            MassObject mass, 
            Material material,
            bool replace)
        {
            _apiPointObject?.SetMass(Name, Mass.GetMassByVolume(material.Name), material.Name, Mass.IsLocalCoordinateSystem, replace);
        }



        /// <summary>
        /// Assigns point mass to a point object as a mass by weight.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddMassByWeight(MassObject mass)
        {
            setMassByWeight(mass, replace: false);
            _mass += mass;
        }

        /// <summary>
        /// Assigns point mass to a point object as a mass by weight.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceMassByWeight(MassObject mass)
        {
            setMassByWeight(mass, replace: true);
            _mass = mass;
        }

        /// <summary>
        /// Assigns point mass to a point object as a mass by weight.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setMassByWeight(MassObject mass, bool replace)
        {
            _apiPointObject?.SetMass(Name, mass.GetMassByWeight(), mass.IsLocalCoordinateSystem, replace);
        }



        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteMass()
        {
            _apiPointObject?.DeleteMass(Name);
            _mass = null;
        }
        #endregion

        #region Point Properties
        /// <summary>
        /// Retrieves the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillIsSpecialPoint()
        {
            _isSpecialPoint = _apiPointObject.GetSpecialPoint(Name);
        }

        /// <summary>
        /// Sets the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAsSpecialPoint()
        {
            _isSpecialPoint = true;
            _apiPointObject?.SetSpecialPoint(Name, IsSpecialPoint);
        }

        /// <summary>
        /// Removes the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveAsSpecialPoint()
        {
            _isSpecialPoint = false;
            _apiPointObject?.SetSpecialPoint(Name, IsSpecialPoint);
        }

        /// <summary>
        /// Deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteSpecialPoint()
        {
            _apiPointObject?.DeleteSpecialPoint(Name);
            _isSpecialPoint = false;
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int FillMergeNumber()
        {
            _apiPointObject
        }

        /// <summary>
        /// Assigns the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <param name="mergeNumber">The merge number assigned to the specified point object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMergeNumber(int mergeNumber)
        {
            _apiPointObject
        }

        
        /// <summary>
        /// Returns the joint pattern value for a specific point object and joint pattern.
        /// Joint pattern values are unitless.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public double FillPatternValue(string patternName)
        {
            _apiPointObject
        }

        /// <summary>
        /// Sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = [(z – zpoint) * w] + u
        /// where z(<paramref name="zCoordinateAtZeroPressure" />), w(<paramref name="weightPerUnitVolume" />) and u(<paramref name="uniformForcePerUnitArea" />) are described in the Parameters section and zpoint is the Z coordinate of the considered point object in the present coordinate system.
        /// All appropriate unit conversions are used to calculate the value in the database units, but thereafter it is assumed to be unitless.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="zCoordinateAtZeroPressure">The Z coordinate at zero pressure in the present coordinate system. [L]</param>
        /// <param name="weightPerUnitVolume">A weight per unit volume. [F/L^3]</param>
        /// <param name="uniformForcePerUnitArea">An added uniform force per unit area. [F/L^2]</param>
        /// <param name="restrictionCurrent">This restriction applies before the pattern value has been added to any existing pattern value assigned to the point object.</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to or replaced any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPatternByPressure(
            string patternName,
            double zCoordinateAtZeroPressure,
            double weightPerUnitVolume,
            double uniformForcePerUnitArea,
            ePatternRestriction restrictionCurrent,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false)
        {
            _apiPointObject
        }

        /// <summary>
        /// Sets the joint pattern value for a specified point object and joint pattern.
        /// The joint pattern value is calculated as:
        /// Value = ax + by + cz + d
        /// where a, b, c and d are function input parameters and x, y and z are the coordinates of the considered point object in the present coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <param name="a">The x-coefficient in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="b">The y-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="c">The z-coefficient a in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="d">The constant in the equation shown in the Remarks section. [1/L]</param>
        /// <param name="restrictionCombined">This restriction applies after the pattern value has been added to or replaced any existing pattern value assigned to the point object.
        /// This restriction applies even if there was no existing joint pattern value on the point object.</param>
        /// <param name="replace">True: Joint pattern value calculated as shown in the Remarks section replaces any previous joint pattern value for the point object.
        /// False: Joint pattern value calculated as shown in the Remarks section is added to any previous joint pattern value for the point object and then the Restriction items are checked.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPatternByCoordinates(
            string patternName,
            double a,
            double b,
            double c,
            double d,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false)
        {
            _apiPointObject
        }

        /// <summary>
        /// Deletes all joint pattern assignments, associated with the specified joint pattern, from the specified point object(s).
        /// </summary>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePatternValue(string patternName)
        {
            _apiPointObject
        }
#endif

        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillPanelZone()
        {
            try
            {
                _panelZone = PanelZone.Factory(_apiApp, Name);
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                _panelZone = null;
            }
        }

        /// <summary>
        /// Sets panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <param name="panelZoneProperties">The panel zone properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPanelZone(PanelZoneProperties panelZoneProperties)
        {
            PanelZone?.SetPanelZone(panelZoneProperties);
        }

        /// <summary>
        /// Deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePanelZone()
        {
            PanelZone?.DeletePanelZone();
            _panelZone = null;
        }
        #endregion

        #region Support & Connections
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        public void FillDiaphragm()
        {
            _diaphragm = Diaphragm.GetDiaphragm(this);
        }

        /// <summary>
        /// Assigns a diaphragm to an object.
        /// </summary>
        /// <param name="diaphragmName">Name of the diaphragm.</param>
        public void AddToDiaphragm(string diaphragmName)
        {
            if (Diaphragm == null)
            {
                _diaphragm = Diaphragm.Factory(_apiApp, diaphragmName);
            }
            Diaphragm?.AddToDiaphragm(this);
        }

        /// <summary>
        /// Removes an object from the diaphragm.
        /// </summary>
        public void RemoveFromDiaphragm()
        { 
            if (Diaphragm != null)
            {
                _diaphragm = Diaphragm.GetDiaphragm(this);
                if (Diaphragm == null) return;
            }
            Diaphragm?.RemoveFromDiaphragm(this);
            _diaphragm = null;
        }
#else
        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillConstraint(ref string[] pointNames,
            ref string[] constraintNames)
        {
            _apiPointObject
        }

        /// <summary>
        /// Sets joint constraint assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="constraintName">The name of the existing joint constraint.</param>
        /// <param name="replace">True: All existing point assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetConstraint(string constraintName,
            bool replace = true)
        {
            _apiPointObject
        }

        /// <summary>
        /// Deletes all constraint assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteConstraint()
        {
            _apiPointObject
        }
#endif

        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillRestraint()
        {
            if (_apiPointObject == null) return;
            _degreesOfFreedom = _apiPointObject.GetRestraint(Name);
        }

        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <param name="degreesOfFreedom">The degrees of freedom.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRestraint(DegreesOfFreedomLocal degreesOfFreedom)
        {
            _apiPointObject?.SetRestraint(Name, degreesOfFreedom);
            _degreesOfFreedom = degreesOfFreedom;
        }

        /// <summary>
        /// Deletes all restraint assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteRestraint()
        {
            _apiPointObject?.DeleteRestraint(Name);
            _degreesOfFreedom = new DegreesOfFreedomLocal();
        }


#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_apiPointObject);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="springName">Name of the spring.</param>
        public override void SetSpringAssignment(string springName)
        {
            setSpringAssignment(_apiPointObject, springName);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _apiPointObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }

        /// <summary>
        /// This function indicates if the spring assignments to a point object are coupled, that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <returns><c>true</c> if [is spring coupled] [the specified name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillIsSpringCoupled()
        {
            if (_apiPointObject == null) return;
            _isSpringCoupled = _apiPointObject.IsSpringCoupled(Name);
        }



        /// <summary>
        /// Retrieves uncoupled spring stiffness assignments for a point object;
        /// that is, it retrieves the diagonal terms in the 6x6 spring matrix for the point object.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point object either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSpring()
        {
            if (_apiPointObject == null) return;
            _stiffness = _apiPointObject.GetSpring(Name);
        }

        /// <summary>
        /// Assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffness">The stiffness.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddSpring(Stiffness stiffness)
        {
            setSpring(stiffness, replace: false);
            _stiffness += stiffness;
        }

        /// <summary>
        /// Assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffness">The stiffness.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceSpring(Stiffness stiffness)
        {
            setSpring(stiffness, replace: true);
            _stiffness = stiffness;
        }

        /// <summary>
        /// Assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffness">The stiffness.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSpring(Stiffness stiffness, bool replace)
        {
            _apiPointObject?.SetSpring(Name, stiffness, replace);
            _stiffness = stiffness;
        }



        /// <summary>
        /// Retrieves coupled spring stiffness assignments for a point object;
        /// that is, it retrieves the spring matrix of 21 stiffness values for the point object.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point element either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSpringCoupled()
        {
            if (_apiPointObject == null) return;
            _stiffnessCoupled = _apiPointObject.GetSpringCoupled(Name);
        }

        /// <summary>
        /// Assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffnessCoupled">The stiffness coupled.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddSpringCoupled(StiffnessCoupled stiffnessCoupled)
        {
            setSpringCoupled(stiffnessCoupled, replace: false);
            _stiffnessCoupled += stiffnessCoupled;
        }

        /// <summary>
        /// Assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffnessCoupled">The stiffness coupled.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceSpringCoupled(StiffnessCoupled stiffnessCoupled)
        {
            setSpringCoupled(stiffnessCoupled, replace: true);
            _stiffnessCoupled = stiffnessCoupled;
        }

        /// <summary>
        /// Assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="stiffnessCoupled">The stiffness coupled.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSpringCoupled(StiffnessCoupled stiffnessCoupled, bool replace)
        {
            _apiPointObject?.SetSpringCoupled(Name, stiffnessCoupled, replace);
        }
        #endregion

        #region Loading
        // LoadForce
        /// <summary>
        /// Returns a list of force load assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadForce()
        {
            if (_apiPointObject == null) return;
            _apiPointObject.GetLoadForce(Name,
                out var names,
                out var loadPatterns,
                out var loadPatternSteps,
                out var coordinateSystem,
                out var forces);

            _loads = new List<NodeLoad>();
            for (int i = 0; i < names.Length; i++)
            {
                NodeLoad load = new NodeLoad
                {
                    LoadPattern = loadPatterns[i],
                    CoordinateSystem = coordinateSystem[i],
                    Force = forces[i],
                    LoadPatternSteps = loadPatternSteps[i]
                };

                _loads.Add(load);
            }
        }

        /// <summary>
        /// Sets point load assignments to point objects.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadForce(NodeLoad load)
        {
            setLoadForce(load, replace: false);
            _loads.Add(load);
        }

        /// <summary>
        /// Sets point load assignments to point objects.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadForce(NodeLoad load)
        {
            setLoadForce(load, replace: true);
            _loads = new List<NodeLoad>() { load };
        }

        /// <summary>
        /// Sets point load assignments to point objects.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadForce(
            NodeLoad load,
            bool replace)
        {
            _apiPointObject?.SetLoadForce(Name,
                load.LoadPattern,
                load.Force,
                replace: replace,
                coordinateSystem: load.CoordinateSystem);
        }

        /// <summary>
        /// Deletes all point load assignments, for the specified load pattern, from the specified point object(s).
        /// </summary>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadForce(string loadPattern)
        {
            _apiPointObject?.DeleteLoadForce(Name, loadPattern);
            deleteLoad(loadPattern, Loads);
        }



        // LoadDisplacement
        /// <summary>
        /// Returns a list of ground displacement load assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadDisplacement()
        {
            if (_apiPointObject == null) return;
            _apiPointObject.GetLoadDisplacement(Name,
            out var names,
                out var loadPatterns,
                out var loadPatternSteps,
                out var coordinateSystem,
                out var displacements);

            _displacements = new List<NodeLoadDisplacement>();
            for (int i = 0; i < names.Length; i++)
            {
                NodeLoadDisplacement displacementLoads = new NodeLoadDisplacement
                {
                    LoadPattern = loadPatterns[i],
                    CoordinateSystem = coordinateSystem[i],
                    Displacement = displacements[i],
                    LoadPatternSteps = loadPatternSteps[i]
                };

                _displacements.Add(displacementLoads);
            }
        }

        /// <summary>
        /// Sets ground displacement load assignments to point objects.
        /// </summary>
        /// <param name="displacement">The displacement.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoadDisplacement(NodeLoadDisplacement displacement)
        {
            setLoadDisplacement(displacement, replace: false);
            _displacements.Add(displacement);
        }

        /// <summary>
        /// Sets ground displacement load assignments to point objects.
        /// </summary>
        /// <param name="displacement">The displacement.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ReplaceLoadDisplacement(NodeLoadDisplacement displacement)
        {
            setLoadDisplacement(displacement, replace: true);
            _displacements = new List<NodeLoadDisplacement>(){ displacement };
        }

        /// <summary>
        /// Sets ground displacement load assignments to point objects.
        /// </summary>
        /// <param name="displacement">The displacement.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadDisplacement(
            NodeLoadDisplacement displacement,
            bool replace)
        {
            _apiPointObject?.SetLoadDisplacement(Name,
                displacement.LoadPattern,
                displacement.Displacement,
                replace: replace,
                coordinateSystem: displacement.CoordinateSystem);
        }


        /// <summary>
        /// Deletes all ground displacement load assignments, for the specified load pattern, from the specified point object(s).
        /// </summary>
        /// <param name="loadPattern">The name of a defined load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDisplacement(string loadPattern)
        {
            _apiPointObject?.DeleteLoadDisplacement(Name, loadPattern);
            deleteLoad(loadPattern, Displacements);
        }
        #endregion

    }
}
