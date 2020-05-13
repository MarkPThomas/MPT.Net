// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-25-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="GroupsDesign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Class GroupsDesign. This class cannot be inherited.
    /// </summary>
    public sealed class GroupsDesign
    {
        #region Fields & Properties

        /// <summary>
        /// The API automatic section
        /// </summary>
        private readonly IAutoSection _apiAutoSection;

        /// <summary>
        /// The groups
        /// </summary>
        private readonly Groups _groups;

        /// <summary>
        /// The group names
        /// </summary>
        private List<string> _groupNames;
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        internal List<string> GroupNames
        {
            get
            {
                if (_groups == null)
                {
                    Fill();
                }

                return _groupNames;
            }
        }

        /// <summary>
        /// The design groups
        /// </summary>
        private List<Group> _designGroups;
        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public ReadOnlyCollection<Group> Groups
        {
            get
            {
                if (_designGroups != null) return new ReadOnlyCollection<Group>(_designGroups);

                _designGroups = new List<Group>();
                foreach (var groupName in GroupNames)
                {
                    _designGroups.Add(_groups.FillItem(groupName));
                }
                return new ReadOnlyCollection<Group>(_designGroups);
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsDesign"/> class.
        /// </summary>
        /// <param name="apiAutoSection">The API automatic section.</param>
        /// <param name="groups">The groups.</param>
        public GroupsDesign(IAutoSection apiAutoSection, Groups groups)
        {
            _apiAutoSection = apiAutoSection;
            _groups = groups;
        }


        #endregion

        #region Fill/Set

        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            _groupNames = new List<string>(_apiAutoSection.GetGroup());
        }

        /// <summary>
        /// Adds the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        public void Add(Group group)
        {
            set(group, selectForDesign: true);
        }

        /// <summary>
        /// Removes the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        public void Remove(Group group)
        {
            set(group, selectForDesign: false);
        }

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// </summary>
        /// <param name="group">An existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design.
        /// False: The group is not selected for steel design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        private void set(Group group, bool selectForDesign)
        {
            _apiAutoSection.SetGroup(group.Name, selectForDesign);

            if (selectForDesign)
            {
                if (!GroupNames.Contains(group.Name)) GroupNames.Add(group.Name);
                if (!Groups.Contains(group)) _designGroups.Add(group);
            }
            else
            {
                GroupNames.Remove(group.Name);
                _designGroups.Remove(group);
            }
        }

        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoSelectNull(Frame frame)
        {
            // TODO: Determine how or whether to tag frame for removal from autoSelect group.
            _apiAutoSection.SetAutoSelectNull(frame.Name);
        }
        #endregion
    }
}
