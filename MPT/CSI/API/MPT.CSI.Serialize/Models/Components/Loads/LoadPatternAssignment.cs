// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="LoadPatternAssignment.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class LoadPatternAssignment.
    /// </summary>
    public class LoadPatternAssignment
    {
        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        public LoadPattern Pattern { get; set; }
        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>The element.</value>
        public StructureObjects Element { get; set; }
    }
}
