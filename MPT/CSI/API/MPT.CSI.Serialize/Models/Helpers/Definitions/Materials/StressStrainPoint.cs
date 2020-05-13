// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="StressStrainPoint.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// The stress-strain point for a stress-strain loading curve.
    /// </summary>
    public class StressStrainPoint : FunctionPoint
    {
        /// <summary>
        /// The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.
        /// </summary>
        /// <value>The point i ds.</value>
        public eStressStrainPointID PointID { get; }

        /// <summary>
        /// The strain at the point on the stress strain curve.
        /// </summary>
        /// <value>The strain.</value>
        public double Strain => X;

        /// <summary>
        /// The stress at the point on the stress strain curve. [F/L^2].
        /// </summary>
        /// <value>The stress.</value>
        public double Stress => Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="StressStrainPoint" /> class.
        /// </summary>
        /// <param name="stress">The stress.</param>
        /// <param name="strain">The strain.</param>
        /// <param name="pointId">The point identifier.</param>
        public StressStrainPoint(double stress = 0, double strain = 0,
            eStressStrainPointID pointId = eStressStrainPointID.None) : base(strain, stress)
        {
            PointID = pointId;
        }

        /// <summary>
        /// Determines whether this instance is origin.
        /// </summary>
        /// <returns><c>true</c> if this instance is origin; otherwise, <c>false</c>.</returns>
        public bool IsOrigin()
        {
            return Math.Abs(Strain) < Constants.Tolerance &&
                   Math.Abs(Stress) < Constants.Tolerance &&
                   PointID == eStressStrainPointID.None;
        }

        /// <summary>
        /// Returns an instance at the origin.
        /// </summary>
        /// <returns>StressStrainPoint.</returns>
        public new static StressStrainPoint Origin()
        {
            return new StressStrainPoint();
        }

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(StressStrainPoint point1, StressStrainPoint point2)
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
        public static bool operator !=(StressStrainPoint point1, StressStrainPoint point2)
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
                case StressStrainPoint point:
                    return Equals(point);
            }

            return false;
        }

        /// <summary>
        /// Equals the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(StressStrainPoint point)
        {
            if ((object)point == null) return false;

            // Return true if the fields match:
            return Math.Abs(Stress - point.Stress) < Constants.Tolerance &&
                   Math.Abs(Strain - point.Strain) < Constants.Tolerance &&
                   PointID == point.PointID;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Stress.GetHashCode() ^ Strain.GetHashCode() ^ PointID.GetHashCode();
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