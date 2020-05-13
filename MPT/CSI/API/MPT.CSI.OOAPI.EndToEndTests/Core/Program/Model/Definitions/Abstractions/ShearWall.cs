using System.Collections.Generic;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    public class ShearWall
    {
        public List<Pier> Piers { get; protected set; }
        public List<Spandrel> Spandrels { get; protected set; }
        public List<Area> Areas { get; protected set; }
        public List<Frame> Frames { get; protected set; }

        // TODO: Add methods for adding areas and frames for the added piers & spandrels

    }
}
