// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="ConstraintAboutDOF.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class ConstraintAboutDOF.
    /// </summary>
    /// <seealso cref="Constraint" />
    public abstract class ConstraintAboutDOF : Constraint
    {
        /// <summary>
        /// The degrees of freedom applied limited by the constraint.
        /// </summary>
        /// <value>The degrees of freedom global.</value>
        public virtual DegreesOfFreedomGlobal DegreesOfFreedomGlobal { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintAboutDOF" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ConstraintAboutDOF(string name) : base(name)
        {
        }
    }
}
