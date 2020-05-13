// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="BodyConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class BodyConstraint.
    /// </summary>
    /// <seealso cref="Constraint" />
    public class BodyConstraint : ConstraintAboutDOF
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyConstraint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal BodyConstraint(string name) : base(name)
        {
        }
    }
}
