// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="DampingProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class DampingProperties.
    /// </summary>
    public class DampingProperties : ApiProperty
    {
        /// <summary>
        /// The modal damping ratio.
        /// </summary>
        /// <value>The modal damping ratio.</value>
        public double ModalDampingRatio { get; set; }

        /// <summary>
        /// The mass coefficient for viscous proportional damping.
        /// </summary>
        /// <value>The viscous mass coefficient.</value>
        public double ViscousMassCoefficient { get; set; }

        /// <summary>
        /// The stiffness coefficient for viscous proportional damping.
        /// </summary>
        /// <value>The viscous stiffness coefficient.</value>
        public double ViscousStiffnessCoefficient { get; set; }

        /// <summary>
        /// The mass coefficient for hysteretic proportional damping.
        /// </summary>
        /// <value>The hysteretic mass coefficient.</value>
        public double HystereticMassCoefficient { get; set; }

        /// <summary>
        /// The stiffness coefficient for hysteretic proportional damping.
        /// </summary>
        /// <value>The hysteretic stiffness coefficient.</value>
        public double HystereticStiffnessCoefficient { get; set; }

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
