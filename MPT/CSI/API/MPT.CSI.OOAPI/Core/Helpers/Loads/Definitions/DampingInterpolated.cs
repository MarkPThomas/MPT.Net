// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="DampingInterpolated.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents interpolated damping settings.
    /// </summary>
    public class DampingInterpolated : ApiProperty
    {
        /// <summary>
        /// The interpolated modal damping type.
        /// </summary>
        /// <value>The type of the damping.</value>
        public eDampingTypeInterpolated DampingType { get; set; }

        /// <summary>
        /// The periods or frequencies, depending on the value of <see cref="DampingType" />. [s]
        /// For <see cref="DampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <see cref="DampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.
        /// </summary>
        /// <value>The periods or frequency.</value>
        public double PeriodOrFrequency { get; set; }

        /// <summary>
        /// The damping for the specified period of frequency (0 &lt;= <see cref="Damping" /> &lt; 1).
        /// </summary>
        /// <value>The damping.</value>
        public double Damping { get; set; }

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
