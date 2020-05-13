// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="LocalConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Grids;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class LocalConstraint.
    /// </summary>
    /// <seealso cref="ConstraintAboutDOF" />
    public class LocalConstraint : ConstraintAboutDOF
    {
        /// <summary>
        /// Gets the name of the coordinate system.
        /// </summary>
        /// <value>The name of the coordinate system.</value>
        public override string CoordinateSystemName => CoordinateSystems.Local;


        /// <summary>
        /// Initializes a new instance of the <see cref="LocalConstraint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal LocalConstraint(string name) : base(name)
        {
        }
    }
}
