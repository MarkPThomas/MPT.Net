// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameOutputStation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameOutputStation.
    /// </summary>
    public class FrameOutputStation : ApiProperty
    {
        /// <summary>
        /// Indicates how the output stations are specified
        /// </summary>
        /// <value>The type of the output station.</value>
        public eOutputStationType OutputStationType { get; set; }

        /// <summary>
        /// The maximum segment size, that is, the maximum station spacing. [L]
        /// This item applies only when <see cref="OutputStationType" /> = <see cref="eOutputStationType.MaxSpacing" />.
        /// </summary>
        /// <value>The maximum station spacing.</value>
        public double MaxStationSpacing { get; set; }

        /// <summary>
        /// The minimum number of stations.
        /// This item applies only when <see cref="OutputStationType" /> = <see cref="eOutputStationType.MinStations" />.
        /// </summary>
        /// <value>The minimum station number.</value>
        public int MinStationNumber { get; set; }

        /// <summary>
        /// True: No additional output stations are added at the ends of line elements when the cable object is internally meshed.
        /// </summary>
        /// <value><c>true</c> if [no output and design at element ends]; otherwise, <c>false</c>.</value>
        public bool NoOutputAndDesignAtElementEnds { get; set; }

        /// <summary>
        /// True: No additional output stations are added at point load locations.
        /// </summary>
        /// <value><c>true</c> if [no output and design at point loads]; otherwise, <c>false</c>.</value>
        public bool NoOutputAndDesignAtPointLoads { get; set; }


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
