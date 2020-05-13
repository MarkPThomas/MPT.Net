// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StructureObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    // ICountable - not used
    // IListableNames - static
    // IGroupAssignable - on Group object

    /// <summary>
    /// Base class for any object located in the model layout.
    /// </summary>
    /// <seealso cref="UniqueName" />
    public abstract class StructureObject : UniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The groups
        /// </summary>
        private readonly List<Group> _groups = new List<Group>();
        /// <summary>
        /// Groups containing the object.
        /// </summary>
        /// <value>The groups.</value>
        public ReadOnlyCollection<Group> Groups => new ReadOnlyCollection<Group>(_groups);

        /// <summary>
        /// The unique name.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique.</value>
        public override string Name
        {
            get => _identifier.Name;
            internal set => _identifier.Name = value;
        }

        // LocalAxesAdvanced

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        protected UniqueLabelNamePair _identifier { get; set; }

        /// <summary>
        /// The story that the object is located on.
        /// </summary>
        /// <value>The story.</value>
        public string Story
        {
            get => _identifier.Story;
            protected set => _identifier.Story = value;
        }

        /// <summary>
        /// The label associated with the object.
        /// This is only unique within a given story.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get => _identifier.Label;
            protected set => _identifier.Label = value;
        }


        /// <summary>
        /// The name of an existing spring property.
        /// </summary>
        /// <value>The spring.</value>
        public string Spring { get; internal set; }
        
        /// <summary>
        /// If blank, program assigns one automatically.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; internal set; }


        /// <summary>
        /// Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.
        /// </summary>
        /// <value>The direction cosines.</value>
        public DirectionCosines DirectionCosines { get; internal set; }


        /// <summary>
        /// This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.
        /// </summary>
        /// <value>The angle offset.</value>
        public AngleLocalAxes AngleOffset { get; internal set; }


        /// <summary>
        /// True: Object local axes orientation was obtained using advanced local axes parameters.
        /// </summary>
        /// <value><c>true</c> if this instance is advanced; otherwise, <c>false</c>.</value>
        public bool IsAdvancedAxes { get; internal set; }


        /// <summary>
        /// True: The object is selected in the application.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected { get; internal set; }

        /// <summary>
        /// The name of each element created from the object.
        /// </summary>
        /// <value>The element names.</value>
        public List<string> ElementNames { get; internal set; }

        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        public static T Factory<T>(string uniqueName, Dictionary<string, T> items) where T : UniqueName, new()
        {
            if (items.Keys.Contains(uniqueName)) return items[uniqueName];

            T item = new T { Name = uniqueName };

            items.Add(uniqueName, item);
            return item;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObject" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected StructureObject(string name) : base(name)
        {
            _identifier.Name = name;
        }
        #endregion

        #region Selection
        /// <summary>
        /// Selects the object.
        /// </summary>
        public void Select()
        {
            IsSelected = true;
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        public void Deselect()
        {
            IsSelected = false;
        }
        #endregion

        #region Groups
        /// <summary>
        /// Adds object to the group.
        /// </summary>
        /// <param name="group">The group.</param>
        public void AddToGroup(Group group)
        {
            HelperFunctions.AddUniqueGroup(group, _groups);
        }

        /// <summary>
        /// Removes object from the group.
        /// </summary>
        /// <param name="group">The group.</param>
        public void RemoveFromGroup(Group group)
        {
            _groups.Remove(group);
        }
        #endregion
        
        #region Helper Functions

        /// <summary>
        /// Deletes the specified load from the provided list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loadPattern">The load pattern.</param>
        /// <param name="loads">The loads.</param>
        protected void deleteLoad<T>(string loadPattern, List<T> loads) where T : Load
        {
            int deleteIndex = loads.FindIndex(f => f.LoadPattern == loadPattern);
            loads.RemoveAt(deleteIndex);
        }
        #endregion
    }
}
