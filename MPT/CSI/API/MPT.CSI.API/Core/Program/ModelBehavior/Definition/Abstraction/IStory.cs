// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IStory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Object has a gettable/settable story.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IStory : IListableNames, IGUID
    {

        /// <summary>
        /// Retrieves the elevation of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <returns>System.Double.</returns>
        double GetElevation(string name);

        /// <summary>
        /// Sets the elevation of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="elevation">The elevation of the story.</param>
        void SetElevation(string name,
            double elevation);

        /// <summary>
        /// Retrieves the height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <returns>System.Double.</returns>
        double GetHeight(string name);

        /// <summary>
        /// Sets the height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="height">The height of the story.</param>
        void SetHeight(string name,
            double height);

        /// <summary>
        /// Retrieves whether a defined story is a master story .
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetMasterStory(string name);

        /// <summary>
        /// Sets whether a defined story is a master story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        void SetMasterStory(string name,
            bool isMasterStory);


        /// <summary>
        /// Retrieves whether a story is a master story, and if not, which master story it is similar to.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="isMasterStory">True: Story is a master story.</param>
        /// <param name="similarToStory">If <paramref name="isMasterStory" /> = False then this is the master story that the requested story is similar to.</param>
        void GetSimilarTo(string name,
            out bool isMasterStory,
            out string similarToStory);

        /// <summary>
        /// Sets the master story that a defined story should be similar to.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="similarToStory">The name of a defined master story that the requested story should be similar to.</param>
        void SetSimilarTo(string name,
            string similarToStory);

        /// <summary>
        /// Retrieves the story splice height, if applicable.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height.</param>
        void GetSplice(string name,
            out bool spliceAbove,
            out double spliceHeight);

        /// <summary>
        /// Sets the splice height of a defined story.
        /// </summary>
        /// <param name="name">The name of a defined story.</param>
        /// <param name="spliceAbove">True: Story has a splice height.</param>
        /// <param name="spliceHeight">The story splice height.</param>
        void SetSplice(string name,
            bool spliceAbove,
            double spliceHeight);

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
        void GetStories(out string[] storyNames,
            out double[] storyElevations,
            out double[] storyHeights,
            out bool[] isMasterStory,
            out string[] similarToStory,
            out bool[] spliceAbove,
            out double[] spliceHeights);

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
        void SetStories(string[] storyNames,
            double[] storyElevations,
            double[] storyHeights,
            bool[] isMasterStory,
            string[] similarToStory,
            bool[] spliceAbove,
            double[] spliceHeights);
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
        void GetStories(out double baseElevation,
            out string[] storyNames,
            out double[] storyElevations,
            out double[] storyHeights,
            out bool[] isMasterStory,
            out string[] similarToStory,
            out bool[] spliceAbove,
            out double[] spliceHeights,
            out int[] color);

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
        void SetStories(double baseElevation, 
            string[] storyNames,
            double[] storyHeights,
            bool[] isMasterStory,
            string[] similarToStory,
            bool[] spliceAbove,
            double[] spliceHeights,
            int[] color);
    }
#endif
}
#endif