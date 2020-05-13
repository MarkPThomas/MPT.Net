// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate3DCylindrical.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// 3D-coordinate by cylindrical values.
    /// </summary>
    public struct Coordinate3DCylindrical
    {
        /// <summary>
        /// The radius coordinate. [L]
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// The angle coordinate for the specified point.
        /// The angle is measured in the XY plane from the positive X axis.
        /// When looking in the XY plane with the positive Z axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The theta.</value>
        public double Theta { get; set; }


        /// <summary>
        /// Coordinate along the Z-axis. [L]
        /// </summary>
        /// <value>The z.</value>
        public double Z { get; set; }
    }
}
