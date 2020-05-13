// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="FrameObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
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

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Represents the frame object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameObject" />
    public class FrameObject : CSiApiBase, IFrameObject
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameObject" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public FrameObject(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Query  
        /// <summary>
        /// Returns the total number of defined frame properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.FrameObj.Count();
        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.FrameObj.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names of all defined frame object properties for a given story.
        /// </summary>
        /// <param name="storyName">Name of the story to filter the area names by.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameListOnStory(string storyName)
        {
            string[] names = new string[0];
            _callCode = _sapModel.FrameObj.GetNameListOnStory(storyName, ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public UniqueLabel GetLabelFromName(string name)
        {
            string label = string.Empty;
            string story = string.Empty;
            _callCode = _sapModel.FrameObj.GetLabelFromName(name, ref label, ref story);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return new UniqueLabel { Label = label, Story = story };
        }

        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public UniqueLabelNamePair[] GetLabelNameList()
        {
            string[] names = new string[0];
            string[] labels = new string[0];
            string[] stories = new string[0];
            _callCode = _sapModel.FrameObj.GetLabelNameList(ref _numberOfItems, ref names, ref labels, ref stories);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            UniqueLabelNamePair[] uniqueLabelNamePairs = new UniqueLabelNamePair[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                UniqueLabelNamePair uniqueLabelNamePair = new UniqueLabelNamePair { Name = names[i], Label = labels[i], Story = stories[i] };
                uniqueLabelNamePairs[i] = uniqueLabelNamePair;
            }

            return uniqueLabelNamePairs;
        }

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level.
        /// </summary>
        /// <param name="label">The object label.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetNameFromLabel(UniqueLabel label)
        {
            string name = string.Empty;
            _callCode = _sapModel.FrameObj.GetNameFromLabel(label.Label, label.Story, ref name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return name;
        }
#endif
        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameObject">The name of an existing object.</param>
        /// <param name="isGlobal">True: Transformation matrix is between the Global coordinate system and the object local coordinate system.
        /// False: Transformation matrix is between the present coordinate system and the object local coordinate system.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public DirectionCosines GetTransformationMatrix(string nameObject,
            bool isGlobal = true)
        {
            double[] directionCosinesArray = new double[9];
            _callCode = _sapModel.FrameObj.GetTransformationMatrix(nameObject, ref directionCosinesArray, isGlobal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            DirectionCosines directionCosines = new DirectionCosines();
            directionCosines.FromArray(directionCosinesArray);
            return directionCosines;
        }

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <param name="name">The name of an existing link object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetPoints(string name)
        {
            string[] points = new string[0];
            string point1 = "";
            string point2 = "";

            _callCode = _sapModel.FrameObj.GetPoints(name, ref point1, ref point2);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            points = new string[2];
            points[0] = point1;
            points[1] = point2;
            return points;
        }

        /// <summary>
        /// Returns the GUID (Global Unique ID) for the specified object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetGUID(string name)
        {
            string GUID = string.Empty;
            _callCode = _sapModel.FrameObj.GetGUID(name, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return GUID;
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGUID(string name,
            string GUID = "")
        {
            _callCode = _sapModel.FrameObj.SetGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns the name of each element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetElement(string name)
        {
            GetElement(name, out var elementNames, out var relativeDistanceI, out var relativeDistanceJ);
            return elementNames;
        }

        /// <summary>
        /// Returns the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="elementNames">The name of each element created from the specified object.</param>
        /// <param name="relativeDistanceI">The relative distance along the frame object to the I-End of the frame element.</param>
        /// <param name="relativeDistanceJ">The relative distance along the frame object to the J-End of the frame element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetElement(string name,
            out string[] elementNames,
            out double[] relativeDistanceI,
            out double[] relativeDistanceJ)
        {
            elementNames = new string[0];
            relativeDistanceI = new double[0];
            relativeDistanceJ = new double[0];
            _callCode = _sapModel.FrameObj.GetElm(name, 
                ref _numberOfItems, 
                ref elementNames,
                ref relativeDistanceI, 
                ref relativeDistanceJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Axes

        /// <summary>GetMass
        /// Returns the local axis angle assignment of the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]</param>
        /// <param name="isAdvanced">True: Object local axes orientation was obtained using advanced local axes parameters.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLocalAxes(string name,
            out AngleLocalAxes angleOffset,
            out bool isAdvanced)
        {
            isAdvanced = false;
            double angle = 0;

            _callCode = _sapModel.FrameObj.GetLocalAxes(name, ref angle, ref isAdvanced);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            angleOffset = new AngleLocalAxes {AngleA = angle};
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLocalAxes(name, 
                            angleOffset.AngleA, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
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
            out bool isActive,
            out int plane2,
            out eReferenceVector planeVectorOption,
            out string planeCoordinateSystem,
            out eReferenceVectorDirection[] planeVectorDirection,
            out string[] planePoint,
            out double[] planeReferenceVector)
        {
            int csiPlaneVectorOption = 0;
            int[] csiPlaneVectorDirection = new int[0];

            _callCode = _sapModel.FrameObj.GetLocalAxesAdvanced(name, ref isActive, ref plane2, ref csiPlaneVectorOption, ref planeCoordinateSystem, ref csiPlaneVectorDirection, ref planePoint, ref planeReferenceVector);
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

            _callCode = _sapModel.FrameObj.SetLocalAxesAdvanced(name, 
                            isActive, plane2, (int)planeVectorOption, planeCoordinateSystem, ref csiPlaneVectorDirection, 
                            ref planePoint, ref planeReferenceVector, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Modifiers

        /// <summary>
        /// Returns the unitless modifier assignments.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public FrameModifier GetModifiers(string name)
        {
            double[] csiModifiers = new double[8];

            _callCode = _sapModel.FrameObj.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            FrameModifier modifiers = new FrameModifier();
            modifiers.FromArray(csiModifiers);
            return modifiers;
        }

        /// <summary>
        /// This function defines the modifier assignment for frame objects.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(string name,
            FrameModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.FrameObj.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteModifiers(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteModifiers(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function gets the modification factors for axial and flexural stiffness for a frame object if the Direct Analysis method is used.
        /// TODO: Handle? The function will return nonzero if the modification factors are not available for the frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame section whose design type is Steel Frame design.</param>
        /// <param name="EAModifier">The modification factor for axial stiffness if the Direct Analysis method is used.</param>
        /// <param name="EIModifier">The modification factor for flexural stiffness if the Direct Analysis method is used.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetDirectAnalysisModifiers(string name,
            out double EAModifier,
            out double EIModifier)
        {
            _callCode = _sapModel.FrameObj.GetDAMModifiers(name, ref EAModifier, ref EIModifier);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Creation & Groups
        /// <summary>
        /// This function changes the name of an existing frame object.
        /// </summary>
        /// <param name="currentName">The existing name of a defined frame object.</param>
        /// <param name="newName">The new name for the frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.FrameObj.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// The function deletes a specified frame object.
        /// It returns an error if the specified object cannot be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.FrameObj.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



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
        /// <exception cref="CSiException">Two coordinates must be provided for a frame object. " +
        /// "Provided: " + coordinates.Length
        /// or <see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string AddByCoordinate(Coordinate3DCartesian[] coordinates,
            string propertyName = "Default",
            string uniqueName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            if (coordinates == null || coordinates.Length != 2)
            {
                throw new ArgumentException("Two coordinates must be provided for a frame object. " +
                                            "Provided: " + coordinates?.Length);
            }
            
            string name = string.Empty;
            _callCode = _sapModel.FrameObj.AddByCoord(
                coordinates[0].X, coordinates[0].Y, coordinates[0].Z,
                coordinates[1].X, coordinates[1].Y, coordinates[1].Z,
                ref name, propertyName, uniqueName, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return name;
        }


        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is Default, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <exception cref="CSiException">Two points must be provided for a frame object. " +
        /// "Provided: " + pointNames.Length
        /// or API_DEFAULT_ERROR_CODE</exception>
        public string AddByPoint(string[] pointNames,
            string propertyName = "Default",
            string uniqueName = "")
        {
            if (pointNames == null || pointNames.Length != 2)
            {
                throw new ArgumentException("Two points must be provided for a frame object. " +
                                            "Provided: " + pointNames?.Length);
            }

            string name = string.Empty;
            _callCode = _sapModel.FrameObj.AddByPoint(pointNames[0], pointNames[1], ref name, propertyName, uniqueName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return name;
        }



        /// <summary>
        /// Adds or removes objects from a specified group.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="groupName">The name of an existing group to which the assignment is made.</param>
        /// <param name="remove">False: The specified objects are added to the group specified by the <paramref name="groupName" /> item.
        /// True: The objects are removed from the group.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGroupAssign(string name,
            string groupName,
            bool remove = false,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetGroupAssign(name, 
                            groupName, remove, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        /// <param name="objectName">The name of an existing object.</param>
        /// <param name="groupNames">The names of the groups to which the object is assigned.</param>
        public void GetGroupAssign(string objectName,
            out string[] groupNames)
        {
            groupNames = new string[0];
            _callCode = _sapModel.FrameObj.GetGroupAssign(objectName,
                           ref _numberOfItems, ref groupNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        #endregion

        #region Selection
        /// <summary>
        /// Returns the selected status for an object.
        /// True: The specified object is selected.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool GetSelected(string name)
        {
            bool isSelected = false;
            _callCode = _sapModel.FrameObj.GetSelected(name, ref isSelected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return isSelected;
        }

        /// <summary>
        /// Returns the selected status for an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isSelected">True: The specified object is selected.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the selected status is set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the selected status is set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the selected status is set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSelected(string name,
            bool isSelected,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetSelected(name, 
                            isSelected, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <param name="name">The name of a defined object/element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetSection(string name)
        {
            GetSection(name, out var propertyName, out var autoSelectList);
            return propertyName;
        }

        /// <summary>
        /// Returns the section property assigned to a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        /// <param name="autoSelectList">Name of the auto select list assigned to the frame object, if any.
        /// If this item is returned as a blank string, no auto select list is assigned to the frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSection(string name,
            out string propertyName,
            out string autoSelectList)
        {
            propertyName = string.Empty;
            autoSelectList = string.Empty;

            _callCode = _sapModel.FrameObj.GetSection(name, ref propertyName, ref autoSelectList);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the frame object specified by the Name item.
        /// If this item is Group, the assignment is made to all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected frame objects, and the Name item is ignored.</param>
        public void SetSection(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
            double nonPrismaticTotalLength = 0;
            double nonPrismaticRelativeStartLocation = 0;

            SetSection(name, propertyName, nonPrismaticTotalLength, nonPrismaticRelativeStartLocation, itemType);
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        /// <param name="nonPrismaticTotalLength">Total assumed length of the nonprismatic section.
        /// Enter 0 for this item to indicate that the section length is the same as the frame object length.
        /// This item is applicable only when the assigned frame section property is a nonprismatic section.</param>
        /// <param name="nonPrismaticRelativeStartLocation">Relative distance along the nonprismatic section to the I-End (start) of the frame object.
        /// This item is ignored when <paramref name="nonPrismaticTotalLength" /> is 0.
        /// This item is applicable only when the assigned frame section property is a nonprismatic section, and the <paramref name="nonPrismaticTotalLength" /> &gt; 0.</param>
        /// <param name="itemType">If this item is Object, the assignment is made to the frame object specified by the Name item.
        /// If this item is Group, the assignment is made to all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, assignment is made to all selected frame objects, and the Name item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSection(string name,
            string propertyName,
            double nonPrismaticTotalLength,
            double nonPrismaticRelativeStartLocation,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetSection(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType), 
                            nonPrismaticTotalLength, nonPrismaticRelativeStartLocation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns the nonprismatic frame section property data assigned to a frame object.
        /// TODO: Handle: The function returns an error if the section property assigned to the frame object is not a nonprismatic property.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="propertyName">The name of the nonprismatic frame section property assigned to the frame object.</param>
        /// <param name="totalLength">This is the total assumed length of the nonprismatic section.
        /// Enter 0 for this item to indicate that the section length is the same as the frame object length.</param>
        /// <param name="relativeStartLocation">This is the relative distance along the nonprismatic section to the I-End (start) of the frame object.
        /// This item is ignored when the sVarTotalLengthitem is 0.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSectionNonPrismatic(string name,
            out string propertyName,
            out double totalLength,
            out double relativeStartLocation)
        {
            propertyName = string.Empty;
            totalLength = -1;
            relativeStartLocation = -1;
            _callCode = _sapModel.FrameObj.GetSectionNonPrismatic(name, ref propertyName, ref totalLength, ref relativeStartLocation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a trapezoidal frame section.
        /// </summary>
        /// <param name="name">The name of an existing trapezoidal frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="materialName">The name of the material property for the section.</param>
        /// <param name="sectionDepth">The section depth. [L]</param>
        /// <param name="sectionTopWidth">The section top width. [L]</param>
        /// <param name="sectionBottomWidth">The section bottom width. [L]</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTrapezoidal(string name,
            ref string fileName,
            ref string materialName,
            ref double sectionDepth,
            ref double sectionTopWidth,
            ref double sectionBottomWidth,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            _callCode = _sapModel.PropFrame.GetTrapezoidal(name, ref fileName, ref materialName, ref sectionDepth, ref sectionTopWidth, ref sectionBottomWidth, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes a solid trapezoidal frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="materialName">The name of the material property for the section.</param>
        /// <param name="sectionDepth">The section depth. [L]</param>
        /// <param name="sectionTopWidth">The section top width. [L]</param>
        /// <param name="sectionBottomWidth">The section bottom width. [L]</param>
        /// <param name="color">The display color assigned to the section.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTrapezoidal(string name,
            string materialName,
            double sectionDepth,
            double sectionTopWidth,
            double sectionBottomWidth,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetTrapezoidal(name, materialName, sectionDepth, sectionTopWidth, sectionBottomWidth, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public double GetMass(string name)
        {
            double value = 0;
            _callCode = _sapModel.FrameObj.GetMass(name, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return value;
        }

        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMass(string name,
            double value,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetMass(name, 
                            value, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteMass(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteMass(name, 
                             EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame object insertion point assignments.
        /// The assignments include the cardinal point and end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing frame object</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the line element. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the line element. [L]</param>
        /// <param name="cardinalPoint">The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="isMirroredLocal2">True: Frame object section is assumed to be mirrored (flipped) about its local 2-axis.</param>
        /// <param name="isStiffnessTransformed">True: Frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="coordinateSystem">This is <see cref="CoordinateSystems.Local" /> or the name of a defined coordinate system.
        /// It is the coordinate system in which the <paramref name="offsetDistancesI" /> and <paramref name="offsetDistancesJ" /> items are specified.</param>
        /// <exception cref="CSiException">Array " + offset1 + " should have 3 elements, but has " + offset1.Length + " elements.
        /// or
        /// Array " + offset2 + " should have 3 elements, but has " + offset2.Length + " elements.
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void GetInsertionPoint(string name,
            out Displacements offsetDistancesI,
            out Displacements offsetDistancesJ,
            out eCardinalInsertionPoint cardinalPoint,
            out bool isMirroredLocal2,
            out bool isStiffnessTransformed,
            out string coordinateSystem)
        {
            isMirroredLocal2 = false;
            isStiffnessTransformed = false;
            coordinateSystem = string.Empty;

            double[] offset1 = new double[0];
            double[] offset2 = new double[0];
            int csiCardinalPoint = 0;

            _callCode = _sapModel.FrameObj.GetInsertionPoint(name, ref csiCardinalPoint, ref isMirroredLocal2, ref isStiffnessTransformed, ref offset1, ref offset2, ref coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            cardinalPoint = (eCardinalInsertionPoint) csiCardinalPoint;

            if (offset1.Length < 3) { throw new CSiException("Array " + nameof(offset1) + " should have 3 elements, but has " + offset1.Length + " elements.");}

            offsetDistancesI = new Displacements
            {
                UX = offset1[0],
                UY = offset1[1],
                UZ = offset1[2]
            };

            if (offset2.Length < 3) { throw new CSiException("Array " + nameof(offset2) + " should have 3 elements, but has " + offset2.Length + " elements."); }

            offsetDistancesJ = new Displacements
            {
                UX = offset2[0],
                UY = offset2[1],
                UZ = offset2[2]
            };
        }

        /// <summary>
        /// Assigns frame object insertion point data.
        /// The assignments are reported as end joint offsets.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="offsetDistancesI">Three joint offset distances, in the Global coordinate system, at the I-End of the frame object. [L]</param>
        /// <param name="offsetDistancesJ">Three joint offset distances, in the Global coordinate system, at the J-End of the frame object. [L]</param>
        /// <param name="cardinalPoint">Specifies the cardinal point for the frame object.
        /// The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="isMirroredLocal2">True: The frame object section is assumed to be mirrored (flipped) about its local 2-axis.</param>
        /// <param name="isStiffnessTransformed">True: The frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.</param>
        /// <param name="coordinateSystem">This is <see cref="ePDeltaDirection.Local_1" /> or the name of a defined coordinate system.
        /// It is the coordinate system in which the <paramref name="offsetDistancesI" /> and <paramref name="offsetDistancesJ" /> items are specified.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetInsertionPoint(string name,
            Displacements offsetDistancesI,
            Displacements offsetDistancesJ,
            eCardinalInsertionPoint cardinalPoint,
            bool isMirroredLocal2,
            bool isStiffnessTransformed,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            double[] offset1 = offsetDistancesI.ToArray();
            double[] offset2 = offsetDistancesJ.ToArray();

            _callCode = _sapModel.FrameObj.SetInsertionPoint(name, 
                            (int)cardinalPoint, isMirroredLocal2, isStiffnessTransformed, ref offset1, ref offset2, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the method to determine the notional size of a frame section for the creep and shrinkage calculations.
        /// </summary>
        /// <param name="name">The name of an frame section property.</param>
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

            _callCode = _sapModel.PropFrame.GetNotionalSize(name, ref csiSizeType, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            sizeType = EnumLibrary.ConvertStringToEnumByDescription<eNotionalSizeType>(csiSizeType);
        }

        /// <summary>
        /// Assigns the method to determine the notional size of a frame section for the creep and shrinkage calculations.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
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

            _callCode = _sapModel.PropFrame.SetNotionalSize(name, csiSizeType, value);
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
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetMaterialTemperature(string name,
            out double temperature,
            out string patternName)
        {
            temperature = 0;
            patternName = string.Empty;
            _callCode = _sapModel.FrameObj.GetMatTemp(name, ref temperature, ref patternName);
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
            _callCode = _sapModel.FrameObj.SetMatTemp(name, 
                            temperature, patternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="name">The name of a defined object/element.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetMaterialOverwrite(string name)
        {
            string propertyName = string.Empty;
            _callCode = _sapModel.FrameObj.GetMaterialOverwrite(name, ref propertyName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return propertyName;
        }

        /// <summary>
        /// Sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="propertyName">This is None or a blank string, indicating that no material overwrite exists for the specified element, or it is the name of an existing material property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMaterialOverwrite(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetMaterialOverwrite(name, 
                            propertyName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns the tension/compression force limit assignments to line elements.
        /// Note that the tension and compression limits are only used in nonlinear analyses.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="limitCompressionExists">True: A compression force limit exists for the line element.</param>
        /// <param name="limitCompression">The compression force limit for the line element. [F]</param>
        /// <param name="limitTensionExists">True: A tension force limit exists for the line element.</param>
        /// <param name="limitTension">The tension force limit for the line element. [F]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTensionCompressionLimits(string name,
            ref bool limitCompressionExists,
            ref double limitCompression,
            ref bool limitTensionExists,
            ref double limitTension)
        {
            _callCode = _sapModel.FrameObj.GetTCLimits(name, ref limitCompressionExists, ref limitCompression, ref limitTensionExists, ref limitTension);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the tension/compression force limit assignments to line elements.
        /// Note that the tension and compression limits are only used in nonlinear analyses.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="limitCompressionExists">True: A compression force limit exists for the line element.</param>
        /// <param name="limitCompression">The compression force limit for the line element. [F]</param>
        /// <param name="limitTensionExists">True: A tension force limit exists for the line element.</param>
        /// <param name="limitTension">The tension force limit for the line element. [F]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTensionCompressionLimits(string name,
            bool limitCompressionExists,
            double limitCompression,
            bool limitTensionExists,
            double limitTension,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetTCLimits(name, 
                            limitCompressionExists, limitCompression, limitTensionExists, limitTension, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Frame Properties

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves select data for all area objects in the model .
        /// </summary>
        /// <param name="frameNames">The name of each frame.</param>
        /// <param name="sectionNames">The names of the sections assigned to each frame.</param>
        /// <param name="storyNames">The story name associated with each frame.</param>
        /// <param name="pointINames">Point I associated with each frame.</param>
        /// <param name="pointJNames">Point J associated with each frame.</param>
        /// <param name="pointICoordinates">Coordinates for point I of each frame. [L].</param>
        /// <param name="pointJCoordinates">Coordinates for point J of each frame. [L].</param>
        /// <param name="angles">Angle of rotation of the local axis about the local-1 axis. [deg].</param>
        /// <param name="pointIOffsets">Three joint offset distances for point I of each frame. [L].</param>
        /// <param name="pointJOffsets">Three joint offset distances for point J of each frame. [L].</param>
        /// <param name="cardinalInsertionPoints">The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the coordinates are returned.</param>
        public void GetAllFrames(out string[] frameNames,
            out string[] sectionNames,
            out string[] storyNames,
            out string[] pointINames,
            out string[] pointJNames,
            out Coordinate3DCartesian[] pointICoordinates,
            out Coordinate3DCartesian[] pointJCoordinates,
            out double[] angles,
            out Displacements[] pointIOffsets,
            out Displacements[] pointJOffsets,
            out eCardinalInsertionPoint[] cardinalInsertionPoints,
            string coordinateSystem = CoordinateSystems.Global)
        {
            frameNames = new string[0];
            sectionNames = new string[0];
            storyNames = new string[0];
            pointINames = new string[0];
            pointJNames = new string[0];
            angles = new double[0];

            int[] csiCardinalInsertionPoints = new int[0];

            double[] coordinatesIX = new double[0];
            double[] coordinatesIY = new double[0];
            double[] coordinatesIZ = new double[0];

            double[] coordinatesJX = new double[0];
            double[] coordinatesJY = new double[0];
            double[] coordinatesJZ = new double[0];

            double[] offsetsIX = new double[0];
            double[] offsetsIY = new double[0];
            double[] offsetsIZ = new double[0];

            double[] offsetsJX = new double[0];
            double[] offsetsJY = new double[0];
            double[] offsetsJZ = new double[0];

            _callCode = _sapModel.FrameObj.GetAllFrames(ref _numberOfItems, ref frameNames, ref sectionNames, ref storyNames,
                ref pointINames, ref pointJNames, 
                ref coordinatesIX, ref coordinatesIY, ref coordinatesIZ,
                ref coordinatesJX, ref coordinatesJY, ref coordinatesJZ,
                ref angles, 
                ref offsetsIX, ref offsetsIY, ref offsetsIZ,
                ref offsetsJX, ref offsetsJY, ref offsetsJZ,
                ref csiCardinalInsertionPoints,
                coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            cardinalInsertionPoints = csiCardinalInsertionPoints.Cast<eCardinalInsertionPoint>().ToArray();

            pointICoordinates = new Coordinate3DCartesian[_numberOfItems];
            pointJCoordinates = new Coordinate3DCartesian[_numberOfItems];
            pointIOffsets = new Displacements[_numberOfItems];
            pointJOffsets = new Displacements[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                pointICoordinates[i].X = coordinatesIX[i];
                pointICoordinates[i].Y = coordinatesIY[i];
                pointICoordinates[i].Z = coordinatesIZ[i];

                pointJCoordinates[i].X = coordinatesJX[i];
                pointJCoordinates[i].Y = coordinatesJY[i];
                pointJCoordinates[i].Z = coordinatesJZ[i];

                pointIOffsets[i].UX = offsetsIX[i];
                pointIOffsets[i].UY = offsetsIY[i];
                pointIOffsets[i].UZ = offsetsIZ[i];

                pointJOffsets[i].UX = offsetsJX[i];
                pointJOffsets[i].UY = offsetsJY[i];
                pointJOffsets[i].UZ = offsetsJZ[i];
            }
        }
        
        /// <summary>
        /// Retrieves support data for a given frame beam object.
        /// </summary>
        /// <param name="name">The name of an existing frame beam object.</param>
        /// <param name="supportNameI">The name of the column frame object, beam frame object or wall area object which supports the beam at its start node.</param>
        /// <param name="supportTypeI">The type of support at the start node.</param>
        /// <param name="supportNameJ">The name of the column frame object, beam frame object or wall area object which supports the beam at its end node.</param>
        /// <param name="supportTypeJ">The type of support at the end node.</param>
        public void GetSupports(string name,
            out string supportNameI,
            out eSupportType supportTypeI,
            out string supportNameJ,
            out eSupportType supportTypeJ)
        {
            supportNameI = string.Empty;
            supportNameJ = string.Empty;
            CSiProgram.eObjType csiSupportTypeI = CSiProgram.eObjType.Area;
            CSiProgram.eObjType csiSupportTypeJ = CSiProgram.eObjType.Area;

            _callCode = _sapModel.FrameObj.GetSupports(name, 
                            ref supportNameI, ref csiSupportTypeI,
                            ref supportNameJ, ref csiSupportTypeJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }


            supportTypeI = 0;
            supportTypeJ = 0;
            supportTypeI = EnumLibrary.Convert(csiSupportTypeI, supportTypeI);
            supportTypeJ = EnumLibrary.Convert(csiSupportTypeJ, supportTypeJ);
        }

#else
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
        public void GetFireProofing(string name,
            out eFireProofing type,
            out double thickness,
            out double perimeter,
            out double density,
            out bool appliedToTopFlange,
            out bool includeInSelfWeight,
            out bool includeInGravityLoads,
            out string includedLoadPattern)
        {
            int CSiType = 0;
            _callCode = _sapModel.FrameObj.GetFireproofing_1(name, ref CSiType, ref thickness, ref perimeter, ref density, ref appliedToTopFlange, ref includeInSelfWeight, ref includeInGravityLoads, ref includedLoadPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            type = (eFireProofing)CSiType;
        }

        /// <summary>
        /// Sets the fireproofing assignments to existing frame objects.
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
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetFireProofing(string name,
            eFireProofing type,
            double thickness,
            double perimeter,
            double density,
            bool appliedToTopFlange,
            bool includeInSelfWeight,
            bool includeInGravityLoads,
            string includedLoadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetFireproofing_1(name, 
                            (int)type, thickness, perimeter, density, appliedToTopFlange, 
                            includeInSelfWeight, includeInGravityLoads, includedLoadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the fireproofing assignments to the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteFireProofing(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteFireproofing(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



#endif

        /// <summary>
        /// Returns the lateral bracing location assignments for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="frameNames">The name of the frame object associated with each bracing assignment.</param>
        /// <param name="bracingTypes">The bracing types assigned for each bracing assignment.</param>
        /// <param name="bracingLocations">The bracing locations for each bracing assignment along the depth of the cross section.</param>
        /// <param name="relativeDistanceStartBracing">The relative location of the point bracing or start of the distributed bracing.</param>
        /// <param name="relativeDistanceEndBracing">The relative location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingTypes" /> = <see cref="eBracingType.Point" />.</param>
        /// <param name="actualDistanceStartBracing">The absolute location of the point bracing or start of the distributed bracing.</param>
        /// <param name="actualDistanceEndBracing">The absolute location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingTypes" /> = <see cref="eBracingType.Point" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLateralBracing(string name,
            out string[] frameNames,
            out eBracingType[] bracingTypes,
            out eBracingLocation[] bracingLocations,
            out double[] relativeDistanceStartBracing,
            out double[] relativeDistanceEndBracing,
            out double[] actualDistanceStartBracing,
            out double[] actualDistanceEndBracing)
        {
            frameNames = new string[0];
            relativeDistanceStartBracing = new double[0];
            relativeDistanceEndBracing = new double[0];
            actualDistanceStartBracing = new double[0];
            actualDistanceEndBracing = new double[0];

            int[] csiBracingTypes = new int[0];
            int[] csiBracingLocations = new int[0];

            _callCode = _sapModel.FrameObj.GetLateralBracing(name, 
                ref _numberOfItems, 
                ref frameNames, 
                ref csiBracingTypes, 
                ref csiBracingLocations, 
                ref relativeDistanceStartBracing, 
                ref relativeDistanceEndBracing, 
                ref actualDistanceStartBracing, 
                ref actualDistanceEndBracing);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            bracingTypes = csiBracingTypes.Cast<eBracingType>().ToArray();
            bracingLocations = csiBracingLocations.Cast<eBracingLocation>().ToArray();
        }

        /// <summary>
        /// Assigns a lateral bracing location to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="bracingType">The bracing type to assign.</param>
        /// <param name="bracingLocation">The bracing location along the depth of the cross section.</param>
        /// <param name="distanceStartBracing">The location of the point bracing or start of the distributed bracing.</param>
        /// <param name="distanceEndBracing">The location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingType" /> = <see cref="eBracingType.Point" />.</param>
        /// <param name="relativeDistance">True: <parameref name="distanceStartBracing" /> and <parameref name="distanceEndBracing" /> are relative distances.
        /// Otherwise, distances are absolute.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLateralBracing(string name,
            eBracingType bracingType,
            eBracingLocation bracingLocation,
            double distanceStartBracing,
            double distanceEndBracing,
            bool relativeDistance = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLateralBracing(name, 
                            (int)bracingType, (int)bracingLocation, 
                            distanceStartBracing, distanceEndBracing, relativeDistance, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the lateral bracing assignments to the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="bracingType">Indicates the bracing type to be deleted.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLateralBracing(string name,
            eBracingType bracingType,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLateralBracing(name, 
                            (int)bracingType, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns definition data for all curved frame objects and returns the data in arrays.
        /// </summary>
        /// <param name="frameObjectNames">Names of the frame objects returned.</param>
        /// <param name="types">Curved frame types for each object returned.</param>
        /// <param name="globalX">The x coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalY">The y coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalZ">The z coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="pointNames">The name of the point object that is the third point on the curved frame.
        /// This item applies for MyType 1 and 4.</param>
        /// <param name="radius">This is an array of the radii of the circular curved frame.
        /// This item only applies for MyType 3. [L]</param>
        /// <param name="numberSegments">This is an array that includes the number of segments into which the program internally divides the curved frame.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetCurved(
            out string[] frameObjectNames,
            out eCurvedFrameType[] types,
            out double[] globalX,
            out double[] globalY,
            out double[] globalZ,
            out string[] pointNames,
            out double[] radius,
            out int[] numberSegments)
        {
            int[] csiTypes = new int[0];
            _callCode = _sapModel.FrameObj.GetCurved_1(ref _numberOfItems, ref frameObjectNames, ref csiTypes, ref globalX, ref globalY, ref globalZ, ref pointNames, ref radius, ref numberSegments);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            types = csiTypes.Cast<eCurvedFrameType>().ToArray();
        }

        /// <summary>
        /// This function changes the curve data for a curved frame object and sets straight frame objects to be curved.
        /// </summary>
        /// <param name="frameObjectName">The name of a defined curved frame object.</param>
        /// <param name="type">Curved frame types for each object returned.</param>
        /// <param name="globalX">The x coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalY">The y coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalZ">The z coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="pointName">The name of the point object that is the third point on the curved frame.
        /// This item applies for MyType 1 and 4.</param>
        /// <param name="radius">This is an array of the radii of the circular curved frame.
        /// This item only applies for MyType 3. [L]</param>
        /// <param name="numberSegments">This is an array that includes the number of segments into which the program internally divides the curved frame.</param>
        /// <param name="coordSystem">This is the coordinate system in which the coordinates x, y and z are defined.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCurved(string frameObjectName,
            eCurvedFrameType type,
            double globalX,
            double globalY,
            double globalZ,
            string pointName,
            double radius,
            int numberSegments,
            string coordSystem = CoordinateSystems.Global)
        {
            _callCode = _sapModel.FrameObj.SetCurved(frameObjectName, (int)type, globalX, globalY, globalZ, pointName, radius, numberSegments, coordSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets a curved frame object straight.
        /// </summary>
        /// <param name="name">The name of a defined curved frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetStraight(string name)
        {
            _callCode = _sapModel.FrameObj.SetStraight(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the type of frame object (straight or curved).
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eFrameType GetType(string name)
        {
            string csiFrameType = "";
            _callCode = _sapModel.FrameObj.GetTypeOAPI(name, ref csiFrameType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return EnumLibrary.ConvertStringToEnumByDescription<eFrameType>(csiFrameType);
        }



        /// <summary>
        /// Returns frame object end skew assignments.
        /// End skew data is only used in the program to plot the extruded view of bridge objects that have been updated as spine models.
        /// TODO: Handle? End skew assignments are only applicable to straight frame objects. An error is returned if skew data is requested for a curved frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="skewI">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the I-End of the frame object (-90 &lt; <paramref name="skewI" /> &lt; 90). [deg]</param>
        /// <param name="skewJ">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the J-End of the frame object (-90 &lt; <paramref name="skewJ" /> &lt; 90). [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetEndSkew(string name,
            out double skewI,
            out double skewJ)
        {
            _callCode = _sapModel.FrameObj.GetEndSkew(name, ref skewI, ref skewJ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns frame object end skew assignments.
        /// End skew data is only used in the program to plot the extruded view of bridge objects that have been updated as spine models.
        /// TODO: Handle? End skew assignments are only applicable to straight frame objects. An error is returned if skew data is requested for a curved frame object.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="skewI">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the I-End of the frame object (-90 &lt; <paramref name="skewI" /> &lt; 90). [deg]</param>
        /// <param name="skewJ">The angle in degrees measured counter clockwise from the positive local 3-axis to a line parallel to the J-End of the frame object (-90 &lt; <paramref name="skewJ" /> &lt; 90). [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetEndSkew(string name,
            double skewI,
            double skewJ,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetEndSkew(name, 
                            skewI, skewJ, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Returns the frame object end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="autoOffset">True: The end length offsets are automatically determined by the program from object connectivity.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the frame object at the I-End of the frame object. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the frame object at the J-End of the frame object. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetEndLengthOffset(string name,
            out bool autoOffset,
            out double lengthIEnd,
            out double lengthJEnd,
            out double rigidZoneFactor)
        {
            autoOffset = false;
            lengthIEnd = -1;
            lengthJEnd = -1;
            rigidZoneFactor = -1;

            _callCode = _sapModel.FrameObj.GetEndLengthOffset(name, ref autoOffset, ref lengthIEnd, ref lengthJEnd, ref rigidZoneFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns the line element end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="autoOffset">True: The end length offsets are automatically determined by the program from object connectivity.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the frame object at the I-End of the frame object. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the frame object at the J-End of the frame object. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetEndLengthOffset(string name,
            bool autoOffset,
            double lengthIEnd,
            double lengthJEnd,
            double rigidZoneFactor)
        {
            _callCode = _sapModel.FrameObj.SetEndLengthOffset(name, autoOffset, lengthIEnd, lengthJEnd, rigidZoneFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function reports the hinge assignments for a specified frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="hingeNumbers">The hinge number for each hinge on the frame object.</param>
        /// <param name="generatedPropertyNames">The name of the generated hinge property for each hinge on the frame object.</param>
        /// <param name="hingeTypes">Type of hinge for each hinge on the frame object.</param>
        /// <param name="hingeBehaviors">The behavior of the hinge for each hinge on the frame object.</param>
        /// <param name="sources">The source of the generated hinge property for each hinge on the frame object.
        /// The source is either Auto or the name of a defined (not generated) hinge property.</param>
        /// <param name="relativeDistances">The relative distance of each hinge along the frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetHingeAssigns(string name,
            out int[] hingeNumbers,
            out string[] generatedPropertyNames,
            out eHingeType[] hingeTypes,
            out eHingeBehavior[] hingeBehaviors,
            out string[] sources,
            out double[] relativeDistances)
        {
            hingeNumbers = new int[0];
            generatedPropertyNames = new string[0];
            sources = new string[0];
            relativeDistances = new double[0];
            int[] csiHingeTypes = new int[0];
            int[] csiHingeBehaviors = new int[0];

            _callCode = _sapModel.FrameObj.GetHingeAssigns(name, ref _numberOfItems, ref hingeNumbers, ref generatedPropertyNames, ref csiHingeTypes, ref csiHingeBehaviors, ref sources, ref relativeDistances);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            hingeTypes = csiHingeTypes.Cast<eHingeType>().ToArray();
            hingeBehaviors = csiHingeBehaviors.Cast<eHingeBehavior>().ToArray();
        }



        /// <summary>
        /// Returns object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L]
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MaxSpacing" />.</param>
        /// <param name="minStationNumber">The minimum number of stations.
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MinStations" />.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetOutputStations(string name,
            out eOutputStationType outputStationType,
            out double maxStationSpacing,
            out int minStationNumber,
            out bool noOutputAndDesignAtElementEnds,
            out bool noOutputAndDesignAtPointLoads)
        {
            maxStationSpacing = -1;
            minStationNumber = -1;
            noOutputAndDesignAtElementEnds = false;
            noOutputAndDesignAtPointLoads = false;

            int csiOutputStationType = 0;
            _callCode = _sapModel.FrameObj.GetOutputStations(name, ref csiOutputStationType, ref maxStationSpacing, ref minStationNumber, ref noOutputAndDesignAtElementEnds, ref noOutputAndDesignAtPointLoads);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            outputStationType = (eOutputStationType) csiOutputStationType;
        }

        /// <summary>
        /// Assigns object output station data.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="outputStationType">Indicates how the output stations are specified.</param>
        /// <param name="maxStationSpacing">The maximum segment size, that is, the maximum station spacing. [L]
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MaxSpacing" />.</param>
        /// <param name="minStationNumber">The minimum number of stations.
        /// This item applies only when <paramref name="outputStationType" /> = <see cref="eOutputStationType.MinStations" />.</param>
        /// <param name="noOutputAndDesignAtElementEnds">True: No additional output stations are added at the ends of line elements when the cable object is internally meshed.</param>
        /// <param name="noOutputAndDesignAtPointLoads">True: No additional output stations are added at point load locations.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetOutputStations(string name,
            eOutputStationType outputStationType,
            double maxStationSpacing,
            int minStationNumber,
            bool noOutputAndDesignAtElementEnds,
            bool noOutputAndDesignAtPointLoads,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetOutputStations(name, 
                            (int)outputStationType, maxStationSpacing, minStationNumber, 
                            noOutputAndDesignAtElementEnds, noOutputAndDesignAtPointLoads, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns  the automatic meshing assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="isAutoMeshed">True: The frame object is automatically meshed by the program when the analysis model is created.</param>
        /// <param name="isAutoMeshedAtPoints">True: The frame object is automatically meshed at intermediate joints along its length.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="isAutoMeshedAtLines">True: The frame object is automatically meshed at intersections with other frames, area object edges and solid object edges.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="minElementNumber">The minimum number of elements into which the frame object is automatically meshed.
        /// If this item is zero, the number of elements is not checked when the automatic meshing is done.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="autoMeshMaxLength">The maximum length of auto meshed frame elements.
        /// If this item is zero, the element length is not checked when the automatic meshing is done. [L]
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAutoMesh(string name,
            out bool isAutoMeshed,
            out bool isAutoMeshedAtPoints,
            out bool isAutoMeshedAtLines,
            out int minElementNumber,
            out double autoMeshMaxLength)
        {
            _callCode = _sapModel.FrameObj.GetAutoMesh(name, ref isAutoMeshed, ref isAutoMeshedAtPoints, ref isAutoMeshedAtLines, ref minElementNumber, ref autoMeshMaxLength);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets automatic meshing assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isAutoMeshed">True: The frame object automatically meshed by the program when the analysis model is created.</param>
        /// <param name="isAutoMeshedAtPoints">True: The frame object is automatically meshed at intermediate joints along its length.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="isAutoMeshedAtLines">True: The frame object is automatically meshed at intersections with other frames, area object edges and solid object edges.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="minElementNumber">The minimum number of elements into which the frame object is automatically meshed.
        /// If this item is zero, the number of elements is not checked when the automatic meshing is done.
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="autoMeshMaxLength">The maximum length of auto meshed frame elements.
        /// If this item is zero, the element length is not checked when the automatic meshing is done. [L]
        /// This item is applicable only when the <paramref name="isAutoMeshed" /> item is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoMesh(string name, 
            bool isAutoMeshed, 
            bool isAutoMeshedAtPoints,
            bool isAutoMeshedAtLines,
            int minElementNumber,
            double autoMeshMaxLength,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetAutoMesh(name, 
                            isAutoMeshed, isAutoMeshedAtPoints, isAutoMeshedAtLines, minElementNumber, autoMeshMaxLength, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Support & Connections
        /// <summary>
        /// Returns the release assignments for a frame end release.
        /// </summary>
        /// <param name="name">The name of an existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetReleases(string name,
            out DegreesOfFreedomLocal iEndRelease,
            out DegreesOfFreedomLocal jEndRelease,
            out Fixity iEndFixity,
            out Fixity jEndFixity)
        {
            bool[] csiIEndReleases = new bool[0];
            bool[] csiJEndReleases = new bool[0];
            double[] csiIEndFixity = new double[0];
            double[] csiJEndFixity = new double[0];

            _callCode = _sapModel.FrameObj.GetReleases(name, ref csiIEndReleases, ref csiJEndReleases, ref csiIEndFixity, ref csiJEndFixity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            iEndRelease = new DegreesOfFreedomLocal();
            iEndRelease.FromArray(csiIEndReleases);

            jEndRelease = new DegreesOfFreedomLocal();
            jEndRelease.FromArray(csiJEndReleases);

            iEndFixity = new Fixity();
            iEndFixity.FromArray(csiIEndFixity);

            jEndFixity = new Fixity();
            jEndFixity.FromArray(csiJEndFixity);
        }

        /// <summary>
        /// This function defines a named frame end release.
        /// </summary>
        /// <param name="name">The name of a new or existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetReleases(string name,
            DegreesOfFreedomLocal iEndRelease,
            DegreesOfFreedomLocal jEndRelease,
            Fixity iEndFixity,
            Fixity jEndFixity)
        {
            bool[] csiIEndReleases = iEndRelease.ToArray();
            bool[] csiJEndReleases = jEndRelease.ToArray();
            double[] csiIEndFixity = iEndFixity.ToArray();
            double[] csiJEndFixity = jEndFixity.ToArray();


            _callCode = _sapModel.FrameObj.SetReleases(name, ref csiIEndReleases, ref csiJEndReleases, ref csiIEndFixity, ref csiJEndFixity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if BUILD_ETABS2016 || BUILD_ETABS2017
        // ===

        /// <summary>
        /// Retrieves the named spring property assignment for an object.</summary>
        /// <param name="name">The name of an existing object.</param>
        public string GetSpringAssignment(string name)
        {
            string nameSpring = string.Empty;
            _callCode = _sapModel.FrameObj.GetSpringAssignment(name, ref nameSpring);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameSpring;
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.</summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetSpringAssignment(string name,
            string nameSpring,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetSpringAssignment(name, nameSpring);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#elif !BUILD_ETABS2015
        /// <summary>
        /// Returns the spring assignments to an object face.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="springTypes">The spring property type.</param>
        /// <param name="stiffnesses">Simple spring stiffness per unit length of the specified object. [F/L^2]
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="springSimpleTypes">The simple spring type.
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="linkProperties">The name of the link property assigned to the spring.
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="springLocalOneTypes">Indicates the method used to specify the spring positive local 1-axis orientation.</param>
        /// <param name="directions">This is 1, 2, 3, -1, -2 or -3, indicating the object local axis that corresponds to the positive local 1-axis of the spring.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.Parallel" />.</param>
        /// <param name="plane23Angle">This is the angle in the frame object 2-3 plane measured counter clockwise from the frame positive 2-axis to the spring positive 1-axis. [deg]
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.Normal" />.</param>
        /// <param name="vectorComponentsX">Each value in this array is the X-axis or object local 1-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="vectorComponentsY">Each value in this array is the Y-axis or object local 2-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="vectorComponentsZ">Each value in this array is the Z-axis or object local 3-axis component (depending on the <paramref name="coordinateSystems" /> specified) of the user specified direction vector for the spring local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystems" /> item.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="angleOffsets">This is the angle that the link local 2-axis is rotated from its default orientation. [deg]
        /// This item applies only when <paramref name="springTypes" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="coordinateSystems">This is Local (meaning the solid object local coordinate system) or the name of a defined coordinate system.
        /// This item is the coordinate system in which the user specified direction vector is specified.
        /// This item applies only when <paramref name="springLocalOneTypes" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSpring(string name,
            ref eSpringType[] springTypes,
            ref double[] stiffnesses,
            ref eSpringSimpleType[] springSimpleTypes,
            ref string[] linkProperties,
            ref eSpringLocalOneType[] springLocalOneTypes,
            ref int[] directions,
            ref double[] plane23Angle,
            ref double[] vectorComponentsX,
            ref double[] vectorComponentsY,
            ref double[] vectorComponentsZ,
            ref double[] angleOffsets,
            ref string[] coordinateSystems)
        {
            int[] csiSpringTypes = new int[0];
            int[] csiSimpleSpringTypes = new int[0];
            int[] csiSpringLocalOneTypes = new int[0];

            _callCode = _sapModel.FrameObj.GetSpring(name, ref _numberOfItems, ref csiSpringTypes, ref stiffnesses, ref csiSimpleSpringTypes, ref linkProperties, ref csiSpringLocalOneTypes, ref directions, ref plane23Angle, ref vectorComponentsX, ref vectorComponentsY, ref vectorComponentsZ, ref coordinateSystems, ref angleOffsets);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            springTypes = csiSpringTypes.Cast<eSpringType>().ToArray();
            springSimpleTypes = csiSimpleSpringTypes.Cast<eSpringSimpleType>().ToArray();
            springLocalOneTypes = csiSpringLocalOneTypes.Cast<eSpringLocalOneType>().ToArray();
        }

        /// <summary>
        /// Sets spring assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="springType">The spring property type.</param>
        /// <param name="stiffness">Simple spring stiffness per unit length of the specified object face. [F/L^2]
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="springSimpleType">The simple spring type.
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Simple" />.</param>
        /// <param name="linkProperty">The name of the link property assigned to the spring.
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="springLocalOneType">Indicates the method used to specify the spring positive local 1-axis orientation.</param>
        /// <param name="direction">This is 1, 2, 3, -1, -2 or -3, indicating the object local axis that corresponds to the positive local 1-axis of the spring.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.Parallel" />.</param>
        /// <param name="plane23Angle">This is the angle in the frame object 2-3 plane measured counter clockwise from the frame positive 2-axis to the spring positive 1-axis. [deg]
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.Normal" />.</param>
        /// <param name="vector">This is an array of three values that define the direction vector of the spring positive local 1-axis.
        /// The direction vector is in the coordinate system specified by the <paramref name="coordinateSystem" /> item.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="angleOffset">This is the angle that the link local 2-axis is rotated from its default orientation. [deg]
        /// This item applies only when <paramref name="springType" /> = <see cref="eSpringType.Link" />.</param>
        /// <param name="replace">True: All existing spring assignments to the object are removed before assigning the specified spring.
        /// False: The specified spring is added to any existing springs already assigned to the object.</param>
        /// <param name="coordinateSystem">This is Local (meaning the solid object local coordinate system) or the name of a defined coordinate system.
        /// This item is the coordinate system in which the user specified direction vector, <paramref name="vector" />, is specified.
        /// This item applies only when <paramref name="springLocalOneType" /> = <see cref="eSpringLocalOneType.User" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSpring(string name,
            eSpringType springType,
            double stiffness,
            eSpringSimpleType springSimpleType,
            string linkProperty,
            eSpringLocalOneType springLocalOneType,
            int direction,
            double plane23Angle,
            double[] vector,
            double angleOffset,
            bool replace,
            string coordinateSystem = CoordinateSystems.Local,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetSpring(name, 
                            (int)springType, stiffness, (int)springSimpleType, linkProperty, 
                            (int)springLocalOneType, direction, plane23Angle, ref vector, angleOffset, replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteSpring(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteSpring(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Design
        /// <summary>
        /// Returns the design procedure for a frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eDesignProcedureType GetDesignProcedure(string name)
        {
            int csiDesignProcedureMaterial = 0;
            _callCode = _sapModel.FrameObj.GetDesignProcedure(name, ref csiDesignProcedureMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eDesignProcedureType) csiDesignProcedureMaterial;
        }

        /// <summary>
        /// Sets the design procedure for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="designProcedure">Design procedure type desired for the specified frame object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDesignProcedure(string name,
            eDesignProcedure designProcedure,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetDesignProcedure(name, 
                            (int)designProcedure, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the frame object column splice overwrite assignment.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="spliceOption">The option used for defining the splice overwrite.</param>
        /// <param name="height">Specifies the height of the splice above the story at the bottom of the column object, if <paramref name="spliceOption"/> = <see cref="eColumnSpliceOption.SpliceAtHeightAboveStoryAtBottomOfColumn"/></param>
        public void GetColumnSpliceOverwrite(string name,
            ref eColumnSpliceOption spliceOption,
            ref double height)
        {
            int csiSpliceOption = 0;

            _callCode = _sapModel.FrameObj.GetColumnSpliceOverwrite(name, ref csiSpliceOption, ref height);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            spliceOption = (eColumnSpliceOption) csiSpliceOption;
        }

        /// <summary>
        /// Sets the frame object column splice overwrite assignment.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="spliceOption">The option used for defining the splice overwrite.</param>
        /// <param name="height">Specifies the height of the splice above the story at the bottom of the column object, if <paramref name="spliceOption"/> = <see cref="eColumnSpliceOption.SpliceAtHeightAboveStoryAtBottomOfColumn"/></param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetColumnSpliceOverwrite(string name,
            eColumnSpliceOption spliceOption,
            double height,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetColumnSpliceOverwrite(name, 
                            (int)spliceOption, height, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the design orientation of a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        public eFrameDesignOrientation GetDesignOrientation(string name)
        {
            CSiProgram.eFrameDesignOrientation csiDesignOrientation = CSiProgram.eFrameDesignOrientation.Other;

            _callCode = _sapModel.FrameObj.GetDesignOrientation(name, ref csiDesignOrientation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            eFrameDesignOrientation designOrientation = 0;
            return EnumLibrary.Convert(csiDesignOrientation, designOrientation);
        }

        // ===

        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        public string GetPier(string name)
        {
            string namePier = string.Empty;
            _callCode = _sapModel.FrameObj.GetPier(name, ref namePier);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return namePier;
        }

        /// <summary>
        /// Sets the pier label assignment of one or more objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="namePier">The name of the pier assignment, if any, or "None".</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetPier(string name,
                string namePier,
                eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetPier(name, 
                            namePier, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        public string GetSpandrel(string name)
        {
            string nameSpandrel = string.Empty;
            _callCode = _sapModel.FrameObj.GetSpandrel(name, ref nameSpandrel);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return nameSpandrel;
        }

        /// <summary>
        /// Sets the spandrel label assignment of one or more objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="nameSpandrel">The name of the spandrel assignment, if any, or "None".</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        public void SetSpandrel(string name,
            string nameSpandrel,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetSpandrel(name, 
                            nameSpandrel, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // PDeltaForce
        /// <summary>
        /// Returns the P-Delta force assignments to line elements.
        /// </summary>
        /// <param name="name">The name of an existing line element.</param>
        /// <param name="numberForces">The number of P-Delta forces assigned to the line element.</param>
        /// <param name="pDeltaForces">P-Delta force values assigned to the line element. [F]</param>
        /// <param name="directions">The direction of each P-Delta force assignment.</param>
        /// <param name="coordinateSystems">This is an array that contains the name of the coordinate system in which each projected P-Delta force is defined.
        /// This item is blank when the <paramref name="directions" /> item is <see cref="ePDeltaDirection.Local_1" />, that is, when the P-Delta force is defined in the line element local 1-axis direction.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPDeltaForce(string name,
            out int numberForces,
            out double[] pDeltaForces,
            out ePDeltaDirection[] directions,
            out string[] coordinateSystems)
        {
            int[] csiDirections = new int[0];

            _callCode = _sapModel.FrameObj.GetPDeltaForce(name, ref numberForces, ref pDeltaForces, ref csiDirections, ref coordinateSystems);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            directions = csiDirections.Cast<ePDeltaDirection>().ToArray();
        }

        /// <summary>
        /// Assigns the P-Delta force assignments to frame objects.
        /// </summary>
        /// <param name="name">The name of a frame line object .</param>
        /// <param name="pDeltaForce">P-Delta force value assigned to the frame object. [F]</param>
        /// <param name="direction">The direction of the P-Delta force assignment.</param>
        /// <param name="replaceExistingLoads">True: All existing P-Delta force assignments to the frame object are removed before assigning the specified P-Delta force.
        /// False: the specified P-Delta force is added to any existing P-Delta forces already assigned to the frame object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the projected P-Delta force is defined.
        /// This item is blank when the <paramref name="direction" /> item is <see cref="ePDeltaDirection.Local_1" />, that is, when the P-Delta force is defined in the frame object local 1-axis direction.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPDeltaForce(string name,
            double pDeltaForce,
            ePDeltaDirection direction,
            bool replaceExistingLoads,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetPDeltaForce(name, 
                            pDeltaForce, (int)direction, replaceExistingLoads, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the P-Delta force assignments for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are set for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are set for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are set for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePDeltaForce(string name,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeletePDeltaForce(name, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        // LoadGravity
        /// <summary>
        /// Returns the gravity load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each gravity load.</param>
        /// <param name="loadPatterns">The name of the coordinate system in which the gravity load multipliers are specified.</param>
        /// <param name="xLoadMultiplier">Gravity load multipliers in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multipliers in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multipliers in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystems">The name of the coordinate system associated with each gravity load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadGravity(string name,
            out string[] names,
            out string[] loadPatterns,
            out double[] xLoadMultiplier,
            out double[] yLoadMultiplier,
            out double[] zLoadMultiplier,
            out string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.GetLoadGravity(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref coordinateSystems, 
                            ref xLoadMultiplier, ref yLoadMultiplier, ref zLoadMultiplier, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns gravity loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the gravity load.</param>
        /// <param name="xLoadMultiplier">Gravity load multiplier in the x direction of the specified coordinate system.</param>
        /// <param name="yLoadMultiplier">Gravity load multiplier in the y direction of the specified coordinate system.</param>
        /// <param name="zLoadMultiplier">Gravity load multiplier in the z direction of the specified coordinate system.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the gravity load.</param>
        /// <param name="replace">True: All previous gravity loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadGravity(string name,
            string loadPattern,
            double xLoadMultiplier,
            double yLoadMultiplier,
            double zLoadMultiplier,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadGravity(name, 
                            loadPattern, xLoadMultiplier, yLoadMultiplier, zLoadMultiplier, 
                            replace, coordinateSystem, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the gravity load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadGravity(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadGravity(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadDeformation
        /// <summary>
        /// Returns the deformation load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each deformation load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each deformation load.</param>
        /// <param name="degreesOfFreedom">Indicates if the considered degree of freedom has a deformation load.</param>
        /// <param name="deformations">Deformation load values.
        /// The deformations specified for a given degree of freedom are applicable only if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadDeformation(string name,
            out string[] names,
            out string[] loadPatterns,
            out DegreesOfFreedomLocal[] degreesOfFreedom,
            out Deformations[] deformations,
            eItemType itemType = eItemType.Object)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] U1 = new double[0];
            double[] U2 = new double[0];
            double[] U3 = new double[0];
            double[] R1 = new double[0];
            double[] R2 = new double[0];
            double[] R3 = new double[0];

            _callCode = _sapModel.FrameObj.GetLoadDeformation(name, ref _numberOfItems, ref names, ref loadPatterns, 
                            ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6,
                            ref U1, ref U2, ref U3, ref R1, ref R2, ref R3,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom = new DegreesOfFreedomLocal[numberItems];
            deformations = new Deformations[numberItems];
            for (int i = 0; i < numberItems; i++)
            {
                degreesOfFreedom[i].U1 = dof1[i];
                degreesOfFreedom[i].U2 = dof2[i];
                degreesOfFreedom[i].U3 = dof3[i];
                degreesOfFreedom[i].R1 = dof4[i];
                degreesOfFreedom[i].R2 = dof5[i];
                degreesOfFreedom[i].R3 = dof6[i];

                deformations[i].U1 = U1[i];
                deformations[i].U2 = U2[i];
                deformations[i].U3 = U3[i];
                deformations[i].R1 = R1[i];
                deformations[i].R2 = R2[i];
                deformations[i].R3 = R3[i];
            }
        }

        /// <summary>
        /// Assigns deformation loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="degreesOfFreedom">Indicates if the considered degree of freedom has a deformation load.</param>
        /// <param name="deformations">Deformation load values.
        /// The deformations specified for a given degree of freedom are applicable only if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadDeformation(string name,
            string loadPattern,
            DegreesOfFreedomLocal degreesOfFreedom,
            Deformations deformations,
            eItemType itemType = eItemType.Object)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            double[] csiDeformations = deformations.ToArray();

            _callCode = _sapModel.FrameObj.SetLoadDeformation(name, 
                            loadPattern, ref csiDegreesOfFreedom, ref csiDeformations, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the deformation load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDeformation(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadDeformation(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadTargetForce
        /// <summary>
        /// Returns the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of deformation loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each target force.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forcesValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocations">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadTargetForce(string name,
            out string[] names,
            out string[] loadPatterns,
            out ForcesActive[] forcesActive,
            out Forces[] forcesValues,
            out Forces[] relativeForcesLocations,
            eItemType itemType = eItemType.Object)
        {
            bool[] dof1 = new bool[0];
            bool[] dof2 = new bool[0];
            bool[] dof3 = new bool[0];
            bool[] dof4 = new bool[0];
            bool[] dof5 = new bool[0];
            bool[] dof6 = new bool[0];

            double[] P = new double[0];
            double[] V2 = new double[0];
            double[] V3 = new double[0];
            double[] T = new double[0];
            double[] M2 = new double[0];
            double[] M3 = new double[0];

            double[] T1 = new double[0];
            double[] T2 = new double[0];
            double[] T3 = new double[0];
            double[] T4 = new double[0];
            double[] T5 = new double[0];
            double[] T6 = new double[0];

            _callCode = _sapModel.FrameObj.GetLoadTargetForce(name, ref _numberOfItems, ref names, ref loadPatterns, 
                            ref dof1, ref dof2, ref dof3, ref dof4, ref dof5, ref dof6,
                            ref P, ref V2, ref V3, ref T, ref M2, ref M3,
                            ref T1, ref T2, ref T3, ref T4, ref T5, ref T6,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forcesActive = new ForcesActive[numberItems - 1];
            forcesValues = new Forces[numberItems - 1];
            relativeForcesLocations = new Forces[numberItems - 1];
            for (int i = 0; i < numberItems; i++)
            {
                forcesActive[i].P = dof1[i];
                forcesActive[i].V2 = dof2[i];
                forcesActive[i].V3 = dof3[i];
                forcesActive[i].T = dof4[i];
                forcesActive[i].M2 = dof5[i];
                forcesActive[i].M3 = dof6[i];

                forcesValues[i].P = P[i];
                forcesValues[i].V2 = V2[i];
                forcesValues[i].V3 = V3[i];
                forcesValues[i].T = T[i];
                forcesValues[i].M2 = M2[i];
                forcesValues[i].M3 = M3[i];

                relativeForcesLocations[i].P = T1[i];
                relativeForcesLocations[i].V2 = T2[i];
                relativeForcesLocations[i].V3 = T3[i];
                relativeForcesLocations[i].T = T4[i];
                relativeForcesLocations[i].M2 = T5[i];
                relativeForcesLocations[i].M3 = T6[i];
            }
        }

        /// <summary>
        /// Assigns the target force load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with each target force.</param>
        /// <param name="forcesActive">Boolean values indicating if the considered degree of freedom has a target force assignment.</param>
        /// <param name="forceValues">Target force values.
        /// The target forces specified for a given degree of freedom are only applicable if the corresponding DOF item for that degree of freedom is True.</param>
        /// <param name="relativeForcesLocation">Relative distances along the line elements where the target force values apply.
        /// The relative distances specified for a given degree of freedom are only applicable if the corresponding dofn item for that degree of freedom is True.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadTargetForce(string name,
            string loadPattern,
            ForcesActive forcesActive,
            Forces forceValues,
            Forces relativeForcesLocation,
            eItemType itemType = eItemType.Object)
        {
            bool[] csiForcesActive = forcesActive.ToArray();
            double[] csiForceValue = forceValues.ToArray();
            double[] csiRelativeForcesLocation = relativeForcesLocation.ToArray();

            _callCode = _sapModel.FrameObj.SetLoadTargetForce(name, 
                            loadPattern, ref csiForcesActive, ref csiForceValue, ref csiRelativeForcesLocation, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the target force load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadTargetForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadTargetForce(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        // LoadStrain
        /// <summary>
        /// Returns the strain load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each strain load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each strain load.</param>
        /// <param name="components">Indicates the strain component associated with each strain load.</param>
        /// <param name="strainLoadValues">The strain values. [L/L]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadStrain(string name,
            out string[] names,
            out string[] loadPatterns,
            out eStrainComponent[] components,
            out double[] strainLoadValues,
            out string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            int[] csiComponents = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadStrain(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiComponents, ref strainLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            components = csiComponents.Cast<eStrainComponent>().ToArray();
        }

        /// <summary>
        /// Assigns strain loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the strain load.</param>
        /// <param name="component">Indicates the strain component associated with the strain load.</param>
        /// <param name="strainLoadValue">The strain value. [L/L]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the strain load.</param>
        /// <param name="replace">True: All previous strain loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
            double strainLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadStrain(name,
                            loadPattern, (int)component, strainLoadValue, replace, jointPatternName, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the strain load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="component">Indicates the strain component associated with the strain load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadStrain(name, 
                            loadPattern, (int)component,
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // LoadDistributedWithGUID
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="startLoadValues">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValues">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="relativeDistanceStartFromI">The relative distance from the I-End of the element to the start of the distributed load.</param>
        /// <param name="relativeDistanceEndFromI">The relative distance from the I-End of the element to the end of the distributed load.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadDistributedWithGUID(string name,
            out string[] names,
            out string[] loadPatterns,
            out string[] GUIDs,
            out eLoadForceType[] forceTypes,
            out eLoadDirection[] loadDirections,
            out double[] startLoadValues,
            out double[] endLoadValues,
            out double[] absoluteDistanceStartFromI,
            out double[] absoluteDistanceEndFromI,
            out double[] relativeDistanceStartFromI,
            out double[] relativeDistanceEndFromI,
            out string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadDistributedWithGUID(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems, 
                            ref csiLoadDirections, ref startLoadValues, ref endLoadValues, 
                            ref absoluteDistanceStartFromI, ref absoluteDistanceEndFromI, 
                            ref relativeDistanceStartFromI, ref relativeDistanceEndFromI, ref GUIDs, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// If the frame object is already assigned a distributed load with a global unique ID matching the specified global unique ID, this function modifies that distributed load.
        /// Otherwise, this function assigns a new distributed load to the frame object and sets its global unique ID to the specified global unique ID.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the distributed load.</param>
        /// <param name="GUID">This is the global unique ID of a distributed load assigned to the frame object or if it is not the global unique id of a distributed load assigned to the frame object and it is not blank, the global unique ID which is assigned to the newly assigned load.
        /// If left blank, a new load is assigned to the frame object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="forceType">Force type for the distributed load for the load pattern.</param>
        /// <param name="loadDirection">Direction that the load is applied in for the load pattern.</param>
        /// <param name="startLoadValue">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValue">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any distributed load assigned to the frame object, all previous distributed loads, if any, assigned to the specified frame object, in the specified load pattern, are deleted before making the new assignment.
        /// If the input GUID is the GUID of a distributed load already assigned to the frame object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadDistributedWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double startLoadValue,
            double endLoadValue,
            double absoluteDistanceStartFromI,
            double absoluteDistanceEndFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
            _callCode = _sapModel.FrameObj.SetLoadDistributedWithGUID(name, loadPattern, (int)forceType, (int)loadDirection, startLoadValue, endLoadValue, absoluteDistanceStartFromI, absoluteDistanceEndFromI, ref GUID, coordinateSystem, distanceIsRelative, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The global unique ID of one of the distributed loads on that frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDistributedWithGUID(string name,
            string GUID)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadDistributedWithGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // LoadPointWithGUID
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="GUIDs">This is an array that includes the global unique ID of each distributed load.</param>
        /// <param name="forceTypes">Force type for the point load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="pointLoadValues">The load value of the point loads.
        /// [F] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="relativeDistanceFromI">The relative distance from the I-End of the element to the location of the point load.</param>
        /// <param name="coordinateSystems">Coordinated system used for each point load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadPointWithGUID(string name,
            out string[] names,
            out string[] loadPatterns,
            out string[] GUIDs,
            out eLoadForceType[] forceTypes,
            out eLoadDirection[] loadDirections,
            out double[] pointLoadValues,
            out double[] absoluteDistanceFromI,
            out double[] relativeDistanceFromI,
            out string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            int[] csiForceTypes = new int[0];
            int[] csiDirectionsApplied = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadPointWithGUID(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems, ref csiDirectionsApplied, 
                            ref pointLoadValues, ref absoluteDistanceFromI, ref relativeDistanceFromI, ref GUIDs, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiDirectionsApplied.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the uniform load.</param>
        /// <param name="GUID">This is the global unique ID of a distributed load assigned to the frame object or if it is not the global unique id of a distributed load assigned to the frame object and it is not blank, the global unique ID which is assigned to the newly assigned load.
        /// If left blank, a new load is assigned to the frame object and the value of this parameter is set to the global unique ID of the newly assigned load.</param>
        /// <param name="forceType">Force type for the point load for the load pattern.</param>
        /// <param name="loadDirection">The direction that the load is applied.</param>
        /// <param name="pointLoadValue">The load value of the point loads.
        /// [F] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the uniform load.</param>
        /// <param name="replace">True: If the input GUID is not the GUID of any distributed load assigned to the frame object, all previous distributed loads, if any, assigned to the specified frame object, in the specified load pattern, are deleted before making the new assignment.
        /// If the input GUID is the GUID of a distributed load already assigned to the frame object, the parameters of the distributed load are updated with the values provided and this item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadPointWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double pointLoadValue,
            double absoluteDistanceFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
            _callCode = _sapModel.FrameObj.SetLoadPointWithGUID(name, loadPattern, (int)loadDirection, (int)forceType, pointLoadValue, absoluteDistanceFromI, ref GUID, coordinateSystem, distanceIsRelative, replace);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The global unique ID of one of the distributed loads on that frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadPointWithGUID(string name,
            string GUID)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadPointWithGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // Load Transfer
        /// <summary>
        /// Returns the load transfer option for a frame object.
        /// It indicates whether the frame receives load from an area object when the area object is loaded with a load of type uniform to frame.
        /// </summary>
        /// <param name="name">The name of an existing frame.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool GetLoadTransfer(string name)
        {
            bool loadIsTransferred = false;
            _callCode = _sapModel.FrameObj.GetLoadTransfer(name, ref loadIsTransferred);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return loadIsTransferred;
        }

        /// <summary>
        /// Returns the load transfer option for frame objects.
        /// It indicates whether the frame receives load from an area object when the area object is loaded with a load of type uniform to frame.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadIsTransferred">Indicates if load is allowed to be transferred from area objects to this frame object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadTransfer(string name,
            bool loadIsTransferred,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadTransfer(name, 
                            loadIsTransferred, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        // LoadTemperature
        /// <summary>
        /// Returns the temperature load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="names">The name of the element associated with each temperature load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each temperature load.</param>
        /// <param name="temperatureLoadTypes">Indicates the type of temperature load.</param>
        /// <param name="temperatureLoadValues">Temperature load values, [T] for <paramref name="temperatureLoadTypes" /> = Temperature, [T/L] for all others.</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadTemperature(string name,
            out string[] names,
            out string[] loadPatterns,
            out eLoadTemperatureType[] temperatureLoadTypes,
            out double[] temperatureLoadValues,
            out string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
            names = new string[0];
            loadPatterns = new string[0];
            temperatureLoadValues = new double[0];
            jointPatternNames = new string[0];
            int[] csiTemperatureLoadTypes = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadTemperature(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiTemperatureLoadTypes, ref temperatureLoadValues, ref jointPatternNames, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            temperatureLoadTypes = csiTemperatureLoadTypes.Cast<eLoadTemperatureType>().ToArray();
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing element or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the temperature load.</param>
        /// <param name="temperatureLoadType">Indicates the type of temperature load.</param>
        /// <param name="temperatureLoadValue">Temperature load value, [T] for <paramref name="temperatureLoadType" /> = Temperature, [T/L] for all others.</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the temperature load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadTemperature(string name,
            string loadPattern,
            eLoadTemperatureType temperatureLoadType,
            double temperatureLoadValue,
            string jointPatternName,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadTemperature(name, 
                            loadPattern, (int)temperatureLoadType, temperatureLoadValue, jointPatternName, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadTemperature(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadTemperature(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        // LoadDistributed
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="startLoadValues">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValues">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="relativeDistanceStartFromI">The relative distance from the I-End of the element to the start of the distributed load.</param>
        /// <param name="relativeDistanceEndFromI">The relative distance from the I-End of the element to the end of the distributed load.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadDistributed(string name,
            out string[] names,
            out string[] loadPatterns,
            out eLoadForceType[] forceTypes,
            out eLoadDirection[] loadDirections,
            out double[] startLoadValues,
            out double[] endLoadValues,
            out double[] absoluteDistanceStartFromI,
            out double[] absoluteDistanceEndFromI,
            out double[] relativeDistanceStartFromI,
            out double[] relativeDistanceEndFromI,
            out string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            names = new string[0];
            loadPatterns = new string[0];
            startLoadValues = new double[0];
            endLoadValues = new double[0];
            absoluteDistanceStartFromI = new double[0];
            absoluteDistanceEndFromI = new double[0];
            relativeDistanceStartFromI = new double[0];
            relativeDistanceEndFromI = new double[0];
            coordinateSystems = new string[0];
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadDistributed(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems, 
                            ref csiLoadDirections, ref startLoadValues, ref endLoadValues, 
                            ref absoluteDistanceStartFromI, ref absoluteDistanceEndFromI, 
                            ref relativeDistanceStartFromI, ref relativeDistanceEndFromI, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the distributed load.</param>
        /// <param name="forceType">Force type for the distributed load for the load pattern.</param>
        /// <param name="loadDirection">Direction that the load is applied in for the load pattern.</param>
        /// <param name="startLoadValue">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValue">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadDistributed(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double startLoadValue,
            double endLoadValue,
            double absoluteDistanceStartFromI,
            double absoluteDistanceEndFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadDistributed(name, 
                            loadPattern, (int)forceType, (int)loadDirection, 
                            startLoadValue, endLoadValue, 
                            absoluteDistanceStartFromI, absoluteDistanceEndFromI, coordinateSystem, distanceIsRelative, replace,  
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDistributed(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadDistributed(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        // LoadPoint
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="forceTypes">Force type for the point load for each load pattern.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="pointLoadValues">The load value of the point loads.
        /// [F] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="relativeDistanceFromI">The relative distance from the I-End of the element to the location of the point load.</param>
        /// <param name="coordinateSystems">Coordinated system used for each point load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadPoint(string name,
            out string[] names,
            out string[] loadPatterns,
            out eLoadForceType[] forceTypes,
            out eLoadDirection[] loadDirections,
            out double[] pointLoadValues,
            out double[] absoluteDistanceFromI,
            out double[] relativeDistanceFromI,
            out string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
            names = new string[0];
            loadPatterns = new string[0];
            pointLoadValues = new double[0];
            absoluteDistanceFromI = new double[0];
            relativeDistanceFromI = new double[0];
            coordinateSystems = new string[0];
            int[] csiForceTypes = new int[0];
            int[] csiLoadDirections = new int[0];

            _callCode = _sapModel.FrameObj.GetLoadPoint(name, ref _numberOfItems, 
                            ref names, ref loadPatterns, ref csiForceTypes, ref coordinateSystems, 
                            ref csiLoadDirections, ref pointLoadValues, 
                            ref absoluteDistanceFromI, ref relativeDistanceFromI, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            forceTypes = csiForceTypes.Cast<eLoadForceType>().ToArray();
            loadDirections = csiLoadDirections.Cast<eLoadDirection>().ToArray();
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the uniform load.</param>
        /// <param name="forceType">Force type for the point load for the load pattern.</param>
        /// <param name="loadDirection">The direction that the load is applied.</param>
        /// <param name="pointLoadValue">The load value of the point loads.
        /// [F] when <paramref name="forceType" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceType" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="distanceIsRelative">True: The specified distance item is a relative distance, otherwise it is an actual distance.</param>
        /// <param name="coordinateSystem">The name of the coordinate system associated with the uniform load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadPoint(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double pointLoadValue,
            double absoluteDistanceFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.SetLoadPoint(name, 
                            loadPattern, (int)loadDirection, (int)forceType, pointLoadValue, 
                            absoluteDistanceFromI, coordinateSystem, distanceIsRelative, replace, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadPoint(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.FrameObj.DeleteLoadPoint(name, 
                            loadPattern, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
