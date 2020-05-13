// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ForcesActive.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Forces that are active based on the corresponding boolean value.
    /// </summary>
    public struct ForcesActive
    {
        /// <summary>
        /// Axial force. [F].
        /// </summary>
        /// <value><c>true</c> if p; otherwise, <c>false</c>.</value>
        public bool P { get; set; }

        /// <summary>
        /// Major axis shear. [F].
        /// </summary>
        /// <value><c>true</c> if v2; otherwise, <c>false</c>.</value>
        public bool V2 { get; set; }


        /// <summary>
        /// Minor axis shear. [F].
        /// </summary>
        /// <value><c>true</c> if v3; otherwise, <c>false</c>.</value>
        public bool V3 { get; set; }


        /// <summary>
        /// Torsion [F*L].
        /// </summary>
        /// <value><c>true</c> if t; otherwise, <c>false</c>.</value>
        public bool T { get; set; }

        /// <summary>
        /// Minor axis bending. [F*L].
        /// </summary>
        /// <value><c>true</c> if m2; otherwise, <c>false</c>.</value>
        public bool M2 { get; set; }

        /// <summary>
        /// Major axis bending [F*L].
        /// </summary>
        /// <value><c>true</c> if m3; otherwise, <c>false</c>.</value>
        public bool M3 { get; set; }
    }
}