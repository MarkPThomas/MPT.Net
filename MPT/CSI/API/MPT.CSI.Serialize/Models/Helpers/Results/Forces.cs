// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="Forces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Internal forces or values associated with forces.
    /// </summary>
    public struct Forces
    {
        /// <summary>
        /// Axial force. [F].
        /// </summary>
        /// <value>The p.</value>
        public double P { get; set; }

        /// <summary>
        /// Major axis shear. [F].
        /// </summary>
        /// <value>The v2.</value>
        public double V2 { get; set; }


        /// <summary>
        /// Minor axis shear. [F].
        /// </summary>
        /// <value>The v3.</value>
        public double V3 { get; set; }


        /// <summary>
        /// Torsion [F*L].
        /// </summary>
        /// <value>The t.</value>
        public double T { get; set; }

        /// <summary>
        /// Minor axis bending. [F*L].
        /// </summary>
        /// <value>The m2.</value>
        public double M2 { get; set; }

        /// <summary>
        /// Major axis bending [F*L].
        /// </summary>
        /// <value>The m3.</value>
        public double M3 { get; set; }
    }
}
