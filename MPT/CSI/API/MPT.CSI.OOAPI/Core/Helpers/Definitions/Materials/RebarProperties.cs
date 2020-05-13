// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="RebarProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class RebarProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.SteelMetalProperties" />
    public class RebarProperties : SteelMetalProperties
    {
        #region Fields & Properties
        /// <summary>
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eRebarStressStrainCurveType StressStrainCurveType { get; set; }

        /// <summary>
        /// The ultimate strain capacity.
        /// This item must be larger than the <see cref="SteelMetalProperties.StrainAtHardening" /> property.
        /// This item applies only when parametric stress-strain curves are used and when <see cref="UseCaltransStressStrainDefaults" /> is False.
        /// </summary>
        /// <value>The strain at hardening.</value>
        public double StrainUltimate { get; set; }

        /// <summary>
        /// True: Program uses Caltrans default controlling strain values, which are bar size dependent.
        /// </summary>
        /// <value><c>true</c> if [use caltrans stress strain defaults]; otherwise, <c>false</c>.</value>
        public bool UseCaltransStressStrainDefaults { get; set; }
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
