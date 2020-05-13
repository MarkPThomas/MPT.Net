using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions.ResponseSpectrum
{
    public class UBC97Spectrum : ResponseSpectrumCodeFunctions<UBC97SpectrumProperties>
    {
        // TODO: This is a convenience function that may not be needed. If only the properties change, then this is not needed.
        protected UBC97Spectrum(string name) : base(name)
        {
        }
    }
}
