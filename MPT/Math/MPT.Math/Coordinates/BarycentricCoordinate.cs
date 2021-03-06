﻿// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 02-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2017
// ***********************************************************************
// <copyright file="BarycentricCoordinate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using NMath = System.Math;

namespace MPT.Math.Coordinates
{
    /// <summary>
    /// A coordinate system in which the location of a point of a simplex (a triangle, tetrahedron, etc.) is specified as the center of mass, or barycenter, of usually unequal masses placed at its vertices.
    /// See <a href="https://en.wikipedia.org/wiki/Barycentric_coordinate_system">Wikipedia</a>. 
    /// </summary>
    /// <seealso cref="System.IEquatable{BarycentricCoordinate}" />
    public struct BarycentricCoordinate : IEquatable<BarycentricCoordinate>
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; }

        /// <summary>
        /// Gets the alpha.
        /// </summary>
        /// <value>The alpha.</value>
        public double Alpha { get; private set; }


        /// <summary>
        /// Gets the beta.
        /// </summary>
        /// <value>The beta.</value>
        public double Beta { get; private set; }


        /// <summary>
        /// Gets the gamma.
        /// </summary>
        /// <value>The gamma.</value>
        public double Gamma { get; private set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="BarycentricCoordinate"/> struct.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <param name="gamma">The gamma.</param>
        /// <param name="tolerance">The tolerance.</param>
        public BarycentricCoordinate(
            double alpha, 
            double beta, 
            double gamma,
            double tolerance = Numbers.ZeroTolerance)
        {
            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
            Tolerance = tolerance;
        }

        /// <summary>
        /// To the cartesian.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate ToCartesian(Point vertexA, Point vertexB, Point vertexC)
        {
            double x = Alpha * vertexA.X + Beta * vertexB.X + Gamma * vertexC.X;
            double y = Alpha * vertexA.Y + Beta * vertexB.Y + Gamma * vertexC.Y;

            return new CartesianCoordinate(x, y, Tolerance);
        }


        /// <summary>
        /// To the trilinear.
        /// </summary>
        /// <param name="vertexA">The vertex a.</param>
        /// <param name="vertexB">The vertex b.</param>
        /// <param name="vertexC">The vertex c.</param>
        /// <returns>TrilinearCoordinate.</returns>
        public TrilinearCoordinate ToTrilinear(Point vertexA, Point vertexB, Point vertexC)
        {
            double sideA = (vertexC - vertexB).Length();
            double sideB = (vertexA - vertexC).Length();
            double sideC = (vertexB - vertexA).Length();

            return new TrilinearCoordinate(
                            x: Alpha / sideA,
                            y: Beta / sideB,
                            z: Gamma / sideC
                        );
        }

        #region Operators & Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(BarycentricCoordinate other)
        {
            return (NMath.Abs(Alpha - other.Alpha) < Tolerance) &&
                   (NMath.Abs(Beta - other.Beta) < Tolerance) &&
                   (NMath.Abs(Gamma - other.Gamma) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BarycentricCoordinate) { return Equals((BarycentricCoordinate)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Alpha.GetHashCode() ^ Beta.GetHashCode() ^ Gamma.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(BarycentricCoordinate a, BarycentricCoordinate b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(BarycentricCoordinate a, BarycentricCoordinate b)
        {
            return !a.Equals(b);
        }
        #endregion
    }
}
