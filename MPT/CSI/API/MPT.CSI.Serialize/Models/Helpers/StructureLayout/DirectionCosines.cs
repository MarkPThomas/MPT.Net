// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 11-24-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-24-2018
// ***********************************************************************
// <copyright file="DirectionCosines.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout
{
    /// <summary>
    /// 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
    /// Direction cosines returned are ordered by row, and then by column.
    /// </summary>
    public struct DirectionCosines
    {
        /// <summary>
        /// Local 1 factor component for global X.
        /// </summary>
        /// <value>The x1.</value>
        public double X1 { get; set; }

        /// <summary>
        /// Local 2 factor component for global X.
        /// </summary>
        /// <value>The x2.</value>
        public double X2 { get; set; }

        /// <summary>
        /// Local 3 factor component for global X.
        /// </summary>
        /// <value>The x3.</value>
        public double X3 { get; set; }


        /// <summary>
        /// Local 1 factor component for global Y.
        /// </summary>
        /// <value>The y1.</value>
        public double Y1 { get; set; }

        /// <summary>
        /// Local 2 factor component for global Y.
        /// </summary>
        /// <value>The y2.</value>
        public double Y2 { get; set; }

        /// <summary>
        /// Local 3 factor component for global Y.
        /// </summary>
        /// <value>The y3.</value>
        public double Y3 { get; set; }


        /// <summary>
        /// Local 1 factor component for global Z.
        /// </summary>
        /// <value>The z1.</value>
        public double Z1 { get; set; }

        /// <summary>
        /// Local 2 factor component for global Z.
        /// </summary>
        /// <value>The z2.</value>
        public double Z2 { get; set; }

        /// <summary>
        /// Local 3 factor component for global Z.
        /// </summary>
        /// <value>The z3.</value>
        public double Z3 { get; set; }
    }
}
