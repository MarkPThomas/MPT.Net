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

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    // IListable - static
    /// <summary>
    /// Class PierSpandrelBase.
    /// </summary>
    public abstract class PierSpandrelBase : UniqueName
    {
        /// <summary>
        /// Gets or sets a value indicating whether this label exists in the application.
        /// </summary>
        /// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
        public bool Exists { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PierSpandrelBase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected PierSpandrelBase(string name) : base(name)
        {
        }
    }
}
