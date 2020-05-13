// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="NodeLoadsDisplacements.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Assignments
{
    /// <summary>
    /// Struct NodeLoadsDisplacements
    /// </summary>
    public class NodeLoadDisplacement : Load
    {
        /// <summary>
        /// The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }

        /// <summary>
        /// The load pattern step for the load.
        /// In most cases this item does not apply and will be returned as 0.
        /// </summary>
        /// <value>The load pattern steps.</value>
        public int LoadPatternSteps { get; set; }

        /// <summary>
        /// The ground displacements assigned along the local or a global axis direction, depending on the specified <see cref="CoordinateSystem" />.
        /// </summary>
        /// <value>The force.</value>
        public Deformations Displacement { get; set; }


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
