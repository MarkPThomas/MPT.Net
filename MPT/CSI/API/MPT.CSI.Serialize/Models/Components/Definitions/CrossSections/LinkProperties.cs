using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    public class LinkProperties : ObjectLists<LinkProperty>
    {
        protected override LinkProperty fillNewItem(string uniqueName)
        {
            return LinkProperty.Factory(uniqueName);
        }
    }
}
