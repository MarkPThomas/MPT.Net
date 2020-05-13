// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="CSiOOAPiName.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Support
{
    // IListable - static    
    /// <summary>
    /// Represents an object that is identified by a unique name.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IUniqueName" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IFill" />
    public abstract class CSiOOAPiName : CSiOoApiBaseBase, 
        IFill,
        IUniqueName
    {
        /// <summary>
        /// The name
        /// </summary>
        protected string _name;
        /// <summary>
        /// The unique name.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique.</value>
        public virtual string Name
        {
            get => _name;
            internal set => _name = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiOOAPiName" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected CSiOOAPiName(ApiCSiApplication app, string name) : base(app)
        {
            _name = name;
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public abstract void FillData();

        #region Methods: Interface
        // IChangeableName
        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void ChangeName(string newName);

        /// <summary>
        /// Changes the name.
        /// </summary>
        /// <param name="newName">The new name.</param>
        protected void changeName(string newName)
        {
            Name = newName;
        }

        // IDeletable
        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal abstract void Delete();
        #endregion

        #region API Functions
        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected static List<string> getNameList(IListableNames app)
        {
            return app == null ? new List<string>() : new List<string>(app.GetNameList());
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void changeName(IChangeableName app, string newName)
        {
            app.ChangeName(Name, newName);
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void delete(IDeletable app)
        {
            app.Delete(Name);
        }
        #endregion
    }
}
