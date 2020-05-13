// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Spandrel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiSpandrel = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.SpandrelLabel;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Spandrel.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions.PierSpandrelBase" />
    /// <seealso cref="PierSpandrelBase" />
    public class Spandrel : PierSpandrelBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The spandrels.</value>
        private ApiSpandrel _spandrelLabel => getApiSpandrel(_apiApp);

        /// <summary>
        /// The results
        /// </summary>
        private readonly SpandrelResults _results;

        /// <summary>
        /// The story drifts
        /// </summary>
        private List<Tuple<PierSpandrelResultsIdentifier, Forces>> _forces;
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces
        {
            get
            {
                if (_forces == null)
                {
                    FillSpandrelForces();
                }

                return _forces;
            }
        }

        /// <summary>
        /// True: Spandrel Label spans multiple story levels.
        /// </summary>
        /// <value><c>true</c> if this instance is multi story; otherwise, <c>false</c>.</value>
        public bool IsMultiStory { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Spandrel Factory(ApiCSiApplication app,
            SpandrelResults spandrelResults, 
            string uniqueName)
        {
            Spandrel item = new Spandrel(app, spandrelResults, uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spandrel" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="name">The name.</param>
        protected Spandrel(ApiCSiApplication app,
            SpandrelResults spandrelResults,
            string name) : base(app, name)
        {
            _results = spandrelResults;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillSpandrel();
        }
        #endregion

        #region Query
        /// <summary>
        /// Retrieves the names of all defined spandrel label property.
        /// Also provides associated multi-story data.
        /// </summary>
        /// <param name="spandrel">The spandrel.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        internal static List<string> GetNameList(ApiSpandrel spandrel, out List<bool> isMultiStory)
        {
            if (spandrel == null)
            {
                isMultiStory = new List<bool>();
                return new List<string>();
            }
            spandrel.GetNameList(out var names, out var isMultiStoryArray);
            isMultiStory = new List<bool>(isMultiStoryArray);
            return new List<string>(names);
        }

        /// <summary>
        /// Retrieves the names of all defined label propertie.
        /// </summary>
        /// <param name="spandrel">The spandrel.</param>
        /// <returns>System.String[].</returns>
        internal static List<string> GetNameList(ApiSpandrel spandrel)
        {
            if (spandrel == null) return new List<string>();
            spandrel.GetNameList(out var names, out var isMultiStory);
            return new List<string>(names);
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// True: The Spandrel Label exists.
        /// </summary>
        public void FillSpandrel()
        {
            Exists = _spandrelLabel.GetSpandrel(Name, out var isMultiStory);
            IsMultiStory = isMultiStory;
        }


        /// <summary>
        /// Fills the spandrel force.
        /// </summary>
        public void FillSpandrelForces()
        {
            _forces = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();
            foreach (var spandrelResult in _results.Forces)
            {
                if (spandrelResult.Item1.PierSpandrelName == Name)
                {
                    _forces.Add(spandrelResult);
                }
            }
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Adds a new Spandrel Label.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="spandrelResults">The spandrel results.</param>
        /// <param name="name">The name.</param>
        /// <param name="isMultiStory">if set to <c>true</c> [is multi story].</param>
        /// <returns>Spandrel.</returns>
        internal static Spandrel AddSpandrel(ApiCSiApplication app, 
            SpandrelResults spandrelResults, 
            string name, 
            bool isMultiStory)
        {
            ApiSpandrel spandrel = getApiSpandrel(app);
            List<string> existingItems = GetNameList(spandrel);
            if (existingItems.Contains(name)) return null;

            spandrel.SetSpandrel(name, isMultiStory);
            return Factory(app, spandrelResults, name);
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <inheritdoc />
        internal override void Delete()
        {
            _spandrelLabel.Delete(Name);
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <inheritdoc />
        public override void ChangeName(string newName)
        {
            _spandrelLabel.ChangeName(Name, newName);
            changeName(newName);
        }

        #endregion
    }
}
#endif