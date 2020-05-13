using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    public class LoadPatternAssignment
    {
        public LoadPattern Pattern { get; set; }
        public StructureObject Element { get; set; }
    }
}
