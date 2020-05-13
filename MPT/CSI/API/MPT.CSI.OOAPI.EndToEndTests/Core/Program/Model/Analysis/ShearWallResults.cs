// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ShearWallResults.cs" company="">
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

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class ShearWallResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class ShearWallResults : AnalysisResults
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }
        /// <summary>
        /// Gets or sets the forces.
        /// </summary>
        /// <value>The forces.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pier. Otherwise it is a spandrel.
        /// </summary>
        /// <value><c>true</c> if this instance is pier; otherwise, <c>false</c>.</value>
        public bool IsPier { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShearWallResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isPier">if set to <c>true</c> [is pier].</param>
        public ShearWallResults(string name, bool isPier = true)
        {
            Name = name;
            IsPier = isPier;
        }

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillPierForce();
            FillSpandrelForce();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Forces?.Clear();
        }

        /// <summary>
        /// Fills the pier force.
        /// </summary>
        public void FillPierForce()
        {
            if (!IsPier) return;

            Forces = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();
            if (Registry.PierResults.Count == 0)
            {
                Registry.PierResults = GetPierForces();
            }

            foreach (var pierResult in Registry.PierResults)
            {
                if (pierResult.Item1.PierSpandrelName == Name)
                {
                    Forces.Add(pierResult);
                }
            }
        }

        /// <summary>
        /// Fills the spandrel force.
        /// </summary>
        public void FillSpandrelForce()
        {
            if (IsPier) return;

            Forces = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();
            if (Registry.SpandrelResults.Count == 0)
            {
                Registry.SpandrelResults = GetSpandrelForces();
            }

            foreach (var spandrelResult in Registry.SpandrelResults)
            {
                if (spandrelResult.Item1.PierSpandrelName == Name)
                {
                    Forces.Add(spandrelResult);
                }
            }
        }

        /// <summary>
        /// Gets the pier forces.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;PierSpandrelResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<PierSpandrelResultsIdentifier, Forces>> GetPierForces()
        {
            _analysisResults.PierForce(
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
                    IsPier = true
                })
                .Select((result, i) => new Tuple<PierSpandrelResultsIdentifier, Forces>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the spandrel forces.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;PierSpandrelResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<PierSpandrelResultsIdentifier, Forces>> GetSpandrelForces()
        {
            _analysisResults.SpandrelForce(
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
    }
}
