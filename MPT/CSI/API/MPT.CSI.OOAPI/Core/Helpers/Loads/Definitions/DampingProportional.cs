// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="DampingProportional.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents proportional damping settings.
    /// </summary>
    public class DampingProportional : ApiProperty
    {
        /// <summary>
        /// The proportional modal damping type.
        /// </summary>
        /// <value>The type of the damping.</value>
        public eDampingTypeProportional DampingType { get; set; }

        /// <summary>
        /// The mass proportional damping coefficient.
        /// </summary>
        /// <value>The mass proportional damping coefficient.</value>
        public double MassProportionalDampingCoefficient { get; set; }

        /// <summary>
        /// The stiffness proportional damping coefficient.
        /// </summary>
        /// <value>The stiffness proportional damping coefficient.</value>
        public double StiffnessProportionalDampingCoefficient { get; set; }

        /// <summary>
        /// The period or frequency for point 1, depending on the value of <see cref="DampingType" />. [s] 
        /// For <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// </summary>
        /// <value>The period or frequency PT1.</value>
        public double PeriodOrFrequencyPt1 { get; set; }

        /// <summary>
        /// The period or frequency for point 2, depending on the value of <see cref="DampingType" />. [s] 
        /// For <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// </summary>
        /// <value>The period or frequency PT2.</value>
        public double PeriodOrFrequencyPt2 { get; set; }

        /// <summary>
        /// The damping for point 1 (0 &lt;= <see cref="DampingPt1" /> &lt; 1).
        /// This item applies only when <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// </summary>
        /// <value>The damping PT1.</value>
        public double DampingPt1 { get; set; }

        /// <summary>
        /// The damping for point 2 (0 &lt;= <see cref="DampingPt2" /> &lt; 1).
        /// This item applies only when <see cref="DampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// </summary>
        /// <value>The damping PT2.</value>
        public double DampingPt2 { get; set; }


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
