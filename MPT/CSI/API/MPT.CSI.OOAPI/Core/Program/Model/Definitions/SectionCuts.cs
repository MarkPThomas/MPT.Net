// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SectionCuts.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiSectionCuts = MPT.CSI.API.Core.Program.ModelBehavior.Definition.SectionCuts;
#endif

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Class SectionCuts.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{SectionCut}" />
    public class SectionCuts : ObjectLists<SectionCut>
    {
        #region Fields & Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The groups.</value>
        private ApiSectionCuts _apiSectionCuts => _apiApp?.Model?.Definitions?.SectionCuts;
#endif
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public SectionCutResults Results { get; protected set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCuts"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        public SectionCuts(ApiCSiApplication app) : base(app)
        {
            Results = new SectionCutResults(_apiApp, string.Empty);
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override SectionCut fillNewItem(string uniqueName)
        {
            return SectionCut.Factory(_apiApp, Results, uniqueName);
        }
        #endregion

        #region Add/Remove   
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a group to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddByQuadrilateral(string uniqueName)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(SectionCut.AddByQuadrilateral(_apiApp, uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a group to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddByGroup(string uniqueName)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(SectionCut.AddByGroup(_apiApp, uniqueName));
            return true;
        }
#endif
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            return SectionCut.GetNameList(_apiSectionCuts);
#else
            return new List<string>();
#endif
        }
        #endregion
    }
}
