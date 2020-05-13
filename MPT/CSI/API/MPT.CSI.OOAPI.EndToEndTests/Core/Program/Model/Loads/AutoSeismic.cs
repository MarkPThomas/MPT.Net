using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    public class AutoSeismic : AutoLoadPattern
    {
        public AutoSeismic(string name) : base(name)
        {
            Type = eLoadPatternType.Quake;
        }
    }
}
