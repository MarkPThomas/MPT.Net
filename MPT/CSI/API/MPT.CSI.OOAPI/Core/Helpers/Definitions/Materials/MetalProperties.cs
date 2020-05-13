// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="MetalProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MetalProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MaterialProperties" />
    public class MetalProperties : MaterialProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The minimum yield stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fy { get; set; }

        /// <summary>
        /// The minimum tensile stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fu { get; set; }


        /// <summary>
        /// The stress-strain hysteresis type.
        /// </summary>
        /// <value>The type of the stress strain hysteresis.</value>
        public eHysteresisType StressStrainHysteresisType { get; set; }


        /// <summary>
        /// This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.
        /// </summary>
        /// <value>The final slope.</value>
        public double FinalSlope { get; set; }
        #endregion

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
