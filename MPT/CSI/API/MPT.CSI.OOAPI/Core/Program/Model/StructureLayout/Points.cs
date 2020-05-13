// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Points.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Points.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Point}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Points : ObjectLists<Point>
    {
        #region Fields & Properties 
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The point object.</value>
        private ApiPointObject _apiPointObject => getApiPointObject(_apiApp);
        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="Points" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal Points(ApiCSiApplication app) : base(app)
        {
        }
        #endregion

        #region Fill         
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Point fillNewItem(string uniqueName)
        {
            return Point.Factory(_apiApp, uniqueName);
        }
        #endregion

        #region Add/Remove        
        /// <summary>
        /// Adds a point object to the model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="uniqueName">Unique name of the point.
        /// If not supplied, the program will generate one automatically.</param>
        /// <param name="mergeOff">False: A new point object that is added at the same location as an existing point object will be merged with the existing point object (assuming the two point objects have the same MergeNumber) and thus only one point object will exist at the location.
        /// True: Points will not merge and two point objects will exist at the same location.</param>
        /// <param name="mergeNumber">Two points objects in the same location will merge only if their merge number assignments are the same.
        /// By default all point objects have a merge number of zero.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(
            Coordinate3DCartesian coordinate,
            string uniqueName = "",
            bool mergeOff = false,
            int mergeNumber = 0)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(Point.AddByCoordinate(_apiApp, coordinate, uniqueName, mergeOff, mergeNumber));
            return true;
        }
        #endregion

        #region Query
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        public List<UniqueLabelNamePair> GetLabelNameList()
        {
            return Point.GetLabelNameList(_apiPointObject);
        }
#endif

        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return Point.GetNameList(_apiPointObject);
        }
        #endregion
    }
}
