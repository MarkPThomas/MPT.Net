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
    /// <seealso cref="SlabExtended" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
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
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected SlabExtended(ApiCSiApplication app, string name) : base(app, name) { }

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
    public abstract class SlabExtended : ApiPropertyObject<SlabExtendedProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabExtended" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected SlabExtended(ApiCSiApplication app, string name) : base(app, name)
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
