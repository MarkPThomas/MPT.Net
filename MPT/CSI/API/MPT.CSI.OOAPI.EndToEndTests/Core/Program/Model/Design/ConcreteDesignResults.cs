using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public class ConcreteDesignResults : DesignResults
    {
        public List<ConcreteResultsSummaryBeam> ConcreteResultsSummaryBeam { get; protected set; }
        public List<ConcreteResultsSummaryColumn> ConcreteResultsSummaryColumn { get; protected set; }
        public List<ConcreteResultsSummaryJoint> ConcreteResultsSummaryJoint { get; protected set; }

        
        public ConcreteDesignResults(string name)
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
            ConcreteResultsSummaryBeam = GetSummaryResultsBeam(Name);
            ConcreteResultsSummaryColumn = GetSummaryResultsColumn(Name);
            ConcreteResultsSummaryJoint = GetSummaryResultsJoint(Name);
        }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public override void FillResultsAreAvailable()
        {
            resultsAreAvailable(_designer.DesignConcrete);
            if (ResultsAreAvailable) return;

            ConcreteResultsSummaryBeam.Clear();
            ConcreteResultsSummaryColumn.Clear();
            ConcreteResultsSummaryJoint.Clear();
        }

        /// <summary>
        /// Retrieves summary results for concrete design of beams.
        /// Torsion results are not included for all codes.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryBeam> GetSummaryResultsBeam(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignConcrete.GetSummaryResultsBeam(name,
                out var frameNames,
                out var location,
                out var topCombo,
                out var topArea,
                out var botCombo,
                out var botArea,
                out var VMajorCombo,
                out var VMajorArea,
                out var TLCombo,
                out var TLArea,
                out var TTCombo,
                out var TTArea,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryBeam> results = new List<ConcreteResultsSummaryBeam>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryBeam result = new ConcreteResultsSummaryBeam()
                {
                    FrameName = frameNames[i],
                    Location = location[i],
                    TopCombo = topCombo[i],
                    TopArea = topArea[i],
                    BottomCombo = botCombo[i],
                    BottomArea = botArea[i],
                    VMajorCombo = VMajorCombo[i],
                    VMajorArea = VMajorArea[i],
                    TLCombo = TLCombo[i],
                    TLArea = TLArea[i],
                    TTCombo = TTCombo[i],
                    TTArea = TTArea[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Retrieves summary results for concrete design of columns.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryColumn> GetSummaryResultsColumn(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignConcrete.GetSummaryResultsColumn(name,
                out var frameNames,
                out var designOption,
                out var locations,
                out var PMMCombo,
                out var PMMArea,
                out var PMMRatio,
                out var VMajorCombo,
                out var AVMajor,
                out var VMinorCombo,
                out var AVMinor,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryColumn> results = new List<ConcreteResultsSummaryColumn>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryColumn result = new ConcreteResultsSummaryColumn()
                {
                    FrameName = frameNames[i],
                    Location = locations[i],
                    DesignOption = designOption[i],
                    PMMCombo = PMMCombo[i],
                    PMMArea = PMMArea[i],
                    PMMRatio = PMMRatio[i],
                    VMajorCombo = VMajorCombo[i],
                    AVMajor = AVMajor[i],
                    VMinorCombo = VMinorCombo[i],
                    AVMinor = AVMinor[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Retrieves summary results for concrete design of joints.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryJoint> GetSummaryResultsJoint(string name, eItemType itemType = eItemType.Object)
        {
            _designer.DesignConcrete.GetSummaryResultsJoint(name,
                out var frameNames,
                out var LCJSRatioMajor,
                out var JSRatioMajor,
                out var LCJSRatioMinor,
                out var JSRatioMinor,
                out var LCBCCRatioMajor,
                out var BCCRatioMajor,
                out var LCBCCRatioMinor,
                out var BCCRatioMinor,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryJoint> results = new List<ConcreteResultsSummaryJoint>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryJoint result = new ConcreteResultsSummaryJoint()
                {
                    FrameName = frameNames[i],
                    JointShearRatioMajorCombo = LCJSRatioMajor[i],
                    JointShearRatioMajor = JSRatioMajor[i],
                    JointShearRatioMinorCombo = LCJSRatioMinor[i],
                    JointShearRatioMinor = JSRatioMinor[i],
                    BCCRatioMajorCombo = LCBCCRatioMajor[i],
                    BCCRatioMajor = BCCRatioMajor[i],
                    BCCRatioMinorCombo = LCBCCRatioMinor[i],
                    BCCRatioMinor = BCCRatioMinor[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }
    }
}
