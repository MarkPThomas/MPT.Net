using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class AutoSelectSection : FrameSection<AutoSelectProperties>
    {
        public AutoSelectSection(Materials.Materials material, string name, eFrameSectionType type = eFrameSectionType.Auto) : base(material, name, type)
        {
        }
    }
}
