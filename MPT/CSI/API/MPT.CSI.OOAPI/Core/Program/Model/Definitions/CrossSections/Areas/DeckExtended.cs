
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class SlabExtended.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DeckExtended" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
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
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DeckExtended(ApiCSiApplication app, string name) : base(app, name)
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
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public abstract class DeckExtended : ApiPropertyObject<DeckExtendedProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckExtended" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DeckExtended(ApiCSiApplication app, string name) : base(app, name)
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
