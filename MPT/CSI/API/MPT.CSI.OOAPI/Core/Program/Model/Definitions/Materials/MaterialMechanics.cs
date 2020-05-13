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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiMaterial = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.MaterialProperties;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Represents the mechanical properties of materials.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class MaterialMechanics : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiMaterial _apiMaterialProperties => getApiMaterialProperties(_apiApp);

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
        public eMaterialSymmetryType SymmetryType { get; protected set; }

        ///// <summary>
        ///// The different temperatures at which properties are specified for the material.
        ///// </summary>
        ///// <value>The temperatures.</value>
        //private List<double> _temperatures = new List<double>();
        ///// <summary>
        ///// The different temperatures at which properties are specified for the material.
        ///// </summary>
        ///// <value>The temperatures.</value>
        //public ReadOnlyCollection<double> Temperatures => new ReadOnlyCollection<double>(_temperatures);

        ///// <summary>
        ///// The properties by temperature
        ///// </summary>
        //private readonly List<MaterialByTemperature> _propertiesByTemperature = new List<MaterialByTemperature>();
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="symmetryType">LoadType of the symmetry.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanics.</returns>
        internal static MaterialMechanics Factory(ApiCSiApplication app, 
            string uniqueName,
            eMaterialSymmetryType symmetryType,
            double temperature = 0)
        {
            switch (symmetryType)
            {
                case eMaterialSymmetryType.Anisotropic:
                    return MaterialMechanicsAnisotropic.Factory(app, uniqueName, temperature);

                case eMaterialSymmetryType.Isotropic:
                    return MaterialMechanicsIsotropic.Factory(app, uniqueName, temperature);

                case eMaterialSymmetryType.Orthotropic:
                    return MaterialMechanicsOrthotropic.Factory(app, uniqueName, temperature);

                case eMaterialSymmetryType.Uniaxial:
                    return MaterialMechanicsUniaxial.Factory(app, uniqueName, temperature);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanics" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="symmetryType">Type of the symmetry.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialMechanics(
            ApiCSiApplication app, 
            string name, 
            eMaterialSymmetryType symmetryType,
            double temperature) : base(app)
        {
            Name = name;
            SymmetryType = symmetryType;
            Temperature = temperature;
        }
        #endregion
    }
}
