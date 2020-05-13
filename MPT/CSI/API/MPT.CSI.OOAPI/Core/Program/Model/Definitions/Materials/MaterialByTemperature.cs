// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialByTemperature.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiMaterial = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.MaterialProperties;
using ProgramMaterialProperties = MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MaterialProperties;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{

    /// <summary>
    /// Class MaterialByTemperature.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MaterialByTemperature" />
    public abstract class MaterialByTemperature<T> : MaterialByTemperature where T : ProgramMaterialProperties, new()
    {

        /// <summary>
        /// The material associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public T Properties => (T)_materialProperties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialByTemperature{T}"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialByTemperature(ApiCSiApplication app,
            string name,
            double temperature = 0)
            : base(app, name, temperature)
        {
            _materialProperties = new T();
        }

        /// <summary>
        /// Modifies material property data for the material.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetProperties(T properties)
        {
            if (properties == null) return;
            set(properties, Temperature);
            _materialProperties = properties;
        }


        /// <summary>
        /// Sets the temperature.
        /// </summary>
        /// <param name="temperature">The temperature.</param>
        internal void SetTemperature(double temperature)
        {
            set(Properties, temperature);
            Temperature = temperature;
            Mechanics.Temperature = temperature;
        }

        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        protected abstract void set(T properties, double temperature);
    }

    /// <summary>
    /// Class MaterialByTemperature.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public abstract class MaterialByTemperature : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiMaterial _apiMaterialProperties => getApiMaterialProperties(_apiApp);

        /// <summary>
        /// The material properties
        /// </summary>
        protected ProgramMaterialProperties _materialProperties;

        /// <summary>
        /// The name of an existing material property.
        /// </summary>
        /// <value>The name.</value>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        public double Temperature { get; protected set; }

        /// <summary>
        /// The mechanics
        /// </summary>
        private MaterialMechanics _mechanics;
        /// <summary>
        /// Mechanical properties of the material.
        /// </summary>
        /// <value>The mechanics.</value>
        public MaterialMechanics Mechanics
        {
            get
            {
                if (_mechanics == null)
                {
                    FillMaterialProperties();
                }

                return _mechanics;
            }
        }

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
        /// Gets or sets the stress strain curve.
        /// </summary>
        /// <value>The stress strain curve.</value>
        public StressStrainCurve StressStrainCurve { get; private set; } = new StressStrainCurve();

        /// <summary>
        /// Gets the damping properties.
        /// </summary>
        /// <value>The damping properties.</value>
        public DampingProperties DampingProperties { get; private set; } = new DampingProperties();
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanics.</returns>
        internal static MaterialByTemperature Factory(ApiCSiApplication app, 
            string uniqueName,
            double temperature = 0)
        {
            Tuple<eMaterialPropertyType, eMaterialSymmetryType> materialTypes = Material.GetMaterialType(getApiMaterialProperties(app), uniqueName);

            switch (materialTypes.Item1)
            {
                case eMaterialPropertyType.Steel:
                    return Steel.Factory(app, uniqueName, temperature);

                case eMaterialPropertyType.Concrete:
                    return Concrete.Factory(app, uniqueName, temperature);

                case eMaterialPropertyType.Masonry:
                    return Masonry.Factory(app, uniqueName, temperature);

                case eMaterialPropertyType.Tendon:
                    return TendonMaterial.Factory(app, uniqueName, temperature);

                case eMaterialPropertyType.Rebar:
                    return Rebar.Factory(app, uniqueName, temperature);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                case eMaterialPropertyType.Aluminum:
                    return Aluminum.Factory(app, uniqueName, temperature);

                case eMaterialPropertyType.ColdFormed:
                    return ColdFormed.Factory(app, uniqueName, temperature);
#endif
                case eMaterialPropertyType.NoDesign:
                    return NoDesign.Factory(app, uniqueName, temperature);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanics" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialByTemperature(ApiCSiApplication app, 
            string name,
            double temperature = 0) : base(app)
        {
            Name = name;
            Temperature = temperature;
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public virtual void FillData()
        {
            FillWeightAndMass();
            FillDamping();
            FillStressStrainCurve();
            FillProperties();
        }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillProperties();

        /// <summary>
        /// Retrieves the weight per unit volume and mass per unit volume of the material.
        /// </summary>
        public void FillWeightAndMass()
        {
            _apiMaterialProperties.GetWeightAndMass(Name,
                out var weightPerVolume,
                out var massPerVolume,
                Temperature);

            WeightPerVolume = weightPerVolume;
            MassPerVolume = massPerVolume;
        }

        /// <summary>
        /// Assigns weight per unit volume or mass per unit volume to a material property.
        /// </summary>
        /// <param name="perUnitVolumeOption">The per unit volume option.
        /// If the weight is specified, the corresponding mass is program calculated based on the specified weight.
        /// Similarly, if the mass is specified, the corresponding weight is program calculated based on the specified mass.</param>
        /// <param name="value">This is either the weight per unit volume or the mass per unit volume, depending on the value of the <paramref name="perUnitVolumeOption" /> item.
        /// [F/L^3] for <paramref name="perUnitVolumeOption" /> = 1 (weight), and [M/L^3] for <paramref name="perUnitVolumeOption" /> = 2 (mass).</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetWeightAndMass(
            eMaterialPerVolumeOption perUnitVolumeOption,
            double value)
        {
            _apiMaterialProperties.SetWeightAndMass(Name, perUnitVolumeOption, value);
            FillWeightAndMass();
        }

        /// <summary>
        /// Fills the material properties.
        /// </summary>
        public void FillMaterialProperties()
        {
            _apiMaterialProperties.GetMaterialType(Name,
                out var materialType,
                out var symmetryType);
            
            _mechanics = MaterialMechanics.Factory(_apiApp, Name, symmetryType);
        }


        /// <summary>
        /// Retrieves the material stress-strain curve.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void FillStressStrainCurve()
        {
            FillStressStrainCurveSpecial();
        }

        /// <summary>
        /// Retrieves the material stress-strain curve.
        /// </summary>
        /// <param name="sectionName">This item applies only if the specified material is concrete with a Mander concrete type.
        /// This is the frame section property for which the Mander stress-strain curve is retrieved.
        /// The section must be round or rectangular.</param>
        /// <param name="rebarArea">This item applies only if the specified material is rebar, which does not have a user-defined stress-strain curve and is specified to use Caltrans default controlling strain values, which are bar size dependent.
        /// This is the area of the rebar for which the stress-strain curve is retrieved.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        internal void FillStressStrainCurveSpecial(
            string sectionName = "",
            double rebarArea = 0)
        {
            _apiMaterialProperties.GetStressStrainCurve(Name,
                out var pointIds,
                out var strains,
                out var stresses,
                sectionName,
                rebarArea,
                Temperature);

            StressStrainCurve = new StressStrainCurve();
            for (int i = 0; i < pointIds.Length; i++)
            {
                StressStrainPoint point = new StressStrainPoint(stresses[i], strains[i], pointIds[i]);
                StressStrainCurve.Add(point);
            }
        }

        /// <summary>
        /// Sets the material stress-strain curve for materials that are specified to have user-defined stress-strain curves.
        /// </summary>
        /// <param name="stressStrainCurve">Stress-strain curve to add.</param>
        public void SetStressStrainCurve(StressStrainCurve stressStrainCurve)
        {
            if (stressStrainCurve == null) return;
            _apiMaterialProperties.SetStressStrainCurve(Name,
                stressStrainCurve.Select(p => p.PointID).ToArray(),
                stressStrainCurve.Select(p => p.Strain).ToArray(),
                stressStrainCurve.Select(p => p.Stress).ToArray(),
                Temperature);

            StressStrainCurve = stressStrainCurve;
        }


        /// <summary>
        /// Retrieves the additional material damping data for the material.
        /// </summary>
        public void FillDamping()
        {
            _apiMaterialProperties.GetDamping(Name,
                out var modalDampingRatio,
                out var viscousMassCoefficient,
                out var viscousStiffnessCoefficient,
                out var hystereticMassCoefficient,
                out var hystereticStiffnessCoefficient,
                Temperature);

            DampingProperties = new DampingProperties
            {
                ModalDampingRatio = modalDampingRatio,
                ViscousMassCoefficient = viscousMassCoefficient,
                ViscousStiffnessCoefficient = viscousStiffnessCoefficient,
                HystereticMassCoefficient = hystereticMassCoefficient,
                HystereticStiffnessCoefficient = hystereticStiffnessCoefficient
            };
        }

        /// <summary>
        /// Sets the additional material damping data for the material.
        /// </summary>
        /// <param name="dampingProperties">The damping properties.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDamping(DampingProperties dampingProperties)
        {
            _apiMaterialProperties.SetDamping(Name,
                dampingProperties.ModalDampingRatio,
                dampingProperties.ViscousMassCoefficient,
                dampingProperties.ViscousStiffnessCoefficient,
                dampingProperties.HystereticMassCoefficient,
                dampingProperties.HystereticStiffnessCoefficient,
                Temperature);
            DampingProperties = dampingProperties;
        }


        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        internal void SetName(string name)
        {
            Name = name;
            Mechanics.Name = name;
        }
        #endregion
    }
}
