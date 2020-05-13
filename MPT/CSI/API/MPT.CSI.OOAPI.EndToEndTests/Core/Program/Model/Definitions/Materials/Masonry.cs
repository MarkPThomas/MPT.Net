using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public class Masonry : Material
    {
        /// <summary>
        /// The modulus of elasticity.
        /// </summary>
        /// <value>The modulus of elasticity.</value>
        public double ModulusOfElasticity => Mechanics.ModulusOfElasticities[0];

        /// <summary>
        /// The thermal coefficient.
        /// </summary>
        /// <value>The thermal coefficient.</value>
        public double ThermalCoefficient => Mechanics.ThermalCoefficients[0];

        /// <summary>
        /// The Poisson's ratio.
        /// </summary>
        /// <value>The poissons ratio.</value>
        public double PoissonsRatio => Mechanics.PoissonsRatios[0];

        /// <summary>
        /// The shear modulus.
        /// </summary>
        /// <value>The shear modulus.</value>
        public double ShearModulus => Mechanics.ShearModuluses[0];

        // TODO: Getter by index for the above, for all of the above

        public double fc { get; protected set; }
        public bool IsLightweight { get; protected set; }
        public double ShearStrengthReductionFactor { get; protected set; }
        public eConcreteStressStrainCurveType StressStrainCurveType { get; protected set; }
        public eHysteresisType StressStrainHysteresisType { get; protected set; }
        public double StrainUnconfinedCompressive { get; protected set; }
        public double StrainUltimate { get; protected set; }
        public double FinalSlope { get; protected set; }
        public double FrictionAngle { get; protected set; }
        public double DilatationalAngle { get; protected set; }

        public new static Masonry Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (Masonry)Registry.Materials[uniqueName];

            Masonry material = new Masonry(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        public Masonry(string name) : base(name)
        {
        }

        public override void FillData()
        {
            FillMasonry();
        }

        public void FillMasonry()
        {
            _materialProperties.GetConcrete(Name,
                out double fc,
                out bool isLightweight,
                out double shearStrengthReductionFactor,
                out eConcreteStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainUnconfinedCompressive,
                out double strainUltimate,
                out double finalSlope,
                out double frictionAngle,
                out double dilatationalAngle);

            this.fc = fc;
            IsLightweight = isLightweight;
            ShearStrengthReductionFactor = shearStrengthReductionFactor;
            StressStrainCurveType = stressStrainCurveType;
            StressStrainHysteresisType = stressStrainHysteresisType;
            StrainUnconfinedCompressive = strainUnconfinedCompressive;
            StrainUltimate = strainUltimate;
            FinalSlope = finalSlope;
            FrictionAngle = frictionAngle;
            DilatationalAngle = dilatationalAngle;
        }
    }
}
