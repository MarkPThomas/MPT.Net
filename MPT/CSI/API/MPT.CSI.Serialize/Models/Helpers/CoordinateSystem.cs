// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="CoordinateSystem.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// A coordinate system comprised of coordinates and labeled gridlines.
    /// </summary>
    public class CoordinateSystem
    {
        /// <summary>
        /// Gets or sets the coordinate system name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of coordinates used in the system.
        /// </summary>
        /// <value>The type of the coordinate.</value>
        public eCoordinateType CoordinateType { get; set; }

        /// <summary>
        /// Gets or sets the origin offset from the default model origin.
        /// </summary>
        /// <value>The origin offset.</value>
        public Displacements OriginOffset { get; set; }

        /// <summary>
        /// Gets or sets the grid lines.
        /// </summary>
        /// <value>The grid lines.</value>
        public GridLines GridLines { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateSystem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CoordinateSystem(string name)
        {
            Name = name;
        }
    }
}
