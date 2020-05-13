// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="Link.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Link.
    /// </summary>
    /// <seealso cref="StructureObject2D{LinkProperties}" />
    public class Link : StructureObject2D<LinkProperty>
    { // TODO: Finish Link
        #region Fields & Properties

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public virtual LinkResults Results { get; internal set; }

        /// <summary>
        /// The properties associated with the link.
        /// </summary>
        /// <value>The section.</value>
        public virtual LinkProperty Section { get; internal set; }

        protected string _sectionNameFrequencyDependent;

        /// <summary>
        /// Gets or sets the section name of the frequency dependent link assigned.
        /// </summary>
        /// <value>The section name frequency dependent.</value>
        internal string SectionNameFrequencyDependent
        {
            get => string.IsNullOrEmpty(_sectionNameFrequencyDependent)
                ? Constants.NONE
                : _sectionNameFrequencyDependent;
            set => _sectionNameFrequencyDependent = value;
        }

        /// <summary>
        /// Analysis modifier for the assigned property.
        /// </summary>
        /// <value>The property modifier.</value>
        public virtual double PropertyModifier { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        internal static Link Factory(
            StructureComponentsProperties<LinkProperty> componentsProperties, 
            string uniqueName)
        {
            Link item = new Link(
                componentsProperties,
                uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Link(
            StructureComponentsProperties<LinkProperty> componentsProperties,
            string name) : base(componentsProperties,
            name)
        { }
        #endregion
    }
}
