// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="ModalCombination.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Represents the modal combination.
    /// </summary>
    public class ModalCombination : ModelProperty
    {
        /// <summary>
        /// The modal combination option.
        /// </summary>
        /// <value>The combination.</value>
        public eModalCombination Combination { get; set; }

        /// <summary>
        /// The GMC f1 factor [cyc/s].
        /// This item does not apply when <see cref="Combination" /> = <see cref="eModalCombination.ABS" />.
        /// </summary>
        /// <value>The GMC f1.</value>
        public double GmcF1 { get; set; }

        /// <summary>
        /// The GMC f2 factor [cyc/s].
        /// This item does not apply when <see crefe="Combination" /> = <see cref="eModalCombination.ABS" />.
        /// </summary>
        /// <value>The GMC f2.</value>
        public double GmcF2
        {
            get => GmcF2; 
            set
            {
                if (Math.Abs(GmcF2) > Constants.Tolerance && value < GmcF1)
                {
                    // TODO: Work out how to best handle. Exception? Messenger?
                }
                else
                {
                    GmcF2 = value;
                }

            }
        }
        /// <summary>
        /// The periodic plus rigid modal combination option.
        /// </summary>
        /// <value>The periodic plus rigid modal combination.</value>
        public ePeriodicPlusRigidModalCombination PeriodicPlusRigidModalCombination { get; set; }

        /// <summary>
        /// The factor Td [s].
        /// This item applies only when <see cref="Combination" /> = <see cref="eModalCombination.DoubleSum" />.
        /// </summary>
        /// <value>The td.</value>
        public double Td { get; set; }


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