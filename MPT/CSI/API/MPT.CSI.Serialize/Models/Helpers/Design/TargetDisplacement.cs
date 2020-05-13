// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="TargetDisplacement.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Class TargetDisplacement.
    /// </summary>
    public class TargetDisplacement : ModelProperty
    {
        /// <summary>
        /// Name of the static linear load case associated with the lateral displacement target.
        /// </summary>
        /// <value>The load case.</value>
        public string LoadCase { get; set; }

        /// <summary>
        /// Name of the point object associated to which the lateral displacement target applies.
        /// </summary>
        /// <value>The name of the point.</value>
        public string PointName { get; set; }

        /// <summary>
        /// Lateral displacement targets. [L]
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
