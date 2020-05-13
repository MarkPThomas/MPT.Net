// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="FrameLoadsDistributed.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct FrameLoadsDistributed
    /// </summary>
    public class FrameLoadDistributed : Load
    {
        /// <summary>
        /// Force type for the distributed load for the load pattern.
        /// </summary>
        /// <value>The type of the force.</value>
        public eLoadForceType ForceType { get; set; }

        /// <summary>
        /// Direction that the load is applied in for the load pattern.
        /// </summary>
        /// <value>The load direction.</value>
        public eLoadDirection LoadDirection { get; set; }

        /// <summary>
        /// The load value at the start of the distributed load.
        /// [F/L] when <see cref="ForceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <see cref="ForceType" /> is <see cref="eLoadForceType.Moment" />.
        /// </summary>
        /// <value>The start load value.</value>
        public double StartLoadValue { get; set; }

        /// <summary>
        /// The load value at the end of the distributed load.
        /// [F/L] when <see cref="ForceType" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <see cref="ForceType" /> is <see cref="eLoadForceType.Moment" />.
        /// </summary>
        /// <value>The end load value.</value>
        public double EndLoadValue { get; set; }

        /// <summary>
        /// The absolute/relative start/end of the distributed load from the I-end.
        /// Minimum values are enforced as 0.
        /// </summary>
        /// <value>The relative distance start bracing.</value>
        public RelativeAbsoluteLength DistanceFromI { get; protected set; }

        /// <summary>
        /// Coordinated system used for the distributed load.
        /// It may be Local or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FrameLoadDistributed"/> class.
        /// </summary>
        /// <param name="frameLength">Length of the frame.</param>
        public FrameLoadDistributed(double frameLength = 0)
        {
            DistanceFromI = new RelativeAbsoluteLength(frameLength);
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
