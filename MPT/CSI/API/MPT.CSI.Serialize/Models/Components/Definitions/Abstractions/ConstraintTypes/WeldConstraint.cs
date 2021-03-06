﻿// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="WeldConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class WeldConstraint.
    /// </summary>
    /// <seealso cref="Constraint" />
    public class WeldConstraint : ConstraintAboutDOF
    {
        /// <summary>
        /// Gets the tolerance.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeldConstraint"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal WeldConstraint(string name) : base(name)
        {
        }
    }
}
