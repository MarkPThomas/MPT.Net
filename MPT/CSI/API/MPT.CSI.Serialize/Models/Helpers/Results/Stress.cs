// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="Stress.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Internal stresses oriented along local axes.
    /// </summary>
    public struct Stress
    {
        /// <summary>
        /// In-plane normal stress along the local 1-1-axis [F/L^2].
        /// </summary>
        /// <value>The S11.</value>
        public double S11 { get; set; }

        /// <summary>
        /// In-plane normal stress along the local 2-2-axis [F/L^2].
        /// </summary>
        /// <value>The S22.</value>
        public double S22 { get; set; }

        /// <summary>
        /// Normal stress along the local 3-3-axis [F/L^2].
        /// </summary>
        /// <value>The S33.</value>
        public double S33 { get; set; }


        /// <summary>
        /// In-plane shear stress along the local 1-2-axis [F/L^2].
        /// </summary>
        /// <value>The S12.</value>
        public double S12 { get; set; }

        /// <summary>
        /// Out-of-plane shear Stress along the local 1-3-axis [F/L^2].
        /// </summary>
        /// <value>The S13.</value>
        public double S13 { get; set; }

        /// <summary>
        /// Out-of-plane shear stress along the local 2-3-axis [F/L^2].
        /// </summary>
        /// <value>The S23.</value>
        public double S23 { get; set; }


        /// <summary>
        /// Maximum principal stress [F/L^2].
        /// </summary>
        /// <value>The s maximum.</value>
        public double SMax { get; set; }

        /// <summary>
        /// Minimum principal stress [F/L^2].
        /// </summary>
        /// <value>The s minimum.</value>
        public double SMin { get; set; }


        /// <summary>
        /// Von Mises stress [F/L^2].
        /// </summary>
        /// <value>The SVM.</value>
        public double SVM { get; set; }


        /// <summary>
        /// The angle measured counter clockwise (when the local 3 axis is pointing toward you) from the area local 1 axis to the direction of the maximum principal stress. [deg].
        /// </summary>
        /// <value>The angle.</value>
        public double Angle { get; set; }
    }
}
