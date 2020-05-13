using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public class SteelDesignResults : DesignResults
    {

        public List<SteelResultsSummary> SteelResultsSummary { get; protected set; }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public List<SteelResultsDetailedText> SteelResultsDetailedText { get; protected set; }
        public List<SteelResultsDetailedNumeric> SteelResultsDetailedNumeric { get; protected set; }
#else
        public List<SteelResultsSummaryExpanded> SteelResultsSummaryExpanded { get; protected set; }
#endif

        public SteelDesignResults(string name)
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
            SteelResultsSummary = GetSummaryResults(Name);
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            SteelResultsDetailedText = GetSteelResultsDetailedText(Name);
            SteelResultsDetailedNumeric = GetSteelResultsDetailedNumeric(Name);
#else
            SteelResultsSummaryExpanded = GetSummaryResultsExpanded(Name);
#endif
        }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public override void FillResultsAreAvailable()
        {
            resultsAreAvailable(_designer.DesignSteel);
            if (ResultsAreAvailable) return;

            SteelResultsSummary.Clear();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            SteelResultsDetailedText.Clear();
            SteelResultsDetailedNumeric.Clear();
#else
            SteelResultsSummaryExpanded.Clear();
#endif
        }

        // === Get ===
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<SteelResultsSummary> GetSummaryResults(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignSteel.GetSummaryResults(name,
                out var frameNames,
                out var ratios,
                out var ratioTypes,
                out var locations,
                out var comboNames,
                out var errorSummaries,
                out var warningSummaries);

            List<SteelResultsSummary> results = new List<SteelResultsSummary>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                SteelResultsSummary result = new SteelResultsSummary()
                {
                    FrameName = frameNames[i],
                    Ratio = ratios[i],
                    RatioType = ratioTypes[i],
                    Location = locations[i],
                    ComboName = comboNames[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }

#if BUILD_ETABS2017
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<SteelResultsSummaryExpanded> GetSummaryResultsExpanded(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignSteel.GetSummaryResults(name,
                out var frameTypes,
                out var designSections,
                out var status,
                out var PMMCombo,
                out var PMMRatio,
                out var PRatio,
                out var MMajorRatio,
                out var MMinorRatio,
                out var VMajorCombo,
                out var VMajorRatio,
                out var VMinorCombo,
                out var VMinorRatio);

            List<SteelResultsSummaryExpanded> results = new List<SteelResultsSummaryExpanded>();
            for (int i = 0; i < frameTypes.Length; i++)
            {
                SteelResultsSummaryExpanded result = new SteelResultsSummaryExpanded()
                {
                    FrameType = frameTypes[i],
                    DesignSection = designSections[i],
                    Status = status[i],
                    PMMCombo = PMMCombo[i],
                    PMMRatio = PMMRatio[i],
                    PRatio = PRatio[i],
                    MMajorRatio = MMajorRatio[i],
                    MMinorRatio = MMinorRatio[i],
                    VMajorCombo = VMajorCombo[i],
                    VMajorRatio = VMajorRatio[i],
                    VMinorCombo = VMinorCombo[i],
                    VMinorRatio = VMinorRatio[i]
                };
                results.Add(result);
            }

            return results;
        }
#endif

        // === Local ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Note that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="name">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with TEXT output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<SteelResultsDetailedText> GetDetailedResultsText(string name,
                                           int table,
                                           string field, 
                                           eItemType itemType = eItemType.Object)
        {
            _designer.DesignSteel.GetDetailedResultsText(name, table, field,
                out var frameNames,
                out var textResults,
                itemType);

            List<SteelResultsDetailedText> results = new List<SteelResultsDetailedText>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                SteelResultsDetailedText result = new SteelResultsDetailedText()
                {
                    FrameName = frameNames[i],
                    Result = textResults[i],
                    TableNumber = table,
                    Field = field
                };
                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Note that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with Numerical output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<SteelResultsDetailedNumeric> GetDetailedResultsNumerical(string itemName,
                                           int table,
                                           string field, 
                                           eItemType itemType = eItemType.Object)
        {
            _designer.DesignSteel.GetDetailedResultsText(name, table, field,
                out var frameNames,
                out var numericalResults,
                itemType);

            List<SteelResultsDetailedNumeric> results = new List<SteelResultsDetailedNumeric>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                SteelResultsDetailedNumeric result = new SteelResultsDetailedNumeric()
                {
                    FrameName = frameNames[i],
                    Result = numericalResults[i],
                    TableNumber = table,
                    Field = field
                };
                results.Add(result);
            }

            return results;
        }
#endif
    }
}
