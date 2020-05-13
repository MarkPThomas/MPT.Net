// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="DiaphragmConstraint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes
{
    /// <summary>
    /// Class DiaphragmConstraint.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.Definitions.Abstractions.ConstraintTypes.ConstraintAboutAxis" />
    public class DiaphragmConstraint : ConstraintAboutAxis
    {
        /// <summary>
        /// The constraint applied to multiple diaphragm levels.
        /// </summary>
        /// <value><c>true</c> if [multi level]; otherwise, <c>false</c>.</value>
        public bool MultiLevel { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiaphragmConstraint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal DiaphragmConstraint(string name) : base(name)
        {
        }
    }
}
