using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    public class Constraints : ObjectLists<Constraint>
    {
        protected override Constraint fillNewItem(string uniqueName)
        {
            throw new NotImplementedException();
        }
    }
}
