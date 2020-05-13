#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    public class SolidResults : AnalysisResults
    {
#region Fields & Properties        
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }
#endregion

#region Initialization

        public SolidResults(ApiCSiApplication app, string name) : base(app)
        {
            ObjectName = name;
        }
#endregion

#region Fill
        public override void FillResults()
        {

        }

        public override void EmptyResults()
        {

        }
#endregion

#region Static

#endregion
    }
}
#endif