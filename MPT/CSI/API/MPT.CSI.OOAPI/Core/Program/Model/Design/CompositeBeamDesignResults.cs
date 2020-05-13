// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="CompositeBeamDesignResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class CompositeBeamDesignResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.DesignResults" />
    public sealed class CompositeBeamDesignResults : DesignResults
    {
        #region Static
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;CompositeBeamResultsSummary&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<CompositeBeamResultsSummary> GetSummaryResults(
            IDesigner app,
            string name,
            eItemType itemType = eItemType.Object)
        {
            app.DesignCompositeBeam.GetSummaryResults(name,
                out var designSections,
                out var beamFy,
                out var studDiameters,
                out var studLayouts,
                out var isBeamShored,
                out var beamCambers,
                out var passFail,
                out var reactionsLeft,
                out var reactionsRight,
                out var MMaxNegative,
                out var MMaxPositive,
                out var percentCompositeConnection,
                out var overallRatios,
                out var studRatios,
                out var strengthRatiosPM,
                out var constructionRatiosPM,
                out var strengthShearRatios,
                out var constructionShearRatios,
                out var deflectionRatiosPostConcreteDL,
                out var deflectionRatiosSDL,
                out var deflectionRatiosLL,
                out var deflectionRatiosTotalCamber,
                out var frequencyRatios,
                out var MDampingRatios);

            List<CompositeBeamResultsSummary> results = new List<CompositeBeamResultsSummary>();
            for (int i = 0; i < designSections.Length; i++)
            {
                CompositeBeamResultsSummary result = new CompositeBeamResultsSummary()
                {
                    DesignSection = designSections[i],
                    BeamFy = beamFy[i],
                    StudDiameter = studDiameters[i],
                    StudLayout = studLayouts[i],
                    IsBeamShored = isBeamShored[i],
                    BeamCambers = beamCambers[i],
                    PassFail = passFail[i],
                    ReactionLeft = reactionsLeft[i],
                    ReactionRight = reactionsRight[i],
                    MMaxNegative = MMaxNegative[i],
                    MMaxPositive = MMaxPositive[i],
                    PercentCompositeConnection = percentCompositeConnection[i],
                    OverallRatio = overallRatios[i],
                    StudRatio = studRatios[i],
                    StrengthRatioPM = strengthRatiosPM[i],
                    ConstructionRatioPM = constructionRatiosPM[i],
                    StrengthShearRatio = strengthShearRatios[i],
                    ConstructionShearRatio = constructionShearRatios[i],
                    DeflectionRatioPostConcreteDL = deflectionRatiosPostConcreteDL[i],
                    DeflectionRatioSDL = deflectionRatiosSDL[i],
                    DeflectionRatioLL = deflectionRatiosLL[i],
                    DeflectionRatioTotalCamber = deflectionRatiosTotalCamber[i],
                    FrequencyRatio = frequencyRatios[i],
                    MDampingRatio = MDampingRatios[i]
                };
                results.Add(result);
            }

            return results;
        }
        #endregion

        #region Fields & Properties

        /// <summary>
        /// The composite beam results summary
        /// </summary>
        List<CompositeBeamResultsSummary> _compositeBeamResultsSummary;

        /// <summary>
        /// Gets the composite beam results summary.
        /// </summary>
        /// <value>The composite beam results summary.</value>
        public List<CompositeBeamResultsSummary> CompositeBeamResultsSummary =>
            _compositeBeamResultsSummary ?? (_compositeBeamResultsSummary = GetSummaryResults(_designer, Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeBeamDesignResults"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public CompositeBeamDesignResults(ApiCSiApplication app, string name) : base(app)
        {
            Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillResultsAreAvailable();
        }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the design results.
        /// </summary>
        public void FillDesignResults()
        {
            _compositeBeamResultsSummary = GetSummaryResults(_designer, Name);
        }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public override void FillResultsAreAvailable()
        {
            resultsAreAvailable(_designer.DesignCompositeBeam);
            if (ResultsAreAvailable) return;

            _compositeBeamResultsSummary?.Clear();
        }
        #endregion
    }
}
#endif
