// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Diaphragms.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System.Collections.Generic;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiDiaphragm = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Diaphragm;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Diaphragms.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Diaphragm}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Diaphragms : ObjectLists<Diaphragm>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The diaphragms.</value>
        private ApiDiaphragm _apiDiaphragm => getApiDiaphragm(_apiApp);
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Diaphragms"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal Diaphragms(ApiCSiApplication app) : base(app)
        {
        }
        #endregion

        #region Fill                
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Diaphragm fillNewItem(string uniqueName)
        {
            return Diaphragm.Factory(_apiApp, uniqueName);
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return Diaphragm.GetNameList(_apiDiaphragm);
        }
        #endregion
    }
}
#endif