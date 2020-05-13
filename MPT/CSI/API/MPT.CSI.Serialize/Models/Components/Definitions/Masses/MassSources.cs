// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="MassSources.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Masses
{
    /// <summary>
    /// Class MassSources.
    /// </summary>
    /// <seealso cref="ObjectLists{MassSource}" />
    public class MassSources : ObjectLists<MassSource>
    {
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override MassSource fillNewItem(string uniqueName)
        {
            MassSource massSource = new MassSource(uniqueName);
            return massSource;
        }
    }
}
