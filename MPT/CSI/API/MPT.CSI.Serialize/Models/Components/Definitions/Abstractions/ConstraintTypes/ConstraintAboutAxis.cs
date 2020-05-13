// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="ConstraintAboutAxis.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class ConstraintAboutAxis.
    /// </summary>
    /// <seealso cref="Constraint" />
    public abstract class ConstraintAboutAxis : Constraint
    {
        /// <summary>
        /// The axis about which the constraint is applied.
        /// </summary>
        /// <value>The axis.</value>
        public eConstraintAxis Axis { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintAboutAxis" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ConstraintAboutAxis(string name) : base(name)
        {
        }
    }
}
