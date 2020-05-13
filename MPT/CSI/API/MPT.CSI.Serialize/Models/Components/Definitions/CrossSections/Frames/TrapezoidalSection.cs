using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class TrapezoidalSection : FrameSection<TrapezoidalSectionProperties>
    {
        public TrapezoidalSection(Materials.Materials material, string name, eFrameSectionType type = eFrameSectionType.Trapezoidal) : base(material, name, type)
        {
        }
    }
}
