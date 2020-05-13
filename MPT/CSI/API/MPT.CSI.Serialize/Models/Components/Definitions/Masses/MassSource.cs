// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-20-2018
// ***********************************************************************
// <copyright file="MassSource.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Masses
{
    /// <summary>
    /// Represents the mass source in the application.
    /// </summary>
    public class MassSource : IUniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The mass source name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {get; internal set; }

        /// <summary>
        /// True: Element self mass is included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from elements; otherwise, <c>false</c>.</value>
        public bool IsFromElements { get; internal set; }

        /// <summary>
        /// True: Assigned masses are included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from masses; otherwise, <c>false</c>.</value>
        public bool IsFromMasses { get; internal set; }

        /// <summary>
        /// True: Specified load patterns are included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from loads; otherwise, <c>false</c>.</value>
        public bool IsFromLoads { get; internal set; }

        /// <summary>
        /// True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.
        /// SAP2000 only.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault { get; internal set; }

        /// <summary>
        /// Load patterns specified for the mass source.
        /// </summary>
        /// <value>The load patterns.</value>
        public LoadPatternTuples LoadPatterns { get; internal set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MassSource" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal MassSource(string name)
        {
            Name = name;
        }
    }
}
