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
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using eObjectType = MPT.CSI.API.Core.Program.ModelBehavior.eObjectType;
using MassObject = MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses.Mass;


namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Node.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject" />
    public class Node : StructureObject
    {
        /// <summary>
        /// Gets the point object.
        /// </summary>
        /// <value>The point object.</value>
        protected static PointObject _pointObject => Registry.ObjectModeler?.PointObject;

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public JointResults Results { get; protected set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; protected set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; protected set; }
        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; protected set; }

        /// <summary>
        /// These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.
        /// </summary>
        /// <value>The degrees of freedom.</value>
        public DegreesOfFreedomLocal DegreesOfFreedom { get; protected set; }


        /// <summary>
        /// The panel zone associated with the node.
        /// </summary>
        /// <value>The panel zone.</value>
        public PanelZone PanelZone { get; protected set; }


        /// <summary>
        /// Spring stiffness values for each decoupled degree of freedom.
        /// </summary>
        /// <value>The stiffness.</value>
        public Stiffness Stiffness { get; protected set; }

        /// <summary>
        /// Spring stiffness values for each coupled degree of freedom.
        /// </summary>
        /// <value>The stiffness coupled.</value>
        public StiffnessCoupled StiffnessCoupled { get; protected set; }

        /// <summary>
        /// Indicates if the spring assignments to a point object are coupled;
        /// that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <value><c>true</c> if this instance is spring coupled; otherwise, <c>false</c>.</value>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool IsSpringCoupled { get; protected set; }


        /// <summary>
        /// True: Specified mass assignments are in the point object local coordinate system.
        /// False: Assignments are in the Global coordinate system.
        /// </summary>
        /// <value><c>true</c> if this instance is local coordinate system; otherwise, <c>false</c>.</value>
        public bool IsLocalCoordinateSystem { get; protected set; }


        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        /// <value>The mass.</value>
        public MassObject Mass { get; protected set; }


        /// <summary>
        /// Gets or sets the diaphragm.
        /// </summary>
        /// <value>The diaphragm.</value>
        public Diaphragm Diaphragm { get; protected set; }


        /// <summary>
        /// Gets or sets the loads.
        /// </summary>
        /// <value>The loads.</value>
        public List<NodeLoad> Loads { get; protected set; } = new List<NodeLoad>();
        /// <summary>
        /// Gets or sets the displacements.
        /// </summary>
        /// <value>The displacements.</value>
        public List<NodeLoadDisplacement> Displacements { get; protected set; } = new List<NodeLoadDisplacement>();


        /// <summary>
        /// True: This instance is a special point.
        /// </summary>
        /// <value><c>true</c> if this instance is special point; otherwise, <c>false</c>.</value>
        public bool IsSpecialPoint { get; protected set; }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List&lt;Node&gt;.</returns>
        public static List<Node> GetAll()
        {
            List<Node> objects = new List<Node>();
            if (_pointObject == null) return objects;

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _pointObject.GetAllPoints(
                out var pointNames,
                out var coordinates,
                Constants.CoordinateSystem);
            for (int i = 0; i < pointNames.Length; i++)
            {
                Node node = Factory(coordinates[i].X, coordinates[i].Y, coordinates[i].Z, pointNames[i]);
                objects.Add(node);
            }
#else
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                Node node = Factory(name);
                objects.Add(node);
            }
#endif

            return objects;
        }


        /// <summary>
        /// Factories the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Node.</returns>
        public static Node Factory(double x, double y, double z, string uniqueName)
        {
            if (Registry.Nodes.Keys.Contains(uniqueName)) return Registry.Nodes[uniqueName];

            Node node = new Node(x, y, z, uniqueName);

            if (_pointObject != null)
            {
                node.FillData();
            }

            Registry.Nodes.Add(uniqueName, node);
            return node;
        }

        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        public static Node Factory(string uniqueName)
        {
            return Factory(uniqueName, _pointObject, Registry.Nodes);
        }


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        public static List<UniqueLabelNamePair> GetLabelNameList()
        {
            return getLabelNameList(_pointObject);
        }
#endif

        // IListableNames
        /// <summary>
        /// Returns the names of all defined point properties.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList()
        {
            return getNameList(_pointObject);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node() : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Node(string name) : base (name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="name">The name.</param>
        public Node(double x, double y, double z, string name = "") : base(name)
        {
            X = x;
            Y = y;
            Z = z;
        }


        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            FillLabelFromName();
            if (X == 0 && Y == 0 && Z == 0)
            {
                FillCoordinates();
            }
            Results = new JointResults(Name);
        }

        #region Query

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
           getNameFromLabel(_pointObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_pointObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_pointObject);
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified point object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_pointObject);
        }

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID()
        {
            setGUID(_pointObject);
        }


        /// <summary>
        /// Retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_pointObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            getTransformationMatrix(_pointObject);
        }



        /// <summary>
        /// Returns the x, y and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillCoordinates()
        {
            if (_pointObject == null) return;
            Coordinate3DCartesian coordinate = GetCoordinate();
            X = coordinate.X;
            Y = coordinate.Y;
            Z = coordinate.Z;
        }

        /// <summary>
        /// Returns the x, y and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DCartesian.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCartesian GetCoordinate()
        {
            return _pointObject?.GetCoordinate(Name, Constants.CoordinateSystem) ?? new Coordinate3DCartesian();
        }

        /// <summary>
        /// Returns the r, Theta and z coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DCylindrical.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCylindrical GetCoordinateCylindrical()
        {
            return _pointObject?.GetCoordinateCylindrical(Name, Constants.CoordinateSystem) ?? new Coordinate3DCylindrical();
        }

        /// <summary>
        /// Returns the r, a and b coordinates of the specified point object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <see cref="Constants.CoordinateSystem" />.
        /// </summary>
        /// <returns>Coordinate3DSpherical.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DSpherical GetCoordinateSpherical()
        {
            return _pointObject?.GetCoordinateSpherical(Name, Constants.CoordinateSystem) ?? new Coordinate3DSpherical();
        }

        /// <summary>
        /// Returns a list of objects and corresponding point numbers connected to a specified point object.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;eObjectType, System.String, System.Int32&gt;&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public List<Tuple<eObjectType, string, int>> GetConnectivity()
        {
            if (_pointObject == null) return new List<Tuple<eObjectType, string, int>>();
            _pointObject.GetConnectivity(Name, out var objectTypes, out var objectNames, out var pointNumbers);

            return objectTypes.Select((t, i) => new Tuple<eObjectType, string, int>(t, objectNames[i], pointNumbers[i])).ToList();
        }

        /// <summary>
        /// Returns the total number of objects (line, area, solid and link) that connect to the specified point object.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int NumberOfObjectsConnected()
        {
            return _pointObject?.GetCommonTo(Name) ?? 0;
        }
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_pointObject);
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
            _pointObject
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
        public void GetLocalAxesAdvanced(string name,
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
            _pointObject
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
            _pointObject
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
            changeName(_pointObject, newName);
        }


        /// <summary>
        /// Deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        public override void Delete()
        {
            delete(_pointObject);
        }


        /// <summary>
        /// Adds a point object to a model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <see cref="Name" /> is specified, the program assigns a default name to the object.
        /// If a <see cref="Name" /> is specified and that name is not used for another object, the <see cref="Name" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.
        /// </summary>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all point objects have a merge number of zero.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddByCoordinate(bool mergeOff = false,
            int mergeNumber = 0)
        {
            Coordinate3DCartesian coordinate = new Coordinate3DCartesian
            {
                X = X,
                Y = Y,
                Z = Z
            };
            
            Label = _pointObject?.AddByCoordinate(
                        coordinate,
                        Name,
                        Constants.CoordinateSystem,
                        mergeOff,
                        mergeNumber); 
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
        public void AddByCoordinateCylindrical(
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _pointObject
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
        public void AddByCoordinateSpherical(
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            _pointObject
        }
#endif
        #endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void GetSelected()
        {
            getSelected(_pointObject);
        }

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Select()
        {
            base.Select();
            setSelected();
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Deselect()
        {
            base.Deselect();
            setSelected();
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected()
        {
            setSelected(_pointObject);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_pointObject);
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
            if (_pointObject == null) return;
            Mass = new MassObject(_pointObject.GetMass(Name));
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// </summary>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMass(bool replace = false)
        {
            _pointObject?.SetMass(Name, Mass.GetMass(), Mass.IsLocalCoordinateSystem, replace);
        }

        /// <summary>
        /// Assigns point mass to a point object.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="materialProperty">The name of an existing material property.</param>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassByVolume(string materialProperty,
            bool replace = false)
        {
            _pointObject?.SetMass(Name, Mass.GetMassByVolume(materialProperty), materialProperty, Mass.IsLocalCoordinateSystem, replace);
        }

        /// <summary>
        /// Assigns point mass to a point object as a mass by weight.
        /// </summary>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassByWeight(bool replace = false)
        {
            _pointObject?.SetMass(Name, Mass.GetMassByWeight(), Mass.IsLocalCoordinateSystem, replace);
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteMass()
        {
            _pointObject?.DeleteMass(Name);
            Mass = null;
        }
        #endregion

        #region Point Properties
        /// <summary>
        /// Retrieves the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillSpecialPoint()
        {
            if (_pointObject == null) return;
            IsSpecialPoint = _pointObject.GetSpecialPoint(Name);
        }

        /// <summary>
        /// Sets the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSpecialPoint()
        {
            _pointObject?.SetSpecialPoint(Name, IsSpecialPoint);
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
            _pointObject?.DeleteSpecialPoint(Name);
            IsSpecialPoint = false;
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the merge number for a point object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int GetMergeNumber()
        {
            _pointObject
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
            _pointObject
        }





        /// <summary>
        /// Returns the joint pattern value for a specific point object and joint pattern.
        /// Joint pattern values are unitless.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public double GetPatternValue(string patternName)
        {
            _pointObject
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
            _pointObject
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
            _pointObject
        }

        /// <summary>
        /// Deletes all joint pattern assignments, associated with the specified joint pattern, from the specified point object(s).
        /// </summary>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePatternValue(string patternName)
        {
            _pointObject
        }
#endif

        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillPanelZone()
        {
            PanelZone = PanelZone.Factory(Name);
        }

        /// <summary>
        /// Sets panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPanelZone()
        {
            PanelZone?.SetPanelZone();
        }

        /// <summary>
        /// Deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePanelZone()
        {
            PanelZone?.DeletePanelZone();
            PanelZone = null;
        }
        #endregion

        #region Support & Connections
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        public void FillDiaphragm()
        {
            Diaphragm = Diaphragm.GetDiaphragm(this);
        }

        /// <summary>
        /// Assigns a diaphragm to an object.
        /// </summary>
        /// <param name="diaphragmName">Name of the diaphragm.</param>
        public void AddToDiaphragm(string diaphragmName)
        {
            if (Diaphragm == null)
            {
                Diaphragm = Diaphragm.Factory(diaphragmName);
            }
            Diaphragm?.AddToDiaphragm(this);
        }

        /// <summary>
        /// Removes an object from the diaphragm.
        /// </summary>
        /// <param name="diaphragmName">Name of the diaphragm.</param>
        public void RemoveFromDiaphragm(string diaphragmName)
        { 
            if (Diaphragm == null)
            {
                Diaphragm = Diaphragm.GetDiaphragm(this);
                if (Diaphragm == null) return;
            }
            Diaphragm?.RemoveFromDiaphragm(this);
            Diaphragm = null;
        }
#else
        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetConstraint(ref string[] pointNames,
            ref string[] constraintNames)
        {
            _pointObject
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
            _pointObject
        }

        /// <summary>
        /// Deletes all constraint assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteConstraint()
        {
            _pointObject
        }
#endif

        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillRestraint()
        {
            if (_pointObject == null) return;
            DegreesOfFreedom = _pointObject.GetRestraint(Name);
        }

        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRestraint()
        {
            _pointObject?.SetRestraint(Name, DegreesOfFreedom);
        }

        /// <summary>
        /// Deletes all restraint assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteRestraint()
        {
            _pointObject?.DeleteRestraint(Name);
            DegreesOfFreedom = new DegreesOfFreedomLocal();
        }


        // TODO: Spring object?
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_pointObject);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        public override void SetSpringAssignment()
        {
            setSpringAssignment(_pointObject);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _pointObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }

        /// <summary>
        /// This function indicates if the spring assignments to a point object are coupled, that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <returns><c>true</c> if [is spring coupled] [the specified name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillIsSpringCoupled()
        {
            if (_pointObject == null) return;
            IsSpringCoupled = _pointObject.IsSpringCoupled(Name);
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
            if (_pointObject == null) return;
            Stiffness = _pointObject.GetSpring(Name);
        }

        /// <summary>
        /// Assigns uncoupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSpring(bool replace = false)
        {
            _pointObject?.SetSpring(Name, Stiffness, IsLocalCoordinateSystem, replace);
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
            if (_pointObject == null) return;
            StiffnessCoupled = _pointObject.GetSpringCoupled(Name);
        }

        /// <summary>
        /// Assigns coupled spring stiffness assignments to a point object.
        /// </summary>
        /// <param name="replace">True: All existing point mass assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSpringCoupled(bool replace = false)
        {
            _pointObject?.SetSpringCoupled(Name, StiffnessCoupled, IsLocalCoordinateSystem, replace);
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
            if (_pointObject == null) return;
            // TODO: Add to API project?
            _pointObject.GetLoadForce(Name,
                out var names,
                out var loadPatterns,
                out var loadPatternSteps,
                out var coordinateSystem,
                out var forces);
            
            for (int i = 0; i < names.Length; i++)
            {
                NodeLoad load = new NodeLoad
                {
                    LoadPattern = loadPatterns[i],
                    CoordinateSystem = coordinateSystem[i],
                    Force = forces[i],
                    LoadPatternSteps = loadPatternSteps[i]
                };

                Loads.Add(load);
            }
        }


        /// <summary>
        /// Sets point load assignments to point objects.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadForce(NodeLoad load,
            bool replace = false)
        {
            _pointObject?.SetLoadForce(Name,
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
            _pointObject?.DeleteLoadForce(Name, loadPattern);
            deleteLoad(loadPattern, Loads);
        }



        // LoadDisplacement
        /// <summary>
        /// Returns a list of ground displacement load assignments made to one or more specified point objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoadDisplacement()
        {
            if (_pointObject == null) return;
            // TODO: Add to API project?
            _pointObject.GetLoadDisplacement(Name,
            out var names,
                out var loadPatterns,
                out var loadPatternSteps,
                out var coordinateSystem,
                out var displacements);

            for (int i = 0; i < names.Length; i++)
            {
                NodeLoadDisplacement displacementLoads = new NodeLoadDisplacement
                {
                    LoadPattern = loadPatterns[i],
                    CoordinateSystem = coordinateSystem[i],
                    Displacement = displacements[i],
                    LoadPatternSteps = loadPatternSteps[i]
                };

                Displacements.Add(displacementLoads);
            }
        }

        /// <summary>
        /// Sets ground displacement load assignments to point objects.
        /// </summary>
        /// <param name="displacement">The displacement.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadDisplacement(NodeLoadDisplacement displacement,
            bool replace = false)
        {
            _pointObject?.SetLoadDisplacement(Name,
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
            _pointObject?.DeleteLoadDisplacement(Name, loadPattern);
            deleteLoad(loadPattern, Displacements);
        }
        #endregion


        /// <summary>
        /// Determines whether the specified node is equal.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if the specified node is equal; otherwise, <c>false</c>.</returns>
        public bool IsEqual(Node node)
        {
            return (Math.Abs(node.X - X) < Constants.Tolerance && 
                    Math.Abs(node.Y - Y) < Constants.Tolerance && 
                    Math.Abs(node.Z - Z) < Constants.Tolerance);
        }

        /// <summary>
        /// Angles the x.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>System.Double.</returns>
        public double AngleX(Node node)
        {
           return Math.Atan((node.Z - Z) / (node.Y - Y));
        }

        /// <summary>
        /// Angles the y.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>System.Double.</returns>
        public double AngleY(Node node)
        {
            return Math.Atan((node.Z - Z) / (node.X - X));
        }

        /// <summary>
        /// Angles the z.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>System.Double.</returns>
        public double AngleZ(Node node)
        {
            return Math.Atan((node.Y - Y) / (node.X - X));
        }

        /// <summary>
        /// Lengthes this instance.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Lengthes the xy.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double LengthXY()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="nodeI">The node i.</param>
        /// <param name="nodeJ">The node j.</param>
        /// <returns>The result of the operator.</returns>
        public static Node operator -(Node nodeI, Node nodeJ)
        {
            return new Node(
                nodeI.X - nodeJ.X,
                nodeI.Y - nodeJ.Y,
                nodeI.Z - nodeJ.Z,
                "delta-" + nodeI.Name + "-" + nodeJ.Name);
        }
    }
}
