// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="StiffnessPendulum.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Stiffnesses
{
    /// <summary>
    /// Stiffness values for each face of a triple pendulum isolator.
    /// </summary>
    public class StiffnessPendulum : ModelProperty
    {
        /// <summary>
        /// Translational stiffness along the outer top sliding surface. [F/L].
        /// </summary>
        /// <value>The outer top sliding surface.</value>
        public double OuterTopSlidingSurface { get; set; }

        /// <summary>
        /// Translational stiffness along outer bottom sliding surface. [F/L].
        /// </summary>
        /// <value>The outer bottom sliding surface.</value>
        public double OuterBottomSlidingSurface { get; set; }


        /// <summary>
        /// Translational stiffness along the inner top sliding surface. [F/L].
        /// </summary>
        /// <value>The inner top sliding surface.</value>
        public double InnerTopSlidingSurface { get; set; }


        /// <summary>
        /// Rotational stiffness along the inner bottom sliding surface. [F/L].
        /// </summary>
        /// <value>The inner bottom sliding surface.</value>
        public double InnerBottomSlidingSurface { get; set; }

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