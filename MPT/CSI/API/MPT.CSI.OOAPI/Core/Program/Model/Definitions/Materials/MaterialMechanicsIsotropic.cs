// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialMechanicsIsotropic.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsIsotropic.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialMechanics" />
    public class MaterialMechanicsIsotropic : MaterialMechanics
    {
        #region Fields & Properties


        /// <summary>
        /// The isotropic properties
        /// </summary>
        private MechanicalIsotropicProperties _isotropicProperties;
        /// <summary>
        /// Gets the isotropic properties.
        /// </summary>
        /// <value>The isotropic properties.</value>
        public MechanicalIsotropicProperties IsotropicProperties => _isotropicProperties;
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsIsotropic.</returns>
        internal static MaterialMechanicsIsotropic Factory(
            ApiCSiApplication app,
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsIsotropic material = new MaterialMechanicsIsotropic(app, uniqueName, temperature);
            material.Fill();
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsIsotropic" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsIsotropic(
            ApiCSiApplication app,
            string name,
            double temperature = 0) : base(app, name, eMaterialSymmetryType.Isotropic, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the mechanical properties for a material with the corresponding directional symmetry type.
        /// </summary>
        public void Fill()
        {
            _apiMaterialProperties.GetMechanicalPropertiesIsotropic(Name,
                out var modulusOfElasticity,
                out var poissonsRatio,
                out var thermalCoefficient,
                out var shearModulus,
                Temperature);

            _isotropicProperties = new MechanicalIsotropicProperties()
            {
                ModulusOfElasticity = modulusOfElasticity,
                ThermalCoefficient = thermalCoefficient,
                PoissonsRatio = poissonsRatio,
                ShearModulus = shearModulus
            };
        }

        /// <summary>
        /// Sets the material directional symmetry type, and assigns the corresponding mechanical properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void Set(MechanicalIsotropicProperties properties)
        {
            _apiMaterialProperties.SetMechanicalPropertiesIsotropic(Name,
                properties.ModulusOfElasticity,
                properties.PoissonsRatio,
                properties.ThermalCoefficient,
                Temperature);

            _isotropicProperties = properties;
        }


        #endregion
    }
}
