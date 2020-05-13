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
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Hinges;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Frame.
    /// </summary>
    /// <seealso cref="StructureObjectWithCrossSection{T}" />
    public class Frame : StructureObjectWithCrossSection<FrameSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The analysis results
        /// </summary>
        protected FrameResults _analysisResults;
        /// <summary>
        /// Analysis results.
        /// </summary>
        /// <value>The results.</value>
        public FrameResults AnalysisResults => _analysisResults ?? (_analysisResults = new FrameResults(Name));
        
        protected FrameDesignResults _frameDesignResults;
        public virtual FrameDesignResults FrameDesignResults => _frameDesignResults ?? (_frameDesignResults = new FrameDesignResults());

        protected FrameDesignOverwrites _frameDesignOverwrites;
        public virtual FrameDesignOverwrites FrameDesignOverwrites => _frameDesignOverwrites ?? (_frameDesignOverwrites = new FrameDesignOverwrites());

        /// <summary>
        /// Gets the design procedure that determines what code is used to design the frame.
        /// </summary>
        /// <value>The design procedure.</value>
        public virtual eFrameDesignProcedure DesignProcedure { get; internal set; }

        /// <summary>
        /// Gets or sets the frame modifier.
        /// </summary>
        /// <value>The frame modifier.</value>
        public virtual FrameModifier FrameModifier { get; internal set; }

        /// <summary>
        /// The lateral bracing
        /// </summary>
        protected List<FrameLateralBracing> _lateralBracing;
        /// <summary>
        /// Gets or sets the lateral bracing.
        /// </summary>
        /// <value>The lateral bracing.</value>
        public virtual ReadOnlyCollection<FrameLateralBracing> LateralBracing => new ReadOnlyCollection<FrameLateralBracing>(_lateralBracing);


        /// <summary>
        /// Gets or sets the hinges.
        /// </summary>
        /// <value>The hinges.</value>
        public virtual ReadOnlyCollection<FrameHinge> Hinges { get; internal set; }


        /// <summary>
        /// Gets or sets the insertion point.
        /// </summary>
        /// <value>The insertion point.</value>
        public virtual FrameInsertionPoint InsertionPoint { get; internal set; }


        /// <summary>
        /// Gets or sets the end length offsets.
        /// </summary>
        /// <value>The end length offsets.</value>
        public virtual FrameEndLengthOffset EndLengthOffsets { get; internal set; }


        /// <summary>
        /// Gets or sets the output stations.
        /// </summary>
        /// <value>The output stations.</value>
        public virtual FrameOutputStation OutputStations { get; internal set; }

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
        /// Gets or sets the frame release i.
        /// </summary>
        /// <value>The frame release i.</value>
        public virtual Release FrameReleaseI { get; internal set; }
        
        /// <summary>
        /// Gets or sets the frame release j.
        /// </summary>
        /// <value>The frame release j.</value>
        public virtual Release FrameReleaseJ { get; internal set; }

        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        internal string DesignSectionName { get; set; }
        /// <summary>
        /// The cross section
        /// </summary>
        protected FrameSection _designSection;
        /// <summary>
        /// Gets or sets the cross section.
        /// </summary>
        /// <value>The cross section.</value>
        public virtual FrameSection DesignSection => _designSection;

        /// <summary>
        /// The frame support i
        /// </summary>
        protected FrameSupport _frameSupportI;

        /// <summary>
        /// Gets or sets the frame support i.
        /// </summary>
        /// <value>The frame support i.</value>
        public virtual FrameSupport FrameSupportI
        {
            get => _frameSupportI;
            internal set => _frameSupportI = value;
        }

        /// <summary>
        /// The frame support j
        /// </summary>
        protected FrameSupport _frameSupportJ;
        /// <summary>
        /// Gets or sets the frame support j.
        /// </summary>
        /// <value>The frame support j.</value>
        public virtual FrameSupport FrameSupportJ
        {
            get => _frameSupportJ;
            internal set => _frameSupportJ = value;
        }

        /// <summary>
        /// The design orientation
        /// </summary>
        protected eFrameDesignOrientation _designOrientation;
        /// <summary>
        /// Gets or sets the design orientation.
        /// </summary>
        /// <value>The design orientation.</value>
        public virtual eFrameDesignOrientation DesignOrientation
        {
            get => _designOrientation;
            internal set => _designOrientation = value;
        }

        protected FrameAutoMesh _autoMesh;
        public virtual FrameAutoMesh AutoMesh
        {
            get => _autoMesh;
            internal set => _autoMesh = value;
        }

        protected FrameFireProofing _fireProofing;
        public virtual FrameFireProofing FireProofing
        {
            get => _fireProofing;
            internal set => _fireProofing = value;
        }

        /// <summary>
        /// The automatic select section name, if assigned.
        /// </summary>
        protected string _autoSelectSectionName;
        /// <summary>
        /// Gets or sets the name of the automatic selection section, if assigned.
        /// </summary>
        /// <value>The name of the automatic select section.</value>
        internal string AutoSelectSectionName
        {
            get => string.IsNullOrEmpty(_autoSelectSectionName) ? "N.A" : _autoSelectSectionName;
            set => _autoSelectSectionName = value;
        }


        /// <summary>
        /// Gets a value indicating whether area loads are allowed to be transferred to the frame.
        /// </summary>
        /// <value><c>true</c> if [load transfer from areas]; otherwise, <c>false</c>.</value>
        public virtual bool LoadTransferFromAreas { get; internal set; }

        /// <summary>
        /// The distributed loads
        /// </summary>
        protected List<FrameLoadDistributed> _distributedLoads;
        /// <summary>
        /// The distributed loads assigned to the frame.
        /// </summary>
        /// <value>The distributed loads.</value>
        public virtual ReadOnlyCollection<FrameLoadDistributed> DistributedLoads => new ReadOnlyCollection<FrameLoadDistributed>(_distributedLoads);

        /// <summary>
        /// The point loads
        /// </summary>
        protected List<FrameLoadPoint> _pointLoads;
        /// <summary>
        /// The point loads assigned to the frame.
        /// </summary>
        /// <value>The point loads.</value>
        public virtual ReadOnlyCollection<FrameLoadPoint> PointLoads => new ReadOnlyCollection<FrameLoadPoint>(_pointLoads);

        /// <summary>
        /// Gets a value indicating whether this frame is curved.
        /// </summary>
        /// <value><c>true</c> if this instance is curved; otherwise, <c>false</c>.</value>
        public virtual bool IsCurved { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Frame Factory(
            StructureComponentsProperties<FrameSection> componentsProperties,
            string uniqueName)
        {
            Frame item = new Frame(
                                componentsProperties, 
                                uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Frame(
            StructureComponentsProperties<FrameSection> componentsProperties,
            string name) : base(componentsProperties,
                                name) { }
        #endregion

        #region Creation
        /// <summary>
        /// Adds a new object whose corner points are at the specified coordinates.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
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
        internal static Frame AddByCoordinate(
            StructureComponentsProperties<FrameSection> componentsProperties,
            Tuple<Coordinate3DCartesian, Coordinate3DCartesian> coordinates,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
            List<Coordinate3DCartesian> coords = new List<Coordinate3DCartesian>()
            {
                coordinates.Item1,
                coordinates.Item2
            };
            List<Point> points = ConvertCoordsToPoints(componentsProperties, coords);

            return AddByPoint(componentsProperties,
                new Tuple<Point, Point>(points[0], points[1]),
                propertyName, 
                uniqueName);
        }

        /// <summary>
        /// Adds a new object whose corner points are specified by name.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="points">The point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="propertyName">This is either Default or the name of a defined property.
        /// If it is <see cref="Constants.DEFAULT" />, the program assigns a default property to the object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="uniqueName">This is an optional user specified name for the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="uniqueName" />.</param>
        /// <returns>Frame.</returns>
        internal static Frame AddByPoint(
            StructureComponentsProperties<FrameSection> componentsProperties,
            Tuple<Point, Point> points,
            string propertyName = Constants.DEFAULT,
            string uniqueName = "")
        {
            Frame item = Factory(componentsProperties, uniqueName);
            item._sectionName = propertyName;
            item._objectPoints = new List<Point>() {points.Item1, points.Item2};

            List<string> pointNames = new List<string>()
            {
                points.Item1.Name,
                points.Item2.Name,
            };
            item.PointNames = pointNames;

            return item;
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        public override void SetMass(double value, bool replace)
        {
            setMass(value, replace);
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        public override void DeleteMass()
        {
            deleteMass();
        }

        

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetMaterialOverwriteName()
        {
            return getMaterialOverwriteName();
        }

        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        public override void AddMaterialOverwrite(Material material)
        {
            setMaterialOverwrite(material);
        }

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        public override void RemoveMaterialOverwrite()
        {
            setMaterialOverwrite(null);
        }

        
        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetSectionName()
        {
            return getSectionName();
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(FrameSection section)
        {
            if (section == null) return;
            setSection(section);
        }
        
        #endregion

        #region Frame Properties
        /// <summary>
        /// Assigns a lateral bracing location to frame objects.
        /// </summary>
        /// <param name="lateralBracing">The lateral bracing.</param>
        public void SetLateralBracing(FrameLateralBracing lateralBracing)
        {
            if (_lateralBracing == null) _lateralBracing = new List<FrameLateralBracing>();
            _lateralBracing.Add(lateralBracing);
        }

        /// <summary>
        /// Deletes the lateral bracing assignments to the specified objects.
        /// </summary>
        /// <param name="bracingType">Indicates the bracing type to be deleted.</param>
        public void DeleteLateralBracing(eBracingType bracingType)
        {
            _lateralBracing?.RemoveAll(x => x.BracingType == bracingType);
        }
        #endregion

        #region Design
        /// <summary>
        /// Modifies the design section for the frame object.
        /// </summary>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame object.</param>
        internal void SetDesignSection(string nameSection)
        {
            DesignSectionName = nameSection;
        }

        /// <summary>
        /// Removes the design section for the specified frame object.
        /// </summary>
        internal void RemoveDesignSection()
        {
            DesignSectionName = string.Empty;
            _designSection = null;
        }
        

        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetPierName()
        {
            return getPierName();
        }

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public override void AddToPier(Pier pier)
        {
            addPier(pier);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromPier()
        {
            removePier();
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetSpandrelName()
        {
            return getSpandrelName();
        }

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public override void AddToSpandrel(Spandrel spandrel)
        {
            addSpandrel(spandrel);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromSpandrel()
        {
            removeSpandrel();
        }
        #endregion

        #region Loading
        // LoadTemperature
        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        public override void AddLoadTemperature(LoadTemperature temperatureLoad)
        {
            setLoadTemperature(temperatureLoad, replace: false);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        public override void ReplaceLoadTemperature(LoadTemperature temperatureLoad)
        {
            setLoadTemperature(temperatureLoad, replace: true);
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        public override void DeleteLoadTemperature(string loadPattern)
        {
            deleteLoad(loadPattern, TemperatureLoads);
        }

        // LoadDistributed
        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="distributedLoad">The distributed load.</param>
        public virtual void AddLoadDistributed(FrameLoadDistributed distributedLoad)
        {
            distributedLoad.DistanceFromI.SetLength(Length);
            addOrReplace(false, distributedLoad, _distributedLoads);
        }

        /// <summary>
        /// Assigns distributed loads to frame objects.
        /// </summary>
        /// <param name="distributedLoad">The distributed load.</param>
        public virtual void ReplaceLoadDistributed(FrameLoadDistributed distributedLoad)
        {
            distributedLoad.DistanceFromI.SetLength(Length);
            addOrReplace(true, distributedLoad, _distributedLoads);
        }

        /// <summary>
        /// Deletes the distributed load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        public virtual void DeleteLoadDistributed(string loadPattern)
        {
            deleteLoad(loadPattern, _distributedLoads);
        }

        // LoadPoint
        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="pointLoad">The point load.</param>
        public virtual void AddLoadPoint(FrameLoadPoint pointLoad)
        {
            pointLoad.DistanceFromI.SetLength(Length);
            addOrReplace(true, pointLoad, _pointLoads);
        }

        /// <summary>
        /// Assigns point loads to objects.
        /// </summary>
        /// <param name="pointLoad">The point load.</param>
        public virtual void ReplaceLoadPoint(FrameLoadPoint pointLoad)
        {
            pointLoad.DistanceFromI.SetLength(Length);
            addOrReplace(true, pointLoad, _pointLoads);
        }

        /// <summary>
        /// Deletes the point load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        public virtual void DeleteLoadPoint(string loadPattern)
        {
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
