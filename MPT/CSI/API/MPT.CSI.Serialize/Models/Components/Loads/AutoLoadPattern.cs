// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="AutoLoadPattern.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class AutoLoadPattern.
    /// </summary>
    public abstract class AutoLoadPattern
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadPatternType Type { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoLoadPattern"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected AutoLoadPattern(string name)
        {
            Name = name;
        }
    }
}
