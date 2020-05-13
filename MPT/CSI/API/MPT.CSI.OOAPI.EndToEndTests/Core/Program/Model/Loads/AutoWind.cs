using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    public class AutoWind : AutoLoadPattern
    {
        public AutoWind(string name) : base(name)
        {
          Type = eLoadPatternType.Wind;
        }
    }
}
