// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StaticNonlinearStaged.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Static nonlinear staged construction load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public sealed class StaticNonlinearStaged : LoadCase,
        IInitialCase, INonlinearSettings//, INonlinearLoadApplication
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The initial case
        /// </summary>
        private InitialCaseHelper _initialCase;
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase
        {
            get
            {
                if (_initialCase != null) return _initialCase;
                _initialCase = InitialCaseHelper.Factory(_loadCases, Name);

                return _initialCase;
            }
        }

        /// <summary>
        /// The mass source
        /// </summary>
        private MassSourceHelper _massSource;
        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSourceHelper MassSource
        {
            get
            {
                if (_massSource != null) return _massSource;
                _massSource = new MassSourceHelper(Name);

                return _massSource;
            }
        }

        /// <summary>
        /// The nonlinear settings
        /// </summary>
        private NonlinearSettingsHelper _nonlinearSettings;
        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public NonlinearSettingsHelper NonlinearSettings
        {
            get
            {
                if (_nonlinearSettings != null) return _nonlinearSettings;
                _nonlinearSettings = new NonlinearSettingsHelper(Name);

                return _nonlinearSettings;
            }
        }

        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        public StageResultsSaved ResultsSaved { get; internal set; }

        /// <summary>
        /// The stage definitions
        /// </summary>
        private StageDefinitions _stageDefinitions;
        /// <summary>
        /// The stage definitions associated with the load case.
        /// </summary>
        /// <value>The stage definitions.</value>
        public StageDefinitions StageDefinitions
        {
            get
            {
                if (_stageDefinitions != null) return _stageDefinitions;
                _stageDefinitions = new StageDefinitions(Name);

                return _stageDefinitions;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static StaticNonlinearStaged Factory(
            Analyzer analyzer,
            LoadCases loadCases, 
            string uniqueName)
        {
            StaticNonlinearStaged loadCase = new StaticNonlinearStaged(analyzer, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticNonlinearStaged" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        private StaticNonlinearStaged(
            Analyzer analyzer, 
            LoadCases loadCases,
            string name) 
            : base(analyzer, name)
        {
            _loadCases = loadCases;
        }
        #endregion
    }
}
