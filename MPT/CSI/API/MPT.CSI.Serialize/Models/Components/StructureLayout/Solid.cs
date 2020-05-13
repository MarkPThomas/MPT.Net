// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-13-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="Solid.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Solid.
    /// </summary>
    /// <seealso cref="StructureObject2D{SolidProperties}" />
    public class Solid : StructureObject2D<SolidProperty>
    {
        #region Fields & Properties
        /// <summary>
        /// The properties associated with the solid.
        /// </summary>
        /// <value>The section.</value>
        public virtual SolidProperty Section { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Solid Factory(
            StructureComponentsProperties<SolidProperty> componentsProperties, 
            string uniqueName)
        {
            Solid item = new Solid(
                componentsProperties,
                uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solid"/> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Solid(
            StructureComponentsProperties<SolidProperty> componentsProperties,
            string name) : base(componentsProperties,
            name)
        { }
        #endregion
    }
}
