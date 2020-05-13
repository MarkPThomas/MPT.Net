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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsAnisotropic.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialMechanics" />
    public class MaterialMechanicsAnisotropic : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// The anisotropic properties
        /// </summary>
        private MechanicalAnisotropicProperties _anisotropicProperties;
        /// <summary>
        /// Gets the anisotropic properties.
        /// </summary>
        /// <value>The anisotropic properties.</value>
        public MechanicalAnisotropicProperties AnisotropicProperties => _anisotropicProperties;
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsAnisotropic.</returns>
        internal static MaterialMechanicsAnisotropic Factory(
            ApiCSiApplication app, 
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsAnisotropic material = new MaterialMechanicsAnisotropic(app, uniqueName, temperature);
            material.Fill();
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsAnisotropic" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsAnisotropic(
            ApiCSiApplication app, 
            string name,
            double temperature = 0) : base(app, name, eMaterialSymmetryType.Anisotropic, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the mechanical properties for a material with the corresponding directional symmetry type.
        /// </summary>
        public void Fill()
        {
            _apiMaterialProperties.GetMechanicalPropertiesAnisotropic(Name,
                out var modulusOfElasticities,
                out var poissonsRatios,
                out var thermalCoefficients,
                out var shearModuluses,
                Temperature);

            _anisotropicProperties = new MechanicalAnisotropicProperties
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
                    U13 = poissonsRatios[1],  U23 = poissonsRatios[2],
                    U14 = poissonsRatios[3],  U24 = poissonsRatios[4],  U34 = poissonsRatios[5],
                    U15 = poissonsRatios[6],  U25 = poissonsRatios[7],  U35 = poissonsRatios[8],  U45 = poissonsRatios[9],
                    U16 = poissonsRatios[10], U26 = poissonsRatios[11], U36 = poissonsRatios[12], U46 = poissonsRatios[13], U56 = poissonsRatios[14]
                },
                
                ThermalCoefficient =
                {
                    A1 = thermalCoefficients[0],
                    A2 = thermalCoefficients[1],
                    A3 = thermalCoefficients[2],
                    A12 = thermalCoefficients[3],
                    A13 = thermalCoefficients[4],
                    A23 = thermalCoefficients[5]
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
        public void Set(MechanicalAnisotropicProperties properties)
        {
            _apiMaterialProperties.SetMechanicalPropertiesAnisotropic(Name,
                properties.ModulusOfElasticity.ToList().ToArray(),
                properties.PoissonsRatio.ToList().ToArray(),
                properties.ThermalCoefficient.ToList().ToArray(),
                properties.ShearModulus.ToList().ToArray(),
                Temperature);

            _anisotropicProperties = properties;
        }


        #endregion
    }
}
