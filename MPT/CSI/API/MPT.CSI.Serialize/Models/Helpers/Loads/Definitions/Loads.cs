// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="Loads.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// External loads/forces or values associated with forces oriented along axes.
    /// </summary>
    public struct Loads
    {
        /// <summary>
        /// Translational force in the local 1-axis or coordinate system X-axis direction [F].
        /// </summary>
        /// <value>The f1.</value>
        public double F1 { get; set; }

        /// <summary>
        /// Translational force in the local 2-axis or coordinate system Y-axis direction [F].
        /// </summary>
        /// <value>The f2.</value>
        public double F2 { get; set; }


        /// <summary>
        /// Translational force in the local 3-axis or coordinate system Z-axis direction [F].
        /// </summary>
        /// <value>The f3.</value>
        public double F3 { get; set; }


        /// <summary>
        /// Moment about the local 1-axis or coordinate system X-axis [F*L].
        /// </summary>
        /// <value>The m1.</value>
        public double M1 { get; set; }

        /// <summary>
        /// Moment about the local 2-axis or coordinate system Yaxis [F*L].
        /// </summary>
        /// <value>The m2.</value>
        public double M2 { get; set; }

        /// <summary>
        /// Moment about the local 3-axis or coordinate system Z-axis [F*L].
        /// </summary>
        /// <value>The m3.</value>
        public double M3 { get; set; }
    }
}
