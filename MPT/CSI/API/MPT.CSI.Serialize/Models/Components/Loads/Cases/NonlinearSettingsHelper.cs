// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="NonlinearSettingsHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class NonlinearSettingsHelper.
    /// </summary>
    public class NonlinearSettingsHelper
    {
        #region Fields & Properties

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The geometric nonlinearity option selected for the load case.
        /// </summary>
        /// <value>The type of the geometric nonlinearity.</value>
        public virtual eGeometricNonlinearity GeometricNonlinearityType { get; internal set; }

        /// <summary>
        /// LoadType of hinge unloading for the selected load case.
        /// </summary>
        /// <value>The type of the hinge unloading.</value>
        public virtual eHingeUnloadingType HingeUnloadingType { get; internal set; }

        /// <summary>
        /// Gets the solution control parameters.
        /// </summary>
        /// <value>The solution control parameters.</value>
        public virtual SolutionControlParameters SolutionControlParameters { get; internal set; }

        /// <summary>
        /// Gets the target force parameters.
        /// </summary>
        /// <value>The target force parameters.</value>
        public virtual TargetForceParameters TargetForceParameters { get; internal set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="NonlinearSettingsHelper"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        internal NonlinearSettingsHelper(string caseName) 
        {
            CaseName = caseName;
        }
        #endregion
    }
}
