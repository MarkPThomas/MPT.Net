// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-29-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="HelperFunctions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Helpers
{
    /// <summary>
    /// Class HelperFunctions.
    /// </summary>
    public static class HelperFunctions
    {
        /// <summary>
        /// Adds the unique item to the provided list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="items">The items.</param>
        public static void AddUniqueItem<T>(T item, List<T> items) where T : Program.Model.StructureLayout.StructureObject
        {
            if (items.All(p => p.Name != item.Name))
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Adds the unique item to the provided list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="items">The items.</param>
        public static void AddUniqueGroup<T>(T item, List<T> items) where T : Group
        {
            if (items.All(p => p.Name != item.Name))
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Gets the name, or 'None'.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public static string GetNameOrNone(CSiOOAPiName item)
        {
            return item == null ? Constants.NONE : item.Name;
        }

        /// <summary>
        /// Gets the name, or empty.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public static string GetNameOrEmpty(CSiOOAPiName item)
        {
            return item == null ? string.Empty : item.Name;
        }

    }
}
