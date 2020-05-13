// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="ConcreteDesignPreferenceProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class ConcreteDesignPreferencesProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.ModelProperty" />
    public abstract class ConcreteDesignPreferencesProperties : ModelProperty
    {
        /// <summary>
        /// Indicates how results for multivalued cases (Time history, Nonlinear static or Multi-step static) are considered in the design.
        /// </summary>
        /// <value>The multi response case.</value>
        public eMultiResponseCase MultiResponseCase { get; set; } = eMultiResponseCase.Envelopes;

        /// <summary>
        /// The number of two-dimensional interaction curves used to make up the three-dimensionalinteraction surface.
        /// This item must be greater than or equal to 4 and divisible by 4. // TODO: Implement this in the set
        /// </summary>
        /// <value>The number of interaction curves.</value>
        public int NumberOfInteractionCurves { get; set; } = 24;

        /// <summary>
        /// The number of points used to define a two-dimensional interaction curve.This item must be greater than or equal to 5 and odd.
        /// </summary>
        /// <value>The number of interaction points.</value>
        public int NumberOfInteractionPoints { get; set; } = 11;

        /// <summary>
        /// Toggle to consider whether minimum eccentricity should be considered in design.
        /// </summary>
        /// <value><c>true</c> if [consider minimum eccentricity]; otherwise, <c>false</c>.</value>
        public bool ConsiderMinimumEccentricity { get; set; } = true;

        /// <summary>
        /// The live load factor for automatic generation of load combinations involving pattern live loads and dead loads.
        /// </summary>
        /// <value>The pattern live load factor.</value>
        public double PatternLiveLoadFactor { get; set; } = 0.75;

        /// <summary>
        /// The stress ratio limit to be used for acceptability.Stress ratios that are less than or equal to this value are considered acceptable.
        /// </summary>
        /// <value>The utilization factor limit.</value>
        public double UtilizationFactorLimit { get; set; } = 0.95;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
