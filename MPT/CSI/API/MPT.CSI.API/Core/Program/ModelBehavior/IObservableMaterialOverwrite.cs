// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservableMaterialOverwrite.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return material overwrites.
    /// </summary>
    public interface IObservableMaterialOverwrite
    {
        /// <summary>
        /// Returns the material overwrite assigned to an element, if any.
        /// This is None if there is no material overwrite assignment, or it is the name of an existing material property.
        /// </summary>
        /// <param name="name">The name of a defined element.</param>
        string GetMaterialOverwrite(string name);
    }
}
