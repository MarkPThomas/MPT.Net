// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SpandrelResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class SpandrelResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    public class SpandrelResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the forces.
        /// </summary>
        /// <value>The forces.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces { get; protected set; }
        #endregion


        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="SpandrelResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal SpandrelResults(ApiCSiApplication app) : base(app) { }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            Forces = GetSpandrelForces(_apiAnalysisResults);
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Forces?.Clear();
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the spandrel forces.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;PierSpandrelResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<PierSpandrelResultsIdentifier, Forces>> GetSpandrelForces(IResults app)
        {
            app.SpandrelForce(
                out var storyNames,
                out var pierNames,
                out var loadCases,
                out var locations,
                out var forces);

            return loadCases.Select((t, i) => new PierSpandrelResultsIdentifier()
            {
                LoadCase = t,
                StoryName = storyNames[i],
                PierSpandrelName = pierNames[i],
                Location = locations[i],
                IsPier = false
            })
                .Select((result, i) => new Tuple<PierSpandrelResultsIdentifier, Forces>(result, forces[i]))
                .ToList();
        }
        #endregion
    }
}
#endif