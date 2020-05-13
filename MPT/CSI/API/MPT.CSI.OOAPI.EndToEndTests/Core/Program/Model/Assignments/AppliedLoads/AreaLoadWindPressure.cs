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
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
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
    }
}
