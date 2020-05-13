// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="DefinitionElement.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions
{
    /// <summary>
    /// Class DefinitionElement.
    /// </summary>
    public abstract class DefinitionElement : UniqueName
    {
        /// <summary>
        /// The display color assigned to the section.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; internal set; }

        /// <summary>
        /// The notes, if any, assigned to the section.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; internal set; }

        /// <summary>
        /// The GUID (global unique identifier), if any, assigned to the section.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionElement" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected DefinitionElement(string name) : base(name) { }
    }
}
