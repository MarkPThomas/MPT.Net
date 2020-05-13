// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Diaphragm.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiDiaphragm = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Diaphragm;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;
using ApiAreaObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.AreaObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Diaphragm.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Diaphragm : CSiOoApiBaseBase,
        IUniqueName
    {

        // TODO: Include all relevant objects
        // TODO: Implement eDiaphragmOption
        // TODO: Consider constraints in general
        #region Fields & Properties

        /// <summary>
        /// The API application diaphragm object.
        /// </summary>
        /// <value>The diaphragm.</value>
        protected ApiDiaphragm _apiDiaphragm => getApiDiaphragm(_apiApp);

        /// <summary>
        /// The API application point object.
        /// </summary>
        /// <value>The point object.</value>
        protected ApiPointObject _apiPointObject => getApiPointObject(_apiApp);

        /// <summary>
        /// The API application area object.
        /// </summary>
        /// <value>The area object.</value>
        protected ApiAreaObject _apiAreaObject => getApiAreaObject(_apiApp);

        /// <summary>
        /// The nodes associated with the diaphragm.
        /// </summary>
        /// <value>The nodes.</value>
        public List<Point> Nodes { get; protected set; }

        /// <summary>
        /// The areas associated with the diaphragm.
        /// </summary>
        /// <value>The areas.</value>
        public List<Area> Areas { get; protected set; }

        /// <summary>
        /// The name of an existing diaphragm.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// True: The diaphragm is semi-rigid.
        /// False: The diaphragm is rigid.
        /// </summary>
        /// <value><c>true</c> if this instance is semi rigid; otherwise, <c>false</c>.</value>
        public bool IsSemiRigid { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Diaphragm.</returns>
        internal static Diaphragm Factory(ApiCSiApplication app, string uniqueName)
        {
            Diaphragm diaphragm = new Diaphragm(app, uniqueName);
            diaphragm.FillData();
           
            return diaphragm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Diaphragm" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Diaphragm(ApiCSiApplication app, string name) : base(app)
        {
            Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillDiaphragmRigidity();
            if (_apiPointObject != null)
            {
                // TODO: Add all point objects to diaphragm?
            }
            if (_apiPointObject != null)
            {
                // TODO: Add all point objects to diaphragm?
            }
        }
        #endregion

        #region Get/Add/Remove
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns>Diaphragm.</returns>
        public Diaphragm GetDiaphragm(Area area)
        {
            if (_apiAreaObject == null) return null;
            string diaphragmName = _apiAreaObject.GetDiaphragm(area.Name);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(_apiApp, diaphragmName);
        }

        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>Diaphragm.</returns>
        public Diaphragm GetDiaphragm(Point node)
        {
            if (_apiPointObject == null) return null;
            _apiPointObject.GetDiaphragm(node.Name, out var diaphragmOption, out var diaphragmName);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(_apiApp, diaphragmName);
        }


        /// <summary>
        /// Adds to diaphragm.
        /// </summary>
        /// <param name="area">The area.</param>
        public void AddToDiaphragm(Area area)
        {
            if (Areas.Contains(area)) return;
            _apiAreaObject?.SetDiaphragm(area.Name, Name);
            Areas.Add(area);
        }

        /// <summary>
        /// Removes from diaphragm.
        /// </summary>
        /// <param name="area">The area.</param>
        public void RemoveFromDiaphragm(Area area)
        {
            if (!Areas.Contains(area)) return;
            _apiAreaObject?.SetDiaphragm(area.Name);
            Areas.Remove(area);
        }


        /// <summary>
        /// Adds to diaphragm.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddToDiaphragm(Point node)
        {
            if (Nodes.Contains(node)) return;
            _apiPointObject?.SetDiaphragm(node.Name, eDiaphragmOption.DefinedDiaphragm, Name);
            Nodes.Add(node);
        }

        /// <summary>
        /// Adds to diaphragm of bounding area.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddToDiaphragmOfBoundingArea(Point node)
        {
            if (Nodes.Contains(node)) return;
            _apiPointObject?.SetDiaphragm(node.Name, eDiaphragmOption.FromShellObject, Name);
            // TODO: Finish - determine how to implement eDiaphragmOption 'inheriting from area object'
            //Points.Add(node);
        }

        /// <summary>
        /// Removes from diaphragm.
        /// </summary>
        /// <param name="node">The node.</param>
        public void RemoveFromDiaphragm(Point node)
        {
            if (!Nodes.Contains(node)) return;
            _apiPointObject?.SetDiaphragm(node.Name, eDiaphragmOption.Disconnect, Name);
            Nodes.Remove(node);
        }
        #endregion


        #region Query
        /// <summary>
        /// Returns the names of all objects.
        /// </summary>
        /// <param name="diaphragms">The diaphragms.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiDiaphragm diaphragms)
        {
            return (diaphragms == null) ? new List<string>() : new List<string>(diaphragms.GetNameList());
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the specified diaphragm rigidity.
        /// </summary>
        public void FillDiaphragmRigidity()
        {
            IsSemiRigid = _apiDiaphragm.GetDiaphragm(Name);
        }

        /// <summary>
        /// Modifies an existing diaphragm to be rigid.
        /// </summary>
        public void SetAsRigid()
        {
            _apiDiaphragm.SetDiaphragm(Name, semiRigid: false);
        }

        /// <summary>
        /// Modifies an existing diaphragm to be semi-rigid.
        /// </summary>
        public void SetAsSemiRigid()
        {
            _apiDiaphragm.SetDiaphragm(Name, semiRigid: true);
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Adds a new diaphragm.
        /// Returns the new obect if diaphragm is successfully added.
        /// Returns null if the diaphragm already exists or fails to be added.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing diaphragm.</param>
        /// <param name="semiRigid">True: Diaphragm is semi-rigid.</param>
        /// <returns>Diaphragm.</returns>
        internal static Diaphragm AddDiaphragm(ApiCSiApplication app,
            string name,
            bool semiRigid)
        {
            ApiDiaphragm apiDiaphragm = getApiDiaphragm(app);
            List<string> existingItems = GetNameList(apiDiaphragm);
            if (existingItems.Contains(name)) return null;

            apiDiaphragm.SetDiaphragm(name, semiRigid);
            
            return Factory(app, name);
        }

        /// <summary>
        /// Deletes the specified diaphragm property.
        /// </summary>
        internal void Delete()
        {
            _apiDiaphragm.Delete(Name);
        }

        /// <summary>
        /// Changes the name of a defined diaphragm property.
        /// </summary>
        /// <param name="nameNew">New name for the diaphragm property.</param>
        public void ChangeName(string nameNew)
        {
            _apiDiaphragm.ChangeName(Name, nameNew);
            Name = nameNew;
        }
        #endregion
    }
}
#endif