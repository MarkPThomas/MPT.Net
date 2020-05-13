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

using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
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
        /// The actual distance from the I-End of the element to the start of the distributed load. [L]
        /// </summary>
        /// <value>The absolute distance start from i.</value>
        public double AbsoluteDistanceStartFromI { get; set; }

        /// <summary>
        /// The actual distance from the I-End of the element to the end of the distributed load. [L]
        /// </summary>
        /// <value>The absolute distance end from i.</value>
        public double AbsoluteDistanceEndFromI { get; set; }

        /// <summary>
        /// The relative distance from the I-End of the element to the start of the distributed load.
        /// </summary>
        /// <value>The relative distance start from i.</value>
        public double RelativeDistanceStartFromI { get; set; }

        /// <summary>
        /// The relative distance from the I-End of the element to the end of the distributed load.
        /// </summary>
        /// <value>The relative distance end from i.</value>
        public double RelativeDistanceEndFromI { get; set; }

        /// <summary>
        /// Coordinated system used for the distributed load.
        /// It may be Local or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }
        
    }
}
