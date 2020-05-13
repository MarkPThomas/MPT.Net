// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-24-2018
// ***********************************************************************
// <copyright file="ConcreteAngleProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class ConcreteAngleProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MaterialProperties" />
    public class ConcreteAngleProperties : MaterialProperties
    {
        /// <summary>
        /// The friction angle
        /// </summary>
        private double _frictionAngle;
        /// <summary>
        /// The Drucker-Prager friction angle, 0 &lt;= <ref name="FrictionAngle" /> &lt; 90. [deg].
        /// </summary>
        /// <value>The friction angle.</value>
        public double FrictionAngle
        {
            get => _frictionAngle;
            set => _frictionAngle = Math.Max(0,Math.Min(value, 90));
        }

        /// <summary>
        /// The dilatational angle
        /// </summary>
        private double _dilatationalAngle;
        /// <summary>
        /// The Drucker-Prager dilatational angle, 0 &lt;= <ref name="DilatationalAngle" /> &lt; 90. [deg].
        /// </summary>
        /// <value>The dilatational angle.</value>
        public double DilatationalAngle
        {
            get => _dilatationalAngle;
            set => _dilatationalAngle = Math.Max(0, Math.Min(value, 90));
        }



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
