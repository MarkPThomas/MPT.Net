using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    public class TendonProperties : ObjectLists<TendonProperty>
    {
        protected override TendonProperty fillNewItem(string uniqueName)
        {
            return TendonProperty.Factory(uniqueName);
        }
    }
}
