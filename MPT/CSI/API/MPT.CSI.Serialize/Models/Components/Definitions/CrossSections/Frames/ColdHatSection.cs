using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class ColdHatSection : FrameSection<ColdHatSectionProperties>
    {
        #region Fields & Properties

        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static ColdHatSection Factory(
            Materials.Materials material,
            string uniqueName,
            ColdHatSectionProperties properties = null)
        {
            ColdHatSection frameSection = new ColdHatSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected ColdHatSection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.ColdHat) : base(material, name, type)
        {

        }
        #endregion
    }
}
