// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="TimeHistoryDirectIntegration.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class TimeHistoryDirectIntegration.
    /// </summary>
    /// <seealso cref="TimeHistory" />
    public abstract class TimeHistoryDirectIntegration : TimeHistory,
        IInitialCase
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        protected readonly LoadCases _loadCases;

        public virtual double MaxModalFrequency { get; internal set; }

        /// <summary>
        /// The initial case
        /// </summary>
        protected InitialCaseHelper _initialCase;
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public virtual InitialCaseHelper InitialCase
        {
            get
            {
                if (_initialCase != null) return _initialCase;
                _initialCase = InitialCaseHelper.Factory(_loadCases, Name);

                return _initialCase;
            }
        }

        /// <summary>
        /// Gets or sets the proportional damping.
        /// </summary>
        /// <value>The damping proportional.</value>
        public virtual DampingProportional DampingProportional { get; internal set; }

        /// <summary>
        /// The damping overwrites of mode #, overwrite.
        /// </summary>
        /// <value>The damping overwrites.</value>
        public virtual List<DampingOverride> DampingOverrides { get; internal set; }

        /// <summary>
        /// The nonlinear settings
        /// </summary>
        protected NonlinearTimeSettingsHelper _nonlinearSettings;
        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public virtual NonlinearTimeSettingsHelper NonlinearSettings
        {
            get
            {
                if (_nonlinearSettings != null) return _nonlinearSettings;
                _nonlinearSettings = new NonlinearTimeSettingsHelper(Name);

                return _nonlinearSettings;
            }
        }

        /// <summary>
        /// Gets the time integration.
        /// </summary>
        /// <value>The time integration.</value>
        public virtual TimeIntegration TimeIntegration { get; internal set; } = new TimeIntegration();
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectIntegration" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryDirectIntegration(
            Analyzer analyzer,
            LoadCases loadCases, 
            string name) 
            : base( analyzer, name)
        {
            _loadCases = loadCases;
        }
        #endregion
    }
}
