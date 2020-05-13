using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    public abstract class AutoLoadPattern
    {
        public string Name { get; protected set; }
        public eLoadPatternType Type { get; protected set; }

        protected AutoLoadPattern(string name)
        {
            Name = name;
        }
    }
}
