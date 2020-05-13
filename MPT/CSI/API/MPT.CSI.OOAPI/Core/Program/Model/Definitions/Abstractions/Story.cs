// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Story.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using System.Collections.Generic;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiStory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Story;
using ApiAreaObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.AreaObject;
using ApiFrameObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.FrameObject;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;
using ApiLinkObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.LinkObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Story.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IUniqueName" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class Story : CSiOoApiBaseBase, 
        IUniqueName
    {
        #region Fields & Properties

        /// <summary>
        /// The API application diaphragm object.
        /// </summary>
        /// <value>The diaphragm.</value>
        protected ApiStory _story => getApiStory(_apiApp);

        /// <summary>
        /// The API application point object.
        /// </summary>
        /// <value>The point object.</value>
        protected ApiPointObject _pointObject => getApiPointObject(_apiApp);

        /// <summary>
        /// The API application frame object.
        /// </summary>
        /// <value>The frame object.</value>
        private ApiFrameObject _frameObject => getApiFrameObject(_apiApp);

        /// <summary>
        /// The API application area object.
        /// </summary>
        /// <value>The area object.</value>
        protected ApiAreaObject _areaObject => getApiAreaObject(_apiApp);

        /// <summary>
        /// Gets the link object.
        /// </summary>
        /// <value>The link object.</value>
        protected ApiLinkObject _linkObject => getApiLinkObject(_apiApp);


        /// <summary>
        /// The results
        /// </summary>
        private readonly StoryResults _results;

        /// <summary>
        /// The story drifts
        /// </summary>
        private List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> _storyDrifts;
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> StoryDrifts
        {
            get
            {
                if (_storyDrifts == null)
                {
                    FillJointDrifts();
                }

                return _storyDrifts;
            }
        }

        /// <summary>
        /// The story name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// The GUID (Global Unique ID)
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; protected set; }

        /// <summary>
        /// The elevation of the story. [L]
        /// </summary>
        /// <value>The elevation.</value>
        public double Elevation { get; protected set; }

        /// <summary>
        /// The height of the story. [L]
        /// </summary>
        /// <value>The height.</value>
        public double Height { get; protected set; }

        /// <summary>
        /// True: Story is a master story. Other stories will be influenced by this story.
        /// </summary>
        /// <value><c>true</c> if this instance is master story; otherwise, <c>false</c>.</value>
        public bool IsMasterStory { get; protected set; }

        /// <summary>
        /// If <see cref="IsMasterStory" /> = False, then this is the master story that this story is similar to.
        /// </summary>
        /// <value>The similar to story.</value>
        public string SimilarToStory { get; protected set; }

        /// <summary>
        /// True: The story has a splice height.
        /// </summary>
        /// <value><c>true</c> if [splice above]; otherwise, <c>false</c>.</value>
        public bool SpliceAbove { get; protected set; }

        /// <summary>
        /// The story splice height. [L]
        /// </summary>
        /// <value>The height of the splice.</value>
        public double SpliceHeight { get; protected set; }

        /// <summary>
        /// The display color for the story specified as an Integer.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; protected set; }
        #endregion

        #region Initialization 
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="results">The results.</param>
        /// <param name="storyName">Name of the unique.</param>
        /// <returns>Diaphragm.</returns>
        internal static Story Factory(ApiCSiApplication app, StoryResults results, string storyName)
        {
            Story item = new Story(app, results, storyName);
            item.FillData();

            return item;
        }

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="results">The results.</param>
        /// <param name="storyName">Name of the story.</param>
        /// <param name="storyElevation">The story elevation. [L]</param>
        /// <param name="storyHeight">The story height. [L]</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If the story is not a master story, which master story the story is similar to .</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height. [L]</param>
        /// <param name="color">The display color for the story specified as an Integer.</param>
        /// <returns>Diaphragm.</returns>
        internal static Story Factory(ApiCSiApplication app, StoryResults results, 
            string storyName,
            double storyElevation,
            double storyHeight,
            bool isMasterStory,
            string similarToStory,
            bool spliceAbove,
            double spliceHeight,
            int color)
        {
            Story item = new Story(app, results, storyName)
            {
                Elevation = storyElevation,
                Height = storyHeight,
                IsMasterStory = isMasterStory,
                SimilarToStory = similarToStory,
                SpliceAbove = spliceAbove,
                SpliceHeight = spliceHeight,
                Color = color
            };
            item.FillGUID();

            return item;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Story" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="results">The results.</param>
        /// <param name="name">The name.</param>
        protected Story(ApiCSiApplication app, StoryResults results, string name) : base(app)
        {
            _results = results;
            Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillGUID();
            FillElevation();
            FillHeight();
            FillSimilarTo();
            FillSplice();
        }
        #endregion

        #region Query
        /// <summary>
        /// Returns the names of all objects.
        /// </summary>
        /// <param name="storyApi">The story API.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static List<string> GetNameList(ApiStory storyApi)
        {
            return (storyApi == null) ? new List<string>() : new List<string>(storyApi.GetNameList());
        }


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns the names of all defined point object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetNodeNameList()
        {
            return (_pointObject == null) ? new List<string>() : new List<string>(_pointObject.GetNameListOnStory(Name));
        }

        /// <summary>
        /// Returns the names of all defined frame object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetFrameNameList()
        {
            return (_frameObject == null) ? new List<string>() : new List<string>(_frameObject.GetNameListOnStory(Name));
        }


        /// <summary>
        /// Returns the names of all defined area properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetAreaNameList()
        {
            return (_areaObject == null) ? new List<string>() : new List<string>(_areaObject.GetNameListOnStory(Name));
        }


        /// <summary>
        /// Returns the names of all defined link properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetLinkNameList()
        {
            return (_linkObject == null) ? new List<string>() : new List<string>(_linkObject.GetNameListOnStory(Name));
        }
#endif
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the GUID (Global Unique ID) for the specified object.
        /// </summary>
        public void FillGUID()
        {
            GUID = _story.GetGUID(Name);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        public void SetGUID(string GUID = "")
        {
            _story.SetGUID(Name, GUID);
            this.GUID = GUID;
        }

        /// <summary>
        /// Retrieves the elevation of a defined story.
        /// </summary>
        public void FillElevation()
        {
            Elevation = _story.GetElevation(Name);
        }

        /// <summary>
        /// Sets the elevation of a defined story.
        /// </summary>
        /// <param name="elevation">The elevation of the story.</param>
        public void SetElevation(double elevation)
        {
            _story.SetElevation(Name, elevation);
            Elevation = elevation;
        }

        /// <summary>
        /// Retrieves the height of a defined story.
        /// </summary>
        public void FillHeight()
        {
            Height = _story.GetHeight(Name);
        }

        /// <summary>
        /// Sets the height of a defined story.
        /// </summary>
        /// <param name="height">The height of the story.</param>
        public void SetHeight(double height)
        {
            _story.SetHeight(Name, height);
            Height = height;
        }

        /// <summary>
        /// Retrieves whether a defined story is a master story .
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetMasterStory()
        {
            return _story.GetMasterStory(Name);
        }

        /// <summary>
        /// Sets whether a defined story is a master story.
        /// </summary>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        public void SetMasterStory(bool isMasterStory)
        {
            _story.SetMasterStory(Name, isMasterStory);
            IsMasterStory = isMasterStory;
        }


        /// <summary>
        /// Retrieves whether a story is a master story, and if not, which master story it is similar to.
        /// </summary>
        public void FillSimilarTo()
        {
            _story.GetSimilarTo(Name, out var isMasterStory, out var similarToStory);
            IsMasterStory = isMasterStory;
            SimilarToStory = similarToStory;
        }

        /// <summary>
        /// Sets the master story that a defined story should be similar to.
        /// </summary>
        /// <param name="similarToStory">The name of a defined master story that the requested story should be similar to.</param>
        public void SetSimilarTo(string similarToStory)
        {
            _story.SetSimilarTo(Name, similarToStory);
            SimilarToStory = similarToStory;
        }

        /// <summary>
        /// Retrieves the story splice height, if applicable.
        /// </summary>
        public void FillSplice()
        {
            _story.GetSplice(Name, out var spliceAbove, out var spliceHeight);
            SpliceAbove = spliceAbove;
            SpliceHeight = spliceHeight;
        }

        /// <summary>
        /// Sets the splice height of a defined story.
        /// </summary>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height.</param>
        public void SetSplice(
            bool spliceAbove,
            double spliceHeight)
        {
            _story.SetSplice(Name, spliceAbove, spliceHeight);
            SpliceAbove = spliceAbove;
            SpliceHeight = spliceHeight;
        }
        #endregion

        #region Results
        /// <summary>
        /// Fills the joint drifts.
        /// </summary>
        public void FillJointDrifts()
        {
            _storyDrifts = new List<Tuple<LabelNameResultsIdentifier, StoryDrifts>>();
            foreach (var result in _results.StoryDrifts)
            {
                if (result.Item1.StoryName == Name)
                {
                    _storyDrifts.Add(result);
                }
            }
        }
        #endregion
    }
}
#endif