// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="FrameLoadsPoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct FrameLoadsPoint
    /// </summary>
    public class FrameLoadPoint : Load
    {
        /// <summary>
        /// Force type for the point load for each load pattern.
        /// </summary>
        /// <value>The type of the force.</value>
        public eLoadForceType ForceType { get; set; }

        /// <summary>
        /// Direction that the load is applied in for each load pattern.
        /// </summary>
        /// <value>The load direction.</value>
        public eLoadDirection LoadDirection { get; set; }

        /// <summary>
        /// The load value of the point loads.
        /// [F] when <see cref="ForceType" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <see cref="ForceType" /> is <see cref="eLoadForceType.Moment" />.
        /// </summary>
        /// <value>The point load values.</value>
        public double PointLoadValue { get; set; }

        /// <summary>
        /// The absolute/relative distance of the point load from the I-end.
        /// Minimum values are enforced as 0.
        /// </summary>
        /// <value>The relative distance start bracing.</value>
        public RelativeAbsoluteCoordinate DistanceFromI { get; protected set; }

        /// <summary>
        /// Coordinate system used for each point load.
        /// This is <see cref="CoordinateSystems.Local" /> or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="FrameLoadPoint"/> class.
        /// </summary>
        /// <param name="frameLength">Length of the frame.</param>
        public FrameLoadPoint(double frameLength = 0)
        {
            DistanceFromI = new RelativeAbsoluteCoordinate(frameLength);
        }

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
