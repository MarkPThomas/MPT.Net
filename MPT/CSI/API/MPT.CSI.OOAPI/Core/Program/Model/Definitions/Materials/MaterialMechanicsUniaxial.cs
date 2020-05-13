// ***********************************************************************
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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class MaterialMechanicsUniaxial.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialMechanics" />
    public class MaterialMechanicsUniaxial : MaterialMechanics
    {
        #region Fields & Properties

        /// <summary>
        /// The uniaxial properties
        /// </summary>
        private MechanicalUniaxialProperties _uniaxialProperties;
        /// <summary>
        /// Gets the uniaxial properties.
        /// </summary>
        /// <value>The uniaxial properties.</value>
        public MechanicalUniaxialProperties UniaxialProperties => _uniaxialProperties;

        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanicsUniaxial.</returns>
        internal static MaterialMechanicsUniaxial Factory(
            ApiCSiApplication app,
            string uniqueName,
            double temperature = 0)
        {
            MaterialMechanicsUniaxial material = new MaterialMechanicsUniaxial(app, uniqueName, temperature);
            material.Fill();
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanicsUniaxial" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanicsUniaxial(
            ApiCSiApplication app,
            string name,
            double temperature = 0) : base(app, name, eMaterialSymmetryType.Uniaxial, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the mechanical properties for a material with the corresponding directional symmetry type.
        /// </summary>
        public void Fill()
        {
            _apiMaterialProperties.GetMechanicalPropertiesUniaxial(Name,
                out var modulusOfElasticity,
                out var thermalCoefficient,
                Temperature);

            _uniaxialProperties = new MechanicalUniaxialProperties()
            {
                ModulusOfElasticity = modulusOfElasticity,
                ThermalCoefficient = thermalCoefficient
            };
        }

        /// <summary>
        /// Sets the material directional symmetry type, and assigns the corresponding mechanical properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void Set(MechanicalUniaxialProperties properties)
        {
            _apiMaterialProperties.SetMechanicalPropertiesUniaxial(Name,
                properties.ModulusOfElasticity,
                properties.ThermalCoefficient,
                Temperature);

            _uniaxialProperties = properties;
        }


        #endregion

    }
}
