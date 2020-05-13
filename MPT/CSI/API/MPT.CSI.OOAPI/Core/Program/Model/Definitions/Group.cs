// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-19-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="Group.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiGroups = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Groups;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Represents a group in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IUniqueName" />
    public class Group : CSiOoApiBaseBase, 
        IUniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The groups.</value>
        private ApiGroups _apiGroups => getApiGroups(_apiApp);

        /// <summary>
        /// The objects
        /// </summary>
        private StructureObjects _objects;


        /// <summary>
        /// The properties
        /// </summary>
        private GroupProperties _properties = new GroupProperties();

        /// <summary>
        /// Name of an existing group. Cannot be "ALL".
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Display color for the group specified.
        /// </summary>
        /// <value>The color.</value>
        public int Color => _properties.Color;

        /// <summary>
        /// True: The group is specified to be used for selection.
        /// </summary>
        /// <value><c>true</c> if [specified for selection]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSelection => _properties.SpecifiedForSelection;

        /// <summary>
        /// True: The group is specified to be used for defining section cuts.
        /// </summary>
        /// <value><c>true</c> if [specified for section cut definition]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSectionCutDefinition => _properties.SpecifiedForSectionCutDefinition;

        /// <summary>
        /// True: The group is specified to be used for defining steel frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelDesign => _properties.SpecifiedForSteelDesign;

        /// <summary>
        /// True: The group is specified to be used for defining concrete frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for concrete design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConcreteDesign => _properties.SpecifiedForConcreteDesign;

        /// <summary>
        /// True: The group is specified to be used for defining stages for nonlinear static analysis.
        /// </summary>
        /// <value><c>true</c> if [specified for static nl active stage]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForStaticNLActiveStage => _properties.SpecifiedForStaticNLActiveStage;

        /// <summary>
        /// True: The group is specified to be used for reporting auto seismic loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic seismic output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoSeismicOutput => _properties.SpecifiedForAutoSeismicOutput;

        /// <summary>
        /// True: The group is specified to be used for reporting auto wind loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic wind output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoWindOutput => _properties.SpecifiedForAutoWindOutput;

        /// <summary>
        /// True: The group is specified to be used for reporting group masses and weight.
        /// </summary>
        /// <value><c>true</c> if [specified for mass and weight]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForMassAndWeight => _properties.SpecifiedForMassAndWeight;

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// True: The group is specified to be used for defining steel joist design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel joist design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelJoistDesign => _properties.SpecifiedForSteelJoistDesign;

        /// <summary>
        /// True: The group is specified to be used for defining wall design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for wall design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForWallDesign => _properties.SpecifiedForWallDesign;

        /// <summary>
        /// True: The group is specified to be used for defining base plate design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for base plate design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBasePlateDesign => _properties.SpecifiedForBasePlateDesign;

        /// <summary>
        /// True: The group is specified to be used for defining connection design design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for connection design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConnectionDesign => _properties.SpecifiedForConnectionDesign;
#else
        /// <summary>
        /// True: The group is specified to be used for defining colf formed frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for cold formed design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForColdFormedDesign => _properties.SpecifiedForColdFormedDesign;
        
        /// <summary>
        /// True: The group is specified to be used for defining alumnimum frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for aluminum design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAluminumDesign => _properties.SpecifiedForAluminumDesign;
        
        /// <summary>
        /// True: The group is specified to be used for reporting bridge response output.
        /// </summary>
        /// <value><c>true</c> if [specified for bridge response output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBridgeResponseOutput  => _properties.SpecifiedForBridgeResponseOutput;
#endif        
        /// <summary>
        /// The points in the current group.
        /// </summary>
        /// <value>The points.</value>
        public List<Point> Points { get; protected set; }

        /// <summary>
        /// The frames in the current group.
        /// </summary>
        /// <value>The frames.</value>
        public List<Frame> Frames { get; protected set; }

        /// <summary>
        /// The areas in the current group.
        /// </summary>
        /// <value>The areas.</value>
        public List<Area> Areas { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The cables in the current group.
        /// </summary>
        /// <value>The cables.</value>
        public List<Cable> Cables { get; protected set; }

        /// <summary>
        /// The tendons in the current group.
        /// </summary>
        /// <value>The tendons.</value>
        public List<Tendon> Tendons { get; protected set; }

        /// <summary>
        /// The solids in the current group.
        /// </summary>
        /// <value>The solids.</value>
        public List<Solid> Solids { get; protected set; }
#endif

        /// <summary>
        /// The links in the current group.
        /// </summary>
        /// <value>The links.</value>
        public List<Link> Links { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Group" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="objects">The objects.</param>
        /// <param name="name">The name.</param>
        protected Group(
            ApiCSiApplication app, 
            StructureObjects objects, 
            string name) : base(app)
        {
            Name = name;
            _objects = objects;
        }


        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillGroup();
            FillAssignments();
        }

        /// <summary>
        /// A factory pattern to create new group objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="objects">The objects.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">Group properties.</param>
        /// <returns>Group.</returns>
        internal static Group Factory(
            ApiCSiApplication app,
            StructureObjects objects, 
            string uniqueName,
            GroupProperties properties = null)
        {
            Group group = new Group(app, objects, uniqueName) { _properties = properties };
            group.FillData();
            return group;
        }
        #endregion

        #region Static
        /// <summary>
        /// Returns the names of all defined groups.
        /// </summary>
        /// <param name="groups">The groups.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiGroups groups)
        {
            return (groups == null) ? new List<string>() : new List<string>(groups.GetNameList());
        }
        #endregion

        #region Query
        /// <summary>
        /// Retrieves the assignments to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillAssignments()
        {
            List<Tuple<eObjectType, string>> assignmentsList = GetAssignmentsList();
            foreach (var tuple in assignmentsList)
            {
                string objectName = tuple.Item2;
                switch (tuple.Item1)
                {
                    case eObjectType.Area:
                        Area area = _objects.Areas.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(area, Areas);
                        break;
                    case eObjectType.Frame:
                        Frame frame = _objects.Frames.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(frame, Frames);
                        break;
                    case eObjectType.Link:
                        Link link = _objects.Links.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(link, Links);
                        break;
                    case eObjectType.Point:
                        Point node = _objects.Points.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(node, Points);
                        break;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                    case eObjectType.Cable:
                        Cable cable = _objects.Cables.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(cable, Cables);
                        break;
                    case eObjectType.Tendon:
                        Tendon tendon = _objects.Tendons.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(tendon, Tendons);
                        break;
                    case eObjectType.Solid:
                        Solid solid = _objects.Solids.FillItem(objectName);
                        HelperFunctions.AddUniqueItem(solid, Solids);
                        break;
#endif
                    default:
                        break;
                }  
            }
        }


        /// <summary>
        /// Retrieves the assignments to the group as the object type and object name.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;eObjectType, System.String&gt;&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public List<Tuple<eObjectType, string>> GetAssignmentsList()
        {
            if (_apiGroups == null) return new List<Tuple<eObjectType, string>>();
            _apiGroups.GetAssignments(Name, out var objectTypes, out var objectNames);

            return objectTypes.Select((t, i) => new Tuple<eObjectType, string>(t, objectNames[i])).ToList();
        }
        #endregion

        #region Selection
        /// <summary>
        /// Selects all objects in the group.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Select()
        {
            _apiApp.Model.Selector.Group(Name, deselect: false);
            //Node
            foreach (var point in Points)
            {
                point.Select(); 
            }
            //Frame
            foreach (var frame in Frames)
            {
                frame.Select(); 
            }
            //Area
            foreach (var area in Areas)
            {
                area.Select(); 
            }
            //Link
            foreach (var link in Links)
            {
                link.Select(); 
            }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            foreach (var cable in Cables)
            {
                cable.Select(); 
            }
            
            foreach (var tendon in Tendons)
            {
                tendon.Select(); 
            }

            foreach (var solid in Solids)
            {
                solid.Select(); 
            }
#endif
        }

        /// <summary>
        /// Deselects all objects in the group.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Deselect()
        {
            _apiApp.Model.Selector.Group(Name, deselect: true);
            //Node
            foreach (var point in Points)
            {
                point.Deselect();
            }
            //Frame
            foreach (var frame in Frames)
            {
                frame.Deselect();
            }
            //Area
            foreach (var area in Areas)
            {
                area.Deselect();
            }
            //Link
            foreach (var link in Links)
            {
                link.Deselect();
            }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            foreach (var tendon in Tendons)
            {
                tendon.Deselect();
            }

            foreach (var cable in Cables)
            {
                cable.Deselect();
            }

            foreach (var solid in Solids)
            {
                solid.Deselect();
            }
#endif
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Gets the group properties, such as display color and usages.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillGroup()
        {
            if (_apiGroups == null) return;
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _apiGroups.GetGroup(Name,
                out var color,
                out var specifiedForSelection,
                out var specifiedForSectionCutDefinition,
                out var specifiedForSteelDesign,
                out var specifiedForConcreteDesign,
                out var specifiedForStaticNLActiveStage,
                out var specifiedForAutoSeismicOutput,
                out var specifiedForAutoWindOutput,
                out var specifiedForMassAndWeight,
                out var specifiedForSteelJoistDesign,
                out var specifiedForWallDesign,
                out var specifiedForBasePlateDesign,
                out var specifiedForConnectionDesign);

            _properties.SpecifiedForSteelJoistDesign = specifiedForSteelJoistDesign;
            _properties.SpecifiedForWallDesign = specifiedForWallDesign;
            _properties.SpecifiedForBasePlateDesign = specifiedForBasePlateDesign;
            _properties.SpecifiedForConnectionDesign = specifiedForConnectionDesign;
#else
            _apiGroups.GetGroup(Name,
                out var color,
                out var specifiedForSelection,
                out var specifiedForSectionCutDefinition,
                out var specifiedForSteelDesign,
                out var specifiedForConcreteDesign,
                out var specifiedForAluminumDesign,
                out var specifiedForColdFormedDesign,
                out var specifiedForStaticNLActiveStage,
                out var specifiedForBridgeResponseOutput,
                out var specifiedForAutoSeismicOutput,
                out var specifiedForAutoWindOutput,
                out var specifiedForMassAndWeight);
    
            SpecifiedForAluminumDesign = specifiedForAluminumDesign;
            SpecifiedForColdFormedDesign = specifiedForColdFormedDesign;
            SpecifiedForBridgeResponseOutput = specifiedForBridgeResponseOutput;
#endif
            _properties.Color = color;
            _properties.SpecifiedForSelection = specifiedForSelection;
            _properties.SpecifiedForSectionCutDefinition = specifiedForSectionCutDefinition;
            _properties.SpecifiedForSteelDesign = specifiedForSteelDesign;
            _properties.SpecifiedForConcreteDesign = specifiedForConcreteDesign;
            _properties.SpecifiedForStaticNLActiveStage = specifiedForStaticNLActiveStage;
            _properties.SpecifiedForAutoSeismicOutput = specifiedForAutoSeismicOutput;
            _properties.SpecifiedForAutoWindOutput = specifiedForAutoWindOutput;
            _properties.SpecifiedForMassAndWeight = specifiedForMassAndWeight;
        }

        /// <summary>
        /// Sets the group.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void SetGroup(GroupProperties properties)
        {
            setGroup(_apiApp, Name, properties);
            _properties = properties;
        }

        /// <summary>
        /// Adds the group.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="objects">The objects.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>Group.</returns>
        internal static Group AddGroup(
            ApiCSiApplication app, 
            StructureObjects objects,
            string name, 
            GroupProperties properties)
        {
            setGroup(app, name, properties);
            return Factory(app, objects, name, properties);
        }


        /// <summary>
        /// Sets group properties, such as display color and usages.
        /// A new group is created if the name is of a nonexisting group.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected static void setGroup(ApiCSiApplication app, string name, GroupProperties properties)
        {
            ApiGroups apiGroups = getApiGroups(app);
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            apiGroups.SetGroup(name,
                properties.Color,
                properties.SpecifiedForSelection,
                properties.SpecifiedForSectionCutDefinition,
                properties.SpecifiedForSteelDesign,
                properties.SpecifiedForConcreteDesign,
                properties.SpecifiedForStaticNLActiveStage,
                properties.SpecifiedForAutoSeismicOutput,
                properties.SpecifiedForAutoWindOutput,
                properties.SpecifiedForMassAndWeight,
                properties.SpecifiedForSteelJoistDesign,
                properties.SpecifiedForWallDesign,
                properties.SpecifiedForBasePlateDesign,
                properties.SpecifiedForConnectionDesign);
#else
            apiGroups.SetGroup(name,
                properties.Color,
                properties.SpecifiedForSelection,
                properties.SpecifiedForSectionCutDefinition,
                properties.SpecifiedForSteelDesign,
                properties.SpecifiedForConcreteDesign,
                properties.SpecifiedForAluminumDesign,
                properties.SpecifiedForColdFormedDesign,
                properties.SpecifiedForStaticNLActiveStage,
                properties.SpecifiedForBridgeResponseOutput,
                properties.SpecifiedForAutoSeismicOutput,
                properties.SpecifiedForAutoWindOutput,
                properties.SpecifiedForMassAndWeight);
#endif
        }

        /// <summary>
        /// Deletes the specified group.
        /// "ALL" is a reserved group name and cannot be deleted.
        /// </summary>
        /// <exception cref="CSiReservedNameException">Cannot delete reserved group name " + All</exception>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal void Delete()
        {
            _apiGroups.Delete(Name);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Changes the name of the group.
        /// "ALL" is a reserved group name and cannot be changed.
        /// </summary>
        /// <param name="nameNew">New name for the group. Cannot be "ALL".</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ChangeName(string nameNew)
        {
            _apiGroups.ChangeName(Name, nameNew);
        }
#endif
        #endregion

        #region Group Assigns
        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Point item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Points);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Point item)
        {
            setGroupAssign(item, remove: true);
            Points.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Frame item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Frames);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Frame item)
        {
            setGroupAssign(item, remove: true);
            Frames.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Area item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Areas);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Area item)
        {
            setGroupAssign(item, remove: true);
            Areas.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Link item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Links);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Link item)
        {
            setGroupAssign(item, remove: true);
            Links.Remove(item);
            item.RemoveFromGroup(this);
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Tendon item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Tendons);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Tendon item)
        {
            setGroupAssign(item, remove: true);
            Tendons.Remove(item);
            item.RemoveFromGroup(this);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Cable item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Cables);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Cable item)
        {
            setGroupAssign(item, remove: true);
            Cables.Remove(item);
            item.RemoveFromGroup(this);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Solid item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Solids);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Solid item)
        {
            setGroupAssign(item, remove: true);
            Solids.Remove(item);
            item.RemoveFromGroup(this);
        }
#endif

        /// <summary>
        /// Clears (removes) all assignments from the specified group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Clear()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _apiGroups?.Model.Definitions.Groups.Clear(Name);
#else
            //Node
            foreach (var point in Points)
            {
                Remove(point);
            }
            //Frame
            foreach (var frame in Frames)
            {
                Remove(frame);
            }
            //Area
            foreach (var area in Areas)
            {
                Remove(area);
            }
            //Link
            foreach (var link in Links)
            {
                Remove(link);
            }
#endif
            Points.Clear();
            Frames.Clear();
            Areas.Clear();
            Links.Clear();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            Cables.Clear();
            Tendons.Clear();
            Solids.Clear();
#endif
        }

        #endregion

        #region API Functions
        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Point item, bool remove)
        {
            _apiApp?.Model.ObjectModel.PointObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Frame item, bool remove)
        {
            _apiApp?.Model.ObjectModel.FrameObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Area item, bool remove)
        {
            _apiApp?.Model.ObjectModel.AreaObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Link item, bool remove)
        {
            _apiApp?.Model.ObjectModel.LinkObject.SetGroupAssign(item.Name, Name, remove);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        protected void setGroupAssign(Tendon item, bool remove)
        {
            _apiApp?.Model.ObjectModel.TendonObject.SetGroupAssign(item.Name, Name, remove);
        }

        protected void setGroupAssign(Cable item, bool remove)
        {
            _apiApp?.Model.ObjectModel.CableObject.SetGroupAssign(item.Name, Name, remove);
        }

        protected void setGroupAssign(Solid item, bool remove)
        {
            _apiApp?.Model.ObjectModel.SolidObject.SetGroupAssign(item.Name, Name, remove);
        }
#endif
        #endregion

    }
}
