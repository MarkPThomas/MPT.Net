// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="SectionCutResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;
using AnalysisLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.Loads;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class SectionCutResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class SectionCutResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the section cut.
        /// </summary>
        /// <value>The name of the section cut.</value>
        public string SectionCutName { get; protected set; }

        private List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> _analysisForces;
        /// <summary>
        /// Gets or sets the analysis forces.
        /// </summary>
        /// <value>The analysis forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> AnalysisForces
        {
            get
            {
                if (_analysisForces == null)
                {
                }

                return _analysisForces;
            }
        }


        private List<Tuple<SectionCutResultsIdentifier, Forces>> _designForces;
        /// <summary>
        /// Gets or sets the design forces.
        /// </summary>
        /// <value>The design forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, Forces>> DesignForces
        {
            get
            {
                if (_designForces == null)
                {
                }

                return _designForces;
            }
        }
        #endregion

        #region Initialization

        internal SectionCutResults(string name) 
        {
            SectionCutName = name;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            AnalysisForces?.Clear();
            DesignForces?.Clear();
        }
        #endregion
    }
}
