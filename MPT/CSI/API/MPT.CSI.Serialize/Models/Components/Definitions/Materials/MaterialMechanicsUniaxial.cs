﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialMechanicsUniaxial.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsUniaxial.
    /// </summary>
    /// <seealso cref="MaterialMechanics" />
    public class MaterialMechanicsUniaxial : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// Gets the uniaxial properties.
        /// </summary>
        /// <value>The uniaxial properties.</value>
        public MechanicalUniaxialProperties UniaxialProperties { get; internal set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsUniaxial.</returns>
        internal static MaterialMechanicsUniaxial Factory(
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsUniaxial material = new MaterialMechanicsUniaxial(uniqueName, temperature);
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsUniaxial" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsUniaxial(
            string name,
            double temperature = 0) : base(name, eMaterialSymmetryType.Uniaxial, temperature)
        {
        }
        #endregion
    }
}
