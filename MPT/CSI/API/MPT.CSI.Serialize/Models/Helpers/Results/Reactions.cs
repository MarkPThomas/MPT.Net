// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="Reactions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Reaction values associated with forces oriented along global axes.
    /// </summary>
    public struct Reactions
    {
        /// <summary>
        /// Translational force in the coordinate system X-axis direction [F].
        /// </summary>
        /// <value>The fx.</value>
        public double Fx { get; set; }

        /// <summary>
        /// Translational force in the coordinate system Y-axis direction [F].
        /// </summary>
        /// <value>The fy.</value>
        public double Fy { get; set; }


        /// <summary>
        /// Translational force in the coordinate system Z-axis direction [F].
        /// </summary>
        /// <value>The fz.</value>
        public double Fz { get; set; }


        /// <summary>
        /// Moment about the coordinate system X-axis [F*L].
        /// </summary>
        /// <value>The mx.</value>
        public double Mx { get; set; }

        /// <summary>
        /// Moment about the coordinate system Yaxis [F*L].
        /// </summary>
        /// <value>My.</value>
        public double My { get; set; }

        /// <summary>
        /// Moment about the coordinate system Z-axis [F*L].
        /// </summary>
        /// <value>The mz.</value>
        public double Mz { get; set; }
    }
}
