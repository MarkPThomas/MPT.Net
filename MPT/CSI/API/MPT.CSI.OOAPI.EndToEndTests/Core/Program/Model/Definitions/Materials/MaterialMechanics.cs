// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-20-2018
// ***********************************************************************
// <copyright file="MaterialMechanics.cs" company="">
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
    /// Represents the mechanical properties of materials.
    /// </summary>
    public class MaterialMechanics
    {
        protected static MaterialProperties _materialProperties => Registry.ProgramDefinitions?.Properties?.MaterialProperties;

        /// <summary>
        /// The name of an existing material property.
        /// </summary>
        protected string _name;

        /// <summary>
        /// Symmetry type of the material.
        /// </summary>
        /// <value>The type.</value>
        public eMaterialSymmetryType SymmetryType { get; protected set; }

        /// <summary>
        /// The modulus of elasticities.
        /// </summary>
        /// <value>The modulus of elasticities.</value>
        public double[] ModulusOfElasticities { get; set; }

        /// <summary>
        /// The poisson's ratios.
        /// </summary>
        /// <value>The poissons ratios.</value>
        public double[] PoissonsRatios { get; set; }

        /// <summary>
        /// The thermal coefficients.
        /// </summary>
        /// <value>The thermal coefficients.</value>
        public double[] ThermalCoefficients { get; set; }

        /// <summary>
        /// The shear moduluses.
        /// </summary>
        /// <value>The shear moduluses.</value>
        public double[] ShearModuluses { get; set; }


        /// <summary>
        /// The weight per unit volume for the material. [F/L^3].
        /// </summary>
        /// <value>The weight per volume.</value>
        public double WeightPerVolume { get; protected set; }

        /// <summary>
        /// The mass per unit volume for the material. [M/L^3].
        /// </summary>
        /// <value>The mass per volume.</value>
        public double MassPerVolume { get; protected set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        public double Temperature { get; set; }

        // TODO: Create an object for each temperature, maybe accessible by index or temperature w/ default of 0.
        /// <summary>
        /// The different temperatures at which properties are specified for the material.
        /// </summary>
        /// <value>The temperatures.</value>
        public double[] Temperatures { get; set; }


        // Stress-Strain Curve
        /// <summary>
        /// The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.
        /// </summary>
        /// <value>The point i ds.</value>
        public eStressStrainPointID[] PointIDs { get; set; }

        /// <summary>
        /// The strain at each point on the stress strain curve.
        /// </summary>
        /// <value>The strains.</value>
        public double[] Strains { get; set; }

        /// <summary>
        /// The stress at each point on the stress strain curve. [F/L^2].
        /// </summary>
        /// <value>The stresses.</value>
        public double[] Stresses { get; set; }

        // Damping
        /// <summary>
        /// The modal damping ratio.
        /// </summary>
        /// <value>The modal damping ratio.</value>
        public double ModalDampingRatio { get; set; }

        /// <summary>
        /// The mass coefficient for viscous proportional damping.
        /// </summary>
        /// <value>The viscous mass coefficient.</value>
        public double ViscousMassCoefficient { get; set; }

        /// <summary>
        /// The stiffness coefficient for viscous proportional damping.
        /// </summary>
        /// <value>The viscous stiffness coefficient.</value>
        public double ViscousStiffnessCoefficient { get; set; }

        /// <summary>
        /// The mass coefficient for hysteretic proportional damping.
        /// </summary>
        /// <value>The hysteretic mass coefficient.</value>
        public double HystereticMassCoefficient { get; set; }

        /// <summary>
        /// The stiffness coefficient for hysteretic proportional damping.
        /// </summary>
        /// <value>The hysteretic stiffness coefficient.</value>
        public double HystereticStiffnessCoefficient { get; set; }

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="symmetryType">LoadType of the symmetry.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="rebarArea">The rebar area.</param>
        /// <param name="temperatures">The temperatures.</param>
        /// <returns>MaterialMechanics.</returns>
        public static MaterialMechanics Factory(string uniqueName, 
            eMaterialSymmetryType symmetryType,
            string sectionName = "",
            double rebarArea = 0,
            double[] temperatures = null)
        {
            //if (Registry.Materials.Keys.Contains(uniqueName)) return (MaterialMechanics)Registry.Materials[uniqueName];

            MaterialMechanics materialMechanics = new MaterialMechanics(uniqueName, symmetryType);
            if (_materialProperties != null)
            {
                materialMechanics.FillTemperature();
                materialMechanics.FillDamping();
                materialMechanics.FillWeightAndMass();
                materialMechanics.FillStressStrainCurve(sectionName, rebarArea);
                materialMechanics.FillMechanicalProperties();
            }
            //Registry.Materials.Add(uniqueName, materialMechanics);
            return materialMechanics;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanics"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="symmetryType">LoadType of the symmetry.</param>
        public MaterialMechanics(string name, eMaterialSymmetryType symmetryType)
        {
            _name = name;
            SymmetryType = symmetryType;
        }

        /// <summary>
        /// Retrieves the temperatures at which properties are specified for a material.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillTemperature()
        {
            _materialProperties.GetTemperature(_name, out var temperatures);
            Temperatures = temperatures;
        }

        /// <summary>
        /// Retrieves the additional material damping data for the material.
        /// </summary>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillDamping(double temperature = 0)
        {
            if (Temperatures.Length == 0 || !Temperatures.Contains(temperature)) temperature = 0;

            _materialProperties.GetDamping(_name,
                out var modalDampingRatio,
                out var viscousMassCoefficient,
                out var viscousStiffnessCoefficient,
                out var hystereticMassCoefficient,
                out var hystereticStiffnessCoefficient,
                temperature);

            ModalDampingRatio = modalDampingRatio;
            ViscousMassCoefficient = viscousMassCoefficient;
            ViscousStiffnessCoefficient = viscousStiffnessCoefficient;
            HystereticMassCoefficient = hystereticMassCoefficient;
            HystereticStiffnessCoefficient = hystereticStiffnessCoefficient;
        }

        /// <summary>
        /// Retrieves the weight per unit volume and mass per unit volume of the material.
        /// </summary>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillWeightAndMass(double temperature = 0)
        {
            if (Temperatures.Length == 0 || !Temperatures.Contains(temperature)) temperature = 0;

            _materialProperties.GetWeightAndMass(_name,
                out var weightPerVolume,
                out var massPerVolume,
                temperature);

            WeightPerVolume = weightPerVolume;
            MassPerVolume = massPerVolume;
        }

        /// <summary>
        /// Retrieves the material stress-strain curve.
        /// </summary>
        /// <param name="sectionName">This item applies only if the specified material is concrete with a Mander concrete type.
        /// This is the frame section property for which the Mander stress-strain curve is retrieved.
        /// The section must be round or rectangular.</param>
        /// <param name="rebarArea">This item applies only if the specified material is rebar, which does not have a user-defined stress-strain curve and is specified to use Caltrans default controlling strain values, which are bar size dependent.
        /// This is the area of the rebar for which the stress-strain curve is retrieved.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillStressStrainCurve(string sectionName = "",
            double rebarArea = 0,
            double temperature = 0)
        {
            if (Temperatures.Length == 0 || !Temperatures.Contains(temperature)) temperature = 0;

            _materialProperties.GetStressStrainCurve(_name,
                out var pointIds,
                out var strains,
                out var stresses,
                sectionName,
                rebarArea,
                temperature);

            PointIDs = pointIds;
            Strains = strains;
            Stresses = stresses;
        }

        /// <summary>
        /// Retrieves the mechanical properties for a material with an specific directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not matching in the program.
        /// </summary>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillMechanicalProperties(double temperature = 0)
        {
            if (Temperatures.Length == 0 || !Temperatures.Contains(temperature)) temperature = 0;

            double modulusOfElasticity = -1;
            double thermalCoefficient = -1;
            double poissonsRatio = -1;
            double shearModulus = -1;

            double[] modulusOfElasticities = new double[0];
            double[] poissonsRatios = new double[0];
            double[] thermalCoefficients = new double[0];
            double[] shearModuluses = new double[0];

            switch (SymmetryType)
            {
                case eMaterialSymmetryType.Anisotropic:
                    _materialProperties.GetMechanicalPropertiesAnisotropic(_name,
                        out modulusOfElasticities,
                        out poissonsRatios,
                        out thermalCoefficients,
                        out shearModuluses,
                        temperature);
                    break;
                case eMaterialSymmetryType.Isotropic:
                    _materialProperties.GetMechanicalPropertiesIsotropic(_name,
                        out modulusOfElasticity,
                        out poissonsRatio,
                        out thermalCoefficient,
                        out shearModulus,
                        temperature);
                    break;
                case eMaterialSymmetryType.Orthotropic:
                    _materialProperties.GetMechanicalPropertiesOrthotropic(_name,
                        out modulusOfElasticities,
                        out poissonsRatios,
                        out thermalCoefficients,
                        out shearModuluses,
                        temperature);
                    break;
                case eMaterialSymmetryType.Uniaxial:
                    _materialProperties.GetMechanicalPropertiesUniaxial(_name,
                        out modulusOfElasticity,
                        out thermalCoefficient,
                        temperature);
                    break;
            }

            if (modulusOfElasticities.Length > 0)
            {
                ModulusOfElasticities = modulusOfElasticities;
                PoissonsRatios = poissonsRatios;
                ThermalCoefficients = thermalCoefficients;
                ShearModuluses = shearModuluses;
            }
            else
            {
                if (modulusOfElasticity > -1)
                {
                    ModulusOfElasticities = new double[1];
                    ModulusOfElasticities[0] = modulusOfElasticity;
                    ThermalCoefficients = new double[1];
                    ThermalCoefficients[0] = thermalCoefficient;
                }

                if (!(poissonsRatio > -1)) return;
                PoissonsRatios = new double[1];
                PoissonsRatios[0] = poissonsRatio;
                ShearModuluses = new double[1];
                ShearModuluses[0] = shearModulus;
            }
        }
    }
}
