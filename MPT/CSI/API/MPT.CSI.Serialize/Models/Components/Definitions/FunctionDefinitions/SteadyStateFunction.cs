using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class SteadyStateFunction : Function, IFunctionCurve<FrequencyValuePoint>
    {
        #region Fields & Properties
        public FunctionCurve<FrequencyValuePoint> FunctionCurve { get; } = new FunctionCurve<FrequencyValuePoint>();

        #endregion

        #region Initialization
        internal static SteadyStateFunction Factory(string uniqueName)
        {
            return new SteadyStateFunction(uniqueName);
        }

        internal SteadyStateFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
