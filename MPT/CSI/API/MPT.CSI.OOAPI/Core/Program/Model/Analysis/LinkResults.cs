// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="LinkResults.cs" company="">
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
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class LinkResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
    public class LinkResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The link forces
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, Forces>> _linkForces;
        /// <summary>
        /// Gets or sets the link forces.
        /// </summary>
        /// <value>The link forces.</value>
        public List<Tuple<ObjectPointResultsIdentifier, Forces>> LinkForces
        {
            get
            {
                if (_linkForces == null)
                {
                    FillLinkForce();
                }

                return _linkForces;
            }
        }

        /// <summary>
        /// The link joint force
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> _linkJointForce;
        /// <summary>
        /// Gets or sets the link joint force.
        /// </summary>
        /// <value>The link joint force.</value>
        public List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> LinkJointForce
        {
            get
            {
                if (_linkJointForce == null)
                {
                    FillLinkJointForce();
                }

                return _linkJointForce;
            }
        }

        /// <summary>
        /// The link deformations
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _linkDeformations;
        /// <summary>
        /// Gets or sets the link deformations.
        /// </summary>
        /// <value>The link deformations.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> LinkDeformations
        {
            get
            {
                if (_linkDeformations == null)
                {
                    FillLinkDeformation();
                }

                return _linkDeformations;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public LinkResults(ApiCSiApplication app, string name) : base(app)
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
            FillLinkForce();
            FillLinkDeformation();
            FillLinkJointForce();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            LinkForces?.Clear();
            LinkDeformations?.Clear();
            LinkJointForce?.Clear();
        }

        /// <summary>
        /// Fills the link force.
        /// </summary>
        public void FillLinkForce()
        {
            _linkForces = GetLinkForces(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the link deformation.
        /// </summary>
        public void FillLinkDeformation()
        {
            _linkDeformations = GetLinkDeformations(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the link joint force.
        /// </summary>
        public void FillLinkJointForce()
        {
            _linkJointForce = GetLinkJointForces(_apiAnalysisResults, ObjectName);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the link forces.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, Forces>> GetLinkForces(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.LinkForce(name,
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
                .Select((result, i) => new Tuple<ObjectPointResultsIdentifier, Forces>(result, forces[i]))
                .ToList();
        }


        /// <summary>
        /// Gets the link joint forces.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> GetLinkJointForces(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.LinkJointForce(name,
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
                .Select((result, i) => new Tuple<ObjectPointResultsIdentifier, AnalysisLoads>(result, forces[i]))
                .ToList();
        }


        /// <summary>
        /// Gets the link deformations.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetLinkDeformations(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.LinkDeformation(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
            {
                LoadCase = t,
                ObjectName = objectNames[i],
                ElementName = elementNames[i],
                StepType = stepTypes[i],
                StepNumber = stepNumbers[i]
            })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }
        #endregion
    }
}
