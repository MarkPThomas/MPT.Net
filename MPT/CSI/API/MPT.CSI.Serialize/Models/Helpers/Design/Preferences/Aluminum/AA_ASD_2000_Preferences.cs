// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="AA_ASD_2000_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum
{
    /// <summary>
    /// Class AA_ASD_2000_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum.AluminumDesignPreferenceProperties" />
    public class AA_ASD_2000_Preferences: AluminumDesignPreferenceProperties
    {

        /// <summary>
        /// The allowable stress increase factor for loading combinations that include wind or seismic loads. 
        /// </summary>
        /// <value>The lateral factor.</value>
        public double LateralFactor { get; set; } = 1.33333333333333;

        /// <summary>
        /// Gets or sets a value indicating whether to use the lateral factor.
        /// </summary>
        /// <value><c>true</c> if [use lateral factor]; otherwise, <c>false</c>.</value>
        public bool UseLateralFactor { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bridge type structure or some other type of structure such as a building-type structure..
        /// </summary>
        /// <value><c>true</c> if this instance is bridge type structure; otherwise, <c>false</c>.</value>
        public bool IsBridgeTypeStructure { get; set; } = false;


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
