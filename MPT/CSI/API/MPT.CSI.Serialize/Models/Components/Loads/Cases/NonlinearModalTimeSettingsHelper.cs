// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-23-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="NonlinearModalTimeSettingsHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class NonlinearModalTimeSettingsHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Loads.Cases.NonlinearTimeSettingsHelper" />
    public class NonlinearModalTimeSettingsHelper : NonlinearTimeSettingsHelper
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the static period.
        /// </summary>
        /// <value>The static period.</value>
        public virtual double StaticPeriod { get; internal set; }
        /// <summary>
        /// Gets the relative force convergence tolerance.
        /// </summary>
        /// <value>The relative force convergence tolerance.</value>
        public virtual double RelativeForceConvergenceTolerance { get; internal set; }
        /// <summary>
        /// Gets the relative energy convergence tolerance.
        /// </summary>
        /// <value>The relative energy convergence tolerance.</value>
        public virtual double RelativeEnergyConvergenceTolerance { get; internal set; }
        /// <summary>
        /// Gets the convergence factor.
        /// </summary>
        /// <value>The convergence factor.</value>
        public virtual double ConvergenceFactor { get; internal set; }
        /// <summary>
        /// Gets the minimum force iteration limit.
        /// </summary>
        /// <value>The minimum force iteration limit.</value>
        public virtual int MinForceIterationLimit { get; internal set; }
        /// <summary>
        /// Gets the maximum force iteration limit.
        /// </summary>
        /// <value>The maximum force iteration limit.</value>
        public virtual int MaxForceIterationLimit { get; internal set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="NonlinearSettingsHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        internal NonlinearModalTimeSettingsHelper(string caseName) : base(caseName)
        {
        }
        #endregion
    }
}
