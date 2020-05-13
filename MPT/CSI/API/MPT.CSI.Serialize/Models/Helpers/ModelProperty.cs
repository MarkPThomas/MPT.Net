// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ApiProperty.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Represents a collection of properties filled.
    /// Objects deriving from this are expected to be treated like values.
    /// Objects deriving from this may exhibit some validation &amp; other simple behavior, but should not make any specialized calls.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public class ModelProperty : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public virtual object Clone()
        {
            return this.Copy();
        }
    }
}
