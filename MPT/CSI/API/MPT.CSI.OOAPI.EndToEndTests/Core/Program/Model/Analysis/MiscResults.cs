// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="MiscResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class MiscResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class MiscResults : AnalysisResults
    {

        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            // Does nothing
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            // Does nothing
        }

        /// <summary>
        /// Empties the results misc.
        /// </summary>
        public static void EmptyResultsMisc()
        {
            Registry.BucklingFactors?.Clear();
            Registry.ModalLoadParticipationRatios?.Clear();
            Registry.ModalParticipatingMassRatios?.Clear();
            Registry.ModalParticipationFactors?.Clear();
            Registry.ModalPeriods?.Clear();
        }

        /// <summary>
        /// Fills the generalized displacements.
        /// </summary>
        public void FillGeneralizedDisplacements()
        {
            // Does nothing
        }

        /// <summary>
        /// Fills the buckling factors.
        /// </summary>
        public static void FillBucklingFactors()
        {
            if (Registry.BucklingFactors.Count == 0)
            {
                Registry.BucklingFactors = GetBucklingFactors();
            }
        }

        /// <summary>
        /// Fills the modal load participation ratios.
        /// </summary>
        public static void FillModalLoadParticipationRatios()
        {
            if (Registry.ModalLoadParticipationRatios.Count == 0)
            {
                Registry.ModalLoadParticipationRatios = GetModalLoadParticipationRatios();
            }
        }

        /// <summary>
        /// Fills the modal participating mass ratios.
        /// </summary>
        public static void FillModalParticipatingMassRatios()
        {
            if (Registry.ModalParticipatingMassRatios.Count == 0)
            {
                Registry.ModalParticipatingMassRatios = GetModalParticipatingMassRatios();
            }
        }

        /// <summary>
        /// Fills the modal participation factors.
        /// </summary>
        public static void FillModalParticipationFactors()
        {
            if (Registry.ModalParticipationFactors.Count == 0)
            {
                Registry.ModalParticipationFactors = GetModalParticipationFactors();
            }
        }

        /// <summary>
        /// Fills the modal periods.
        /// </summary>
        public static void FillModalPeriods()
        {
            if (Registry.ModalPeriods.Count == 0)
            {
                Registry.ModalPeriods = GetModalPeriods();
            }
        }

        /// <summary>
        /// Gets the buckling factors.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, System.Double&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, double>> GetBucklingFactors()
        {
            _analysisResults.BucklingFactor(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var bucklingFactors);

            return loadCases.Select((t, i) => new JointLabelNameResultsIdentifier
                {
                    LoadCase = t,
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((identifier, i) => new Tuple<StepResultsIdentifier, double>(identifier, bucklingFactors[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the modal load participation ratios.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio&gt;&gt;.</returns>
        public static List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> GetModalLoadParticipationRatios()
        {
            _analysisResults.ModalLoadParticipationRatios(
                out var loadCases,
                out var itemTypes,
                out var items,
                out var percentStaticLoadParticipationRatio,
                out var percentDynamicLoadParticipationRatio);

            List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> resultItems = new List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                ModalLoadParticipationResultsIdentifier identifier =
                    new ModalLoadParticipationResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        ItemType = itemTypes[i],
                        Item = items[i]
                    };

                ModalLoadParticipationRatio results = new ModalLoadParticipationRatio
                {
                    PercentStaticLoadParticipationRatio = percentStaticLoadParticipationRatio[i],
                    PercentDynamicLoadParticipationRatio = percentDynamicLoadParticipationRatio[i]
                };

                resultItems.Add(new Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>(identifier, results));
            }

            return resultItems;
        }

        /// <summary>
        /// Gets the modal participating mass ratios.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalParticipatingMassRatio&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> GetModalParticipatingMassRatios()
        {
            _analysisResults.ModalParticipatingMassRatios(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var periods,
                out var massRatios,
                out var massRatioSums);

            List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> resultItems = new List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                StepResultsIdentifier identifier =
                    new StepResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i]
                    };

                ModalParticipatingMassRatio results = new ModalParticipatingMassRatio
                {
                    Period = periods[i],
                    MassRatio = massRatios[i],
                    MassRatioSum = massRatioSums[i]
                };

                resultItems.Add(new Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>(identifier, results));
            }

            return resultItems;
        }

        /// <summary>
        /// Gets the modal participation factors.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalParticipationFactor&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> GetModalParticipationFactors()
        {
            _analysisResults.ModalParticipationFactors(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var periods,
                out var participationFactors,
                out var modalMasses,
                out var modalStiffnesses);

            List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> resultItems = new List<Tuple<StepResultsIdentifier, ModalParticipationFactor>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                StepResultsIdentifier identifier =
                    new StepResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i]
                    };

                ModalParticipationFactor results = new ModalParticipationFactor
                {
                    Period = periods[i],
                    ParticipationFactor = participationFactors[i],
                    ModalMass = modalMasses[i],
                    ModalStiffness = modalStiffnesses[i]
                };

                resultItems.Add(new Tuple<StepResultsIdentifier, ModalParticipationFactor>(identifier, results));
            }

            return resultItems;
        }

        /// <summary>
        /// Gets the modal periods.
        /// </summary>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalPeriod&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalPeriod>> GetModalPeriods()
        {
            _analysisResults.ModalPeriod(
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var periods,
                out var frequencies,
                out var circularFrequencies,
                out var eigenvalues);

            List<Tuple<StepResultsIdentifier, ModalPeriod>> resultItems = new List<Tuple<StepResultsIdentifier, ModalPeriod>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                StepResultsIdentifier identifier =
                    new StepResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i]
                    };

                ModalPeriod results = new ModalPeriod
                {
                    Period = periods[i],
                    Frequency = frequencies[i],
                    CircularFrequency = circularFrequencies[i],
                    Eigenvalue = eigenvalues[i]
                };

                resultItems.Add(new Tuple<StepResultsIdentifier, ModalPeriod>(identifier, results));
            }

            return resultItems;
        }

        /// <summary>
        /// Gets the generalized displacement.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement for which results are returned.
        /// If the program does not recognize this name as a defined generalized displacement, it returns results for all selected generalized displacements, if any.
        /// For example, entering a blank string (i.e., "") for the name will prompt the program to return results for all selected generalized displacementse.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, GeneralizedDisplacementResult&gt;&gt;.</returns>
        public static List<Tuple<GeneralizedDisplacementResultsIdentifier, GeneralizedDisplacementResult>> GetGeneralizedDisplacement(string name)
        {
            _analysisResults.GeneralizedDisplacement(name,
                out var names,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var types,
                out var generalizedDisplacements);

            List<Tuple<GeneralizedDisplacementResultsIdentifier, GeneralizedDisplacementResult>> resultItems = new List<Tuple<GeneralizedDisplacementResultsIdentifier, GeneralizedDisplacementResult>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                GeneralizedDisplacementResultsIdentifier identifier =
                    new GeneralizedDisplacementResultsIdentifier
                    {
                        Name = names[i],
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i]
                    };

                GeneralizedDisplacementResult results = new GeneralizedDisplacementResult
                {
                    Type = types[i],
                    GeneralizedDisplacement = generalizedDisplacements[i]
                };

                resultItems.Add(new Tuple<GeneralizedDisplacementResultsIdentifier, GeneralizedDisplacementResult>(identifier, results));
            }

            return resultItems;
        }
    }
}
