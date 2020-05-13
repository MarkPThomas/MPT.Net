// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Concrete.cs" company="">
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
    /// Class Concrete.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{ConcreteProperties}" />
    public class Concrete : MaterialByTemperature<ConcreteProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Concrete.</returns>
        internal new static Concrete Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            Concrete material = new Concrete(app, uniqueName, temperature);
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
        protected Concrete(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        {
        }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        public override void FillProperties()
        {
            _apiMaterialProperties.GetConcrete(Name,
                out double fc,
                out bool isLightweight,
                out double shearStrengthReductionFactor,
                out eConcreteStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainUnconfinedCompressive,
                out double strainUltimate,
                out double finalSlope,
                out double frictionAngle,
                out double dilatationalAngle,
                Temperature);

            _materialProperties = new ConcreteProperties()
            {
                fc = fc,
                IsLightweight = isLightweight,
                ShearStrengthReductionFactor = shearStrengthReductionFactor,
                StressStrainCurveType = stressStrainCurveType,
                StressStrainHysteresisType = stressStrainHysteresisType,
                StrainUnconfinedCompressive = strainUnconfinedCompressive,
                StrainUltimate = strainUltimate,
                FinalSlope = finalSlope,
                Angles =
                {
                    FrictionAngle = frictionAngle,
                    DilatationalAngle = dilatationalAngle
                }
            };
        }


        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        protected override void set(ConcreteProperties properties, double temperature)
        {
            _apiMaterialProperties.SetConcrete(Name,
                properties.fc,
                properties.IsLightweight,
                properties.ShearStrengthReductionFactor,
                properties.StressStrainCurveType,
                properties.StressStrainHysteresisType,
                properties.StrainUnconfinedCompressive,
                properties.StrainUltimate,
                properties.FinalSlope,
                properties.Angles.FrictionAngle,
                properties.Angles.DilatationalAngle,
                temperature);
        }

        /// <summary>
        /// Fills the stress strain curve special.
        /// </summary>
        /// <param name="sectionName">This item applies only if the specified material is concrete with a Mander concrete type.
        /// This is the frame section property for which the Mander stress-strain curve is retrieved.
        /// The section must be round or rectangular.</param>
        public void FillStressStrainCurveSpecial(string sectionName)
        {
            if (Properties.StressStrainCurveType != eConcreteStressStrainCurveType.ParametricMander) return;
            FillStressStrainCurveSpecial(sectionName: sectionName);
        }
        #endregion
    }
}
