using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
    public class CompositeBeamDesignResults : DesignResults
    {
        public List<CompositeBeamResultsSummary> CompositeBeamResultsSummary;


        public CompositeBeamDesignResults(string name)
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


        public void FillDesignResults()
        {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            CompositeBeamResultsSummary = GetSummaryResults(Name);
#endif
        }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public override void FillResultsAreAvailable()
        {
            resultsAreAvailable(_designer.DesignCompositeBeam);
            if (ResultsAreAvailable) return;

            CompositeBeamResultsSummary.Clear();
        }

        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<CompositeBeamResultsSummary> GetSummaryResults(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignCompositeBeam.GetSummaryResults(name,
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
    }
#endif
}
