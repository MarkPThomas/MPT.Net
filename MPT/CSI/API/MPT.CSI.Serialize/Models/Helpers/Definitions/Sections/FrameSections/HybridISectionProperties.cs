namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    public class HybridISectionProperties : FrameSectionProperties
    {
        /// <summary>
        /// Total height. [L]
        /// </summary>
        /// <value>The h total.</value>
        public double hTotal { get; set; }

        /// <summary>
        /// The web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; set; }

        /// <summary>
        /// The thickness of the top cover plate. [L]
        /// If the <see cref="tcTop" /> or the <see cref="bcTop" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The tcTop.</value>
        public double tcTop { get; set; }

        /// <summary>
        /// The width of the top cover plate. [L]
        /// If the <see cref="tcTop" /> or the <see cref="bcTop" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The bcTop.</value>
        public double bcTop { get; set; }


        /// <summary>
        /// The thickness of the bottom cover plate. [L]
        /// If the <see cref="tcBottom" /> or the <see cref="bcBottom" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The tc bottom.</value>
        public double tcBottom { get; set; }

        /// <summary>
        /// The width of the bottom cover plate. [L]
        /// If the <see cref="tcBottom" /> or the <see cref="bcBottom" /> item is less than or equal to 0, no top cover plate exists.
        /// </summary>
        /// <value>The bc bottom.</value>
        public double bcBottom { get; set; }
    }
}
