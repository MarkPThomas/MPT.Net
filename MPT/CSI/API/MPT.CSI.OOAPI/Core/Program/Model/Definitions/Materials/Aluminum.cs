// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Aluminum.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    /// <summary>
    /// Class Aluminum.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials.Material" />
    public class Aluminum : MaterialByTemperature<AluminumProperties>
    {
#region Fields & Properties



#endregion

#region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Aluminum.</returns>
        internal new static Aluminum Factory(ApiCSiApplication app, string uniqueName, double temperature = 0)
        {
            Aluminum material = new Aluminum(app, uniqueName, temperature);
            material.FillData();

            return material;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Aluminum"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected Aluminum(ApiCSiApplication app, string name) : base(app, name)
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
        /// Returns the other material property data for aluminum materials.
        /// The function returns an error if the specified material is not aluminum.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillAluminum(string name,
            ref eAluminumType aluminumType,
            ref string alloy,
            ref double Fcy,
            ref double Fty,
            ref double Ftu,
            ref double Fsu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {

        }

        /// <summary>
        /// Sets the other material property data for aluminum materials.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetAluminum(string name,
            eAluminumType aluminumType,
            string alloy,
            double Fcy,
            double Fty,
            double Ftu,
            double Fsu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {

        }
#endregion
    }
}
#endif