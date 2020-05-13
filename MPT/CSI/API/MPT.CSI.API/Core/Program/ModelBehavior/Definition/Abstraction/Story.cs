// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Story.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a story object in the application..
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.IStory" />
    public class Story : CSiApiBase, IStory
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Story"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Story(CSiApiSeed seed) : base(seed)
        {

        }
        #endregion

        #region Methods: Interface

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.Story.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <summary>
        /// Returns the GUID (Global Unique ID) for the specified object. 
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        public string GetGUID(string name)
        {
            string GUID = string.Empty;
            _callCode = _sapModel.Story.GetGUID(name, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return GUID;
        }

        /// <summary>
        /// Sets the GUID for the specified object. 
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="GUID">The GUID (Global Unique ID) for the specified object.</param>
        public void SetGUID(string name,
            string GUID = "")
        {
            _callCode = _sapModel.Story.SetGUID(name, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the elevation of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        public double GetElevation(string name)
        {
            double elevation = -0;
            _callCode = _sapModel.Story.GetElevation(name, ref elevation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return elevation;
        }

        /// <summary>
        /// Sets the elevation of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="elevation">The elevation of the story.</param>
        public void SetElevation(string name,
            double elevation)
        {
            _callCode = _sapModel.Story.SetElevation(name, elevation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        public double GetHeight(string name)
        {
            double height = -1;
            _callCode = _sapModel.Story.GetHeight(name, ref height);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return height;
        }

        /// <summary>
        /// Sets the height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="height">The height of the story.</param>
        public void SetHeight(string name,
            double height)
        {
            _callCode = _sapModel.Story.SetHeight(name, height);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves whether a defined story is a master story .
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        public bool GetMasterStory(string name)
        {
            bool isMasterStory = false;
            _callCode = _sapModel.Story.GetMasterStory(name, ref isMasterStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return isMasterStory;
        }

        /// <summary>
        /// Sets whether a defined story is a master story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        public void SetMasterStory(string name,
            bool isMasterStory)
        {
            _callCode = _sapModel.Story.SetMasterStory(name, isMasterStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Retrieves whether a story is a master story, and if not, which master story it is similar to.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If <paramref name="isMasterStory"/> = False then this is the master story that the requested story is similar to.</param>
        public void GetSimilarTo(string name,
            out bool isMasterStory,
            out string similarToStory)
        {
            isMasterStory = false;
            similarToStory = string.Empty;

            _callCode = _sapModel.Story.GetSimilarTo(name, ref isMasterStory, ref similarToStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the master story that a defined story should be similar to.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="similarToStory">The name of a defined master story that the requested story should be similar to.</param>
        public void SetSimilarTo(string name,
            string similarToStory)
        {
            _callCode = _sapModel.Story.SetSimilarTo(name, similarToStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the story splice height, if applicable.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height.</param>
        public void GetSplice(string name,
            out bool spliceAbove,
            out double spliceHeight)
        {
            spliceAbove = false;
            spliceHeight = -1;

            _callCode = _sapModel.Story.GetSplice(name, ref spliceAbove, ref spliceHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the splice height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height.</param>
        public void SetSplice(string name,
            bool spliceAbove,
            double spliceHeight)
        {
            _callCode = _sapModel.Story.SetSplice(name, spliceAbove, spliceHeight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016
        /// <summary>
        /// Retrieves the story information for the current tower.
        /// </summary>
        /// <param name="storyNames">The names of the stories. 
        /// This array will include "Base" .</param>
        /// <param name="storyElevations">The story elevations. 
        /// The first value is the "Base" elevation. </param>
        /// <param name="storyHeights">The story heights. 
        /// The first value is the "Base" height.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If the story is not a master story, which master story the story is similar to .</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeights">The story splice heights.</param>
        public void GetStories(out string[] storyNames,
            out double[] storyElevations,
            out double[] storyHeights,
            out bool[] isMasterStory,
            out string[] similarToStory,
            out bool[] spliceAbove,
            out double[] spliceHeights)
        {
            storyNames = new string[0];
            storyElevations = new double[0];
            storyHeights = new double[0];
            isMasterStory = new bool[0];
            similarToStory = new string[0];
            spliceAbove = new bool[0];
            spliceHeights = new double[0];

            _callCode = _sapModel.Story.GetStories(ref _numberOfItems,
                            ref storyNames,
                            ref storyElevations,
                            ref storyHeights,
                            ref isMasterStory,
                            ref similarToStory,
                            ref spliceAbove,
                            ref spliceHeights);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the stories for the current tower.
        /// </summary>
        /// <param name="storyNames">The names of the stories. 
        /// This array will include "Base" .</param>
        /// <param name="storyElevations">The story elevations. 
        /// The first value is the "Base" elevation. </param>
        /// <param name="storyHeights">The story heights. 
        /// The first value is the "Base" height.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If the story is not a master story, which master story the story is similar to .</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeights">The story splice heights.</param>
        public void SetStories(string[] storyNames,
            double[] storyElevations,
            double[] storyHeights,
            bool[] isMasterStory,
            string[] similarToStory,
            bool[] spliceAbove,
            double[] spliceHeights)
        {
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(storyElevations), storyElevations.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(storyHeights), storyHeights.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(isMasterStory), isMasterStory.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(similarToStory), similarToStory.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(spliceAbove), spliceAbove.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(spliceHeights), spliceHeights.Length);

            _callCode = _sapModel.Story.SetStories(storyNames,
                            storyElevations,
                            storyHeights,
                            isMasterStory,
                            similarToStory,
                            spliceAbove,
                            spliceHeights);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#else

        /// <summary>
        /// Retrieves the story information for the current tower.
        /// </summary>
        /// <param name="baseElevation">The base elevation. [L]</param>
        /// <param name="storyNames">The names of the stories.
        /// This array will include the base story.</param>
        /// <param name="storyElevations">The story elevations.
        /// The first value is the base elevation. [L]</param>
        /// <param name="storyHeights">The story heights.
        /// The first value is the base height. [L]</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If the story is not a master story, which master story the story is similar to .</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeights">The story splice heights. [L]</param>
        /// <param name="color">The display color for the story specified as an Integer.</param>
        public void GetStories(out double baseElevation,
            out string[] storyNames,
            out double[] storyElevations,
            out double[] storyHeights,
            out bool[] isMasterStory,
            out string[] similarToStory,
            out bool[] spliceAbove,
            out double[] spliceHeights,
            out int[] color)
        {
            baseElevation = -1;
            storyNames = new string[0];
            storyElevations = new double[0];
            storyHeights = new double[0];
            isMasterStory = new bool[0];
            similarToStory = new string[0];
            spliceAbove = new bool[0];
            spliceHeights = new double[0];
            color = new int[0];

            _callCode = _sapModel.Story.GetStories_2(ref baseElevation,
                            ref _numberOfItems,
                            ref storyNames,
                            ref storyElevations,
                            ref storyHeights,
                            ref isMasterStory,
                            ref similarToStory,
                            ref spliceAbove,
                            ref spliceHeights,
                            ref color);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the stories for the current tower.
        /// </summary>
        /// <param name="baseElevation">The base elevation. [L]</param>
        /// <param name="storyNames">The names of the stories.
        /// This array will include base story.</param>
        /// <param name="storyHeights">The story heights.
        /// The first value is the base height. [L]</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If the story is not a master story, which master story the story is similar to .</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeights">The story splice heights. [L]</param>
        /// <param name="color">The display color for the story specified as an Integer.</param>
        public void SetStories(double baseElevation,
            string[] storyNames,
            double[] storyHeights,
            bool[] isMasterStory,
            string[] similarToStory,
            bool[] spliceAbove,
            double[] spliceHeights,
            int[] color)
        {
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(storyHeights), storyHeights.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(isMasterStory), isMasterStory.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(similarToStory), similarToStory.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(spliceAbove), spliceAbove.Length);
            arraysLengthMatch(nameof(storyNames), storyNames.Length, nameof(spliceHeights), spliceHeights.Length);

            _callCode = _sapModel.Story.SetStories_2(baseElevation,
                            storyNames.Length,
                            ref storyNames,
                            ref storyHeights,
                            ref isMasterStory,
                            ref similarToStory,
                            ref spliceAbove,
                            ref spliceHeights,
                            ref color);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
#endif