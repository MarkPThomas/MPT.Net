using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class HybridISection : FrameSection<HybridISectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static HybridISection Factory(
            Materials.Materials material,
            string uniqueName,
            HybridISectionProperties properties = null)
        {
            HybridISection frameSection = new HybridISection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected HybridISection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.BuiltUpIHybrid) : base(material, name, type)
        {

        }
        #endregion
    }
}
