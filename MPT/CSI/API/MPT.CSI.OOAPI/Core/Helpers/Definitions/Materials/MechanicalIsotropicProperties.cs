// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MechanicalIsotropicProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MechanicalIsotropicProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MechanicalUniaxialProperties" />
    public class MechanicalIsotropicProperties : MechanicalUniaxialProperties
    {
        /// <summary>
        /// Gets or sets the poissons ratio.
        /// </summary>
        /// <value>The poissons ratio.</value>
        public double PoissonsRatio { get; set; }

        /// <summary>
        /// Gets or sets the shear modulus.
        /// </summary>
        /// <value>The shear modulus.</value>
        public double ShearModulus { get; set; }

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
