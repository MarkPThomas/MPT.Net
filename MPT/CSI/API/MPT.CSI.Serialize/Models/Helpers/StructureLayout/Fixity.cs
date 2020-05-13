// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-14-2017
// ***********************************************************************
// <copyright file="Fixity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Fixities along the local degrees-of-freedom.
    /// </summary>
    public struct Fixity
    {
        /// <summary>
        /// Translational fixity along the 1-axis [F/L].
        /// </summary>
        /// <value>The u1.</value>
        public double U1 { get; set; }

        /// <summary>
        /// Translational fixity along the 2-axis [F/L].
        /// </summary>
        /// <value>The u2.</value>
        public double U2 { get; set; }


        /// <summary>
        /// Translational fixity along the 3-axis [F/L].
        /// </summary>
        /// <value>The u3.</value>
        public double U3 { get; set; }


        /// <summary>
        /// Rotational fixity along the 1-axis [FL/rad].
        /// </summary>
        /// <value>The r1.</value>
        public double R1 { get; set; }

        /// <summary>
        /// Rotational fixity along the 2-axis [FL/rad].
        /// </summary>
        /// <value>The r2.</value>
        public double R2 { get; set; }

        /// <summary>
        /// Rotational fixity along the 3-axis [FL/rad].
        /// </summary>
        /// <value>The r3.</value>
        public double R3 { get; set; }
    }
}
