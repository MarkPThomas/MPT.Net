// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservableObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Element can return the corresponding object name.
    /// </summary>
    public interface IObservableObject
    {
        /// <summary>
        /// Returns the name of the object from which an element was created.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        string GetObject(string name);
    }
}
