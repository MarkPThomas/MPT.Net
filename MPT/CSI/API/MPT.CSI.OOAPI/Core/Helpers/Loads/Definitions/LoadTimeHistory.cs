// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="LoadTimeHistory.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents a load used in time history cases.
    /// </summary>
    public class LoadTimeHistory : LoadPatternTuple
    {
        /// <summary>
        /// The time scale factor of the load assigned to the load case.
        /// </summary>
        /// <value>The time factor.</value>
        public double TimeFactor { get; set; } = 1;

        /// <summary>
        /// The arrival time of the load assigned to the load case.
        /// </summary>
        /// <value>The arrival time.</value>
        public double ArrivalTime { get; set; }


        // TODO: Replace function with object
        /// <summary>
        /// The loading function.
        /// </summary>
        /// <value>The function.</value>
        public string Function { get; set; }

        /// <summary>
        /// The name of the coordinate system associated with the load.
        /// If this item is a blank string, the Global coordinate system is assumed.
        /// This item applies only when <see cref="LoadPatternTuple.LoadType" /> = <see cref="eLoadType.Accel" />.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }

        /// <summary>
        /// The angle between the acceleration local 1 axis and the +X-axis of the coordinate system specified by <see cref="CoordinateSystem" />.
        /// The rotation is about the Z-axis of the specified coordinate system. [deg].
        /// This item applies only when <see cref="LoadPatternTuple.LoadType" /> = <see cref="eLoadType.Accel" />.
        /// </summary>
        /// <value>The angle.</value>
        public double Angle { get; set; }


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
