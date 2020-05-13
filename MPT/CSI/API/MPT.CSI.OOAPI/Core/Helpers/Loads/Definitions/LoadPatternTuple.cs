// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="LoadPatternTuple.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Contains data related to the application of a load pattern.
    /// </summary>
    public class LoadPatternTuple : ApiProperty
    {
        /// <summary>
        /// The load assigned to the load case.
        /// If <see cref="LoadType" /> = <see cref="eLoadType.Load" />, this item is the defined load pattern.
        /// If <see cref="LoadType" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.
        /// </summary>
        /// <value>The name of the load.</value>
        public LoadPattern Load { get; set; }

        /// <summary>
        /// The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.
        /// </summary>
        /// <value>The scale.</value>
        public double ScaleFactor { get; set; } = 1;

        /// <summary>
        /// Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.
        /// </summary>
        /// <value>The type.</value>
        public eLoadType LoadType { get; set; } = eLoadType.Load;


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
