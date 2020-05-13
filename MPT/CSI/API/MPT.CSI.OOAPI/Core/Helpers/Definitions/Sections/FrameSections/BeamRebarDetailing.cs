// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="BeamRebarDetailing.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class BeamRebarDetailing.
    /// </summary>
    public class BeamRebarDetailing : ApiProperty
    {
        /// <summary>
        /// The name of the rebar material property for the longitudinal rebar.
        /// </summary>
        /// <value>The material name longitudinal.</value>
        public string MaterialNameLongitudinal { get; set; }

        /// <summary>
        /// The name of the rebar material property for the confinement rebar.
        /// </summary>
        /// <value>The material name confinement.</value>
        public string MaterialNameConfinement { get; set; }

        /// <summary>
        /// The distance from the top of the beam to the centroid of the top longitudinal reinforcement. [L].
        /// </summary>
        /// <value>The cover top.</value>
        public double CoverTop { get; set; }

        /// <summary>
        /// The distance from the bottom of the beam to the centroid of the bottom longitudinal reinforcement. [L].
        /// </summary>
        /// <value>The cover bottom.</value>
        public double CoverBottom { get; set; }

        /// <summary>
        /// The total area of longitudinal reinforcement at the top left end of the beam. [L^2].
        /// </summary>
        /// <value>The top left area.</value>
        public double TopLeftArea { get; set; }

        /// <summary>
        /// The total area of longitudinal reinforcement at the top right end of the beam. [L^2].
        /// </summary>
        /// <value>The top right area.</value>
        public double TopRightArea { get; set; }

        /// <summary>
        /// The total area of longitudinal reinforcement at the bottom left end of the beam. [L^2].
        /// </summary>
        /// <value>The bottom left area.</value>
        public double BottomLeftArea { get; set; }

        /// <summary>
        /// The total area of longitudinal reinforcement at the bottom right end of the beam. [L^2].
        /// </summary>
        /// <value>The bottom right area.</value>
        public double BottomRightArea { get; set; }

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