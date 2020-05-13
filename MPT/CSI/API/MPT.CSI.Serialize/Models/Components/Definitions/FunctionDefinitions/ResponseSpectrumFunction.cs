namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class ResponseSpectrumFunction : Function
    {
        #region Fields & Properties

        public double FunctionDampingRatio { get; set; }

        #endregion

        #region Initialization

        internal ResponseSpectrumFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
