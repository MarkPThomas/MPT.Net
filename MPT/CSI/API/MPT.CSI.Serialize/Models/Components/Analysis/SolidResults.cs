namespace MPT.CSI.Serialize.Models.Components.Analysis
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

        public SolidResults(string name) 
        {
            ObjectName = name;
        }
        #endregion

        #region Fill
        public override void EmptyResults()
        {

        }
        #endregion
    }
}