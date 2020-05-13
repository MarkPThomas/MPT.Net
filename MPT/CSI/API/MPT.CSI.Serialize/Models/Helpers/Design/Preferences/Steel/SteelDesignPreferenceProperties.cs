// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="SteelDesignPreferenceProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel
{
    /// <summary>
    /// Class SteelDesignPreferenceProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.ModelProperty" />
    public abstract class SteelDesignPreferenceProperties : ModelProperty
    {
        /// <summary>
        /// Indicates how results for multivalued cases (Time history, Nonlinear static or Multi-step static) are considered in the design.
        /// </summary>
        /// <value>The multi response case.</value>
        public eMultiResponseCase MultiResponseCase { get; set; } = eMultiResponseCase.Envelopes;

        /// <summary>
        /// The live load factor for automatic generation of load combinations involving pattern live loads and dead loads.
        /// </summary>
        /// <value>The pattern live load factor.</value>
        public double PatternLiveLoadFactor { get; set; } = 0.75;

        /// <summary>
        /// The demand/capacity ratio limit to be used for acceptability.
        /// D/C ratios that are less than or equal to this value are considered acceptable.
        /// </summary>
        /// <value>The demand capacity ratio limit.</value>
        public double DemandCapacityRatioLimit { get; set; } = 0.95;

        /// <summary>
        /// Gets or sets the maximum iterations.
        /// </summary>
        /// <value>The maximum iterations.</value>
        public int MaximumIterations { get; set; } = 1;

        /// <summary>
        /// Toggle to consider whether deflection limitations should be considered in design.
        /// </summary>
        /// <value><c>true</c> if this instance is deflection considered; otherwise, <c>false</c>.</value>
        public bool IsDeflectionConsidered { get; set; } = false;

        /// <summary>
        /// Deflection limitation for dead load.
        /// Inputting 120 means that the limit is L/120. 
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio dl.</value>
        public double RatioDL { get; set; } = 120;

        /// <summary>
        /// Deflection limitation for superimposed dead plus live load.
        /// Inputting 120 means that the limit is L/120.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio SDL and ll.</value>
        public double RatioSDLAndLL { get; set; } = 120;

        /// <summary>
        /// Deflection limitation for superimposed live load.
        /// Inputting 360 means that the limit is L/360.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio ll.</value>
        public double RatioLL { get; set; } = 360;

        /// <summary>
        /// Deflection limitation for total load.
        /// Inputting 240 means that the limit is L/240.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio total.</value>
        public double RatioTotal { get; set; } = 240;

        /// <summary>
        /// Limitation for net deflection.
        /// Camber is subtracted from the total load deflection to get net deflection.
        /// Inputting 240 means that the limit is L/240.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio net.</value>
        public double RatioNet { get; set; } = 240;

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
