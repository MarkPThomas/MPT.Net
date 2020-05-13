using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    public class CableProperties : ObjectLists<CableProperty>
    {
        protected override CableProperty fillNewItem(string uniqueName)
        {
            return CableProperty.Factory(uniqueName);
        }
    }
}
