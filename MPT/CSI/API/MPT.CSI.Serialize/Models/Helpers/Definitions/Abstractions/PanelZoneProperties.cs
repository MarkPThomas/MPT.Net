// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-23-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="PanelZoneProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions
{
    /// <summary>
    /// Class PanelZoneProperties.
    /// </summary>
    public class PanelZoneProperties : ModelProperty
    {
        /// <summary>
        /// Method by which properties are determined for panel zones.
        /// </summary>
        /// <value>The type of the property.</value>
        public ePanelZonePropertyType PropertyType { get; set; }

        /// <summary>
        /// The thickness of the doubler plate.
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }

        /// <summary>
        /// The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]
        /// </summary>
        /// <value>The K1.</value>
        public double K1 { get; set; }

        /// <summary>
        /// The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]
        /// </summary>
        /// <value>The K2.</value>
        public double K2 { get; set; }

        /// <summary>
        /// The name of the link property used to define the panel zone.
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.
        /// </summary>
        /// <value>The link property.</value>
        public string LinkProperty { get; set; }

        /// <summary>
        /// Panel zone connection types.
        /// </summary>
        /// <value>The connectivity.</value>
        public ePanelZoneConnectivity Connectivity { get; set; }

        /// <summary>
        /// Method by which the local axis is defined.
        /// The <see cref="LocalAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <see cref="PropertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.
        /// </summary>
        /// <value>The local axis from.</value>
        public ePanelZoneLocalAxis LocalAxisFrom { get; set; }

        /// <summary>
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <see cref="LocalAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]
        /// </summary>
        /// <value>The local axis angle.</value>
        public double LocalAxisAngle { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
