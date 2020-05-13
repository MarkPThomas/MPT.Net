// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="TargetPeriod.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Class TargetPeriod.
    /// </summary>
    public class TargetPeriod : ModelProperty
    {
        /// <summary>
        /// Mode number associated with each target.
        /// </summary>
        /// <value>The mode number.</value>
        public int ModeNumber { get; set; }

        /// <summary>
        /// Target period. [s]
        /// </summary>
        /// <value>The value.</value>
        public double Value { get; set; }



        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
