﻿// ***********************************************************************
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
using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
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
        public eLoadDirection Direction { get; set; }

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
    }
}
