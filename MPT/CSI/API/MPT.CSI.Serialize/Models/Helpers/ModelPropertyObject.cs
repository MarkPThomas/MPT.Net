// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ModelPropertyObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// This represents properties that are sub-objects of main program objects.
    /// Derivates should have minimal properties beyond those derived from <see cref="ModelProperty" /> and ideally be set up for lazy initialization from the parent object property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ModelPropertyObject" />
    public abstract class ModelPropertyObject<T> : ModelPropertyObject where T : ModelProperty
    {

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public new T Properties => (T)_properties.Clone();


        /// <summary>
        /// Initializes a new instance of the <see cref="ModelPropertyObject{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ModelPropertyObject(string name) : base(name)
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
    /// Class ModelPropertyObject.
    /// </summary>
    /// <seealso cref="ModelPropertyObject" />
    public abstract class ModelPropertyObject 
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties
        /// </summary>
        protected ModelProperty _properties;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public ModelProperty Properties => (ModelProperty)_properties.Clone();

        /// <summary>
        /// The name of an existing parent property.
        /// </summary>
        /// <value>The name.</value>
        internal string Name { get; set; }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ModelPropertyObject"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ModelPropertyObject(string name) 
        {
            Name = name;
        }
    }
}
