﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-18-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate2DCartesian.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// 2D-coordinate by Cartesian values.
    /// </summary>
    public struct Coordinate2DCartesian
    {
        /// <summary>
        /// Coordinate along the X-axis. [L]
        /// </summary>
        /// <value>The x.</value>
        public double X { get; set; }

        /// <summary>
        /// Coordinate along the Y-axis. [L]
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; set; }
    }
}
