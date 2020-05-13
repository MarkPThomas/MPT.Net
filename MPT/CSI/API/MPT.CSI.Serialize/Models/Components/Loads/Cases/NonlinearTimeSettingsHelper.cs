// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-23-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="NonlinearTimeSettingsHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class NonlinearTimeSettingsHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Loads.Cases.NonlinearSettingsHelper" />
    public class NonlinearTimeSettingsHelper : NonlinearSettingsHelper
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the minimum size of the substep.
        /// </summary>
        /// <value>The minimum size of the substep.</value>
        public virtual double MinSubstepSize { get; internal set; }
        /// <summary>
        /// Gets the maximum size of the substep.
        /// </summary>
        /// <value>The maximum size of the substep.</value>
        public virtual double MaxSubstepSize { get; internal set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="NonlinearSettingsHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        internal NonlinearTimeSettingsHelper(string caseName) : base(caseName)
        {
        }
        #endregion
    }
}
