// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="Function.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class Function.
    /// </summary>
    public abstract class Function : IUniqueName
    {
        /// <summary>
        /// The unique name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        #region Initialization

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="functionType">Type of the function.</param>
        /// <returns>Function.</returns>
        internal static Function Factory(
            string uniqueName,
            eFunctionTypes functionType)
        {
            switch (functionType)
            {
                case eFunctionTypes.PowerSpectralDensityUser:
                    return PowerSpectralDensityFunction.Factory(uniqueName);
                case eFunctionTypes.SteadyStateUser:
                    return SteadyStateFunction.Factory(uniqueName);
                case eFunctionTypes.ResponseSpectrumUser:
                    return ResponseSpectrumUserFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistoryCosine:
                    return TimeHistoryCosineFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistoryRamp:
                    return TimeHistoryRampFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistorySawtooth:
                    return TimeHistorySawtoothFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistorySine:
                    return TimeHistorySineFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistoryTriangular:
                    return TimeHistoryTriangularFunction.Factory(uniqueName);
                case eFunctionTypes.TimeHistoryUser:
                    return TimeHistoryUserFunction.Factory(uniqueName);
                case eFunctionTypes.ResponseSpectrumCodeUBC97:
                    return ResponseSpectrumCodeFunctions<UBC97SpectrumProperties>.Factory(uniqueName);
                default:
                    return null;
            }
        }

        protected Function(string name)
        {
            Name = name;
        }
        #endregion
    }
}
