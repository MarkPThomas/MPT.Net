// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="Gridlines.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// The gridlines corresponding to a single coordinate system.
    /// </summary>
    public class GridLines
    {
        /// <summary>
        /// The coordinate system that contains the gridlines.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }

        /// <summary>
        /// Gets or sets the lines.
        /// </summary>
        /// <value>The lines.</value>
        public List<GridLine> Lines { get; } = new List<GridLine>();
    }
}
