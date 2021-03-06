﻿// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-07-2019
// ***********************************************************************
// <copyright file="BS_8110_1997_Preferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete
{
    /// <summary>
    /// Class BS_8110_1997_Preferences.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete.ConcreteDesignPreferencesProperties" />
    public class BS_8110_1997_Preferences : ConcreteDesignPreferencesProperties
    {
        /// <summary>
        /// The strength reduction factor GammaS for steel.
        /// </summary>
        /// <value>The gamma s.</value>
        public double GammaS { get; set; } = 1.15;

        /// <summary>
        /// The strength reduction factor GammaC for concrete.
        /// </summary>
        /// <value>The gamma c.</value>
        public double GammaC { get; set; } = 1.5;

        /// <summary>
        /// The strength reduction factor for concrete in shear.
        /// </summary>
        /// <value>The gamma m.</value>
        public double GammaM { get; set; } = 1.25;



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
