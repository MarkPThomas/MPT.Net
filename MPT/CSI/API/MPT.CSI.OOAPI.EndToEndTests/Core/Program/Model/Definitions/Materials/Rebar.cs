using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public class Rebar : SteelMetal
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
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eRebarStressStrainCurveType StressStrainCurveType { get; protected set; }

        /// <summary>
        /// The ultimate strain capacity.
        /// This item must be larger than the <paramref name="StrainAtHardening" /> property.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="UseCaltransStressStrainDefaults" /> is False.
        /// </summary>
        /// <value>The strain at hardening.</value>
        public double StrainUltimate { get; protected set; }

        /// <summary>
        /// True: Program uses Caltrans default controlling strain values, which are bar size dependent.
        /// </summary>
        /// <value><c>true</c> if [use caltrans stress strain defaults]; otherwise, <c>false</c>.</value>
        public bool UseCaltransStressStrainDefaults { get; protected set; }

        public new static Rebar Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (Rebar)Registry.Materials[uniqueName];

            Rebar material = new Rebar(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        public Rebar(string name) : base(name)
        { }

        public override void FillData()
        {
            FillRebar();
        }

        public void FillRebar()
        {
            _materialProperties.GetRebar(Name,
                out double Fy,
                out double Fu,
                out double expectedFy,
                out double expectedFu,
                out eRebarStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainAtHardening,
                out double strainUltimate,
                out double finalSlope,
                out bool useCaltransStressStrainDefaults);


            this.Fy = Fy;
            this.Fu = Fu;
            Fye = expectedFy;
            Fue = expectedFu;
            StressStrainCurveType = stressStrainCurveType;
            StressStrainHysteresisType = stressStrainHysteresisType;
            StrainAtHardening = strainAtHardening;
            StrainUltimate = strainUltimate;
            FinalSlope = finalSlope;
            UseCaltransStressStrainDefaults = useCaltransStressStrainDefaults;
        }

        
    }
}
