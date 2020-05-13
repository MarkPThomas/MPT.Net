// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameInsertionPoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames
{
    /// <summary>
    /// Class FrameInsertionPoint.
    /// </summary>
    public class FrameInsertionPoint : ModelProperty
    {
        /// <summary>
        /// Three joint offset distances, in the Global coordinate system, at the I-End of the frame object. [L]
        /// </summary>
        /// <value>The offset distances i.</value>
        public Displacements OffsetDistancesI { get; set; }

        /// <summary>
        /// Three joint offset distances, in the Global coordinate system, at the J-End of the frame object. [L]
        /// </summary>
        /// <value>The offset distances j.</value>
        public Displacements OffsetDistancesJ { get; set; }

        /// <summary>
        /// Specifies the cardinal point for the frame object.
        /// The cardinal point specifies the relative position of the frame section on the line representing the frame object.
        /// </summary>
        /// <value>The cardinal point.</value>
        public eCardinalInsertionPoint CardinalPoint { get; set; }

        /// <summary>
        /// True: The frame object section is assumed to be mirrored (flipped) about its local 2-axis.
        /// </summary>
        /// <value><c>true</c> if this instance is mirrored local2; otherwise, <c>false</c>.</value>
        public bool IsMirroredLocal2 { get; set; }

        /// <summary>
        /// True: The frame object section is assumed to be mirrored (flipped) about its local 3-axis.
        /// </summary>
        /// <value><c>true</c> if this instance is mirrored local3; otherwise, <c>false</c>.</value>
        public bool IsMirroredLocal3 { get; set; }

        /// <summary>
        /// True: The frame object stiffness is transformed for cardinal point and joint offsets from the frame section centroid.
        /// </summary>
        /// <value><c>true</c> if this instance is stiffness transformed; otherwise, <c>false</c>.</value>
        public bool IsStiffnessTransformed { get; set; }

        /// <summary>
        /// Coordinate system in which the <see cref="OffsetDistancesI" /> and <see cref="OffsetDistancesJ" /> items are specified.
        /// This is <see cref="Components.Grids.CoordinateSystems.Local" /> or the name of a defined coordinate system.
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
