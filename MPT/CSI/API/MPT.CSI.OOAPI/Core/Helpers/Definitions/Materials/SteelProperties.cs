// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="SteelProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class SteelProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.SteelMetalProperties" />
    public class SteelProperties : SteelMetalProperties
    {
        #region Fields & Properties
        /// <summary>
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eSteelStressStrainCurveType StressStrainCurveType { get; set; }

        /// <summary>
        /// The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at maximum stress.</value>
        public double StrainAtMaxStress { get; set; }

        /// <summary>
        /// The strain at rupture.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at rupture.</value>
        public double StrainAtRupture { get; set; }
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
