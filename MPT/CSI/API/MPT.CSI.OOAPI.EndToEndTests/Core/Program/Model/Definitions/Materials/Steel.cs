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
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Steel material.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.SteelMetal" />
    public class Steel : SteelMetal
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
        /// LoadType of the stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eSteelStressStrainCurveType StressStrainCurveType { get; set; }

        /// <summary>
        /// The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at maximum stress.</value>
        public double StrainAtMaxStress { get; set; }

        /// <summary>
        /// The strain at rupture.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at rupture.</value>
        public double StrainAtRupture { get; set; }




        /// <summary>
        /// Returns a new steel material class.
        /// </summary>
        /// <param name="uniqueName">Unique material name.</param>
        /// <returns>Steel.</returns>
        public new static Steel Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (Steel)Registry.Materials[uniqueName];

            Steel material = new Steel(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Steel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Steel(string name) : base(name)
        {
        }

        public override void FillData()
        {
            FillSteel();
        }

        /// <summary>
        /// Gets the steel material data.
        /// </summary>
        public void FillSteel()
        {
           _materialProperties.GetSteel(Name,
                out double Fy,
                out double Fu,
                out double expectedFy,
                out double expectedFu,
                out eSteelStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double strainAtHardening,
                out double strainAtMaxStress,
                out double strainAtRupture,
                out double finalSlope);

            this.Fy = Fy;
            this.Fu = Fu;
            this.Fye = expectedFy;
            this.Fue = expectedFu;
            this.StressStrainCurveType = stressStrainCurveType;
            this.StressStrainHysteresisType = stressStrainHysteresisType;
            this.StrainAtHardening = strainAtHardening;
            this.StrainAtMaxStress = strainAtMaxStress;
            this.StrainAtRupture = strainAtRupture;
            this.FinalSlope = finalSlope;
        }
    }
}
