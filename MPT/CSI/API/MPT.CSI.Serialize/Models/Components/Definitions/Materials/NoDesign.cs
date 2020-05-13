// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="NoDesign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class NoDesign.
    /// </summary>
    /// <seealso cref="MaterialByTemperature{T}" />
    public class NoDesign : MaterialByTemperature<ConcreteAngleProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Concrete.</returns>
        internal static NoDesign Factory(
            string uniqueName, 
            double temperature = 0)
        {
            NoDesign material = new NoDesign(uniqueName, temperature);

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Concrete" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <!-- Badly formed XML comment ignored for member "M:MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature`1.#ctor(MPT.CSI.API.Core.Program.CSiApplication,System.String,System.Double)" -->
        protected NoDesign(
            string name, 
            double temperature = 0) : base(name, temperature)
        {
        }
        #endregion
    }
}
