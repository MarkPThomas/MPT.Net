// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MechanicalProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MechanicalProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MechanicalIsotropicProperties" />
    public class MechanicalOrthotropicProperties : ApiProperty
    {
        /// <summary>
        /// Gets or sets the modulus of elasticity.
        /// </summary>
        /// <value>The modulus of elasticity.</value>
        public ElasticModulusProperties ModulusOfElasticity { get; set; } = new ElasticModulusProperties();

        /// <summary>
        /// Gets or sets the thermal coefficient.
        /// </summary>
        /// <value>The thermal coefficient.</value>
        public ThermalExpansionOrthotropicCoefficients ThermalCoefficient { get; set; } = new ThermalExpansionOrthotropicCoefficients();

        /// <summary>
        /// Gets or sets the poissons ratio.
        /// </summary>
        /// <value>The poissons ratio.</value>
        public PoissonsRatioOrthotropicProperties PoissonsRatio { get; set; } = new PoissonsRatioOrthotropicProperties();

        /// <summary>
        /// Gets or sets the shear modulus.
        /// </summary>
        /// <value>The shear modulus.</value>
        public ShearModulusProperties ShearModulus { get; set; } = new ShearModulusProperties();

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
