// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="ResponseSpectrumUserFunction.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class ResponseSpectrumUserFunction.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions.ResponseSpectrumFunction" />
    public class ResponseSpectrumUserFunction : ResponseSpectrumFunction, IFunctionCurve<PeriodAccelerationPoint>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the function curve.
        /// </summary>
        /// <value>The function curve.</value>
        public FunctionCurve<PeriodAccelerationPoint> FunctionCurve { get; } = new FunctionCurve<PeriodAccelerationPoint>();

        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>ResponseSpectrumUserFunction.</returns>
        internal static ResponseSpectrumUserFunction Factory(string uniqueName)
        {
            return new ResponseSpectrumUserFunction(uniqueName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSpectrumUserFunction"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ResponseSpectrumUserFunction(string name) : base(name)
        {
        }
        #endregion

        #region Methods

        

        #endregion
    }
}
