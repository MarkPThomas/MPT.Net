// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="Steel.cs" company="">
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
    /// Steel material.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{SteelProperties}" />
    public class Steel : MaterialByTemperature<SteelProperties>
    {

        #region Initialization
        /// <summary>
        /// Returns a new steel material class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Unique material name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Steel.</returns>
        internal new static Steel Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            Steel material = new Steel(app, uniqueName, temperature);
            material.FillData();

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Steel" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <!-- Badly formed XML comment ignored for member "M:MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature`1.#ctor(MPT.CSI.API.Core.Program.CSiApplication,System.String,System.Double)" -->
        protected Steel(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        public override void FillProperties()
        {
            _apiMaterialProperties.GetSteel(Name,
                out double FyOut,
                out double FuOut,
                out double expectedFy,
                out double expectedFu,
                out eSteelStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainAtHardening,
                out double strainAtMaxStress,
                out double strainAtRupture,
                out double finalSlope,
                Temperature);

            _materialProperties = new SteelProperties()
            {
                Fy = FyOut,
                Fu = FuOut,
                Fye = expectedFy,
                Fue = expectedFu,
                StressStrainCurveType = stressStrainCurveType,
                StressStrainHysteresisType = stressStrainHysteresisType,
                StrainAtHardening = strainAtHardening,
                StrainAtMaxStress = strainAtMaxStress,
                StrainAtRupture = strainAtRupture,
                FinalSlope = finalSlope
            };
        }

        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        protected override void set(SteelProperties properties, double temperature)
        {
            _apiMaterialProperties.SetSteel(Name,
                properties.Fy,
                properties.Fu,
                properties.Fye,
                properties.Fue,
                properties.StressStrainCurveType,
                properties.StressStrainHysteresisType,
                properties.StrainAtHardening,
                properties.StrainAtMaxStress,
                properties.StrainAtRupture,
                properties.FinalSlope,
                temperature);
        }


        #endregion
    }
}
