// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="FunctionPoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class FunctionPoint.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.ModelProperty" />
    public class FunctionPoint : ModelProperty
    {
        /// <summary>
        /// The x-coordinate of the curve.
        /// </summary>
        /// <value>The strain.</value>
        public double X { get; }

        /// <summary>
        /// The y-coordinate of the curve.
        /// </summary>
        /// <value>The stress.</value>
        public double Y { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionPoint"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public FunctionPoint(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns an instance at the origin.
        /// </summary>
        /// <returns>StressStrainPoint.</returns>
        public static FunctionPoint Origin()
        {
            return new FunctionPoint();
        }

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(FunctionPoint point1, FunctionPoint point2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(point1, point2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)point1 == null) || ((object)point2 == null))
            {
                return false;
            }

            // Return true if the fields match:
            return point1.Equals(point2);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(FunctionPoint point1, FunctionPoint point2)
        {
            return !(point1 == point2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case FunctionPoint point:
                    return Equals(point);
            }

            return false;
        }

        /// <summary>
        /// Equals the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(FunctionPoint point)
        {
            if ((object)point == null) return false;

            // Return true if the fields match:
            return Math.Abs(X - point.X) < Constants.Tolerance &&
                   Math.Abs(Y - point.Y) < Constants.Tolerance;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
        #endregion

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
