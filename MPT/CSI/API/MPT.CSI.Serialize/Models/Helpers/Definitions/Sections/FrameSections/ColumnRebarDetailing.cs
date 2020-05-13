// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="ColumnRebarDetailing.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections
{
    /// <summary>
    /// Class ColumnRebarDetailing.
    /// </summary>
    public class ColumnRebarDetailing : ModelProperty
    {
        /// <summary>
        /// The name of the rebar material property for the longitudinal rebar.
        /// </summary>
        /// <value>The material name longitudinal.</value>
        public string MaterialNameLongitudinal{ get; set; }

        /// <summary>
        /// The name of the rebar material property for the confinement rebar.
        /// </summary>
        /// <value>The material name confinement.</value>
        public string MaterialNameConfinement{ get; set; }

        /// <summary>
        /// The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular" />; otherwise an error is returned.
        /// </summary>
        /// <value>The rebar configuration.</value>
        public eRebarConfiguration RebarConfiguration{ get; internal set; }

        /// <summary>
        /// Type of the confinement.
        /// This item applies only when <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// If <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />, the confinement bar type is assumed to be <see cref="eConfinementType.Ties" />.
        /// </summary>
        /// <value>The type of the confinement.</value>
        public eConfinementType ConfinementType{ get; internal set; }

        /// <summary>
        /// The clear cover for the confinement steel (ties).
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].
        /// </summary>
        /// <value>The cover.</value>
        public double Cover{ get; set; }

        /// <summary>
        /// The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// </summary>
        /// <value>The number of circular bars.</value>
        public int NumberOfCircularBars{ get; internal set; }

        /// <summary>
        /// The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.
        /// </summary>
        /// <value>The number of rectangular bars2 axis.</value>
        public int NumberOfRectangularBars2Axis { get; internal set; }

        /// <summary>
        /// The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.
        /// </summary>
        /// <value>The number of rectangular bars3 axis.</value>
        public int NumberOfRectangularBars3Axis{ get; internal set; }

        /// <summary>
        /// The rebar name for the longitudinal rebar in the column.
        /// </summary>
        /// <value>The size of the rebar.</value>
        public string RebarSize{ get; set; }

        /// <summary>
        /// The rebar name for the confinement rebar in the column.
        /// </summary>
        /// <value>The size of the tie.</value>
        public string TieSize{ get; set; }

        /// <summary>
        /// The longitudinal spacing of the confinement bars (ties). [L].
        /// </summary>
        /// <value>The tie spacing longitudinal.</value>
        public double TieSpacingLongitudinal{ get; set; }

        /// <summary>
        /// It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.
        /// </summary>
        /// <value>The number of confinement bars2 axis.</value>
        public int NumberOfConfinementBars2Axis{ get; internal set; }

        /// <summary>
        /// It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <see cref="RebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.
        /// </summary>
        /// <value>The number of confinement bars3 axis.</value>
        public int NumberOfConfinementBars3Axis{ get; internal set; }

        /// <summary>
        /// True: The column longitudinal rebar is to be designed; otherwise it is to be checked.
        /// </summary>
        /// <value><c>true</c> if [to be designed]; otherwise, <c>false</c>.</value>
        public bool ToBeDesigned{ get; set; }


        /// <summary>
        /// Sets the rebar configuration.
        /// </summary>
        /// <param name="rebarConfiguration">The rebar configuration.</param>
        public void SetRebarConfiguration(eRebarConfiguration rebarConfiguration)
        {
            RebarConfiguration = rebarConfiguration;
            if (rebarConfiguration == eRebarConfiguration.Rectangular)
            {
                ConfinementType = eConfinementType.Ties;
                NumberOfCircularBars = 0;
            }
            else
            {
                NumberOfRectangularBars2Axis = 0;
                NumberOfRectangularBars3Axis = 0;
                NumberOfConfinementBars2Axis = 0;
                NumberOfConfinementBars3Axis = 0;
            }
        }


        /// <summary>
        /// Sets the type of the confinement.
        /// </summary>
        /// <param name="confinementType">Type of the confinement.</param>
        public void SetConfinementType(eConfinementType confinementType)
        {
            if (RebarConfiguration == eRebarConfiguration.Rectangular) return;
            ConfinementType = confinementType;
        }


        /// <summary>
        /// Sets the circular bars.
        /// </summary>
        /// <param name="numberOfBars">The number of bars.</param>
        public void SetCircularBars(int numberOfBars)
        {
            if (RebarConfiguration == eRebarConfiguration.Rectangular) return;
            NumberOfCircularBars = numberOfBars;
        }


        /// <summary>
        /// Sets the rectangular bars.
        /// </summary>
        /// <param name="numberOfBars2Axis">The number of bars2 axis.</param>
        /// <param name="numberOfBars3Axis">The number of bars3 axis.</param>
        public void SetRectangularBars(int numberOfBars2Axis, int numberOfBars3Axis)
        {
            if (RebarConfiguration == eRebarConfiguration.Circular) return;
            NumberOfRectangularBars2Axis = numberOfBars2Axis;
            NumberOfRectangularBars3Axis = numberOfBars3Axis;
        }


        /// <summary>
        /// Sets the confinement bars.
        /// </summary>
        /// <param name="numberOfBars2Axis">The number of bars2 axis.</param>
        /// <param name="numberOfBars3Axis">The number of bars3 axis.</param>
        public void SetConfinementBars(int numberOfBars2Axis, int numberOfBars3Axis)
        {
            if (RebarConfiguration == eRebarConfiguration.Circular) return;
            NumberOfConfinementBars2Axis = numberOfBars2Axis;
            NumberOfConfinementBars3Axis = numberOfBars3Axis;
        }

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
