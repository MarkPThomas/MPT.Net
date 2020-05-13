using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public class PrecastISection : FrameSection<TubeSectionProperties>
    {
        #region Initialization
        /// <summary>
        /// Returns a new cross-section.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Unique name.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TubeSection.</returns>
        internal static PrecastISection Factory(
            Materials.Materials material,
            string uniqueName,
            PrecastISectionProperties properties = null)
        {
            PrecastISection frameSection = new PrecastISection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        protected PrecastISection(
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.PreCastConcreteGirderI) : base(material, name, type)
        {

        }
        #endregion
    }
}