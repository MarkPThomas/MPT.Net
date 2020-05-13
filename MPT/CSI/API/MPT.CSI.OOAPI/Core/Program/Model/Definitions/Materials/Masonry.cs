// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Masonry.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class Masonry.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.MaterialByTemperature{MasonryProperties}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.Material" />
    public class Masonry : MaterialByTemperature<MasonryProperties>
    {
        #region Fields & Properties
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="temperature">The temperature.</param>
        /// <returns>Masonry.</returns>
        internal new static Masonry Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            Masonry material = new Masonry(app, uniqueName, temperature);
            material.FillData();

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Masonry" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="temperature">The temperature.</param>
        protected Masonry(ApiCSiApplication app, string name, double temperature = 0) : base(app, name, temperature)
        {
        }

        #endregion

        #region Fill/Set

        /// <summary>
        /// Returns material property data for the material.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillProperties()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// This function initializes a material property.
        /// If this function is called for an existing material property, all items for the material are reset to their default value.
        /// </summary>
        /// <param name="properties">The properties to apply to the material.</param>
        /// <param name="temperature">The temperature.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void set(MasonryProperties properties, double temperature)
        {
            throw new System.NotImplementedException();
        }
        #endregion


    }
}
