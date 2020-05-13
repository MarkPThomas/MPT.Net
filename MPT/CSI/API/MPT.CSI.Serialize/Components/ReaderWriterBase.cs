// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-11-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-11-2019
// ***********************************************************************
// <copyright file="ReaderWriterBase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.Components
{
    /// <summary>
    /// Class ReaderWriterBase.
    /// </summary>
    public abstract class ReaderWriterBase
    {
        /// <summary>
        /// The model
        /// </summary>
        protected Model _model;
        /// <summary>
        /// The tables
        /// </summary>
        protected Dictionary<string, List<Dictionary<string, string>>> _tables;

        /// <summary>
        /// Sets the single table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="setFunction">The set function.</param>
        public void SetSingleTable(
            string tableName,
            Action<Model, List<Dictionary<string, string>>> setFunction)
        {
            if (containsTable(tableName))
            {
                setFunction(_model, _tables[tableName]);
            }
        }

        /// <summary>
        /// Determines whether the specified table name contains table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns><c>true</c> if the specified table name contains table; otherwise, <c>false</c>.</returns>
        protected bool containsTable(string tableName)
        {
            return _tables.ContainsKey(tableName);
        }
    }
}
