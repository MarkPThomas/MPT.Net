// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="eTemperature.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Units
{
    /// <summary>
    /// Temperature unit types available in the application.
    /// </summary>
    public enum eTemperature
    {
        /// <summary>
        /// Not applicable.
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// Fahrenheit.
        /// </summary>
        F = 1,

        /// <summary>
        /// Celcius.
        /// </summary>
        C = 2
    }
}