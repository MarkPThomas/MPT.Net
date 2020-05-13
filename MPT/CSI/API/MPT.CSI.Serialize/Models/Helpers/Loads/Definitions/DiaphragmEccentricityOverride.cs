// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="DiaphragmEccentricityOverride.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents the diaphragm eccentricity override.
    /// </summary>
    public class DiaphragmEccentricityOverride : ModelProperty
    {
        internal string DiaphragmName { get; set; }
        // TODO: Finish DiaphragmEccentricityOverride Diaphragm intialization
        /// <summary>
        /// The diaphragm.
        /// </summary>
        /// <value>The mode number.</value>
        public Diaphragm Diaphragm { get; set; }

        /// <summary>
        /// The eccentricity applied to each diaphragm. [L].
        /// </summary>
        /// <value>The damping.</value>
        public double Eccentricity { get; set; }


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
