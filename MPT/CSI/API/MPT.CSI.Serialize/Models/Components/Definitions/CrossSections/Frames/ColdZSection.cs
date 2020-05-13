using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class ColdZSection : FrameSection<ColdZSectionProperties>
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
        internal static ColdZSection Factory(
            Materials.Materials material,   
            string uniqueName,
            ColdZSectionProperties properties = null)
        {
            ColdZSection frameSection = new ColdZSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected ColdZSection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.ColdZ) : base(material, name, type)
        {

        }
        #endregion
    }
}
