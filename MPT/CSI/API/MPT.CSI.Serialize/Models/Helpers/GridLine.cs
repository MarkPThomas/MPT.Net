// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="GridLine.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// A gridline that is part of a grid system in a coordinate system.
    /// </summary>
    public class GridLine
    {
        /// <summary>
        /// Gets or sets the axis direction of the grid.
        /// </summary>
        /// <value>The axis direction.</value>
        public eDirection AxisDirection { get; set; }

        /// <summary>
        /// Gets or sets the grid identifier.
        /// </summary>
        /// <value>The grid identifier.</value>
        public string GridID { get; set; }

        /// <summary>
        /// Gets or sets the coordinate of the grid offset from the origin.
        /// </summary>
        /// <value>The coordinate.</value>
        public double Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the hierchal type of line.
        /// </summary>
        /// <value>The type of the line.</value>
        public eLineType LineType { get; set; }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public string LineColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridLine"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the bubble location along the line.
        /// </summary>
        /// <value>The bubble location.</value>
        public eBubbleLocation BubbleLocation { get; set; }
        

        /// <summary>
        /// Gets or sets a value indicating whether [all visible].
        /// </summary>
        /// <value><c>true</c> if [all visible]; otherwise, <c>false</c>.</value>
        public bool AllVisible { get; set; }

        /// <summary>
        /// Gets or sets the size of the bubble.
        /// </summary>
        /// <value>The size of the bubble.</value>
        public double BubbleSize { get; set; }
    }
}
