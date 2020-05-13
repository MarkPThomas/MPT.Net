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
using MPT.CSI.OOAPI.Core.Helpers.Results;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class LinkResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class LinkResults : AnalysisResults
    {
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }
        /// <summary>
        /// Gets or sets the link forces.
        /// </summary>
        /// <value>The link forces.</value>
        public List<Tuple<ObjectPointResultsIdentifier, Forces>> LinkForces { get; protected set; }
        /// <summary>
        /// Gets or sets the link joint force.
        /// </summary>
        /// <value>The link joint force.</value>
        public List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> LinkJointForce { get; protected set; }
        /// <summary>
        /// Gets or sets the link deformations.
        /// </summary>
        /// <value>The link deformations.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> LinkDeformations { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LinkResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LinkResults(string name)
        {
            ObjectName = name;
        }

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
            LinkForces = GetLinkForces(ObjectName);
        }

        /// <summary>
        /// Fills the link deformation.
        /// </summary>
        public void FillLinkDeformation()
        {
            LinkDeformations = GetLinkDeformations(ObjectName);
        }

        /// <summary>
        /// Fills the link joint force.
        /// </summary>
        public void FillLinkJointForce()
        {
            LinkJointForce = GetLinkJointForces(ObjectName);
        }

        /// <summary>
        /// Gets the link forces.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, Forces>> GetLinkForces(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.LinkForce(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> GetLinkJointForces(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.LinkJointForce(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetLinkDeformations(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.LinkDeformation(name,
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
    }
}
