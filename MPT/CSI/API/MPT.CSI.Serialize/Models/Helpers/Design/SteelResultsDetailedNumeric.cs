// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="SteelResultsDetailedNumeric.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Repesents the detailed design results numeric data for a steel frame.
    /// </summary>
    public class SteelResultsDetailedNumeric: ModelProperty
    {
        /// <summary>
        /// Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.
        /// </summary>
        /// <value>The table number.</value>
        public int TableNumber { get; set; }

        /// <summary>
        /// Field name with TEXT output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; set; }

        /// <summary>
        /// Frame object names for which results are obtained.
        /// </summary>
        /// <value>The name of the frame.</value>
        public string FrameName { get; set; }

        /// <summary>
        /// Design results with Numerical output data type of the request field in the request table for the specified frame objects.
        /// </summary>
        /// <value>The result.</value>
        public double Result { get; set; }



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
