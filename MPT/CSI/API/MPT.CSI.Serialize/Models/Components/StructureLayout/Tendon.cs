// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-13-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="Tendon.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Tendon.
    /// </summary>
    /// <seealso cref="StructureObject2D{TendonProperties}" />
    public class Tendon : StructureObject2D<TendonProperty>
    {
        #region Fields & Properties
        internal string LoadGroupName { get; set; }
        public virtual Group LoadGroup { get; internal set; }

        public double MaxDiscretizationLength { get; internal set; }

        public List<Tuple<eTendonGeometryDefinition, Coordinate3DCartesian>> Segments { get; internal set; } = new List<Tuple<eTendonGeometryDefinition, Coordinate3DCartesian>>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Tendon Factory(
            StructureComponentsProperties<TendonProperty> componentsProperties, 
            string uniqueName)
        {
            Tendon item = new Tendon(
                componentsProperties,
                uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tendon"/> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Tendon(
            StructureComponentsProperties<TendonProperty> componentsProperties,
            string name) : base(componentsProperties,
            name)
        { }
        #endregion
    }
}