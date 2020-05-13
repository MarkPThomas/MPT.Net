// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="EqualConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class EqualConstraint.
    /// </summary>
    /// <seealso cref="Constraint" />
    public class EqualConstraint : ConstraintAboutDOF
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualConstraint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal EqualConstraint(string name) : base(name)
        {
        }
    }
}
