// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-21-2017
// ***********************************************************************
// <copyright file="IObservableSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return the section/property assignment name.
    /// </summary>
    public interface IObservableSection
    {
        /// <summary>
        /// Returns the name of the section property assigned to an element/object.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <param name="name">The name of a defined element/object.</param>
        string GetSection(string name);
    }
}
