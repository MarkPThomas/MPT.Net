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
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Helpers
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



        /// <summary>
        /// Assigns array values to struct properties.
        /// Array must have 9 entries.
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="directionCosines">The direction cosines.</param>
        /// <exception cref="CSiException">Array has " + directionCosines.Length + " elements when 9 elements was expected.</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array has " + stiffnesses.Length + " elements when 21 elements was expected.</exception>
        public void FromArray(double[] directionCosines)
        {
            if (directionCosines.Length != 9) { throw new CSiException("Array has " + directionCosines.Length + " elements when 9 elements was expected."); }
            X1 = directionCosines[0];
            Y1 = directionCosines[1];
            Z1 = directionCosines[2];

            X2 = directionCosines[3];
            Y2 = directionCosines[4];
            Z2 = directionCosines[5];

            X3 = directionCosines[6];
            Y3 = directionCosines[7];
            Z3 = directionCosines[8];

        }

        /// <summary>
        /// Return a 1x9 matrix of direction cosine values.
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <returns>System.Double[].</returns>
        public double[] ToArray()
        {
            double[] directionCosines =
            {
                X1, Y1, Z1,
                X2, Y2, Z2,
                X3, Y3, Z3
            };

            return directionCosines;
        }
    }
}
