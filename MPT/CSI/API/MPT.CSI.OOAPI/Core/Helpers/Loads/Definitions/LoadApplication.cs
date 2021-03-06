﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-20-2018
// ***********************************************************************
// <copyright file="LoadApplication.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data for load application of nonlinear cases.
    /// </summary>
    public class LoadApplication : ApiProperty
    {
        /// <summary>
        /// The structure is loaded to a monitored displacement of this magnitude.
        /// [L] when <see cref="DegreeOfFreedom" /> = <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" /> and [rad] when <see cref="DegreeOfFreedom" /> = <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />.
        /// This item applies only when displacement control is used, that is, <see cref="LoadControl" /> = <see cref="eLoadControl.DisplacementControl" />.
        /// </summary>
        /// <value>The target displacement.</value>
        public double TargetDisplacement { get; set; }

        /// <summary>
        /// LoadType of monitored displacement.
        /// </summary>
        /// <value>The type of the monitored displacement.</value>
        public eMonitoredDisplacementType MonitoredDisplacementType { get; set; }

        /// <summary>
        /// The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <see cref="MonitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />
        /// </summary>
        /// <value>The degree of freedom.</value>
        public eDegreeOfFreedom DegreeOfFreedom { get; set; }

        /// <summary>
        /// The name of the point object at which the displacement is monitored.
        /// This item applies only when <see cref="MonitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.
        /// </summary>
        /// <value>The name point.</value>
        public string NamePoint { get; set; }

        /// <summary>
        /// The name of the generalized displacement for which the displacement is monitored.
        /// This item applies only when <see cref="MonitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.GeneralizedDisplacement" />.
        /// </summary>
        /// <value>The name generalized displacement.</value>
        public string NameGeneralizedDisplacement { get; set; }

        /// <summary>
        /// The load application control method.
        /// </summary>
        /// <value>The load control.</value>
        public eLoadControl LoadControl { get; set; }

        /// <summary>
        /// LoadType of control displacement.
        /// </summary>
        /// <value>The type of the control displacement.</value>
        public eLoadControlDisplacement ControlDisplacementType { get; set; }


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
