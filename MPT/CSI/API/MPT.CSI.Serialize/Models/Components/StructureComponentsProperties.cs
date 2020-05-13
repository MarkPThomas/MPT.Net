// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-13-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-14-2019
// ***********************************************************************
// <copyright file="StructureComponentsProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureComponentsProperties.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class StructureComponentsProperties<T> where T : ObjectProperties
    {
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public Points Points { get; set; }
        /// <summary>
        /// Gets or sets the cross sections.
        /// </summary>
        /// <value>The cross sections.</value>
        public ObjectLists<T> CrossSections { get; set; }
        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        /// <value>The materials.</value>
        public Materials Materials { get; set; }
        /// <summary>
        /// Gets or sets the piers.
        /// </summary>
        /// <value>The piers.</value>
        public Piers Piers { get; set; }
        /// <summary>
        /// Gets or sets the spandrels.
        /// </summary>
        /// <value>The spandrels.</value>
        public Spandrels Spandrels { get; set; }
    }
}
