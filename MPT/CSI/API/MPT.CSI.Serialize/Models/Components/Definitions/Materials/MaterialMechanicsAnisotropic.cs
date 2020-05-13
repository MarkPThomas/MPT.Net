// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialMechanicsAnisotropic.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsAnisotropic.
    /// </summary>
    /// <seealso cref="MaterialMechanics" />
    public class MaterialMechanicsAnisotropic : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// Gets the anisotropic properties.
        /// </summary>
        /// <value>The anisotropic properties.</value>
        public MechanicalAnisotropicProperties AnisotropicProperties { get; internal set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsAnisotropic.</returns>
        internal static MaterialMechanicsAnisotropic Factory(
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsAnisotropic material = new MaterialMechanicsAnisotropic(uniqueName, temperature);
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsAnisotropic" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsAnisotropic(
            string name,
            double temperature = 0) : base(name, eMaterialSymmetryType.Anisotropic, temperature)
        {
        }
        #endregion
    }
}
