// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-23-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="TimeIntegration.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Class TimeIntegration.
    /// </summary>
    public class TimeIntegration
    {
        /// <summary>
        /// Gets the type of the integration.
        /// </summary>
        /// <value>The type of the integration.</value>
        public virtual eTimeIntegrationType IntegrationType { get; internal set; }
        /// <summary>
        /// Gets the alpha.
        /// </summary>
        /// <value>The alpha.</value>
        public virtual double Alpha { get; internal set; }
        /// <summary>
        /// Gets the beta.
        /// </summary>
        /// <value>The beta.</value>
        public virtual double Beta { get; internal set; }
        /// <summary>
        /// Gets the gamma.
        /// </summary>
        /// <value>The gamma.</value>
        public virtual double Gamma { get; internal set; }
        /// <summary>
        /// Gets the theta.
        /// </summary>
        /// <value>The theta.</value>
        public virtual double Theta { get; internal set; }
        /// <summary>
        /// Gets the alpha m.
        /// </summary>
        /// <value>The alpha m.</value>
        public virtual double AlphaM { get; internal set; }
    }
}
