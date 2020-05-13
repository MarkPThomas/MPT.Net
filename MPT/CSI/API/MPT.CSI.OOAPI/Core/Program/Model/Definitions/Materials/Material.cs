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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiMaterial = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.MaterialProperties;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class Material.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.DefinitionElement" />
    /// <seealso cref="DefinitionElement" />
    public class Material : DefinitionElement
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiMaterial _apiMaterialProperties => getApiMaterialProperties(_apiApp);

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
        public eMaterialPropertyType Type { get; protected set; }

        /// <summary>
        /// The different temperatures at which properties are specified for the material.
        /// </summary>
        /// <value>The temperatures.</value>
        private List<double> _temperatures = new List<double>();
        /// <summary>
        /// The different temperatures at which properties are specified for the material.
        /// </summary>
        /// <value>The temperatures.</value>
        public ReadOnlyCollection<double> Temperatures => new ReadOnlyCollection<double>(_temperatures);

        /// <summary>
        /// The properties by temperature
        /// </summary>
        private readonly List<MaterialByTemperature> _propertiesByTemperature = new List<MaterialByTemperature>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new material class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">Unique material name.</param>
        /// <returns>Steel.</returns>
        internal static Material Factory(ApiCSiApplication app, string name)
        {
            Material material = new Material(app, name);
            material.FillData();
            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Material(ApiCSiApplication app, string name) : base(app, name)
        {
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            FillMaterial();
            FillTemperature();
            FillTemperatureDependentProperties();
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

        #region Fill/Set
        /// <summary>
        /// Fills the material.
        /// </summary>
        public void FillMaterial()
        {
            if (_apiMaterialProperties == null) return;

            _apiMaterialProperties.GetMaterial(Name,
                out var materialType,
                out var color,
                out var notes,
                out var guid);
            Type = materialType;
            Color = color;
            Notes = notes;
            GUID = guid;
        }

        /// <summary>
        /// Fills the temperature dependent properties.
        /// </summary>
        public void FillTemperatureDependentProperties()
        {
            _propertiesByTemperature.Clear();
            foreach (double temperature in _temperatures)
            {
                MaterialByTemperature property = MaterialByTemperature.Factory(_apiApp, Name, temperature);
                _propertiesByTemperature.Add(property);
            }
        }

        /// <summary>
        /// Retrieves the temperatures at which properties are specified for a material.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillTemperature()
        {
            _apiMaterialProperties.GetTemperature(Name, out var temperatures);
            _temperatures = new List<double>(temperatures);
        }

        /// <summary>
        /// Assigns the temperatures at which properties are specified for a material.
        /// This data is required only for materials whose properties are temperature dependent..
        /// </summary>
        /// <param name="temperatures">The different temperatures at which properties are specified for the material.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetTemperature(List<double> temperatures)
        {
            _apiMaterialProperties.SetTemperature(Name, temperatures.ToArray());
            _temperatures = temperatures;
        }
        #endregion

        #region Creation
        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _apiMaterialProperties.ChangeName(Name, newName);
            Name = newName;
        }


        /// <summary>
        /// Adds the material.
        /// Returns the name that the program ultimately assigns for the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <returns>Material.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static Material AddMaterial(ApiCSiApplication app, MaterialTemplate material)
        {
            ApiMaterial apiMaterialProperties = getApiMaterialProperties(app);
            string uniqueName = apiMaterialProperties.AddMaterial(material.MaterialType,
                material.RegionName(),
                material.Standard,
                material.Grade,
                material.Name);
            return Factory(app, uniqueName);
        }



        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        internal override void Delete()
        {
            _apiMaterialProperties.Delete(Name);
        }
        #endregion

        #region Static
        /// <summary>
        /// Returns the names of all defined material properties.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList(ApiMaterial app)
        {
            return new List<string>(app.GetNameList());
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <param name="materialProperties">The material properties.</param>
        /// <param name="name">The name.</param>
        /// <returns>Tuple&lt;eMaterialPropertyType, eMaterialSymmetryType&gt;.</returns>
        public static Tuple<eMaterialPropertyType, eMaterialSymmetryType> GetMaterialType(ApiMaterial materialProperties, string name)
        {
            if (materialProperties == null) return null;
            materialProperties.GetMaterialType(name,
                out var materialType,
                out var symmetryType);
            return new Tuple<eMaterialPropertyType, eMaterialSymmetryType>(materialType, symmetryType);
        }
        #endregion
    }
}
