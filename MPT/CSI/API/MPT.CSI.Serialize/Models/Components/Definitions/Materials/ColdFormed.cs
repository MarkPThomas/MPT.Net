// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="ColdFormed.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class ColdFormed.
    /// </summary>
    /// <seealso cref="Material" />
    public class ColdFormed : MaterialByTemperature<ColdFormedSteelProperties>
    {
        #region Fields & Properties



        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>ColdFormed.</returns>
        internal static ColdFormed Factory(string uniqueName, double temperature = 0)
        {
            ColdFormed material = new ColdFormed(uniqueName, temperature);

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColdFormed"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ColdFormed(string name,
            double temperature = 0) : base(name, temperature)
        {
        }
        #endregion
    }
}