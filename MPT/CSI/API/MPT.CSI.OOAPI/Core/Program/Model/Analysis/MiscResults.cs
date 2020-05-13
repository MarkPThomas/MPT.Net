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
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class MiscResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
    public class MiscResults : AnalysisResults
    {
        #region Fields & Properties

        /// <summary>
        /// The modal load participation ratios
        /// </summary>
        private List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> _modalLoadParticipationRatios;
        /// <summary>
        /// Gets the modal load participation ratios.
        /// </summary>
        /// <value>The modal load participation ratios.</value>
        public List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> ModalLoadParticipationRatios
        {
            get
            {
                if (_modalLoadParticipationRatios == null)
                {
                    FillModalLoadParticipationRatios();
                }

                return _modalLoadParticipationRatios;
            }
        }

        /// <summary>
        /// The modal participating mass ratios
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> _modalParticipatingMassRatios;
        /// <summary>
        /// Gets the modal participating mass ratios.
        /// </summary>
        /// <value>The modal participating mass ratios.</value>
        public List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> ModalParticipatingMassRatios
        {
            get
            {
                if (_modalParticipatingMassRatios == null)
                {
                    FillModalParticipatingMassRatios();
                }

                return _modalParticipatingMassRatios;
            }
        }

        /// <summary>
        /// The modal participation factors
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> _modalParticipationFactors;
        /// <summary>
        /// Gets the modal participation factors.
        /// </summary>
        /// <value>The modal participation factors.</value>
        public List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> ModalParticipationFactors
        {
            get
            {
                if (_modalParticipationFactors == null)
                {
                    FillModalParticipationFactors();
                }

                return _modalParticipationFactors;
            }
        }

        /// <summary>
        /// The modal periods
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalPeriod>> _modalPeriods;
        /// <summary>
        /// Gets the modal periods.
        /// </summary>
        /// <value>The modal periods.</value>
        public List<Tuple<StepResultsIdentifier, ModalPeriod>> ModalPeriods
        {
            get
            {
                if (_modalPeriods == null)
                {
                    FillModalPeriods();
                }

                return _modalPeriods;
            }
        }

        /// <summary>
        /// The buckling factors
        /// </summary>
        private List<Tuple<StepResultsIdentifier, double>> _bucklingFactors;
        /// <summary>
        /// Gets the buckling factors.
        /// </summary>
        /// <value>The buckling factors.</value>
        public List<Tuple<StepResultsIdentifier, double>> BucklingFactors
        {
            get
            {
                if (_bucklingFactors == null)
                {
                    FillBucklingFactors();
                }

                return _bucklingFactors;
            }
        }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="MiscResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal MiscResults(ApiCSiApplication app) : base(app) { }

        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillBucklingFactors();
            FillModalLoadParticipationRatios();
            FillModalParticipatingMassRatios();
            FillModalParticipationFactors();
            FillModalPeriods();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            _bucklingFactors.Clear();
            _modalLoadParticipationRatios.Clear();
            _modalParticipatingMassRatios.Clear();
            _modalParticipationFactors.Clear();
            _modalPeriods.Clear();
        }


        /// <summary>
        /// Fills the generalized displacements.
        /// </summary>
        public void FillGeneralizedDisplacements()
        {
            // TODO: Implement generalized displacements?
            // Does nothing.
        }

        /// <summary>
        /// Fills the buckling factors.
        /// </summary>
        public void FillBucklingFactors()
        {
            _bucklingFactors = GetBucklingFactors(_apiAnalysisResults);
        }

        /// <summary>
        /// Fills the modal load participation ratios.
        /// </summary>
        public void FillModalLoadParticipationRatios()
        {
            _modalLoadParticipationRatios = GetModalLoadParticipationRatios(_apiAnalysisResults);
        }

        /// <summary>
        /// Fills the modal participating mass ratios.
        /// </summary>
        public void FillModalParticipatingMassRatios()
        {
            _modalParticipatingMassRatios = GetModalParticipatingMassRatios(_apiAnalysisResults);
        }

        /// <summary>
        /// Fills the modal participation factors.
        /// </summary>
        public void FillModalParticipationFactors()
        {
            _modalParticipationFactors = GetModalParticipationFactors(_apiAnalysisResults);
        }

        /// <summary>
        /// Fills the modal periods.
        /// </summary>
        public void FillModalPeriods()
        {
            _modalPeriods = GetModalPeriods(_apiAnalysisResults);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the buckling factors.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, System.Double&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, double>> GetBucklingFactors(IResults app)
        {
            app.BucklingFactor(
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
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio&gt;&gt;.</returns>
        public static List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> GetModalLoadParticipationRatios(
            IResults app)
        {
            app.ModalLoadParticipationRatios(
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
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalParticipatingMassRatio&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> GetModalParticipatingMassRatios(IResults app)
        {
            app.ModalParticipatingMassRatios(
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
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalParticipationFactor&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> GetModalParticipationFactors(IResults app)
        {
            app.ModalParticipationFactors(
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
        /// <param name="app">The application.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, ModalPeriod&gt;&gt;.</returns>
        public static List<Tuple<StepResultsIdentifier, ModalPeriod>> GetModalPeriods(IResults app)
        {
            app.ModalPeriod(
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
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing generalized displacement for which results are returned.
        /// If the program does not recognize this name as a defined generalized displacement, it returns results for all selected generalized displacements, if any.
        /// For example, entering a blank string (i.e., "") for the name will prompt the program to return results for all selected generalized displacementse.</param>
        /// <returns>List&lt;Tuple&lt;StepResultsIdentifier, GeneralizedDisplacementResult&gt;&gt;.</returns>
        public static List<Tuple<GeneralizedDisplacementResultsIdentifier, GeneralizedDisplacementResult>> GetGeneralizedDisplacement(
            IResults app, 
            string name)
        {
            app.GeneralizedDisplacement(name,
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
        #endregion
    }
}
