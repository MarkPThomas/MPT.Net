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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    // ICountable - not used
    // IListableNames - static
    // IGroupAssignable - on Group object

    /// <summary>
    /// Base class for any object located in the model layout.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public abstract class StructureObject : CSiOOAPiName
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

#if !BUILD_ETABS2016 && !BUILD_ETABS2017
        // LocalAxesAdvanced
#else
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
        /// The spring
        /// </summary>
        private string _spring;
        /// <summary>
        /// The name of an existing spring property.
        /// </summary>
        /// <value>The spring.</value>
        public string Spring
        {
            get
            {
                if (_spring == null)
                {
                    FillSpringAssignment();
                }

                return _spring;
            }
        }

#endif
        /// <summary>
        /// The unique identifier
        /// </summary>
        private string _guid;
        /// <summary>
        /// If blank, program assigns one automatically.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID
        {
            get
            {
                if (_guid == null)
                {
                    FillGUID();
                }

                return _guid;
            }
        }

        /// <summary>
        /// The direction cosines
        /// </summary>
        private DirectionCosines? _directionCosines;
        /// <summary>
        /// Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.
        /// </summary>
        /// <value>The direction cosines.</value>
        public DirectionCosines DirectionCosines
        {
            get
            {
                if (_directionCosines == null)
                {
                    FillTransformationMatrix();
                }

                return _directionCosines ?? new DirectionCosines();
            }
        }


        /// <summary>
        /// The angle offset
        /// </summary>
        protected AngleLocalAxes? _angleOffset;
        /// <summary>
        /// This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.
        /// </summary>
        /// <value>The angle offset.</value>
        public AngleLocalAxes AngleOffset
        {
            get
            {
                if (_angleOffset == null)
                {
                    FillLocalAxes();
                }

                return _angleOffset ?? new AngleLocalAxes();
            }
        }

        /// <summary>
        /// The is advanced axes
        /// </summary>
        private bool? _isAdvancedAxes;
        /// <summary>
        /// True: Object local axes orientation was obtained using advanced local axes parameters.
        /// </summary>
        /// <value><c>true</c> if this instance is advanced; otherwise, <c>false</c>.</value>
        public bool IsAdvancedAxes
        {
            get
            {
                if (_isAdvancedAxes == null)
                {
                    FillLocalAxes();
                }

                return _isAdvancedAxes ?? false;
            }
        }

        /// <summary>
        /// The is selected
        /// </summary>
        private bool? _isSelected;
        /// <summary>
        /// True: The object is selected in the application.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected
        {
            get
            {
                if (_isSelected == null)
                {
                    FillSelected();
                }

                return _isSelected ?? false;
            }
        }

        /// <summary>
        /// The element names
        /// </summary>
        private List<string> _elementNames;
        /// <summary>
        /// The name of each element created from the object.
        /// </summary>
        /// <value>The element names.</value>
        public List<string> ElementNames
        {
            get
            {
                if (_elementNames == null)
                {
                    FillElement();
                }

                return _elementNames;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="apiObject">The API object.</param>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        public static T Factory<T>(string uniqueName, CSiApiBase apiObject, Dictionary<string, T> items) where T : CSiOOAPiName, new()
        {
            if (items.Keys.Contains(uniqueName)) return items[uniqueName];

            T item = new T { Name = uniqueName };

            if (apiObject != null && Constants.FillAllProperties)
            {
                item.FillData();
            }

            items.Add(uniqueName, item);
            return item;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObject" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected StructureObject(ApiCSiApplication app, string name) : base(app, name)
        {
            _identifier.Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
#if BUILD_ETABS2016 || BUILD_ETABS2017
            FillLabelFromName();
#else
            FillLocalAxesAdvanced();
#endif
        }
        #endregion

        #region Query
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        // ILabel
        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public abstract List<string> GetNameListOnStory();

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public abstract void FillNameFromLabel();

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public abstract void FillLabelFromName();
#endif


        // IGUID
        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillGUID();

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetGUID(string guid = "");


        // IObservableElement
        /// <summary>
        /// Retrieves the name of the point element (analysis model) associated with a specified object in the object-based model.
        /// Null is returned if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillElement();

        // IObservableTransformationMatrixObject
        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillTransformationMatrix();

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract DirectionCosines GetTransformationMatrix(bool isGlobal);
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillLocalAxes();
        #endregion

        #region Selection
        // ISelectable
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillSelected();

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void Select();

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void Deselect();

        #endregion

        #region Support & Connections
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public abstract void FillSpringAssignment();

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="springName">Name of the spring.</param>
        public abstract void SetSpringAssignment(string springName);

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public virtual void DeleteSpring()
        {
            _spring = string.Empty;
        }
#endif
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

        #region API Functions
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        protected static List<UniqueLabelNamePair> getLabelNameList(ILabel app)
        {
            return app == null ? new List<UniqueLabelNamePair>() : new List<UniqueLabelNamePair>(app.GetLabelNameList());
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        protected List<string> getNameListOnStory(ILabel app)
        {
            return app == null ? new List<string>() : new List<string>(app.GetNameListOnStory(Name));
        }

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getNameFromLabel(ILabel app)
        {
            Name = app?.GetNameFromLabel(_identifier.GetUniqueLabel());
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getLabelFromName(ILabel app)
        {
            UniqueLabel label = app?.GetLabelFromName(Name);
            Label = label?.Label;
            Story = label?.Story;
        }
#endif

        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getSelected(ISelectable app)
        {
            if (app == null) return;
            _isSelected = app.GetSelected(Name);
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected(ISelectable app, bool isSelected)
        {
            app?.SetSelected(Name, isSelected);
            _isSelected = isSelected;
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        protected string[] getGroupsAssigned(ISelectable app)
        {
            return app?.GetGroupAssign(Name);
        }
#endif

        /// <summary>
        /// Retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// Null is returned if the analysis model does not exist.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getElement(IObservableElement app)
        {
            if (app == null) return;
            try
            {
                _elementNames = new List<string>(app.GetElement(Name));
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                _elementNames = null;
            }
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void fillTransformationMatrix(IObservableTransformationMatrixObject app)
        {
            if (app == null) return;
            _directionCosines = getTransformationMatrix(app, isGlobal: true);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <returns>DirectionCosines.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected DirectionCosines getTransformationMatrix(IObservableTransformationMatrixObject app, bool isGlobal)
        {
            return app.GetTransformationMatrix(Name, isGlobal);
        }

        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getGUID(IGUID app)
        {
            _guid = app?.GetGUID(Name);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setGUID(IGUID app, string guid)
        {
            app?.SetGUID(Name, guid);
            _guid = guid;
        }



        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getLocalAxes(IObservableLocalAxes app)
        {
            app.GetLocalAxes(Name, out var angleOffset, out var isAdvanced);
            _angleOffset = angleOffset;
            _isAdvancedAxes = isAdvanced;
        }




        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getSpringAssignment(ISpringAssignment app)
        {
           _spring = app.GetSpringAssignment(Name);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="springName">Name of the spring.</param>
        protected void setSpringAssignment(ISpringAssignment app, string springName)
        {
            try
            {
                app.SetSpringAssignment(Name, springName);
                _spring = springName;
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
            }
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
