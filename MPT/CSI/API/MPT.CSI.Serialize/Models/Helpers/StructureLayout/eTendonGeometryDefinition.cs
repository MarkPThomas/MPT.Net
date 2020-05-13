﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eTendonGeometryDefinition.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Tendon definition used in the program for defining tendon geometry.
    /// </summary>
    public enum eTendonGeometryDefinition
    {
        /// <summary>
        /// Start of tendon.
        /// </summary>
        [Description("Start of Tendon")]
        StartOfTendon = 1,

        /// <summary>
        /// The segment preceding the point is linear.
        /// </summary>
        [Description("Linear")]
        SegmentPrecedingPointIsLinear = 2,

        /// <summary>
        /// The specified point is the end of a parabola.
        /// </summary>
        [Description("Parabola End Point")]
        ParabolaEndPoint = 6,

        /// <summary>
        /// The specified point is an intermediate point on a parabola.
        /// </summary>
        [Description("Parabola Intermediate Point")]
        ParabolaIntermediatePoint = 7,

        /// <summary>
        /// The specified point is the end of a circle.
        /// </summary>
        [Description("Circle End Point")]
        CircleEndPoint = 8,

        /// <summary>
        /// The specified point is an intermediate point on a circle.
        /// </summary>
        [Description("Circle Intermediate Point")]
        CircleIntermediatePoint = 9
    }
}