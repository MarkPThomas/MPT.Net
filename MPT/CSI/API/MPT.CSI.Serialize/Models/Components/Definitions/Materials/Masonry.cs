// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Masonry.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class Masonry.
    /// </summary>
    /// <seealso cref="MaterialByTemperature{T}" />
    /// <seealso cref="Material" />
    public class Masonry : MaterialByTemperature<MasonryProperties>
    {

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Masonry.</returns>
        internal static Masonry Factory(string uniqueName, double temperature = 0)
        {
            Masonry material = new Masonry(uniqueName, temperature);

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Masonry" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected Masonry(string name, double temperature = 0) : base(name, temperature)
        {
        }

        #endregion
    }
}
