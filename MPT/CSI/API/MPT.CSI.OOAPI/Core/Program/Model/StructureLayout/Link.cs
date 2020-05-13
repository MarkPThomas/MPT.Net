// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Link.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Links;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLinkObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.LinkObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Link.
    /// </summary>
    /// <seealso cref="StructureObject" />
    public class Link : StructureObject
    { // TODO: Finish Link
        #region Fields & Properties
        /// <summary>
        /// Gets the link object.
        /// </summary>
        /// <value>The link object.</value>
        protected ApiLinkObject _linkObject => getApiLinkObject(_apiApp);

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public LinkResults Results { get; protected set; }

        /// <summary>
        /// The properties associated with the link.
        /// </summary>
        /// <value>The section.</value>
        public LinkProperties Section { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Link" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Link(ApiCSiApplication app, string name) : base(app, name) { }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {  
            Results = new LinkResults(_apiApp, Name);
        }

        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        internal static Link Factory(ApiCSiApplication app, string uniqueName)
        {
            Link item = new Link(app, uniqueName);
            item.FillData();
            return item;
        }
        #endregion

        #region Fill/Set



        #endregion

        #region CRUD



        #endregion

        #region Static
        /// <summary>
        /// Returns the names of all defined objects.
        /// </summary>
        /// <param name="apiLink">The API link.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static List<string> GetNameList(ApiLinkObject apiLink)
        {
            return getNameList(apiLink);
        }
        #endregion

        #region Protected



        #endregion

        #region API Functions



        #endregion


        #region Loading

        #endregion

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<string> GetNameListOnStory()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillNameFromLabel()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillLabelFromName()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the GUID for the specified point object.
        /// </summary>
        public override void FillGUID()
        {
            getGUID(_linkObject);
        }

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public override void SetGUID(string guid)
        {
            setGUID(_linkObject, guid);
        }

        /// <summary>
        /// Retrieves the name of the point element (analysis model) associated with a specified point object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillElement()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillTransformationMatrix()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="isGlobal">if set to <c>true</c> [is global].</param>
        /// <returns>DirectionCosines.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override DirectionCosines GetTransformationMatrix(bool isGlobal)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the local axis angle assignment for the point element.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillLocalAxes()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ChangeName(string newName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        internal override void Delete()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillSelected()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Select()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Deselect()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillSpringAssignment()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        /// <param name="springName">Name of the spring.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetSpringAssignment(string springName)
        {
            throw new System.NotImplementedException();
        }
    }
}
