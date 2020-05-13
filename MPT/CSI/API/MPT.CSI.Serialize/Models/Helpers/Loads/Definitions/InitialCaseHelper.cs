// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="InitialCaseHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class InitialCaseHelper.
    /// </summary>
    public class InitialCaseHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The initial load case
        /// </summary>
        private LoadCase _initialLoadCase;
        /// <summary>
        /// This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.
        /// </summary>
        /// <value>The initial case.</value>
        public string InitialCase => _initialLoadCase == null ? Constants.NONE : _initialLoadCase.Name;
        #endregion


        internal static InitialCaseHelper Factory(
            LoadCases loadCases,
            string caseName)
        {
            InitialCaseHelper initialCase = new InitialCaseHelper(loadCases, caseName);
            return initialCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialCaseHelper" /> class.
        /// </summary>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="caseName">Name of the case.</param>
        private InitialCaseHelper(
            LoadCases loadCases,
            string caseName)
        {
            _loadCases = loadCases;
            CaseName = caseName;
        }

       
        /// <summary>
        /// Sets the initial condition for the load case.
        /// </summary>
        /// <param name="initialCase">The initial case.</param>
        public void SetInitialCase(LoadCase initialCase)
        {
            _initialLoadCase = initialCase;
        }

        /// <summary>
        /// Removes the initial condition for the load case.
        /// </summary>
        public void RemoveInitialCase()
        {
            _initialLoadCase = null;
        }
    }
}
