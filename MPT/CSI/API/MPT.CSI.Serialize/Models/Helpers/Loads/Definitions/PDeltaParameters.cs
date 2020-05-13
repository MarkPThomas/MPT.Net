// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-15-2017
// ***********************************************************************
// <copyright file="PDeltaParameters.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// The P-delta moment parameters at each end of the line element used in the application.
    /// </summary>
    public struct PDeltaParameters
    {
        /// <summary>
        /// M2 P-delta to I-end of link as moment, M2I.
        /// </summary>
        /// <value>The m2 i.</value>
        public double M2I { get; set; }

        /// <summary>
        /// M2 P-delta to J-end of link as moment, M2J.
        /// </summary>
        /// <value>The m2 j.</value>
        public double M2J { get; set; }


        /// <summary>
        /// M3 P-delta to I-end of link as moment, M3I.
        /// </summary>
        /// <value>The m3 i.</value>
        public double M3I { get; set; }


        /// <summary>
        /// M3 P-delta to J-end of link as moment, M3J.
        /// </summary>
        /// <value>The m3 j.</value>
        public double M3J { get; set; }
    }
}
