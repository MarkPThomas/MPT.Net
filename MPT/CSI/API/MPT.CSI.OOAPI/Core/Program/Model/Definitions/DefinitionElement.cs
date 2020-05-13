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
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Class DefinitionElement.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBase" />
    public abstract class DefinitionElement : CSiOoApiBase
    {
        /// <summary>
        /// The display color assigned to the section.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; protected set; }

        /// <summary>
        /// The notes, if any, assigned to the section.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; protected set; }

        /// <summary>
        /// The GUID (global unique identifier), if any, assigned to the section.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionElement" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected DefinitionElement(ApiCSiApplication app, string name) : base(app, name)
        {
        }
    }
}
