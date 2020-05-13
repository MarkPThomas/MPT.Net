// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-11-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="IUniqueName.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Support
{
    /// <summary>
    /// Object has a name proprety that serves as a unique identifier.
    /// </summary>
    public interface IUniqueName
    {
        /// <summary>
        /// The unique name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
    }
}
