// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="AutoWind.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class AutoWind.
    /// </summary>
    /// <seealso cref="AutoLoadPattern" />
    public class AutoWind : AutoLoadPattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWind"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal AutoWind(string name) : base(name)
        {
          Type = eLoadPatternType.Wind;
        }
    }
}
