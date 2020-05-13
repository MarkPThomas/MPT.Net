// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-14-2017
// ***********************************************************************
// <copyright file="DegreesOfFreedomGlobal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Global degrees of freedom, the inclusion of which is indicated by the corresponding boolean.
    /// </summary>
    public struct DegreesOfFreedomGlobal
    {
        /// <summary>
        /// Translational degree-of-freedom along the X-axis.
        /// </summary>
        /// <value><c>true</c> if ux; otherwise, <c>false</c>.</value>
        public bool UX { get; set; }

        /// <summary>
        /// Translational degree-of-freedom along the Y-axis.
        /// </summary>
        /// <value><c>true</c> if uy; otherwise, <c>false</c>.</value>
        public bool UY { get; set; }


        /// <summary>
        /// Translational degree-of-freedom along the Z-axis.
        /// </summary>
        /// <value><c>true</c> if uz; otherwise, <c>false</c>.</value>
        public bool UZ { get; set; }


        /// <summary>
        /// Rotational degree-of-freedom along the X-axis.
        /// </summary>
        /// <value><c>true</c> if rx; otherwise, <c>false</c>.</value>
        public bool RX { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the Y-axis.
        /// </summary>
        /// <value><c>true</c> if ry; otherwise, <c>false</c>.</value>
        public bool RY { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the Z-axis.
        /// </summary>
        /// <value><c>true</c> if rz; otherwise, <c>false</c>.</value>
        public bool RZ { get; set; }
    }
}
