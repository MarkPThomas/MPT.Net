using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class PrecastUSection : FrameSection<TubeSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static PrecastUSection Factory(
            Materials.Materials material,
            string uniqueName,
            PrecastUSectionProperties properties = null)
        {
            PrecastUSection frameSection = new PrecastUSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected PrecastUSection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.PreCastConcreteGirderU) : base(material, name, type)
        {

        }
        #endregion

    }
}