// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="ConcreteProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ConcreteProperties.
    /// </summary>
    public class ConcreteProperties : MaterialProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The concrete compressive strength. [F/L^2].
        /// </summary>
        /// <value>The fc.</value>
        public double fc { get; set; }

        /// <summary>
        /// True: The concrete is assumed to be lightweight concrete.
        /// </summary>
        /// <value><c>true</c> if this instance is lightweight; otherwise, <c>false</c>.</value>
        public bool IsLightweight { get; set; }

        /// <summary>
        /// The shear strength reduction factor for lightweight concrete.
        /// </summary>
        /// <value>The shear strength reduction factor.</value>
        public double ShearStrengthReductionFactor { get; set; }

        /// <summary>
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eConcreteStressStrainCurveType StressStrainCurveType { get; set; }

        /// <summary>
        /// The stress-strain hysteresis type.
        /// </summary>
        /// <value>The type of the stress strain hysteresis.</value>
        public eHysteresisType StressStrainHysteresisType { get; set; }

        /// <summary>
        /// The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain unconfined compressive.</value>
        public double StrainUnconfinedCompressive { get; set; }

        /// <summary>
        /// The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain ultimate.</value>
        public double StrainUltimate { get; set; }

        /// <summary>
        /// This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.
        /// </summary>
        /// <value>The final slope.</value>
        public double FinalSlope { get; set; }


        /// <summary>
        /// Gets or sets the angles.
        /// </summary>
        /// <value>The angles.</value>
        public ConcreteAngleProperties Angles { get; set; }
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
