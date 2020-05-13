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

using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
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
        /// The actual distance from the I-End of the element to the location of the point load. [L]
        /// </summary>
        /// <value>The absolute distance from i.</value>
        public double AbsoluteDistanceFromI { get; set; }

        /// <summary>
        /// The relative distance from the I-End of the element to the location of the point load.
        /// </summary>
        /// <value>The relative distance from i.</value>
        public double RelativeDistanceFromI { get; set; }

        /// <summary>
        /// Coordinate system used for each point load.
        /// It may be Local or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }
        
    }
}
