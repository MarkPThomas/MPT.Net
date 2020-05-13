namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Object contains a function curve consisting of the provided point type.
    /// </summary>
    /// <typeparam name="T">Point type used in the function curve.</typeparam>
    public interface IFunctionCurve<T> where T: FunctionPoint
    {
        FunctionCurve<T> FunctionCurve { get; }
    }
}
