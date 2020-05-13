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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsOrthotropic.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialMechanics" />
    public class MaterialMechanicsOrthotropic : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// The orthotropic properties
        /// </summary>
        private MechanicalOrthotropicProperties _orthotropicProperties;
        /// <summary>
        /// Gets the orthotropic properties.
        /// </summary>
        /// <value>The orthotropic properties.</value>
        public MechanicalOrthotropicProperties OrthotropicProperties => _orthotropicProperties;
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsOrthotropic.</returns>
        internal static MaterialMechanicsOrthotropic Factory(
            ApiCSiApplication app,
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsOrthotropic material = new MaterialMechanicsOrthotropic(app, uniqueName, temperature);
            material.Fill();
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsOrthotropic" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsOrthotropic(
            ApiCSiApplication app,
            string name,
            double temperature = 0) : base(app, name, eMaterialSymmetryType.Orthotropic, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the mechanical properties for a material with the corresponding directional symmetry type.
        /// </summary>
        public void Fill()
        {
            _apiMaterialProperties.GetMechanicalPropertiesOrthotropic(Name,
                out var modulusOfElasticities,
                out var poissonsRatios,
                out var thermalCoefficients,
                out var shearModuluses,
                Temperature);

            _orthotropicProperties = new MechanicalOrthotropicProperties
            {
                ModulusOfElasticity =
                {
                    E1 = modulusOfElasticities[0],
                    E2 = modulusOfElasticities[1],
                    E3 = modulusOfElasticities[2]
                },

                PoissonsRatio =
                {
                    U12 = poissonsRatios[0],
                    U13 = poissonsRatios[1],
                    U23 = poissonsRatios[2]
                },

                ThermalCoefficient =
                {
                    A1 = thermalCoefficients[0],
                    A2 = thermalCoefficients[1],
                    A3 = thermalCoefficients[2]
                },

                ShearModulus =
                {
                    G12 = shearModuluses[0],
                    G13 = shearModuluses[1],
                    G23 = shearModuluses[2]
                }
            };
        }

        /// <summary>
        /// Sets the material directional symmetry type, and assigns the corresponding mechanical properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void Set(MechanicalOrthotropicProperties properties)
        {
            _apiMaterialProperties.SetMechanicalPropertiesOrthotropic(Name,
                properties.ModulusOfElasticity.ToList().ToArray(),
                properties.PoissonsRatio.ToList().ToArray(),
                properties.ThermalCoefficient.ToList().ToArray(),
                properties.ShearModulus.ToList().ToArray(),
                Temperature);

            _orthotropicProperties = properties;
        }
        #endregion
    }
}
