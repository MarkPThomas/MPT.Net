using System.Linq;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public class Concrete : Material
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

        /// <summary>
        /// The concrete compressive strength. [F/L^2].
        /// </summary>
        /// <value>The fc.</value>
        public double fc { get; protected set; }

        /// <summary>
        /// True: The concrete is assumed to be lightweight concrete.
        /// </summary>
        /// <value><c>true</c> if this instance is lightweight; otherwise, <c>false</c>.</value>
        public bool IsLightweight { get; protected set; }

        /// <summary>
        /// The shear strength reduction factor for lightweight concrete.
        /// </summary>
        /// <value>The shear strength reduction factor.</value>
        public double ShearStrengthReductionFactor { get; protected set; }

        /// <summary>
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eConcreteStressStrainCurveType StressStrainCurveType { get; protected set; }

        /// <summary>
        /// The stress-strain hysteresis type.
        /// </summary>
        /// <value>The type of the stress strain hysteresis.</value>
        public eHysteresisType StressStrainHysteresisType { get; protected set; }

        /// <summary>
        /// The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain unconfined compressive.</value>
        public double StrainUnconfinedCompressive { get; protected set; }

        /// <summary>
        /// The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain ultimate.</value>
        public double StrainUltimate { get; protected set; }

        /// <summary>
        /// This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.
        /// </summary>
        /// <value>The final slope.</value>
        public double FinalSlope { get; protected set; }

        /// <summary>
        /// The Drucker-Prager friction angle, 0 &lt;= <ref name="FrictionAngle" /> &lt; 90. [deg].
        /// </summary>
        /// <value>The friction angle.</value>
        public double FrictionAngle { get; protected set; }

        /// <summary>
        /// The Drucker-Prager dilatational angle, 0 &lt;= <ref name="DilatationalAngle" /> &lt; 90. [deg].
        /// </summary>
        /// <value>The dilatational angle.</value>
        public double DilatationalAngle { get; protected set; }



        public new static Concrete Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (Concrete)Registry.Materials[uniqueName];

            Concrete material = new Concrete(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        public Concrete(string name) : base(name)
        {
        }

        public override void FillData()
        {
            FillConcrete();
        }

        public void FillConcrete()
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
