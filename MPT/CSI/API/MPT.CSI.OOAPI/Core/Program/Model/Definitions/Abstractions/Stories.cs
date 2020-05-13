// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Stories.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiStory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Story;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Stories.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{Story}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Stories : ObjectLists<Story>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The stories.</value>
        private ApiStory _apiStory => getApiStory(_apiApp);

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public StoryResults Results { get; protected set; }

        /// <summary>
        /// Gets or sets the base elevation.
        /// </summary>
        /// <value>The base elevation.</value>
        public double BaseElevation { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombinations" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal Stories(ApiCSiApplication app) : base(app)
        {
            Results = new StoryResults(_apiApp);
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Story fillNewItem(string uniqueName)
        {
            return Story.Factory(_apiApp, Results, uniqueName);
        }

        /// <summary>
        /// Retrieves the story information for the current tower.
        /// </summary>
        public override List<Story> FillAllItems()
        {
            _items = new List<Story>();
            List<Story> items = new List<Story>();

            _apiStory.GetStories(
                out var baseElevation,
                out var storyNames,
                out var storyElevations,
                out var storyHeights,
                out var isMasterStory,
                out var similarToStory,
                out var spliceAbove,
                out var spliceHeights,
                out var color);

            BaseElevation = baseElevation;

            for (int i = 0; i < storyNames.Length; i++)
            {
                if (Contains(storyNames[i])) continue;

                Story story = Story.Factory(_apiApp, Results,
                    storyNames[i],
                    storyElevations[i],
                    storyHeights[i],
                    isMasterStory[i],
                    similarToStory[i],
                    spliceAbove[i],
                    spliceHeights[i],
                    color[i]
                );
                _items.Add(story);
                items.Add(story);
            }

            return items;
        }
        #endregion

        #region Query                        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return Story.GetNameList(_apiStory);
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Sets the stories for the current tower.
        /// </summary>
        /// <param name="baseElevation">The elevation of the base. [L]</param>
        /// <param name="stories">The stories.</param>
        public void SetStories(double baseElevation, List<Story> stories)
        {
            _apiStory.SetStories(baseElevation,
                _items.Select(p => p.Name).ToArray(),
                _items.Select(p => p.Height).ToArray(),
                _items.Select(p => p.IsMasterStory).ToArray(),
                _items.Select(p => p.SimilarToStory).ToArray(),
                _items.Select(p => p.SpliceAbove).ToArray(),
                _items.Select(p => p.SpliceHeight).ToArray(),
                _items.Select(p => p.Color).ToArray());

            BaseElevation = baseElevation;
            _items = new List<Story>(stories);
        }
        #endregion
    }
}
#endif