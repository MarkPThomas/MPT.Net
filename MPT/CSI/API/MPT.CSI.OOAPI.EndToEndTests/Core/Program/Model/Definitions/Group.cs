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
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Represents a group in the application.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        protected static CSiApplication _app => Registry.Application;

        /// <summary>
        /// Name of an existing group. Cannot be "ALL".
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Display color for the group specified.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; protected set; } = -1;

        /// <summary>
        /// True: The group is specified to be used for selection.
        /// </summary>
        /// <value><c>true</c> if [specified for selection]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSelection  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining section cuts.
        /// </summary>
        /// <value><c>true</c> if [specified for section cut definition]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSectionCutDefinition  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining steel frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelDesign  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining concrete frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for concrete design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConcreteDesign { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining stages for nonlinear static analysis.
        /// </summary>
        /// <value><c>true</c> if [specified for static nl active stage]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForStaticNLActiveStage  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting auto seismic loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic seismic output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoSeismicOutput  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting auto wind loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic wind output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoWindOutput  { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting group masses and weight.
        /// </summary>
        /// <value><c>true</c> if [specified for mass and weight]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForMassAndWeight  { get; protected set; } = true;

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// True: The group is specified to be used for defining steel joist design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel joist design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelJoistDesign { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining wall design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for wall design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForWallDesign { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining base plate design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for base plate design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBasePlateDesign { get; protected set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining connection design design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for connection design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConnectionDesign { get; protected set; } = true;
#else
        /// <summary>
        /// True: The group is specified to be used for defining colf formed frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for cold formed design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForColdFormedDesign { get; protected set; } = true;
        
        /// <summary>
        /// True: The group is specified to be used for defining alumnimum frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for aluminum design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAluminumDesign { get; protected set; } = true;
        
        /// <summary>
        /// True: The group is specified to be used for reporting bridge response output.
        /// </summary>
        /// <value><c>true</c> if [specified for bridge response output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBridgeResponseOutput  { get; protected set; } = true;
#endif        
        /// <summary>
        /// The points in the current group.
        /// </summary>
        /// <value>The points.</value>
        public List<Node> Points { get; protected set; }

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

        /// <summary>
        /// A factory pattern to create new group objects.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Group.</returns>
        public static Group Factory(string uniqueName)
        {
            if (Registry.Groups.Keys.Contains(uniqueName)) return Registry.Groups[uniqueName];

            Group group = new Group(uniqueName);

            if (_app != null)
            {
                group.FillData();
            }

            Registry.Groups.Add(uniqueName, group);
            return group;
        }

        /// <summary>
        /// Retrieves the names of all defined groups.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList()
        {
            return new List<string>(_app.Model.Definitions.Groups.GetNameList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Group" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Group(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillGroup();
            FillAssignments();
        }

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
                switch (tuple.Item1)
                {
                    case eObjectType.Area:
                        Area area = Area.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(area, Areas);
                        break;
                    case eObjectType.Frame:
                        Frame frame = Frame.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(frame, Frames);
                        break;
                    case eObjectType.Link:
                        Link link = Link.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(link, Links);
                        break;
                    case eObjectType.Point:
                        Node node = Node.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(node, Points);
                        break;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                    case eObjectType.Cable:
                        Cable cable = Cable.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(cable, Cables);
                        break;
                    case eObjectType.Tendon:
                        Tendon tendon = Tendon.Factory(tuple.Item2);
                        HelperFunctions.AddUniqueItem(tendon, Tendons);
                        break;
                    case eObjectType.Solid:
                        Solid solid = Solid.Factory(tuple.Item2);
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
            if (_app == null) return new List<Tuple<eObjectType, string>>();
            _app.Model.Definitions.Groups.GetAssignments(Name, out var objectTypes, out var objectNames);

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
            _app?.Model.Selector.Group(Name, deselect: false);
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
            _app?.Model.Selector.Group(Name, deselect: true);
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
            if (_app == null) return;
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _app.Model.Definitions.Groups.GetGroup(Name,
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

            SpecifiedForSteelJoistDesign = specifiedForSteelJoistDesign;
            SpecifiedForWallDesign = specifiedForWallDesign;
            SpecifiedForBasePlateDesign = specifiedForBasePlateDesign;
            SpecifiedForConnectionDesign = specifiedForConnectionDesign;
#else
            _app.Model.Definitions.Groups.GetGroup(Name,
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
            Color = color;
            SpecifiedForSelection = specifiedForSelection;
            SpecifiedForSectionCutDefinition = specifiedForSectionCutDefinition;
            SpecifiedForSteelDesign = specifiedForSteelDesign;
            SpecifiedForConcreteDesign = specifiedForConcreteDesign;
            SpecifiedForStaticNLActiveStage = specifiedForStaticNLActiveStage;
            SpecifiedForAutoSeismicOutput = specifiedForAutoSeismicOutput;
            SpecifiedForAutoWindOutput = specifiedForAutoWindOutput;
            SpecifiedForMassAndWeight = specifiedForMassAndWeight;
        }

        /// <summary>
        /// Sets group properties, such as display color and usages.
        /// A new group is created if the name is of a nonexisting group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGroup()
        {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _app?.Model.Definitions.Groups.SetGroup(Name,
                Color,
                SpecifiedForSelection,
                SpecifiedForSectionCutDefinition,
                SpecifiedForSteelDesign,
                SpecifiedForConcreteDesign,
                SpecifiedForStaticNLActiveStage,
                SpecifiedForAutoSeismicOutput,
                SpecifiedForAutoWindOutput,
                SpecifiedForMassAndWeight,
                SpecifiedForSteelJoistDesign,
                SpecifiedForWallDesign,
                SpecifiedForBasePlateDesign,
                SpecifiedForConnectionDesign);
#else
            _app?.Model.Definitions.Groups.SetGroup(Name,
                Color,
                SpecifiedForSelection,
                SpecifiedForSectionCutDefinition,
                SpecifiedForSteelDesign,
                SpecifiedForConcreteDesign,
                SpecifiedForAluminumDesign,
                SpecifiedForColdFormedDesign,
                SpecifiedForStaticNLActiveStage,
                SpecifiedForBridgeResponseOutput,
                SpecifiedForAutoSeismicOutput,
                SpecifiedForAutoWindOutput,
                SpecifiedForMassAndWeight);
#endif
        }

        /// <summary>
        /// Deletes the specified group.
        /// "ALL" is a reserved group name and cannot be deleted.
        /// </summary>
        /// <exception cref="CSiReservedNameException">Cannot delete reserved group name " + All</exception>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Delete()
        {
            _app?.Model.Definitions.Groups.Delete(Name);
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
            _app?.Model.Definitions.Groups.ChangeName(Name, nameNew);
        }
#endif
        #endregion

        #region Group Assigns
        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Node item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Points);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Node item)
        {
            setGroupAssign(item, remove: true);
            Points.Remove(item);
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
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Tendon item)
        {
            setGroupAssign(item, remove: true);
            Tendons.Remove(item);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Cable item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Cables);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Cable item)
        {
            setGroupAssign(item, remove: true);
            Cables.Remove(item);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(Solid item)
        {
            setGroupAssign(item, remove: false);
            HelperFunctions.AddUniqueItem(item, Solids);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Remove(Solid item)
        {
            setGroupAssign(item, remove: true);
            Solids.Remove(item);
        }
#endif

        /// <summary>
        /// Clears (removes) all assignments from the specified group.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Clear()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _app?.Model.Definitions.Groups.Clear(Name);
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
        protected void setGroupAssign(Node item, bool remove)
        {
            _app?.Model.ObjectModel.PointObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Frame item, bool remove)
        {
            _app?.Model.ObjectModel.FrameObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Area item, bool remove)
        {
            _app?.Model.ObjectModel.AreaObject.SetGroupAssign(item.Name, Name, remove);
        }

        /// <summary>
        /// Sets the group assign.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        protected void setGroupAssign(Link item, bool remove)
        {
            _app?.Model.ObjectModel.LinkObject.SetGroupAssign(item.Name, Name, remove);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        protected void setGroupAssign(Tendon item, bool remove)
        {
         _app?.Model.ObjectModel.TendonObject.SetGroupAssign(item.Name, Name, remove);
        }

        protected void setGroupAssign(Cable item, bool remove)
        {
        _app?.Model.ObjectModel.CableObject.SetGroupAssign(item.Name, Name, remove);
        }

        protected void setGroupAssign(Solid item, bool remove)
        {
            _app?.Model.ObjectModel.SolidObject.SetGroupAssign(item.Name, Name, remove);
        }
#endif
        #endregion

    }
}
