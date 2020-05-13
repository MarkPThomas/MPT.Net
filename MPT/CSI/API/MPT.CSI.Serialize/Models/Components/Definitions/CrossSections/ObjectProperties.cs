using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    public abstract class ObjectProperties : UniqueName
    {
        /// <summary>
        /// Gets the name of the color.
        /// </summary>
        /// <value>The name of the color.</value>
        public virtual string ColorName { get; internal set; }

        /// <summary>
        /// The display color assigned to the section.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; internal set; }

        /// <summary>
        /// The notes, if any, assigned to the section.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; internal set; }

        protected ObjectProperties(string name) : base(name)
        {
        }
    }
}
