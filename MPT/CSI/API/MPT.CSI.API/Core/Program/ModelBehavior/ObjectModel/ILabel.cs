// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ILabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can return label information. 
    /// </summary>
    public interface ILabel
    {
        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        UniqueLabel GetLabelFromName(string name);

        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        UniqueLabelNamePair[] GetLabelNameList();

        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        /// <param name="label">The object label and story.</param>
        string GetNameFromLabel(UniqueLabel label);

        /// <summary>
        /// Retrieves the names of all defined object properties for a given story.
        /// </summary>
        /// <param name="storyName">Name of the story to filter the object names by.</param>
        string[] GetNameListOnStory(string storyName);
    }
}
#endif