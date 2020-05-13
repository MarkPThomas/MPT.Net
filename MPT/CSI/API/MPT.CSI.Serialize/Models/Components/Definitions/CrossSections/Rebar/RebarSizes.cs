// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="RebarSizes.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Rebar
{
    /// <summary>
    /// Class RebarSizes.
    /// </summary>
    /// <seealso cref="ObjectLists{Bar}" />
    public class RebarSizes : ObjectLists<Bar>
    {
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Bar fillNewItem(string uniqueName)
        {
            return new Bar(uniqueName);
        }
    }
}
