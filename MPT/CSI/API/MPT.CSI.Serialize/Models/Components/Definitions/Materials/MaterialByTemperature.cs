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
using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;
using ProgramMaterialProperties = MPT.CSI.Serialize.Models.Helpers.Definitions.Materials.MaterialProperties;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
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
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialByTemperature(
            string name,
            double temperature = 0)
            : base(name, temperature)
        {
            _materialProperties = new T();
        }

        /// <summary>
        /// Modifies material property data for the material.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void SetProperties(T properties)
        {
            if (properties == null) return;
            _materialProperties = properties;
        }


        /// <summary>
        /// Sets the temperature.
        /// </summary>
        /// <param name="temperature">The temperature.</param>
        internal void SetTemperature(double temperature)
        {
            Temperature = temperature;
            Mechanics.Temperature = temperature;
        }
    }

    /// <summary>
    /// Class MaterialByTemperature.
    /// </summary>
    /// <seealso cref="MaterialByTemperature" />
    public abstract class MaterialByTemperature 
    {
        #region Fields & Properties
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
        public double Temperature { get; internal set; }

        /// <summary>
        /// Mechanical properties of the material.
        /// </summary>
        /// <value>The mechanics.</value>
        public virtual MaterialMechanics Mechanics { get; internal set; } 

        /// <summary>
        /// The weight per unit volume for the material. [F/L^3].
        /// </summary>
        /// <value>The weight per volume.</value>
        public double WeightPerVolume { get; internal set; }

        /// <summary>
        /// The mass per unit volume for the material. [M/L^3].
        /// </summary>
        /// <value>The mass per volume.</value>
        public double MassPerVolume { get; internal set; }

        /// <summary>
        /// Gets or sets the stress strain curve.
        /// </summary>
        /// <value>The stress strain curve.</value>
        public StressStrainCurve StressStrainCurve { get; } = new StressStrainCurve();

        /// <summary>
        /// Gets the damping properties.
        /// </summary>
        /// <value>The damping properties.</value>
        public DampingProperties DampingProperties { get; } = new DampingProperties();
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>MaterialMechanics.</returns>
        internal static MaterialByTemperature Factory(
            string uniqueName,
            eMaterialPropertyType materialPropertyType,
            double temperature = 0)
        {
            switch (materialPropertyType)
            {
                case eMaterialPropertyType.Steel:
                    return Steel.Factory(uniqueName, temperature);

                case eMaterialPropertyType.Concrete:
                    return Concrete.Factory(uniqueName, temperature);

                case eMaterialPropertyType.Masonry:
                    return Masonry.Factory(uniqueName, temperature);

                case eMaterialPropertyType.Tendon:
                    return TendonMaterial.Factory(uniqueName, temperature);

                case eMaterialPropertyType.Rebar:
                    return Rebar.Factory(uniqueName, temperature);
                    
                case eMaterialPropertyType.Aluminum:
                    return Aluminum.Factory(uniqueName, temperature);

                case eMaterialPropertyType.ColdFormed:
                    return ColdFormed.Factory(uniqueName, temperature);

                case eMaterialPropertyType.NoDesign:
                    return NoDesign.Factory(uniqueName, temperature);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialMechanics" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected MaterialByTemperature(
            string name,
            double temperature = 0)
        {
            Name = name;
            Temperature = temperature;
        }
        
        #endregion
        
        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        internal void SetName(string name)
        {
            Name = name;
            Mechanics.Name = name;
        }
    }
}
