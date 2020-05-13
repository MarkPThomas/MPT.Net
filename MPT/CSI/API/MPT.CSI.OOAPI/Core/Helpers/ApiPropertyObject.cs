// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ApiPropertyObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Helpers
{
    /// <summary>
    /// This represents properties that are sub-objects of main program objects.
    /// Typically derivates only have a single set of fill, add, and set methods that interact with the API.
    /// Derivates should have minimal properties beyond those derived from <see cref="ApiProperty" /> and ideally be set up for lazy initialization from the parent object property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiPropertyObject" />
    public abstract class ApiPropertyObject<T> : ApiPropertyObject where T : ApiProperty
    {

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public new T Properties => (T)_properties.Clone();


        /// <summary>
        /// Initializes a new instance of the <see cref="ApiPropertyObject{T}"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ApiPropertyObject(ApiCSiApplication app, string name) : base(app, name)
        {
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public abstract void Set(T properties);

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        protected virtual void set(T properties)
        {
            _properties = properties;
        }
    }


    /// <summary>
    /// Class ApiPropertyObject.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiPropertyObject" />
    public abstract class ApiPropertyObject : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties
        /// </summary>
        protected ApiProperty _properties;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public ApiProperty Properties => (ApiProperty)_properties.Clone();

        /// <summary>
        /// The name of an existing parent property.
        /// </summary>
        /// <value>The name.</value>
        internal string Name { get; set; }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ApiPropertyObject"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ApiPropertyObject(ApiCSiApplication app, string name) : base(app)
        {
            Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public virtual void FillData()
        {
            Fill();
        }

        /// <summary>
        /// Fills this instance.
        /// </summary>
        public abstract void Fill();
    }
}
