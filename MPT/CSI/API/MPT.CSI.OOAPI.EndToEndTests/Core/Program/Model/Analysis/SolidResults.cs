namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    public class SolidResults : Analysis.AnalysisResults
    {
        public string ObjectName { get; protected set; }


        public SolidResults(string name)
        {
            ObjectName = name;
        }

        public override void FillResults()
        {

        }

        public override void EmptyResults()
        {

        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
#endif
    }
}
