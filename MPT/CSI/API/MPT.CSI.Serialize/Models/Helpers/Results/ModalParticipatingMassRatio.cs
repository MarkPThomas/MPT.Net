// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ModalParticipatingMassRatio.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Masses;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class ModalParticipatingMassRatio.
    /// </summary>
    public class ModalParticipatingMassRatio : ModelProperty
    {
        /// <summary>
        /// The period for the result. [s].
        /// </summary>
        /// <value>The period.</value>
        public double Period { get; set; }

        /// <summary>
        /// The modal participating mass ratio for the structure for each global degree of freedom.
        /// The ratio applies to the specified mode.
        /// </summary>
        /// <value>The mass ratio.</value>
        public MassProperties MassRatio { get; set; }

        /// <summary>
        /// The cumulative sum of the modal participating mass ratios for the each degree of freedom.
        /// </summary>
        /// <value>The mass ratio sum.</value>
        public MassProperties MassRatioSum { get; set; }


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
