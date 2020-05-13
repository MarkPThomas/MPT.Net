// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="PanelZoneResults.cs" company="">
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

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class PanelZoneResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class PanelZoneResults : AnalysisResults
    {
        /// <summary>
        /// Gets or sets the name of the joint.
        /// </summary>
        /// <value>The name of the joint.</value>
        public string JointName { get; protected set; }

        /// <summary>
        /// Gets or sets the forces.
        /// </summary>
        /// <value>The forces.</value>
        public List<Tuple<ElementPointResultsIdentifier, Forces>> Forces { get; protected set; }
        /// <summary>
        /// Gets or sets the deformations.
        /// </summary>
        /// <value>The deformations.</value>
        public List<Tuple<ElementResultsIdentifier, Deformations>> Deformations { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PanelZoneResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public PanelZoneResults(string name)
        {
            JointName = name;
        }

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillForces();
            FillDeformations();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Forces?.Clear();
            Deformations?.Clear();
        }

        /// <summary>
        /// Fills the forces.
        /// </summary>
        public void FillForces()
        {
            Forces = GetPanelZoneForces(JointName);
        }

        /// <summary>
        /// Fills the deformations.
        /// </summary>
        public void FillDeformations()
        {
            Deformations = GetPanelZoneDeformations(JointName);
        }

        /// <summary>
        /// Gets the panel zone forces.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ElementPointResultsIdentifier, Forces&gt;&gt;.</returns>
        public static List<Tuple<ElementPointResultsIdentifier, Forces>> GetPanelZoneForces(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.PanelZoneForce(name,
                itemType,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ElementPointResultsIdentifier()
                {
                    LoadCase = t,
                    PointName = pointNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ElementPointResultsIdentifier, Forces>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the panel zone deformations.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ElementResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ElementResultsIdentifier, Deformations>> GetPanelZoneDeformations(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.PanelZoneDeformation(name,
                itemType,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var deformations);

            return loadCases.Select((t, i) => new ElementResultsIdentifier()
                {
                    LoadCase = t,
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ElementResultsIdentifier, Deformations>(result, deformations[i]))
                .ToList();
        }
    }
}
