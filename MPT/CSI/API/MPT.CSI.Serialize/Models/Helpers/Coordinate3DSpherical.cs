// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-18-2017
// ***********************************************************************
// <copyright file="Coordinate3DSpherical.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// 3D-coordinate by spherical values.
    /// </summary>
    public struct Coordinate3DSpherical
    {
        /// <summary>
        /// The radius coordinate. [L]
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// The plan angle coordinate.
        /// This angle is measured in the XY plane from the positive global X axis.
        /// When looking in the XY plane with the positive Z axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The theta.</value>
        public double Theta { get; set; }


        /// <summary>
        /// The elevation angle coordinate.
        /// This angle is measured in an X'Z plane that is perpendicular to the XY plane with the positive X' axis oriented at angle <see cref="Theta" /> from the positive global X axis.
        /// Angle is measured from the positive global Z axis.
        /// When looking in the X’Z plane with the positive Y' axis pointing toward you, a positive angle is counter clockwise. [deg]
        /// </summary>
        /// <value>The phi.</value>
        public double Phi { get; set; }
    }
}
