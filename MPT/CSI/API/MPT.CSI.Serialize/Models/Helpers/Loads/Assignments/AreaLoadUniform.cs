// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-22-2018
// ***********************************************************************
// <copyright file="AreaLoadUniform.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct AreaLoadUniform
    /// </summary>
    public class AreaLoadUniform : Load
    {
        /// <summary>
        /// The direction that the load is applied.
        /// </summary>
        /// <value>The direction.</value>
        public eLoadDirection LoadDirection { get; set; }

        /// <summary>
        /// The uniform load values. [F/L^2]
        /// </summary>
        /// <value>The value.</value>
        public double Value { get; set; }

        /// <summary>
        /// The name of the coordinate system associated with the uniform load.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }

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
