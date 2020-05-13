// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="Bar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Rebar
{
    /// <summary>
    /// Class Bar.
    /// </summary>
    public class Bar : IUniqueName
    {
        /// <summary>
        /// The unique name/identifier of the bar size.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <value>The area.</value>
        public double Area { get; internal set; }

        /// <summary>
        /// Gets the diameter.
        /// </summary>
        /// <value>The diameter.</value>
        public double Diameter { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bar"/> class.
        /// </summary>
        /// <param name="rebarId">The rebar identifier.</param>
        public Bar(string rebarId)
        {
            Name = rebarId;
        }
    }
}
