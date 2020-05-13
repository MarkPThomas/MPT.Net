using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class CompositeBeamDesignOverwrite : DesignOverwrites, IFrameDesign
    {
        /// <summary>
        /// The design section for the selected frame objects.When this overwrite is applied, any previous auto select section assigned to the frame object is removed.
        /// Program determined/null value means it is taken from the analysis section.
        /// </summary>
        /// <value>The design section.</value>
        public virtual FrameSection DesignSection { get; set; }
    }
}
