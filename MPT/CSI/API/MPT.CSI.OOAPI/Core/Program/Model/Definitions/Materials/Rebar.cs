// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Rebar.cs" company="">
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
    /// Class Rebar.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{RebarProperties}" />
    public class Rebar : MaterialByTemperature<RebarProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Rebar.</returns>
        internal new static Rebar Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            Rebar material = new Rebar(app, uniqueName, temperature);
            material.FillData();

            return material;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rebar" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        /// <!-- Badly formed XML comment ignored for member "M:MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature`1.#ctor(MPT.CSI.API.Core.Program.CSiApplication,System.String,System.Double)" -->
        protected Rebar(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        { }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        public override void FillProperties()
        {

            _apiMaterialProperties.GetRebar(Name,
                out double FyOut,
                out double FuOut,
                out double expectedFy,
                out double expectedFu,
                out eRebarStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainAtHardening,
                out double strainUltimate,
                out double finalSlope,
                out bool useCaltransStressStrainDefaults,
                Temperature);

            _materialProperties = new RebarProperties
            {
                Fy = FyOut,
                Fu = FuOut,
                Fye = expectedFy,
                Fue = expectedFu,
                StressStrainCurveType = stressStrainCurveType,
                StressStrainHysteresisType = stressStrainHysteresisType,
                StrainAtHardening = strainAtHardening,
                StrainUltimate = strainUltimate,
                FinalSlope = finalSlope,
                UseCaltransStressStrainDefaults = useCaltransStressStrainDefaults
            };
        }

        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        protected override void set(RebarProperties properties, double temperature)
        {
            _apiMaterialProperties.SetRebar(Name,
                properties.Fy,
                properties.Fu,
                properties.Fye,
                properties.Fue,
                properties.StressStrainCurveType,
                properties.StressStrainHysteresisType,
                properties.StrainAtHardening,
                properties.StrainUltimate,
                properties.FinalSlope,
                properties.UseCaltransStressStrainDefaults,
                temperature);
        }


        /// <summary>
        /// Fills the stress strain curve special.
        /// </summary>
        /// <param name="rebarArea">This item applies only if the specified material is rebar, which does not have a user-defined stress-strain curve and is specified to use Caltrans default controlling strain values, which are bar size dependent.
        /// This is the area of the rebar for which the stress-strain curve is retrieved.</param>
        public void FillStressStrainCurveSpecial(double rebarArea)
        {
            if (!Properties.UseCaltransStressStrainDefaults) return;
            FillStressStrainCurveSpecial(rebarArea: rebarArea);
        }
        #endregion
    }
}
