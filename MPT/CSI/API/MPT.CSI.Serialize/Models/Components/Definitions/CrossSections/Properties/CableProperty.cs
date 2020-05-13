
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties
{
    /// <summary>
    /// Class TendonProperties.
    /// </summary>
    public class CableProperty : ObjectProperties
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the material used.
        /// </summary>
        /// <value>The name of the material.</value>
        internal string MaterialName { get; set; }

        /// <summary>
        /// The material overwrite
        /// </summary>
        protected Material _material;
        /// <summary>
        /// The material overwrite assigned to the object.
        /// This overwrites the material used in the cross-section.
        /// </summary>
        /// <value>The material overwrite.</value>
        public virtual Material Material => _material ?? (_material = null);

        public eCableSpecification CableSpecification { get; internal set; }
        public virtual double Area { get; internal set; }
        public virtual double Diameter { get; internal set; }

        /// <summary>
        /// Gets or sets the frame modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public virtual FrameModifier Modifiers { get; internal set; } = new FrameModifier();
        #endregion

        #region Initialization
        internal static CableProperty Factory(string name)
        {
            CableProperty property = new CableProperty(name);
            return property;
        }

        protected CableProperty(string name) : base(name)
        {
        }
        #endregion
    }
}