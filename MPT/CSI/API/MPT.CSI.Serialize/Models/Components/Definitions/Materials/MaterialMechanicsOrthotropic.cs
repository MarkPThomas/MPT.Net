// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialMechanicsOrthotropic.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsOrthotropic.
    /// </summary>
    /// <seealso cref="MaterialMechanics" />
    public class MaterialMechanicsOrthotropic : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// Gets the orthotropic properties.
        /// </summary>
        /// <value>The orthotropic properties.</value>
        public MechanicalOrthotropicProperties OrthotropicProperties { get; internal set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsOrthotropic.</returns>
        internal static MaterialMechanicsOrthotropic Factory(
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsOrthotropic material = new MaterialMechanicsOrthotropic(uniqueName, temperature);
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsOrthotropic" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsOrthotropic(
            string name,
            double temperature = 0) : base(name, eMaterialSymmetryType.Orthotropic, temperature)
        {
        }
        #endregion
    }
}
