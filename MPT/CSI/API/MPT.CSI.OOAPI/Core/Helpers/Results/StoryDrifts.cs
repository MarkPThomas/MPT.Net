// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="StoryDrifts.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Represents story drift analysis results.
    /// </summary>
    public class StoryDrifts : ApiProperty
    {
        /// <summary>
        /// The direction of the drift/displacement.
        /// </summary>
        /// <value>The direction.</value>
        public string Direction { get; set; }

        /// <summary>
        /// The drift.
        /// </summary>
        /// <value>The drift.</value>
        public double Drift { get; set; }

        /// <summary>
        /// The displacement in the x-direction [L].
        /// </summary>
        /// <value>The displacement x.</value>
        public double DisplacementX { get; set; }

        /// <summary>
        /// The displacement in the y-direction [L].
        /// </summary>
        /// <value>The displacement y.</value>
        public double DisplacementY { get; set; }

        /// <summary>
        /// The displacement in the z-direction [L].
        /// </summary>
        /// <value>The displacement z.</value>
        public double DisplacementZ { get; set; }


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
