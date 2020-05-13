// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="ResponseSpectrum.cs" company="">
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
    /// Response spectrum load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public sealed class ResponseSpectrum : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The list of associated load functions with their respective scale factors and other properties.
        /// </summary>
        /// <value>The patterns.</value>
        public List<LoadResponseSpectrum> Loads { get; internal set; }

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
        /// Gets or sets the direction combination.
        /// </summary>
        /// <value>The direction combination.</value>
        public DirectionCombination DirectionCombination { get; internal set; }

        /// <summary>
        /// Gets or sets the modal combination.
        /// </summary>
        /// <value>The modal combination.</value>
        public ModalCombination ModalCombination { get; internal set; }

        /// <summary>
        /// Eccentricity ratio that applies to all diaphragms for the load case.
        /// </summary>
        /// <value>The eccentricity.</value>
        public double Eccentricity { get; internal set; }

        /// <summary>
        /// The diaphragm eccentricity overwrites of diaphragm, overwrite.
        /// </summary>
        /// <value>The diaphragm eccentricity overwrites.</value>
        public List<DiaphragmEccentricityOverride> DiaphragmEccentricityOverwrites { get; internal set; }
        
        /// <summary>
        /// The damping associated with the load case.
        /// </summary>
        /// <value>The damping.</value>
        public DampingHelper Damping { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static ResponseSpectrum Factory(
            Analyzer analyzer,
            LoadCases loadCases, 
            string uniqueName)
        {
            ResponseSpectrum loadCase = new ResponseSpectrum(analyzer, loadCases, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.Serialize.Models.Components.Loads.Cases.ResponseSpectrum" /> class.
        /// </summary>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        private ResponseSpectrum(
            Analyzer analyzer,
            LoadCases loadCases,
            string name) : base(analyzer, name)
        {
            _loadCases = loadCases;
        }
        #endregion
    }
}
