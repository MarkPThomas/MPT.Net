using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class ColdCSection : FrameSection<ColdCSectionProperties>
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
        internal static ColdCSection Factory(
            Materials.Materials material,
            string uniqueName,
            ColdCSectionProperties properties = null)
        {
            ColdCSection frameSection = new ColdCSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected ColdCSection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.ColdC) : base(material, name, type)
        {

        }
        #endregion
    }
}
