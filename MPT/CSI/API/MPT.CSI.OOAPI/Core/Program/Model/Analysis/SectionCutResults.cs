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
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
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
                    FillAnalysisForces();
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
                    FillDesignForces();
                }

                return _designForces;
            }
        }
        #endregion

        #region Initialization

        internal SectionCutResults(ApiCSiApplication app, string name) : base(app)
        {
            SectionCutName = name;
        }

        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillAnalysisForces();
            FillDesignForces();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            AnalysisForces?.Clear();
            DesignForces?.Clear();
        }

        /// <summary>
        /// Fills the analysis forces.
        /// </summary>
        public void FillAnalysisForces()
        {
            _analysisForces = GetSectionCutAnalysis(_apiAnalysisResults);
        }

        /// <summary>
        /// Fills the design forces.
        /// </summary>
        public void FillDesignForces()
        {
            _designForces = GetSectionCutDesign(_apiAnalysisResults);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the section cut analysis.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;SectionCutResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> GetSectionCutAnalysis(IResults app)
        {
            app.SectionCutAnalysis(
                out var sectionCuts,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var analysisForces);

            return loadCases.Select((t, i) => new SectionCutResultsIdentifier()
                {
                    LoadCase = t,
                    SectionCutName = sectionCuts[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<SectionCutResultsIdentifier, AnalysisLoads>(result, analysisForces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the section cut design.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;SectionCutResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<SectionCutResultsIdentifier, Forces>> GetSectionCutDesign(IResults app)
        {
            app.SectionCutDesign(
                out var sectionCuts,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var designForces);

            return loadCases.Select((t, i) => new SectionCutResultsIdentifier()
                {
                    LoadCase = t,
                    SectionCutName = sectionCuts[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<SectionCutResultsIdentifier, Forces>(result, designForces[i]))
                .ToList();
        }
        #endregion
    }
}
