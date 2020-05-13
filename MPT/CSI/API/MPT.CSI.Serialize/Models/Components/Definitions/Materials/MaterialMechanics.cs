// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-20-2018
// ***********************************************************************
// <copyright file="MaterialMechanics.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Represents the mechanical properties of materials.
    /// </summary>
    public class MaterialMechanics
    {
        #region Fields & Properties
        /// <summary>
        /// The name of an existing material property.
        /// </summary>
        /// <value>The name.</value>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        internal double Temperature { get; set; }

        /// <summary>
        /// Symmetry type of the material.
        /// </summary>
        /// <value>The type.</value>
        public eMaterialSymmetryType SymmetryType { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="symmetryType">Material symmetry type.</param>
        /// <param name="temperature">The temperature associated with the mechanical properties.</param>
        /// <returns>MaterialMechanics.</returns>
        internal static MaterialMechanics Factory(
            string uniqueName,
            eMaterialSymmetryType symmetryType,
            double temperature = 0)
        {
            switch (symmetryType)
            {
                case eMaterialSymmetryType.Anisotropic:
                    return MaterialMechanicsAnisotropic.Factory(uniqueName, temperature);

                case eMaterialSymmetryType.Isotropic:
                    return MaterialMechanicsIsotropic.Factory(uniqueName, temperature);

                case eMaterialSymmetryType.Orthotropic:
                    return MaterialMechanicsOrthotropic.Factory(uniqueName, temperature);

                case eMaterialSymmetryType.Uniaxial:
                    return MaterialMechanicsUniaxial.Factory(uniqueName, temperature);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanics" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="symmetryType">Type of the symmetry.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanics(
            string name, 
            eMaterialSymmetryType symmetryType,
            double temperature) 
        {
            Name = name;
            SymmetryType = symmetryType;
            Temperature = temperature;
        }
        #endregion
    }
}
