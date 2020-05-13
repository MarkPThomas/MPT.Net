using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class SlabExtended.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DeckExtended" />
    public abstract class DeckExtended<T> : DeckExtended where T : DeckExtendedProperties
    {
        /// <summary>
        /// The extended section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public new T Properties => (T)_properties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckExtended{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected DeckExtended(string name) : base(name)
        {
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public virtual void Set(T properties)
        {
            base.set(properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected virtual void set(T properties)
        {
            base.set(properties);
        }
    }

    /// <summary>
    /// Class SlabExtended.
    /// </summary>
    public abstract class DeckExtended : ModelPropertyObject<DeckExtendedProperties>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckExtended" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected DeckExtended(string name) : base(name)
        {
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(DeckExtendedProperties properties)
        {
            set(properties);
        }
    }
}
