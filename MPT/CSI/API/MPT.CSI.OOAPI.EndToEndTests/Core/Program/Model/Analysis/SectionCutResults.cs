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
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class SectionCutResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class SectionCutResults : AnalysisResults
    {

        /// <summary>
        /// Gets or sets the name of the section cut.
        /// </summary>
        /// <value>The name of the section cut.</value>
        public string SectionCutName { get; protected set; }

        /// <summary>
        /// Gets or sets the analysis forces.
        /// </summary>
        /// <value>The analysis forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> AnalysisForces { get; protected set; }
        /// <summary>
        /// Gets or sets the design forces.
        /// </summary>
        /// <value>The design forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, Forces>> DesignForces { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCutResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SectionCutResults(string name)
        {
            SectionCutName = name;
        }

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
            AnalysisForces = new List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>>();
            if (Registry.SectionCutAnalysisForces.Count == 0)
            {
                Registry.SectionCutAnalysisForces = GetSectionCutAnalysis();
            }

            foreach (var result in Registry.SectionCutAnalysisForces)
            {
                if (result.Item1.SectionCutName == SectionCutName)
                {
                    AnalysisForces.Add(result);
                }
            }
        }

        /// <summary>
        /// Fills the design forces.
        /// </summary>
        public void FillDesignForces()
        {
            DesignForces = new List<Tuple<SectionCutResultsIdentifier, Forces>>();
            if (Registry.SectionCutDesignForces.Count == 0)
            {
                Registry.SectionCutDesignForces = GetSectionCutDesign();
            }

            foreach (var result in Registry.SectionCutDesignForces)
            {
                if (result.Item1.SectionCutName == SectionCutName)
                {
                    DesignForces.Add(result);
                }
            }
        }

        /// <summary>
        /// Gets the section cut analysis.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;SectionCutResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> GetSectionCutAnalysis()
        {
            _analysisResults.SectionCutAnalysis(
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
        public static List<Tuple<SectionCutResultsIdentifier, Forces>> GetSectionCutDesign()
        {
            _analysisResults.SectionCutDesign(
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
    }
}
