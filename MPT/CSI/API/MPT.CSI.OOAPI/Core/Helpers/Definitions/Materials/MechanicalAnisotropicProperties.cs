// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MechanicalAnisotropicProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MechanicalAnisotropicProperties.
    /// </summary>
    public class MechanicalAnisotropicProperties : ApiProperty
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
        public ThermalExpansionAnisotropicCoefficients ThermalCoefficient { get; set; } = new ThermalExpansionAnisotropicCoefficients();

        /// <summary>
        /// Gets or sets the poissons ratio.
        /// </summary>
        /// <value>The poissons ratio.</value>
        public PoissonsRatioAnisotropicProperties PoissonsRatio { get; set; } = new PoissonsRatioAnisotropicProperties();

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
