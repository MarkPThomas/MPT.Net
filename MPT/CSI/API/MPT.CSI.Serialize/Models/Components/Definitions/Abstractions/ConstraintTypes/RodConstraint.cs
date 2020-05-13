// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="RodConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class RodConstraint.
    /// </summary>
    /// <seealso cref="Constraint" />
    public class RodConstraint : ConstraintAboutAxis
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RodConstraint"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal RodConstraint(string name) : base(name)
        {
        }
    }
}
