// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Aluminum.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class Aluminum.
    /// </summary>
    /// <seealso cref="Material" />
    public class Aluminum : MaterialByTemperature<AluminumProperties>
    {
        #region Fields & Properties



        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Aluminum.</returns>
        internal static Aluminum Factory(string uniqueName, double temperature = 0)
        {
            Aluminum material = new Aluminum(uniqueName, temperature);

            return material;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Aluminum"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Aluminum(string name,
                            double temperature = 0) : base(name, temperature)
        {
        }
        #endregion
    }
}