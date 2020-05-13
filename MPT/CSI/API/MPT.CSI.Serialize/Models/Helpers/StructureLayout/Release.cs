// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="Release.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// Class Release.
    /// </summary>
    public class Release : ModelProperty
    {
        /// <summary>
        /// The end release active degrees of freedom.
        /// </summary>
        /// <value>The end release.</value>
        public DegreesOfFreedomLocal EndRelease { get; set; }

        /// <summary>
        ///The end fixity.
        /// </summary>
        /// <value>The end fixity.</value>
        public Fixity EndFixity { get; set; }


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
