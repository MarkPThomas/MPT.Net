// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Steel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Steel material.
    /// </summary>
    /// <seealso cref="Definitions.Materials.MaterialByTemperature{T}" />
    public class Steel : MaterialByTemperature<SteelProperties>
    {

        #region Initialization
        /// <summary>
        /// Returns a new steel material class.
        /// </summary>
        /// <param name="uniqueName">Unique material name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Steel.</returns>
        internal static Steel Factory(
            string uniqueName, 
            double temperature = 0)
        {
            Steel material = new Steel(uniqueName, temperature);

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Steel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected Steel(
            string name, 
            double temperature = 0) : base(name, temperature)
        {
        }
        #endregion
    }
}
