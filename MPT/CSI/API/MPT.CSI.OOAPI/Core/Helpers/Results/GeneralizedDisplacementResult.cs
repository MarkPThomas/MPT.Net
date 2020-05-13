// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="GeneralizedDisplacementResult.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class GeneralizedDisplacementResult.
    /// </summary>
    public class GeneralizedDisplacementResult : ApiProperty
    {
        /// <summary>
        /// he generalized displacement name associated with the result.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// The generalized displacement type for the result.
        /// It is either Translation or Rotation.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// The the generalized displacement values for the result.[L] when <see cref="Type" /> is Translation , [rad] when <see cref="Type" /> is Rotation.
        /// </summary>
        /// <value>The generalized displacement.</value>
        public double GeneralizedDisplacement { get; set; }


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
