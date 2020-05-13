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
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;

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
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        protected UniqueLabelNamePair _identifier { get; set; }

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
        /// If blank, program assigns one automatically.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; protected set; }


        /// <summary>
        /// Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.
        /// </summary>
        /// <value>The direction cosines.</value>
        public DirectionCosines DirectionCosines { get; protected set; }

        /// <summary>
        /// True: Transformation matrix is between the Global coordinate system and the object local coordinate system.
        /// False: Transformation matrix is between the present coordinate system and the object local coordinate system.
        /// </summary>
        /// <value><c>true</c> if this instance is global; otherwise, <c>false</c>.</value>
        public bool IsGlobal { get; protected set; }
        // TODO: Determine how IsGlobal is set/determined

        /// <summary>
        /// This is the angle 'a' that the local 1 and 2 axes are rotated about the positive local 3 axis from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +3 axis is pointing toward you. [deg]
        /// For some objects, the following rotations are also performed:
        /// 2. Rotate about the resulting 2 axis by angle b.
        /// 3. Rotate about the resulting 1 axis by angle c.
        /// </summary>
        /// <value>The angle offset.</value>
        public AngleLocalAxes AngleOffset { get; protected set; }

        /// <summary>
        /// True: Object local axes orientation was obtained using advanced local axes parameters.
        /// </summary>
        /// <value><c>true</c> if this instance is advanced; otherwise, <c>false</c>.</value>
        public bool IsAdvancedAxes { get; protected set; }


        /// <summary>
        /// The name of an existing point spring property.
        /// </summary>
        /// <value>The spring.</value>
        public string Spring { get; protected set; }



        /// <summary>
        /// True: The node is selected in the application.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected { get; protected set; }

        /// <summary>
        /// The name of each element created from the object.
        /// </summary>
        /// <value>The element names.</value>
        public List<string> ElementNames { get; protected set; }



        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="apiObject">The API object.</param>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        public static T Factory<T>(string uniqueName, CSiApiBase apiObject, Dictionary<string, T> items) where T:CSiOOAPiName, new()
        {
            if (items.Keys.Contains(uniqueName)) return items[uniqueName];

            T item = new T {Name = uniqueName};

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
        /// <param name="name">The name.</param>
        protected StructureObject(string name) : base(name)
        {
            _identifier.Name = name;
        }

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
        /// Retrieves the GUID for the specified point object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillGUID();

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetGUID();


        // IObservableElement
        /// <summary>
        /// Retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
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

        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the point element.
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
        public abstract void GetSelected();

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public virtual void Select()
        {
            IsSelected = true;
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public virtual void Deselect()
        {
            IsSelected = false;
        }

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
        public abstract void SetSpringAssignment();

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public virtual void DeleteSpring()
        {
            Spring = string.Empty;
        }
#endif
        #endregion

        #region API Functions
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
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
            IsSelected = app.GetSelected(Name);
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected(ISelectable app)
        {
            app?.SetSelected(Name, IsSelected);
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
        /// Retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getElement(IObservableElement app)
        {
            if (app == null) return;
            ElementNames = new List<string>(app.GetElement(Name));
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getTransformationMatrix(IObservableTransformationMatrixObject app)
        {
            if (app == null) return;
            DirectionCosines = app.GetTransformationMatrix(Name, IsGlobal);
        }

        /// <summary>
        /// Retrieves the GUID for the specified point object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getGUID(IGUID app)
        {
            GUID = app?.GetGUID(Name);
        }

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setGUID(IGUID app)
        {
            app?.SetGUID(Name, GUID);
        }


        /// <summary>
        /// Retrieves the local axis angle assignment for the point element.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getLocalAxes(IObservableLocalAxes app)
        {
            app.GetLocalAxes(Name, out var angleOffset, out var isAdvanced);
            AngleOffset = angleOffset;
            IsAdvancedAxes = isAdvanced;
        }

        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getSpringAssignment(ISpringAssignment app)
        {
           Spring = app.GetSpringAssignment(Name);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void setSpringAssignment(ISpringAssignment app)
        {
            app.SetSpringAssignment(Name, Spring);
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
