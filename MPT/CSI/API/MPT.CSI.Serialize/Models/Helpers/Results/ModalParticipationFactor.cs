// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ModalParticipationFactor.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Masses;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class ModalParticipationFactor.
    /// </summary>
    public class ModalParticipationFactor : ModelProperty
    {
        /// <summary>
        /// The period for the result. [s].
        /// </summary>
        /// <value>The period.</value>
        public double Period { get; set; }

        /// <summary>
        /// The modal participation factors for the structure translation and rotation degrees of freedom.
        /// The factor applies to the specified mode. [F*s^2] for translational, [F*L*s^2] for rotational.
        /// </summary>
        /// <value>The participation factor.</value>
        public MassProperties ParticipationFactor { get; set; }

        /// <summary>
        /// The modal mass for the specified mode.
        /// This is a measure of the kinetic energy in the structure as it is deforming in the specified mode. [F*L*s^2].
        /// </summary>
        /// <value>The modal mass.</value>
        public double ModalMass { get; set; }

        /// <summary>
        /// The modal stiffness for the specified mode.
        /// This is a measure of the strain energy in the structure as it is deforming in the specified mode. [F*L].
        /// </summary>
        /// <value>The modal stiffness.</value>
        public double ModalStiffness { get; set; }



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
