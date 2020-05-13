// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-11-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PointElement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Represents the point element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IPointElement" />
    public class PointElement : CSiApiBase, IPointElement
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="PointElement" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PointElement(CSiApiSeed seed) : base(seed) {}

        #endregion

        #region Query
        /// <summary>
        /// Returns the total number of defined point elements in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PointElm.Count();
        }


        /// <summary>
        /// If the <paramref name="name" /> item is provided, the function returns the total number of constraint assignments made to the specified point element.
        /// If the <paramref name="name" /> item is not specified or is specified as an empty string, the function returns the total number of constraint assignments to all point elements in the model.
        /// If the <paramref name="name" /> item is specified but it is not recognized by the program as a valid point element, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int CountConstraint(string name = "")
        {
            int count = 0;

            _callCode = _sapModel.PointElm.CountConstraint(ref count, name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }

        /// <summary>
        /// Returns the total number of point elements in the model with restraint assignments.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CountRestraint()
        {
            return _sapModel.PointElm.CountRestraint();
        }

        /// <summary>
        /// Returns the total number of point elements in the model with spring assignments.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CountSpring()
        {
            return _sapModel.PointElm.CountSpring();
        }

        /// <summary>
        /// If neither the <paramref name="name" /> item nor the <paramref name="loadPattern" /> item is provided, the function returns the total number of point load assignments to point elements in the model.
        /// If the <paramref name="name" /> item is provided but not the <paramref name="loadPattern" /> item, the function returns the total number of point load assignments made for the specified point element.
        /// If the <paramref name="name" /> item is not provided but the <paramref name="loadPattern" /> item is specified, the function returns the total number of point load assignments made to all point elements for the specified load pattern.
        /// If both the <paramref name="name" /> item and the <paramref name="loadPattern" /> item are provided, the function the total number of point load assignments made to the specified point element for the specified load pattern.
        /// If the <paramref name="name" /> item or the <paramref name="loadPattern" /> item is provided but is not recognized by the program as valid, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="loadPattern">The name of an existing load pattern.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int CountLoadForce(string name = "",
            string loadPattern = "")
        {
            int count = 0;

            _callCode = _sapModel.PointElm.CountLoadForce(ref count, name, loadPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }

        /// <summary>
        /// If neither the <paramref name="name" /> item nor the <paramref name="loadPattern" /> item is provided, the function returns the total number of ground displacement load assignments to point elements in the model.
        /// If the <paramref name="name" /> item is provided but not the <paramref name="loadPattern" /> item, the function returns the total number of ground displacement load assignments made for the specified point element.
        /// If the <paramref name="name" /> item is not provided but the <paramref name="loadPattern" /> item is specified, the function returns the total number of ground displacement load assignments made to all point elements for the specified load pattern.
        /// If both the <paramref name="name" /> item and the <paramref name="loadPattern" /> item are provided, the function the total number of ground displacement load assignments made to the specified point element for the specified load pattern.
        /// If the <paramref name="name" /> item or the <paramref name="loadPattern" /> item is provided but is not recognized by the program as valid, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="loadPattern">The name of an existing load pattern.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int CountLoadDisplacements(string name = "",
            string loadPattern = "")
        {
            int count = 0;

            _callCode = _sapModel.PointElm.CountLoadDispl(ref count, name, loadPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return count;
        }


        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.PointElm.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        
        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameElement">The name of an existing element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public DirectionCosines GetTransformationMatrix(string nameElement)
        {
            double[] directionCosinesArray = new double[9];
            _callCode = _sapModel.PointElm.GetTransformationMatrix(nameElement, ref directionCosinesArray);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            DirectionCosines directionCosines = new DirectionCosines();
            directionCosines.FromArray(directionCosinesArray);
            return directionCosines;
        }

        /// <summary>
        /// Returns the name of the object from which the element was created.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetObject(string name)
        {
            GetObject(name, out var nameObject, out var csiObjectType);
            return nameObject;
        }

        /// <summary>
        /// Returns the name of the point object from which a point element was created.
        /// It also retrieves the type of point object that it is.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <param name="nameObject">The name of the point object from which the point element was created.</param>
        /// <param name="objectType">Type of object or defined item that is associated with the point element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetObject(string name,
            out string nameObject,
            out ePointTypeObject objectType)
        {
            nameObject = string.Empty;
            int csiObjectType = 0;
            _callCode = _sapModel.PointElm.GetObj(name, ref nameObject, ref csiObjectType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectType = (ePointTypeObject)csiObjectType;
        }



        /// <summary>
        /// Returns the cartesian x-, y-, z-coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCartesian GetCoordinate(string name,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double x = 0;
            double y = 0;
            double z = 0;

            _callCode = _sapModel.PointElm.GetCoordCartesian(name, ref x, ref y, ref z, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            Coordinate3DCartesian coordinate = new Coordinate3DCartesian
            {
                X = x,
                Y = y,
                Z = z
            };
            return coordinate;
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the cylindrical r-, theta-, z-coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DCylindrical GetCoordinateCylindrical(string name,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double radius = 0;
            double theta = 0;
            double z = 0;

            _callCode = _sapModel.PointElm.GetCoordCylindrical(name, ref radius, ref theta, ref z, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            Coordinate3DCylindrical coordinate = new Coordinate3DCylindrical();
            coordinate.Radius = radius;
            coordinate.Theta = theta;
            coordinate.Z = z;
            return coordinate;
        }

        /// <summary>
        /// Returns the spherical r-, a-, b-coordinates of the specified point element/object in the Present Units.
        /// The coordinates are reported in the coordinate system specified by <paramref name="coordinateSystem" />.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the joint coordinates are returned.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Coordinate3DSpherical GetCoordinateSpherical(string name,
            string coordinateSystem = CoordinateSystems.Global)
        {
            double radius = 0;
            double theta = 0;
            double phi = 0;

            _callCode = _sapModel.PointElm.GetCoordSpherical(name, ref radius, ref theta, ref phi, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            Coordinate3DSpherical coordinate = new Coordinate3DSpherical();
            coordinate.Radius = radius;
            coordinate.Theta = theta;
            coordinate.Phi = phi;
            return coordinate;
        }
#endif

        /// <summary>
        /// Returns a list of elements connected to a specified point element/object.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="objectTypes">The element/object type of each element/object connected to the specified point element/object.</param>
        /// <param name="objectNames">The element/object name of each element/object connected to the specified point element/object.</param>
        /// <param name="pointNumbers">The point number within the considered element/object that corresponds to the specified point element/object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetConnectivity(string name,
            out eObjectType[] objectTypes,
            out string[] objectNames,
            out int[] pointNumbers)
        {
            objectNames = new string[0];
            pointNumbers = new int[0];
            int[] csiObjectTypes = new int[0];
            
            _callCode = _sapModel.PointElm.GetConnectivity(name, ref _numberOfItems, ref csiObjectTypes, ref objectNames, ref pointNumbers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            objectTypes = csiObjectTypes.Cast<eObjectType>().ToArray();
        }
        #endregion

        #region Axes
        /// <summary>
        /// Returns the local axis angle assignment for the point element.
        /// This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public AngleLocalAxes GetLocalAxes(string name)
        {
            double angleA = 0;
            double angleB = 0;
            double angleC = 0;

            _callCode = _sapModel.PointElm.GetLocalAxes(name, 
                ref angleA, 
                ref angleB, 
                ref angleC);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            AngleLocalAxes angleOffset = new AngleLocalAxes
            {
                AngleA = angleA,
                AngleB = angleB,
                AngleC = angleC
            };
            return angleOffset;
        }


        #endregion


        #region Point Properties

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the merge number for a point element/object.
        /// By default the merge number for a point is zero.
        /// Points with different merge numbers are not automatically merged by the program.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="mergeNumber">The merge number assigned to the specified point element/object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetMergeNumber(string name,
            ref int mergeNumber)
        {
            _callCode = _sapModel.PointElm.GetMergeNumber(name, ref mergeNumber);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        /// <summary>
        /// Returns the joint pattern value for a specific point element/object and joint pattern.
        /// Joint pattern values are unitless.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="patternName">The name of a defined joint pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public double GetPatternValue(string name,
            string patternName)
        {
            double value = -1;
            _callCode = _sapModel.PointElm.GetPatternValue(name, patternName, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return value;
        }

        #endregion

        #region Support & Connections
        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetConstraint(string name,
            out string[] pointNames,
            out string[] constraintNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            pointNames = new string[0];
            constraintNames = new string[0];
            _callCode = _sapModel.PointElm.GetConstraint(name, ref _numberOfItems, 
                ref pointNames, ref constraintNames, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns a list of constraint assignments made to one or more specified point elements.
        /// These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public DegreesOfFreedomLocal GetRestraint(string name)
        {

            bool[] values = new bool[0];

            _callCode = _sapModel.PointElm.GetRestraint(name, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            DegreesOfFreedomLocal degreesOfFreedom = new DegreesOfFreedomLocal();
            degreesOfFreedom.FromArray(values);
            return degreesOfFreedom;
        }



        /// <summary>
        /// Returns uncoupled spring stiffness assignments for a point element;
        /// that is, it retrieves the diagonal terms in the 6x6 spring matrix for the point element.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point element either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public Stiffness GetSpring(string name)
        {
            double[] csiStiffnesses = new double[0];

            _callCode = _sapModel.PointElm.GetSpring(name, ref csiStiffnesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            Stiffness stiffnesses = new Stiffness();
            stiffnesses.FromArray(csiStiffnesses);
            return stiffnesses;
        }

        /// <summary>
        /// Returns coupled spring stiffness assignments for a point element;
        /// that is, it retrieves the spring matrix of 21 stiffness values for the point element.
        /// The spring stiffnesses reported are the sum of all springs assigned to the point element either directly or indirectly through line, area and solid spring assignments.
        /// The spring stiffness values are reported in the point local coordinate system.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public StiffnessCoupled GetSpringCoupled(string name)
        {
            double[] csiStiffnesses = new double[0];

            _callCode = _sapModel.PointElm.GetSpringCoupled(name, ref csiStiffnesses);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            StiffnessCoupled stiffnesses = new StiffnessCoupled();
            stiffnesses.FromArray(csiStiffnesses);
            return stiffnesses;
        }

        /// <summary>
        /// This function indicates if the spring assignments to a point element are coupled, that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <returns><c>true</c> if [is spring coupled] [the specified name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool IsSpringCoupled(string name)
        {
            bool isCoupled = false;
            
            _callCode = _sapModel.PointElm.IsSpringCoupled(name, ref isCoupled);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return isCoupled;
        }


        #endregion


        #region Loads

        /// <summary>
        /// Returns a list of force load assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of force load assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified force load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="forces">The forces assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadForce(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            double[] F1 = new double[0];
            double[] F2 = new double[0];
            double[] F3 = new double[0];
            double[] M1 = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            _callCode = _sapModel.PointElm.GetLoadForce(name, ref numberItems, 
                ref pointNames, ref loadPatterns, ref loadPatternSteps, ref coordinateSystem, 
                ref F1, ref F2, ref F3, ref M1, ref M2, ref M3,  
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forces = new Loads[numberItems-1];
            for (int i = 0; i < numberItems; i++)
            {
                forces[i].F1 = F1[i];
                forces[i].F2 = F2[i];
                forces[i].F3 = F3[i];
                forces[i].M1 = M1[i];
                forces[i].M2 = M2[i];
                forces[i].M3 = M3[i];
            }
        }



        /// <summary>
        /// Returns a list of ground displacement load assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of ground displacement load assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified ground displacement load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the ground displacement.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="displacements">The ground displacements assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadDisplacement(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Displacements[] displacements,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.PointElm.GetLoadDispl(name, ref numberItems, 
                ref pointNames, ref loadPatterns, ref loadPatternSteps, ref coordinateSystem, 
                ref U1, ref U2, ref U3, ref R1, ref R2, ref R3, 
                EnumLibrary.Convert<eItemTypeElement, CSiProgram.eItemTypeElm>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            displacements = new Displacements[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                displacements[i].UX = U1[i];
                displacements[i].UY = U2[i];
                displacements[i].UZ = U3[i];
                displacements[i].RX = R1[i];
                displacements[i].RY = R2[i];
                displacements[i].RZ = R3[i];
            }
        }

        #endregion
    }
}
