using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class HybridUSection : FrameSection<HybridUSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static HybridUSection Factory(
            Materials.Materials material,
            string uniqueName,
            HybridUSectionProperties properties = null)
        {
            HybridUSection frameSection = new HybridUSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected HybridUSection(
            Materials.Materials material, 
            string name, 
            eFrameSectionType type = eFrameSectionType.BuiltUpUHybrid) : base(material, name, type)
        {

        }
        #endregion
    }
}
