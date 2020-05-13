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
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Represents material used for tendon sections/elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.Metal" />
    public class TendonMaterial : Metal
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
        /// LoadType of stress-strain curve.
        /// </summary>
        /// <value>The type of the stress strain curve.</value>
        public eTendonStressStrainCurveType StressStrainCurveType { get; protected set; }

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>TendonMaterial.</returns>
        public new static TendonMaterial Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (TendonMaterial)Registry.Materials[uniqueName];

            TendonMaterial material = new TendonMaterial(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }
       
        /// <summary>
        /// Initializes a new instance of the <see cref="TendonMaterial"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TendonMaterial(string name) : base(name)
        {
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
           FillTendon();
        }

        /// <summary>
        /// Gets the tendon.
        /// </summary>
        public void FillTendon()
        {
            _materialProperties.GetTendon(Name,
                out double Fy,
                out double Fu,
                out eTendonStressStrainCurveType stressStrainCurveType,
                out eHysteresisType stressStrainHysteresisType,
                out double finalSlope);

            this.Fy = Fy;
            this.Fu = Fu;
            StressStrainCurveType = stressStrainCurveType;
            StressStrainHysteresisType = stressStrainHysteresisType;
            FinalSlope = finalSlope;
        }

    }
}
