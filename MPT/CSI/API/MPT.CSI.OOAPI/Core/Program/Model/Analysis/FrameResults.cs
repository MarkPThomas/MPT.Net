// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="FrameResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using PointLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class FrameResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
    public class FrameResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The frame forces
        /// </summary>
        private List<Tuple<StationResultsIdentifier, Forces>> _frameForces;
        /// <summary>
        /// Gets or sets the frame forces.
        /// </summary>
        /// <value>The frame forces.</value>
        public List<Tuple<StationResultsIdentifier, Forces>> FrameForces
        {
            get
            {
                if (_frameForces == null)
                {
                    FillFrameForce();
                }

                return _frameForces;
            }
        }

        /// <summary>
        /// The point forces
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, PointLoads>> _pointForces;
        /// <summary>
        /// Gets or sets the point forces.
        /// </summary>
        /// <value>The point forces.</value>
        public List<Tuple<ObjectPointResultsIdentifier, PointLoads>> PointForces
        {
            get
            {
                if (_pointForces == null)
                {
                    FillPointForce();
                }

                return _pointForces;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public FrameResults(ApiCSiApplication app, string name) : base(app)
        {
            ObjectName = name;
        }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillFrameForce();
            FillPointForce();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            FrameForces?.Clear();
            PointForces?.Clear();
        }

        /// <summary>
        /// Fills the frame force.
        /// </summary>
        public void FillFrameForce()
        {
            _frameForces = GetFrameForces(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the point force.
        /// </summary>
        public void FillPointForce()
        {
            _pointForces = GetFrameJointForces(_apiAnalysisResults, ObjectName);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the frame forces.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;StationResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<StationResultsIdentifier, Forces>> GetFrameForces(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.FrameForce(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var objectStations,
                out var elementStations,
                out var forces);

            return loadCases.Select((t, i) => new StationResultsIdentifier()
            {
                LoadCase = t,
                ObjectName = objectNames[i],
                ElementName = elementNames[i],
                StepType = stepTypes[i],
                StepNumber = stepNumbers[i],
                ObjectStation = objectStations[i],
                ElementStation = elementStations[i]
            })
                .Select((result, i) => new Tuple<StationResultsIdentifier, Forces>(result, forces[i]))
                .ToList();
        }


        /// <summary>
        /// Gets the frame joint forces.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, PointLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, PointLoads>> GetFrameJointForces(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.FrameJointForce(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectPointResultsIdentifier()
            {
                LoadCase = t,
                ObjectName = objectNames[i],
                ElementName = elementNames[i],
                StepType = stepTypes[i],
                StepNumber = stepNumbers[i],
                PointName = pointNames[i]
            })
                .Select((result, i) => new Tuple<ObjectPointResultsIdentifier, PointLoads>(result, forces[i]))
                .ToList();
        }
        #endregion
    }
}
