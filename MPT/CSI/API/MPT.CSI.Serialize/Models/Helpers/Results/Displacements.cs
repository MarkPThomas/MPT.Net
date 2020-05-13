// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-20-2017
// ***********************************************************************
// <copyright file="Displacements.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Displacements or offsets along the Global degrees of freedom.
    /// </summary>
    public struct Displacements
    {
        /// <summary>
        /// Translational displacement/offset along the X-axis.
        /// </summary>
        /// <value>The ux.</value>
        public double UX { get; set; }

        /// <summary>
        /// Translational displacement/offset along the Y-axis.
        /// </summary>
        /// <value>The uy.</value>
        public double UY { get; set; }


        /// <summary>
        /// Translational displacement/offset along the Z-axis.
        /// </summary>
        /// <value>The uz.</value>
        public double UZ { get; set; }


        /// <summary>
        /// Rotational displacement/offset along the X-axis.
        /// </summary>
        /// <value>The rx.</value>
        public double RX { get; set; }

        /// <summary>
        /// Rotational displacement/offset along the Y-axis.
        /// </summary>
        /// <value>The ry.</value>
        public double RY { get; set; }

        /// <summary>
        /// Rotational displacement/offset along the Z-axis.
        /// </summary>
        /// <value>The rz.</value>
        public double RZ { get; set; }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <returns>List&lt;System.Double&gt;.</returns>
        public List<double> ToArray()
        {
            List<double> displacements = new List<double>(){ UX, UY, UZ, RX, RY, RZ};
            return displacements;
        }
    }
}
