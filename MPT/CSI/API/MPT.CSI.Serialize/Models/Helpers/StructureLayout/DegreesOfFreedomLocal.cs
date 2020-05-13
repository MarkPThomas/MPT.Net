// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-16-2017
// ***********************************************************************
// <copyright file="DegreesOfFreedomLocal.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Local degrees of freedom, the inclusion of which is indicated by the corresponding boolean.
    /// </summary>
    public struct DegreesOfFreedomLocal
    {
        /// <summary>
        /// Translational degree-of-freedom along the 1-axis.
        /// </summary>
        /// <value><c>true</c> if u1; otherwise, <c>false</c>.</value>
        public bool U1 { get; set; }

        /// <summary>
        /// Translational degree-of-freedom along the 2-axis.
        /// </summary>
        /// <value><c>true</c> if u2; otherwise, <c>false</c>.</value>
        public bool U2 { get; set; }


        /// <summary>
        /// Translational degree-of-freedom along the 3-axis.
        /// </summary>
        /// <value><c>true</c> if u3; otherwise, <c>false</c>.</value>
        public bool U3 { get; set; }


        /// <summary>
        /// Rotational degree-of-freedom along the 1-axis.
        /// </summary>
        /// <value><c>true</c> if r1; otherwise, <c>false</c>.</value>
        public bool R1 { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the 2-axis.
        /// </summary>
        /// <value><c>true</c> if r2; otherwise, <c>false</c>.</value>
        public bool R2 { get; set; }

        /// <summary>
        /// Rotational degree-of-freedom along the 3-axis.
        /// </summary>
        /// <value><c>true</c> if r3; otherwise, <c>false</c>.</value>
        public bool R3 { get; set; }
    }
}
