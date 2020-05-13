﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StaticNonlinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Static nonlinear load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public class StaticNonlinear : LoadCase,
        IInitialCase, ILoadsApplied, INonlinearSettings, INonlinearLoadApplication
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The load patterns
        /// </summary>
        private readonly LoadPatterns _loadPatterns;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        private LoadsAppliedHelper _loads;
        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        /// <value>The loads.</value>
        public LoadsAppliedHelper Loads
        {
            get
            {
                if (_loads != null) return _loads;
                _loads = new LoadsAppliedHelper(Name, _loadPatterns);

                return _loads;
            }
        }

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
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        private ModalCaseHelper _modalCase;
        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase
        {
            get
            {
                if (_modalCase != null) return _modalCase;
                _modalCase = ModalCaseHelper.Factory(Name, _loadCases);

                return _modalCase;
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
        public ResultsSaved ResultsSaved { get; internal set; }

        /// <summary>
        /// The load application associated with the load case.
        /// </summary>
        /// <value>The load application.</value>
        public virtual LoadApplication LoadApplication { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static StaticNonlinear Factory(
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            StaticNonlinear loadCase = new StaticNonlinear(analyzer, loadPatterns, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticNonlinear" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        private StaticNonlinear(
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string name) 
            : base(analyzer, name)
        {
            _loadPatterns = loadPatterns;
            _loadCases = loadCases;
        }
        #endregion
    }
}
