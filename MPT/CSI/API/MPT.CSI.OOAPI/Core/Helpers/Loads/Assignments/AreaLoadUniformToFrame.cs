// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-22-2018
// ***********************************************************************
// <copyright file="AreaLoadUniformToFrame.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct AreaLoadUniformToFrame
    /// </summary>
    public class AreaLoadUniformToFrame : Load
    {
        /// <summary>
        /// The direction that the load is applied.
        /// </summary>
        /// <value>The direction.</value>
        public eLoadDirection Direction { get; set; }

        /// <summary>
        /// Load distribution type for how the load is tranferred to element edges.
        /// </summary>
        /// <value>The type of the distribution.</value>
        public eLoadDistributionType DistributionType { get; set; }

        /// <summary>
        /// The uniform load value. [F/L^2]
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
