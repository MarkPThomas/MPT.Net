// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="SlabExtended.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class SlabExtended.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SlabExtended" />
    public abstract class SlabExtended<T> : SlabExtended where T : SlabExtendedProperties
    {
        /// <summary>
        /// The extended section properties associated with the section.
        /// </summary>
        /// <value>The section properties.</value>
        public new T Properties => (T)_properties.Clone();

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabExtended{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected SlabExtended(string name) : base(name) { }

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
    public abstract class SlabExtended : ModelPropertyObject<SlabExtendedProperties>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlabExtended" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected SlabExtended(string name) : base(name)
        {
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(SlabExtendedProperties properties)
        {
            set(properties);
        }
    }
}
