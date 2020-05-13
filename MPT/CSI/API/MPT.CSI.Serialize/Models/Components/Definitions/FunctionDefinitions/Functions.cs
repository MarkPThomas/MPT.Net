using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class Functions : ObjectLists<Function>
    {
        public static eFunctionTypes FunctionType { get; set; }
        protected override Function fillNewItem(string uniqueName)
        {
            return Function.Factory(uniqueName, FunctionType);
        }
    }
}
