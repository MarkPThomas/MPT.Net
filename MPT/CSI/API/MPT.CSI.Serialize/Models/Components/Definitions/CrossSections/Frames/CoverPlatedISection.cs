// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="CoverPlatedISection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class CoverPlatedISection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class CoverPlatedISection : FrameSection<CoverPlatedISectionProperties>
    {   // TODO: Refactor CoverPlatedISectionProperties as a composite section to remove base material property
        #region Fields & Properties

        /// <summary>
        /// The frame sections.
        /// </summary>
        protected readonly FrameSections _frameSections;

        /// <summary>
        /// The existing I-type frame section property that is used for the I-section portion of the coverplated I section.
        /// </summary>
        /// <value>The section.</value>
        internal virtual string SectionName
        {
            get => SectionProperties.SectionName;
            set => SectionProperties.SectionName = value;
        }
        /// <summary>
        /// The section
        /// </summary>
        protected FrameSection _section;
        /// <summary>
        /// The existing I-type frame section property that is used for the I-section portion of the coverplated I section.
        /// </summary>
        /// <value>The section.</value>
        public virtual ISection Section
        {
            get
            {
                if (_section != null) return (ISection)_section;
                
                if (_section is ISection section) return section;
                return null;
            }
        }

        /// <summary>
        /// The material property for the top cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcTop" /> and the <see cref="CoverPlatedISectionProperties.bcTop" /> items are greater than 0.
        /// </summary>
        /// <value>The material top.</value>
        internal virtual string MaterialNameTop
        {
            get => SectionProperties.MaterialNameTop;
            set => SectionProperties.MaterialNameTop = value;
        }
        /// <summary>
        /// The material
        /// </summary>
        protected Material _materialTop;
        /// <summary>
        /// The material property for the top cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcTop" /> and the <see cref="CoverPlatedISectionProperties.bcTop" /> items are greater than 0.
        /// </summary>
        /// <value>The material top.</value>
        public virtual Material MaterialTop => _materialTop ??
                                       (_materialTop = null);

        /// <summary>
        /// The material property for the bottom cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcBottom" /> and the <see cref="CoverPlatedISectionProperties.bcBottom" /> items are greater than 0.
        /// </summary>
        /// <value>The material bottom.</value>
        internal string MaterialNameBottom
        {
            get => SectionProperties.MaterialNameBottom;
            set => SectionProperties.MaterialNameBottom = value;
        }
        /// <summary>
        /// The material
        /// </summary>
        protected Material _materialBottom;
        /// <summary>
        /// The material property for the bottom cover plate.
        /// This item applies only if both the <see cref="CoverPlatedISectionProperties.tcBottom" /> and the <see cref="CoverPlatedISectionProperties.bcBottom" /> items are greater than 0.
        /// </summary>
        /// <value>The material bottom.</value>
        public Material MaterialBottom => _materialBottom ??
                                          (_materialBottom = null);
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="frameSections">The frame sections.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>TeeSection.</returns>
        internal static CoverPlatedISection Factory(
            Materials.Materials material,
            FrameSections frameSections,
            string uniqueName, 
            CoverPlatedISectionProperties properties = null)
        {
            CoverPlatedISection frameSection = new CoverPlatedISection(material, frameSections, uniqueName)
                                                    { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverPlatedISection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="frameSections">The frame sections.</param>
        /// <param name="name">The name.</param>
        protected CoverPlatedISection(
            Materials.Materials material,
            FrameSections frameSections,
            string name) : base(material, name, eFrameSectionType.BuiltUpICoverPlate)
        {
            _frameSections = frameSections;
        }
        #endregion
    }
}
