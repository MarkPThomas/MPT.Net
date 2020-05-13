// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Material.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Materials
{
    /// <summary>
    /// Class Material.
    /// </summary>
    /// <seealso cref="DefinitionElement" />
    public class Material : DefinitionElement
    {
        #region Fields & Properties
        /// <summary>
        /// The unique name.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique.</value>
        public override string Name
        {
            get => _name;
            internal set
            {
                _name = value;
                SetName(value);
            }
        }


        /// <summary>
        /// Property type of the material.
        /// </summary>
        /// <value>The type.</value>
        public eMaterialPropertyType Type { get; internal set; }
        
        /// <summary>
        /// Symmetry type of the material.
        /// </summary>
        /// <value>The type.</value>
        public eMaterialSymmetryType SymmetryType { get; internal set; }

        // TODO: Consider a better grouping object for PropertiesByTemperature
        /// <summary>
        /// The different temperatures at which properties are specified for the material.
        /// </summary>
        /// <value>The temperatures.</value>
        private readonly List<double> _temperatures = new List<double>();
        /// <summary>
        /// The different temperatures at which properties are specified for the material.
        /// </summary>
        /// <value>The temperatures.</value>
        public ReadOnlyCollection<double> Temperatures => new ReadOnlyCollection<double>(_temperatures);
        /// <summary>
        /// The properties by temperature
        /// </summary>
        private readonly List<MaterialByTemperature> _propertiesByTemperature = new List<MaterialByTemperature>();
        public ReadOnlyCollection<MaterialByTemperature> PropertiesByTemperature => new ReadOnlyCollection<MaterialByTemperature>(_propertiesByTemperature);
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new material class.
        /// </summary>
        /// <param name="name">Unique material name.</param>
        /// <returns>Steel.</returns>
        internal static Material Factory(string name)
        {
            Material material = new Material(name);
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Material(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>MaterialByTemperature.</returns>
        public MaterialByTemperature GetProperties(int index = 0)
        {
            return index > _propertiesByTemperature.Count ? null : _propertiesByTemperature[index];
        }

        internal void SetMaterialByTemperature(MaterialByTemperature material)
        {
            if (_temperatures.Contains(material.Temperature)) return;
            _propertiesByTemperature.Add(material);
            _temperatures.Add(material.Temperature);
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        internal void SetName(string name)
        {
            Name = name;
            foreach (var propertyByTemperature in _propertiesByTemperature)
            {
                propertyByTemperature.SetName(name);
            }
        }
        #endregion
    }
}
