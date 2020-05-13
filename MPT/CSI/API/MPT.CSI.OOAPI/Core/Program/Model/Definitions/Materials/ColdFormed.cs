// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="ColdFormed.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class ColdFormed.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.Material" />
    public class ColdFormed : MaterialByTemperature<ColdFormedSteelProperties>
    {
#region Fields & Properties



#endregion

#region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>ColdFormed.</returns>
        internal new static ColdFormed Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            ColdFormed material = new ColdFormed(app, uniqueName, temperature);
            material.FillData();

            return material;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColdFormed"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ColdFormed(ApiCSiApplication app, string name) : base(app, name)
        {
        }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
        }


#endregion

#region Fill/Set
        /// <summary>
        /// Returns the other material property data for cold formed materials.
        /// The function returns an error if the specified material is not cold formed.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillColdFormed(string name,
            ref double Fy,
            ref double Fu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {

        }

        /// <summary>
        /// Sets the other material property data for cold formed materials.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetColdFormed(string name,
            double Fy,
            double Fu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {

        }
#endregion
    }
}
#endif