// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="TendonMaterial.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Represents material used for tendon sections/elements.
    /// </summary>
    /// <seealso cref="MaterialByTemperature{T}" />
    public class TendonMaterial : MaterialByTemperature<TendonMaterialProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>TendonMaterial.</returns>
        internal static TendonMaterial Factory(
            string uniqueName, 
            double temperature = 0)
        {
            TendonMaterial material = new TendonMaterial(uniqueName, temperature);

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TendonMaterial" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected TendonMaterial(
            string name, 
            double temperature = 0) : base(name, temperature)
        {
        }
        #endregion
    }
}
