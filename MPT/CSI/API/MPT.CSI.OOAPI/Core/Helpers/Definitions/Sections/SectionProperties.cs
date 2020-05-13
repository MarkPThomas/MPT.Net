// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="SectionProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections
{
    /// <summary>
    /// Class SectionProperties.
    /// </summary>
    public class SectionProperties : ApiProperty
    {
        /// <summary>
        /// The material name.
        /// </summary>
        /// <value>The material.</value>
        public string MaterialName { get; internal set; }

        /// <summary>
        /// The misc properties such as color, notes, etc.
        /// </summary>
        /// <value>The misc properties.</value>
        public GeneralData GeneralData { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="SectionProperties" /> class.
        /// </summary>
        public SectionProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public SectionProperties(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="material">The material.</param>
        public virtual void SetMaterial(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
