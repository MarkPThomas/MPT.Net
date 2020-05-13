// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="ResponseSpectrumCodeFunctions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class ResponseSpectrumCodeFunctions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions.ResponseSpectrumFunction" />
    public class ResponseSpectrumCodeFunctions<T> : ResponseSpectrumFunction where T : ResponseSpectrumProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected ResponseSpectrumProperties _codeProperties;

        /// <summary>
        /// The code properties associated with the response spectrum.
        /// </summary>
        /// <value>The code properties.</value>
        public T CodeProperties => (T)_codeProperties.Clone();
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>ResponseSpectrumCodeFunctions&lt;T&gt;.</returns>
        internal static ResponseSpectrumCodeFunctions<T> Factory(string uniqueName, ResponseSpectrumProperties properties = null)
        {
            ResponseSpectrumCodeFunctions<T> responseSpectrum = new ResponseSpectrumCodeFunctions<T>(uniqueName)
                                                                    { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSpectrumCodeFunctions{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ResponseSpectrumCodeFunctions(string name) : base(name)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void SetProperties(T properties)
        {
            _codeProperties = properties;
        }
        #endregion
    }
}
