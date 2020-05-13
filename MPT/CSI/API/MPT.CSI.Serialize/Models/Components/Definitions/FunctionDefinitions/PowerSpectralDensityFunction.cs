using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class PowerSpectralDensityFunction : Function, IFunctionCurve<FrequencyValuePoint>
    {
        #region Fields & Properties
        public FunctionCurve<FrequencyValuePoint> FunctionCurve { get; } = new FunctionCurve<FrequencyValuePoint>();

        #endregion

        #region Initialization
        internal static PowerSpectralDensityFunction Factory(string uniqueName)
        {
            return new PowerSpectralDensityFunction(uniqueName);
        }

        internal PowerSpectralDensityFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
