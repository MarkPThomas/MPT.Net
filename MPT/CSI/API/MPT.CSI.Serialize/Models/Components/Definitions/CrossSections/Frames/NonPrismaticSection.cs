using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class NonPrismaticSection : FrameSection<NonPrismaticSectionProperties>
    {
        public NonPrismaticSection(Materials.Materials material, string name, eFrameSectionType type = eFrameSectionType.Variable) : base(material, name, type)
        {
        }
    }
}
