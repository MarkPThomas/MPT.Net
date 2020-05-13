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

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions
{
    /// <summary>
    /// Represents a group in the application.
    /// </summary>
    /// <seealso cref="IUniqueName" />
    public class Group : 
        IUniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The objects
        /// </summary>
        protected readonly StructureObjects _objects;


        /// <summary>
        /// The properties
        /// </summary>
        protected GroupProperties _properties = new GroupProperties();

        internal void SetProperties(GroupProperties groupProperties)
        {
            _properties = groupProperties;
        }

        /// <summary>
        /// Name of an existing group. Cannot be "ALL".
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Display color for the group specified.
        /// </summary>
        /// <value>The color.</value>
        public virtual int Color => _properties.Color;
        
        /// <summary>
        /// Display color for the group specified.
        /// </summary>
        /// <value>The color.</value>
        public string ColorName { get; internal set; }

        /// <summary>
        /// True: The group is specified to be used for selection.
        /// </summary>
        /// <value><c>true</c> if [specified for selection]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForSelection => _properties.SpecifiedForSelection;

        /// <summary>
        /// True: The group is specified to be used for defining section cuts.
        /// </summary>
        /// <value><c>true</c> if [specified for section cut definition]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForSectionCutDefinition => _properties.SpecifiedForSectionCutDefinition;

        /// <summary>
        /// True: The group is specified to be used for defining steel frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForSteelDesign => _properties.SpecifiedForSteelDesign;

        /// <summary>
        /// True: The group is specified to be used for defining concrete frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for concrete design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForConcreteDesign => _properties.SpecifiedForConcreteDesign;

        /// <summary>
        /// True: The group is specified to be used for defining stages for nonlinear static analysis.
        /// </summary>
        /// <value><c>true</c> if [specified for static nl active stage]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForStaticNLActiveStage => _properties.SpecifiedForStaticNLActiveStage;

        /// <summary>
        /// True: The group is specified to be used for reporting auto seismic loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic seismic output]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForAutoSeismicOutput => _properties.SpecifiedForAutoSeismicOutput;

        /// <summary>
        /// True: The group is specified to be used for reporting auto wind loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic wind output]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForAutoWindOutput => _properties.SpecifiedForAutoWindOutput;

        /// <summary>
        /// True: The group is specified to be used for reporting group masses and weight.
        /// </summary>
        /// <value><c>true</c> if [specified for mass and weight]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForMassAndWeight => _properties.SpecifiedForMassAndWeight;

        /// <summary>
        /// True: The group is specified to be used for defining steel joist design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel joist design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForSteelJoistDesign => _properties.SpecifiedForSteelJoistDesign;

        /// <summary>
        /// True: The group is specified to be used for defining wall design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for wall design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForWallDesign => _properties.SpecifiedForWallDesign;

        /// <summary>
        /// True: The group is specified to be used for defining base plate design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for base plate design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForBasePlateDesign => _properties.SpecifiedForBasePlateDesign;

        /// <summary>
        /// True: The group is specified to be used for defining connection design design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for connection design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForConnectionDesign => _properties.SpecifiedForConnectionDesign;

        /// <summary>
        /// True: The group is specified to be used for defining colf formed frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for cold formed design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForColdFormedDesign => _properties.SpecifiedForColdFormedDesign;

        /// <summary>
        /// True: The group is specified to be used for defining alumnimum frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for aluminum design]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForAluminumDesign => _properties.SpecifiedForAluminumDesign;

        /// <summary>
        /// True: The group is specified to be used for reporting bridge response output.
        /// </summary>
        /// <value><c>true</c> if [specified for bridge response output]; otherwise, <c>false</c>.</value>
        public virtual bool SpecifiedForBridgeResponseOutput  => _properties.SpecifiedForBridgeResponseOutput;

        /// <summary>
        /// True: The group is specified to be used for defining steel joist design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel joist design]; otherwise, <c>false</c>.</value>
        public virtual bool SelectedForSteelDesign => _properties.SelectedForSteelDesign;

        /// <summary>
        /// True: The group is specified to be used for defining colf formed frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for cold formed design]; otherwise, <c>false</c>.</value>
        public virtual bool SelectedForColdFormedDesign => _properties.SelectedForColdFormedDesign;

        /// <summary>
        /// True: The group is specified to be used for defining alumnimum frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for aluminum design]; otherwise, <c>false</c>.</value>
        public virtual bool SelectedForAluminumDesign => _properties.SelectedForAluminumDesign;

        /// <summary>
        /// The points in the current group.
        /// </summary>
        /// <value>The points.</value>
        public virtual List<Point> Points { get; protected set; }

        /// <summary>
        /// The frames in the current group.
        /// </summary>
        /// <value>The frames.</value>
        public virtual List<Frame> Frames { get; protected set; }

        /// <summary>
        /// The areas in the current group.
        /// </summary>
        /// <value>The areas.</value>
        public virtual List<Area> Areas { get; protected set; }

        /// <summary>
        /// The cables in the current group.
        /// </summary>
        /// <value>The cables.</value>
        public virtual List<Cable> Cables { get; protected set; }

        /// <summary>
        /// The tendons in the current group.
        /// </summary>
        /// <value>The tendons.</value>
        public virtual List<Tendon> Tendons { get; protected set; }

        /// <summary>
        /// The solids in the current group.
        /// </summary>
        /// <value>The solids.</value>
        public virtual List<Solid> Solids { get; protected set; }

        /// <summary>
        /// The links in the current group.
        /// </summary>
        /// <value>The links.</value>
        public virtual List<Link> Links { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Group" /> class.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <param name="name">The name.</param>
        protected Group(
            StructureObjects objects, 
            string name) 
        {
            Name = name;
            _objects = objects;
        }

        /// <summary>
        /// A factory pattern to create new group objects.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">Group properties.</param>
        /// <returns>Group.</returns>
        internal static Group Factory(
            StructureObjects objects, 
            string uniqueName,
            GroupProperties properties = null)
        {
            Group group = new Group(objects, uniqueName) { _properties = properties };
            return group;
        }
        #endregion
        
        #region Selection
        /// <summary>
        /// Selects all objects in the group.
        /// </summary>
        public void Select()
        {
            foreach (var point in Points)
            {
                point.Select(); 
            }

            foreach (var frame in Frames)
            {
                frame.Select(); 
            }

            foreach (var area in Areas)
            {
                area.Select(); 
            }

            foreach (var link in Links)
            {
                link.Select(); 
            }

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
        }

        /// <summary>
        /// Deselects all objects in the group.
        /// </summary>
        public void Deselect()
        {
            foreach (var point in Points)
            {
                point.Deselect();
            }

            foreach (var frame in Frames)
            {
                frame.Deselect();
            }

            foreach (var area in Areas)
            {
                area.Deselect();
            }

            foreach (var link in Links)
            {
                link.Deselect();
            }

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
        }
        #endregion

        #region Group Assigns
        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(Point item)
        {
            HelperFunctions.AddUniqueItem(item, Points);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(Point item)
        {
            Points.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(Frame item)
        {
            HelperFunctions.AddUniqueItem(item, Frames);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(Frame item)
        {
            Frames.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(Area item)
        {
            HelperFunctions.AddUniqueItem(item, Areas);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(Area item)
        {
            Areas.Remove(item);
            item.RemoveFromGroup(this);
        }


        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(Link item)
        {
            HelperFunctions.AddUniqueItem(item, Links);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(Link item)
        {
            Links.Remove(item);
            item.RemoveFromGroup(this);
        }
        
        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        public void Add(Tendon item)
        {
            HelperFunctions.AddUniqueItem(item, Tendons);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        public void Remove(Tendon item)
        {
            Tendons.Remove(item);
            item.RemoveFromGroup(this);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        public void Add(Cable item)
        {
            HelperFunctions.AddUniqueItem(item, Cables);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        public void Remove(Cable item)
        {
            Cables.Remove(item);
            item.RemoveFromGroup(this);
        }

        /// <summary>
        /// Adds objects to the group.
        /// </summary>
        public void Add(Solid item)
        {
            HelperFunctions.AddUniqueItem(item, Solids);
            item.AddToGroup(this);
        }

        /// <summary>
        /// Removes objects from the group.
        /// </summary>
        public void Remove(Solid item)
        {
            Solids.Remove(item);
            item.RemoveFromGroup(this);
        }

        /// <summary>
        /// Clears (removes) all assignments from the specified group.
        /// </summary>
        public void Clear()
        {
            foreach (var point in Points)
            {
                Remove(point);
            }

            foreach (var frame in Frames)
            {
                Remove(frame);
            }

            foreach (var area in Areas)
            {
                Remove(area);
            }

            foreach (var link in Links)
            {
                Remove(link);
            }

            Points.Clear();
            Frames.Clear();
            Areas.Clear();
            Links.Clear();
            Cables.Clear();
            Tendons.Clear();
            Solids.Clear();
        }

        #endregion
    }
}
