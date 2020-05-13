using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model
{
    internal class StructureComponentsProperties<T> where T : CrossSection
    {
        public Points Points { get; set; }
        public ObjectLists<T> CrossSections { get; set; }
        public Materials Materials { get; set; }
        public Piers Piers { get; set; }
        public Spandrels Spandrels { get; set; }
    }
}
