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
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Links;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Link.
    /// </summary>
    /// <seealso cref="StructureObject" />
    public class Link : StructureObject
    {
        /// <summary>
        /// Gets the link object.
        /// </summary>
        /// <value>The link object.</value>
        protected static LinkObject _linkObject => Registry.ObjectModeler?.LinkObject;

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


        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        public static Link Factory(string uniqueName)
        {
            return Factory(uniqueName, _linkObject, Registry.Links);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Link(string name) : base(name) { }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            Results = new LinkResults(Name);
        }


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
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillGUID()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sets the GUID for the specified point object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the point object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetGUID()
        {
            throw new System.NotImplementedException();
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
        public override void Delete()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void GetSelected()
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetSpringAssignment()
        {
            throw new System.NotImplementedException();
        }
    }
}
