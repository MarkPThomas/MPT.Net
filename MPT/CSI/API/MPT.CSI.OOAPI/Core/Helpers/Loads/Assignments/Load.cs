// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-24-2018
// ***********************************************************************
// <copyright file="Load.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments
{
    /// <summary>
    /// Class Load.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiProperty" />
    public abstract class Load : ApiProperty
    {
        /// <summary>
        /// The name of the load pattern associated with the load.
        /// </summary>
        /// <value>The load pattern.</value>
        public string LoadPattern { get; set; }

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
