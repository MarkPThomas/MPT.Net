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
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class NoDesign.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{ConcreteAngleProperties}" />
    public class NoDesign : MaterialByTemperature<ConcreteAngleProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Concrete.</returns>
        internal new static NoDesign Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            NoDesign material = new NoDesign(app, uniqueName, temperature);
            material.FillData();

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Concrete" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <!-- Badly formed XML comment ignored for member "M:MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature`1.#ctor(MPT.CSI.API.Core.Program.CSiApplication,System.String,System.Double)" -->
        protected NoDesign(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the other material property data for no design type materials.
        /// The function returns an error if the specified material is not concrete.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public override void FillProperties()
        {
            _apiMaterialProperties.GetNoDesign(Name,
                out var frictionAngle,
                out var dilatationalAngle,
                Temperature);

            _materialProperties = new ConcreteAngleProperties
            {
                FrictionAngle = frictionAngle,
                DilatationalAngle = dilatationalAngle
            };
        }

        /// <summary>
        /// Sets the other material property data for no design type materials.
        /// </summary>
        /// <param name="angles">The angles.</param>
        /// <param name="temperature">The temperature.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        protected override void set(ConcreteAngleProperties angles, double temperature)
        {
            _apiMaterialProperties.SetNoDesign(Name,
                angles.FrictionAngle,
                angles.DilatationalAngle,
                temperature);
        }
        #endregion
    }
}
