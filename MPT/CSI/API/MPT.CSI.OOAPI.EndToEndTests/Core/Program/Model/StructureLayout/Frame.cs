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
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads;
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.Design;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using FrameSection = MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.FrameSections.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Frame.
    /// </summary>
    /// <seealso cref="StructureObject2D" />
    public class Frame : StructureObject2D
    {
        /// <summary>
        /// The frame object API object.
        /// </summary>
        /// <value>The frame object.</value>
        protected static FrameObject _frameObject => Registry.ObjectModeler?.FrameObject;

        /// <summary>
        /// Analysis results.
        /// </summary>
        /// <value>The results.</value>
        public FrameResults AnalysisResults { get; protected set; }

        /// <summary>
        /// Steel design results.
        /// </summary>
        /// <value>The steel design results.</value>
        public SteelDesignResults SteelDesignResults { get; protected set; }

        /// <summary>
        /// Concrete design results.
        /// </summary>
        /// <value>The concrete design results.</value>
        public ConcreteDesignResults ConcreteDesignResults { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Aluminum beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public AluminumDesignResults AluminumDesignResults { get; protected set; }

        /// <summary>
        /// Cold-formed steel beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public SteelColdFormedDesignResults SteelColdFormedDesignResults { get; protected set; }
#else        
        /// <summary>
        /// Composite beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public CompositeBeamDesignResults CompositeBeamDesignResults { get; protected set; }
#endif

        /// <summary>
        /// Gets or sets the node i.
        /// </summary>
        /// <value>The node i.</value>
        public Node NodeI { get; protected set; }

        /// <summary>
        /// Gets or sets the node j.
        /// </summary>
        /// <value>The node j.</value>
        public Node NodeJ { get; protected set; }

        /// <summary>
        /// Angle of rotation of the local axis about the local-1 axis. [deg].
        /// </summary>
        /// <value>The angle.</value>
        public double Angle { get; protected set; }

        /// <summary>
        /// Rotation of frame about local 1-axis counter-clockwise from default orientation.
        /// </summary>
        /// <value>The local axis alpha.</value>
        public LocalAxis LocalAxis { get; set; }

        /// <summary>
        /// The cross-section associated with the frame.
        /// </summary>
        /// <value>The section.</value>
        public FrameSection Section { get; protected set; }

        /// <summary>
        /// Three joint offset distances for point I of the frame. [L].
        /// </summary>
        /// <value>The point i offset.</value>
        public Displacements PointIOffset { get; protected set; }

        /// <summary>
        /// Three joint offset distances for point J of the frame. [L].
        /// </summary>
        /// <value>The point j offset.</value>
        public Displacements PointJOffset { get; protected set; }

        /// <summary>
        /// The cardinal point specifies the relative position of the frame section on the line representing the frame object.
        /// </summary>
        /// <value>The cardinal insertion point.</value>
        public eCardinalInsertionPoint CardinalInsertionPoint { get; protected set; }

        /// <summary>
        /// Te distributed loads assigned to the frame.
        /// </summary>
        /// <value>The distributed loads.</value>
        public List<FrameLoadDistributed> DistributedLoads { get; protected set; } = new List<FrameLoadDistributed>();

        /// <summary>
        /// The point loads assigned to the frame.
        /// </summary>
        /// <value>The point loads.</value>
        public List<FrameLoadPoint> PointLoads { get; protected set; } = new List<FrameLoadPoint>();

        /// <summary>
        /// Gets all frame objects from the application.
        /// </summary>
        /// <returns>List&lt;Frame&gt;.</returns>
        public static List<Frame> GetAll()
        {
            List<Frame> objects = new List<Frame>();
            if (_frameObject == null) return objects;
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _frameObject.GetAllFrames(
                out var frameNames,
                out var sectionNames,
                out var storyNames,
                out var pointINames,
                out var pointJNames,
                out var pointICoordinates,
                out var pointJCoordinates,
                out var angles,
                out var pointIOffsets,
                out var pointJOffsets,
                out var cardinalInsertionPoints,
                Constants.CoordinateSystem);
            for (int i = 0; i < frameNames.Length; i++)
            {
                Node nodeI = Node.Factory(pointICoordinates[i].X, pointICoordinates[i].Y, pointICoordinates[i].Z, pointINames[i]);
                Node nodeJ = Node.Factory(pointJCoordinates[i].X, pointJCoordinates[i].Y, pointJCoordinates[i].Z, pointJNames[i]);
                FrameSection section = FrameSection.Factory(sectionNames[i]);

                Frame frame = Factory(frameNames[i], nodeI, nodeJ,
                    storyNames[i], angles[i], pointIOffsets[i], pointJOffsets[i], cardinalInsertionPoints[i], section);

                objects.Add(frame);
            }
#else
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                Frame frame = Factory(name);
                objects.Add(frame);
            }
#endif
            return objects;
        }

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="nodeI">The node i.</param>
        /// <param name="nodeJ">The node j.</param>
        /// <param name="storyName">Name of the story.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="pointIOffset">The point i offset.</param>
        /// <param name="pointJOffset">The point j offset.</param>
        /// <param name="cardinalInsertionPoint">The cardinal insertion point.</param>
        /// <param name="section">The section.</param>
        /// <returns>Frame.</returns>
        public static Frame Factory(string uniqueName, Node nodeI, Node nodeJ,
            string storyName = "",
            double angle = 0,
            Displacements pointIOffset = new Displacements(),
            Displacements pointJOffset = new Displacements(),
            eCardinalInsertionPoint cardinalInsertionPoint = eCardinalInsertionPoint.Centroid,
            FrameSection section = null)
        {
            if (Registry.Frames.Keys.Contains(uniqueName)) return Registry.Frames[uniqueName];

            Frame frame = new Frame(nodeI, nodeJ, uniqueName)
            {
                    Story = storyName,
                    Angle = angle,
                    PointIOffset = pointIOffset,
                    PointJOffset = pointJOffset,
                    CardinalInsertionPoint = cardinalInsertionPoint,
                    Section = section
            };
            
            Registry.Frames.Add(uniqueName, frame);
            return Registry.Frames[uniqueName];
        }

        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        public static Frame Factory(string uniqueName)
        {
            return Factory(uniqueName, _frameObject, Registry.Frames);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        public static List<UniqueLabelNamePair> GetLabelNameList(CSiApplication app)
        {
            return getLabelNameList(_frameObject);
        }
#endif

        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList()
        {
            return getNameList(_frameObject);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Frame(string name) : base(name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="nodeI">The node i.</param>
        /// <param name="nodeJ">The node j.</param>
        /// <param name="name">The name.</param>
        public Frame(Node nodeI, Node nodeJ, string name = "") : base(name)
        {
            NodeI = nodeI;
            NodeJ = nodeJ;
            delta = nodeJ - nodeI;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// This method is not meant to be used publicly but is necessary for the factory method.
        /// </summary>
        public Frame() : base(string.Empty) { }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            AnalysisResults = new FrameResults(Name);
            SteelDesignResults = new SteelDesignResults(Name);
            ConcreteDesignResults = new ConcreteDesignResults(Name);
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            AluminumDesignResults = new AluminumDesignResults(Name);
            SteelColdFormedDesignResults = new SteelColdFormedDesignResults(Name);
#else
            CompositeBeamDesignResults = new CompositeBeamDesignResults(Name);
#endif
        }
        
        public bool FillDesignResults()
        {
            Material currentMaterialUsed = materialUsed();
            switch (currentMaterialUsed)
            {
                case Steel _:
                    if (SteelDesignResults.ResultsAreAvailable)
                    {
                        SteelDesignResults.FillDesignResults();
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
                        CompositeBeamDesignResults.FillDesignResults();
#endif
                        return true;
                    }
                    break;
                case Concrete _:
                    if (ConcreteDesignResults.ResultsAreAvailable)
                    {
                        ConcreteDesignResults.FillDesignResults();
                        return true;
                    }
                    break;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                case Aluminum _:
                    if (AluminumDesignResults.ResultsAreAvailable)
                    {
                        AluminumDesignResults.FillDesignResults();
                        return true;
                    }
                    break;
                case ColdFormed _:
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

        protected Material materialUsed()
        {
            return MaterialOverwrite ?? Section.Material;
        }

        #region Query

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
            getNameFromLabel(_frameObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_frameObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_frameObject);
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_frameObject);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID()
        {
            setGUID(_frameObject);
        }


        /// <summary>
        /// Retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_frameObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            getTransformationMatrix(_frameObject);
        }

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillPoints()
        {
            string[] points = getPoints(_frameObject);
            if (points.Length > 0)
            {
                NodeI = Node.Factory(points[0]);
            }

            if (points.Length > 1)
            {
                NodeJ = Node.Factory(points[1]);
            }
        }
#endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_frameObject);
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLocalAxes(AngleLocalAxes angleOffset)
        {
            setLocalAxes(_frameObject, angleOffset);
            AngleOffset = angleOffset;
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
            changeName(_frameObject, newName);
        }


        /// <summary>
        /// The function deletes a specified frame object.
        /// It returns an error if the specified object cannot be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Delete()
        {
            delete(_frameObject);
        }
#endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void GetSelected()
        {
            getSelected(_frameObject);
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
            setSelected(_frameObject);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_app?.Model.ObjectModel.FrameObject);
        }
#endif
#endregion

        #region Modifiers

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteModifiers()
        {
            deleteModifiers(_frameObject);
        }
#endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMass()
        {
            getMass(_frameObject);
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
            setMass(_frameObject, value, replace);
        }


        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteMass()
        {
            deleteMass(_frameObject);
        }


        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMaterialOverwrite()
        {
            getMaterialOverwrite(_frameObject);
        }


        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddMaterialOverwrite(Material material)
        {
            setMaterialOverwrite(_frameObject, material);
        }

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void RemoveMaterialOverwrite()
        {
            setMaterialOverwrite(_frameObject, null);
        }


        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillSection()
        {
            string sectionName = getSection(_frameObject);
            Section = FrameSection.Factory(sectionName);
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(FrameSection section)
        {
            if (section == null) return;
            Section = section;
            SetSection();
        }

        /// <summary>
        /// Assigns the section property to an object.
        /// </summary>
        public override void SetSection()
        {
            setSection(_frameObject, Section?.Name);
        }
#endregion

        #region Frame Properties
        #endregion

        #region Support & Connections
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_frameObject);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        public override void SetSpringAssignment()
        {
            setSpringAssignment(_frameObject);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _frameObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }
#endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        public override void FillPier()
        {
            getPier(_frameObject);
        }

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public override void AddToPier(Pier pier)
        {
            addPier(_frameObject, pier);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromPier()
        {
            removePier(_frameObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        public override void FillSpandrel()
        {
            getSpandrel(_frameObject);
        }

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public override void AddToSpandrel(Spandrel spandrel)
        {
            addSpandrel(_frameObject, spandrel);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromSpandrel()
        {
            removeSpandrel(_frameObject);
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
            getLoadTemperature(_frameObject);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLoadTemperature(LoadTemperature temperatureLoad, bool replace)
        {
           setLoadTemperature(_frameObject, temperatureLoad, replace);
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadTemperature(CSiApplication app, string loadPattern)
        {
            app.Model.ObjectModel.FrameObject.DeleteLoadTemperature(Name, loadPattern);
            deleteLoad(loadPattern, TemperatureLoads);
        }

        // LoadDistributed
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadDistributed(CSiApplication app)
        {
            // TODO: Add to API project?
            app.Model.ObjectModel.FrameObject.GetLoadDistributed(Name, 
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

            for (int i = 0; i < names.Length; i++)
            {
                FrameLoadDistributed distributedLoad = new FrameLoadDistributed
                {
                    LoadPattern = loadPatterns[i],
                    ForceType = forceTypes[i],
                    LoadDirection = loadDirections[i],
                    StartLoadValue = startLoadValues[i],
                    EndLoadValue = endLoadValues[i],
                    AbsoluteDistanceStartFromI = absoluteDistanceStartFromI[i],
                    AbsoluteDistanceEndFromI = absoluteDistanceEndFromI[i],
                    RelativeDistanceStartFromI = relativeDistanceStartFromI[i],
                    RelativeDistanceEndFromI = relativeDistanceEndFromI[i],
                    CoordinateSystem = coordinateSystems[i],
                };

                DistributedLoads.Add(distributedLoad);
            }
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="distributedLoad">The distributed load.</param>
        /// <param name="replace">True: All previous distributed loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadDistributed(CSiApplication app,
            FrameLoadDistributed distributedLoad,
            bool replace = true)
        {
            // TODO: Add to API project?
            // TODO: For load, ensure that absolute distance values are always created, even if relative values are used.
            app.Model.ObjectModel.FrameObject.SetLoadDistributed(Name,
                distributedLoad.LoadPattern,
                distributedLoad.ForceType,
                distributedLoad.LoadDirection,
                distributedLoad.StartLoadValue,
                distributedLoad.EndLoadValue,
                distributedLoad.AbsoluteDistanceStartFromI,
                distributedLoad.AbsoluteDistanceEndFromI,
                distanceIsRelative: false,
                coordinateSystem: distributedLoad.CoordinateSystem,
                replace: replace);
        }

        /// <summary>
        /// Deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadDistributed(CSiApplication app, string loadPattern)
        {
            app.Model.ObjectModel.FrameObject.DeleteLoadDistributed(Name, loadPattern);
            int deleteIndex = DistributedLoads.FindIndex(f => f.LoadPattern == loadPattern);
            DistributedLoads.RemoveAt(deleteIndex);
        }


        // LoadPoint
        /// <summary>
        /// Returns the distributed load assignments to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadPoint(CSiApplication app)
        {
            // TODO: Add to API project?
            app.Model.ObjectModel.FrameObject.GetLoadPoint(Name, 
                out var names,
                out var loadPatterns,
                out var forceTypes,
                out var loadDirections,
                out var pointLoadValues,
                out var absoluteDistanceFromI,
                out var relativeDistanceFromI,
                out var coordinateSystems);

            for (int i = 0; i < names.Length; i++)
            {
                FrameLoadPoint pointLoad = new FrameLoadPoint
                {
                    LoadPattern = loadPatterns[i],
                    ForceType = forceTypes[i],
                    LoadDirection = loadDirections[i],
                    PointLoadValue = pointLoadValues[i],
                    AbsoluteDistanceFromI = absoluteDistanceFromI[i],
                    RelativeDistanceFromI = relativeDistanceFromI[i],
                    CoordinateSystem = coordinateSystems[i],
                };

                PointLoads.Add(pointLoad);
            }
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pointLoad">The point load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadPoint(CSiApplication app,
            FrameLoadPoint pointLoad,
            bool replace = true)
        {
            // TODO: Add to API project?
            // TODO: For load, ensure that absolute distance values are always created, even if relative values are used.
            app.Model.ObjectModel.FrameObject.SetLoadPoint(Name, 
                pointLoad.LoadPattern,
                pointLoad.ForceType,
                pointLoad.LoadDirection,
                pointLoad.PointLoadValue,
                pointLoad.AbsoluteDistanceFromI,
                distanceIsRelative: false,
                coordinateSystem: pointLoad.CoordinateSystem,
                replace: true);
        }

        /// <summary>
        /// Deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadPoint(CSiApplication app, string loadPattern)
        {
            app.Model.ObjectModel.FrameObject.DeleteLoadPoint(Name, loadPattern);
            deleteLoad(loadPattern, PointLoads);
        }
#endregion





        /// <summary>
        /// Gets or sets the delta.
        /// </summary>
        /// <value>The delta.</value>
        protected Node delta { get; set; }

        /// <summary>
        /// Lengthes this instance.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return delta.Length();
        }

        /// <summary>
        /// Rotation in the X-Y plane about the vertical Z-axis.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double ThetaZ()
        {
            return Math.Atan(delta.Y/delta.X);
        }

        /// <summary>
        /// Nodes the intersects.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool NodeIntersects(Node node)
        {
            // TODO: This can use geometry library.
            // Check bounds
            if ((node.X > NodeI.X && node.X > NodeI.X) ||
                (node.X < NodeI.X && node.X < NodeI.X)) return false;

            if ((node.Y > NodeI.Y && node.Y > NodeI.Y) ||
                (node.Y < NodeI.Y && node.Y < NodeI.Y)) return false;

            if ((node.Z > NodeI.Z && node.Z > NodeI.Z) ||
                (node.Z < NodeI.Z && node.Z < NodeI.Z)) return false;

            // Check line
            double xRatio = (node.X - NodeI.X) / delta.X;
            double yRatio = (node.Y - NodeI.Y) / delta.Y;
            double zRatio = (node.Z - NodeI.Z) / delta.Z;
            return (Math.Abs(xRatio - yRatio) < Constants.Tolerance && 
                    Math.Abs(xRatio - zRatio) < Constants.Tolerance);
        }

        /// <summary>
        /// Determines whether [is x coordinate matching] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if [is x coordinate matching] [the specified node]; otherwise, <c>false</c>.</returns>
        public bool IsXCoordinateMatching(Node node)
        {
            return (Math.Abs(node.X - NodeI.X) < Constants.Tolerance ||
                    Math.Abs(node.X - NodeJ.X) < Constants.Tolerance);
        }
        /// <summary>
        /// Determines whether [is y coordinate matching] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if [is y coordinate matching] [the specified node]; otherwise, <c>false</c>.</returns>
        public bool IsYCoordinateMatching(Node node)
        {
            return (Math.Abs(node.Y - NodeI.Y) < Constants.Tolerance ||
                    Math.Abs(node.Y - NodeJ.Y) < Constants.Tolerance);
        }

        /// <summary>
        /// Determines whether [is xy coordinate matching] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if [is xy coordinate matching] [the specified node]; otherwise, <c>false</c>.</returns>
        public bool IsXYCoordinateMatching(Node node)
        {
            return ((Math.Abs(node.Y - NodeI.Y) < Constants.Tolerance && 
                     Math.Abs(node.X - NodeI.X) < Constants.Tolerance)||
                    (Math.Abs(node.Y - NodeJ.Y) < Constants.Tolerance &&
                     Math.Abs(node.X - NodeJ.X) < Constants.Tolerance));
        }
        /// <summary>
        /// Determines whether [is z coordinate matching] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if [is z coordinate matching] [the specified node]; otherwise, <c>false</c>.</returns>
        public bool IsZCoordinateMatching(Node node)
        {
            return (Math.Abs(node.Z - NodeI.Z) < Constants.Tolerance ||
                    Math.Abs(node.Z - NodeJ.Z) < Constants.Tolerance);
        }

        /// <summary>
        /// Determines whether [is intersecting in plane xy] [the specified frame].
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns><c>true</c> if [is intersecting in plane xy] [the specified frame]; otherwise, <c>false</c>.</returns>
        public bool IsIntersectingInPlaneXY(Frame frame)
        {
            return (IsXYCoordinateMatching(frame.NodeI) ||
                   (IsXYCoordinateMatching(frame.NodeJ)));
        }

        /// <summary>
        /// Determines whether the specified frame is connected.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns><c>true</c> if the specified frame is connected; otherwise, <c>false</c>.</returns>
        public bool IsConnected(Frame frame)
        {
            return (frame.NodeI.IsEqual(NodeI) ||
                    frame.NodeI.IsEqual(NodeJ) ||
                    frame.NodeJ.IsEqual(NodeJ));
        }
    }
}
