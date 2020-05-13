// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="Constraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Base class for all constraints in the program.
    /// </summary>
    /// <seealso cref="IUniqueName" />
    public abstract class Constraint : IUniqueName
    {
        /// <summary>
        /// The unique name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the coordinate system.
        /// </summary>
        /// <value>The name of the coordinate system.</value>
        public virtual string CoordinateSystemName { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Constraint"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Constraint(string name)
        {
            Name = name;
        }
    }
}
