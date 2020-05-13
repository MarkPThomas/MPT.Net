// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="PierSpandrelBase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    // IListable - static
    /// <summary>
    /// Class PierSpandrelBase.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBase" />
    public abstract class PierSpandrelBase : CSiOoApiBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this label exists in the application.
        /// </summary>
        /// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
        public bool Exists { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PierSpandrelBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected PierSpandrelBase(string name) : base(name)
        {
        }

        /// <summary>
        /// Adds the pier spandrel.
        /// </summary>
        protected void addPierSpandrel()
        {
            Exists = true;
        }
    }
}
