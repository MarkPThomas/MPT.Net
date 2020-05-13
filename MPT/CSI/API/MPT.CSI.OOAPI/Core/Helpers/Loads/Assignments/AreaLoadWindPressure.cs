// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-22-2018
// ***********************************************************************
// <copyright file="AreaLoadWindPressure.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct AreaLoadWindPressure
    /// </summary>
    public class AreaLoadWindPressure : Load
    {
        /// <summary>
        /// Wind pressure type.
        /// </summary>
        /// <value>The type of the wind pressure.</value>
        public eWindPressureApplication WindPressureType { get; set; }

        /// <summary>
        /// This is the wind pressure coefficient.
        /// </summary>
        /// <value>The pressure coefficient.</value>
        public double PressureCoefficient { get; set; }

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
