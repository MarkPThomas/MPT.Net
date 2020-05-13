// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="UniqueName.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Support
{ 
    /// <summary>
    /// Represents an object that is identified by a unique name.
    /// </summary>
    public abstract class UniqueName : IUniqueName
    {
        /// <summary>
        /// The name
        /// </summary>
        protected string _name;
        /// <summary>
        /// The unique name.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique.</value>
        public virtual string Name
        {
            get => _name;
            internal set => _name = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueName" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected UniqueName(string name)
        {
            _name = name;
        }
    }
}
