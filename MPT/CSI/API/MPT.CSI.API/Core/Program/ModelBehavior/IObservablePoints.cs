// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservablePoints.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return the names of all associated points.
    /// </summary>
    public interface IObservablePoints
    {
        /// <summary>
        /// Returns the names of the point elements that define an element.
        /// The point names are listed in the positive order around the element.
        /// </summary>
        /// <param name="name">The name of an existing element.</param>
        string[] GetPoints(string name);
    }
}
