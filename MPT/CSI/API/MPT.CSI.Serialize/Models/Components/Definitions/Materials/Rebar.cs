// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Rebar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class Rebar.
    /// </summary>
    /// <seealso cref="MaterialByTemperature{T}" />
    public class Rebar : MaterialByTemperature<RebarProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Rebar.</returns>
        internal static Rebar Factory(string uniqueName, double temperature = 0)
        {
            Rebar material = new Rebar(uniqueName, temperature);

            return material;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rebar" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected Rebar(string name, double temperature = 0) : base(name, temperature)
        { }
        #endregion
    }
}
