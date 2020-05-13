// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="JointDrifts.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Represents joint drift analysis results.
    /// </summary>
    public class JointDrifts : ApiProperty
    {
        /// <summary>
        /// The drift x in the x-direction.
        /// </summary>
        /// <value>The drift x.</value>
        public double DriftX { get; set; }

        /// <summary>
        /// The drift y in the y-direction.
        /// </summary>
        /// <value>The drift y.</value>
        public double DriftY { get; set; }

        /// <summary>
        /// The displacement in the x-direction [L]..
        /// </summary>
        /// <value>The displacement x.</value>
        public double DisplacementX { get; set; }

        /// <summary>
        /// The displacement in the y-direction [L].
        /// </summary>
        /// <value>The displacement y.</value>
        public double DisplacementY { get; set; }


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
