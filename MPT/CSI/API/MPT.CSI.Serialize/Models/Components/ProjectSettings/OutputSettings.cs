// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-31-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-31-2019
// ***********************************************************************
// <copyright file="OutputSettings.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    /// <summary>
    /// Class OutputSettings.
    /// </summary>
    /// <seealso cref="ObjectLists{TableSet}" />
    public class OutputSettings : ObjectLists<TableSet>
    {
        /// <summary>
        /// Gets a value indicating whether an output file will be saved after an analysis has been run.
        /// </summary>
        /// <value><c>true</c> if [save file]; otherwise, <c>false</c>.</value>
        public bool SaveFile { get; internal set; }

        /// <summary>
        /// The filename of the saved output. Must include file extension, such as *.xml.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the named set that will be saved to a file.
        /// </summary>
        /// <value>The named set.</value>
        public TableSet NamedSet { get; internal set; }

        /// <summary>
        /// The group by which output results are filtered.
        /// </summary>
        /// <value>The group.</value>
        public Group Group { get; internal set; }

        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override TableSet fillNewItem(string uniqueName)
        {
            TableSet tableSet = new TableSet(uniqueName);
            return tableSet;
        }
    }
}
