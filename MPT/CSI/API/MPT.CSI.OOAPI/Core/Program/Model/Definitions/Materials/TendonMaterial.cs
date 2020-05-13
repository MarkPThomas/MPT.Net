// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="TendonMaterial.cs" company="">
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
    /// Represents material used for tendon sections/elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{TendonMaterialProperties}" />
    public class TendonMaterial : MaterialByTemperature<TendonMaterialProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>TendonMaterial.</returns>
        internal new static TendonMaterial Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            TendonMaterial material = new TendonMaterial(app, uniqueName, temperature);
            material.FillData();

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TendonMaterial" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <!-- Badly formed XML comment ignored for member "M:MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature`1.#ctor(MPT.CSI.API.Core.Program.CSiApplication,System.String,System.Double)" -->
        protected TendonMaterial(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        {
        }
        #endregion

        #region Fill/Set


        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        public override void FillProperties()
        {
            _apiMaterialProperties.GetTendon(Name,
                out double FyOut,
                out double FuOut,
                out eTendonStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double finalSlope,
                Temperature);

            _materialProperties = new TendonMaterialProperties
            {
                Fy = FyOut,
                Fu = FuOut,
                StressStrainCurveType = stressStrainCurveType,
                StressStrainHysteresisType = stressStrainHysteresisType,
                FinalSlope = finalSlope
            };
        }

        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        protected override void set(TendonMaterialProperties properties, double temperature)
        {
            _apiMaterialProperties.SetTendon(Name,
                properties.Fy,
                properties.Fu,
                properties.StressStrainCurveType,
                properties.StressStrainHysteresisType,
                properties.FinalSlope,
                temperature);
        }
        #endregion
    }
}
