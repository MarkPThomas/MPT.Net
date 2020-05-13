// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="StressStrainCurve.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// A stress-strain loading curve.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{StressStrainPoint}" />
    public class StressStrainCurve: 
       FunctionCurve<StressStrainPoint>
    {
        #region Fields & Properties

        /// <summary>
        /// The has default positive
        /// </summary>
        private bool _hasDefaultPositive = true;
        /// <summary>
        /// The has default negative
        /// </summary>
        private bool _hasDefaultNegative = true;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="StressStrainCurve"/> class.
        /// </summary>
        public StressStrainCurve()
        {
            // Curves must always have 3 points, one of which passes through the origin
            _items.Add(StressStrainPoint.Origin());
            prependDefaultNegative();
            appendDefaultPositive();
        }

        /// <summary>
        /// Prepends the default negative.
        /// </summary>
        private void prependDefaultNegative()
        {
            _items.Insert(0, new StressStrainPoint(strain: -1));
        }

        /// <summary>
        /// Appends the default positive.
        /// </summary>
        private void appendDefaultPositive()
        {
            _items.Add(new StressStrainPoint(strain: 1));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inserts the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="index">The index.</param>
        protected override void insert(StressStrainPoint point, int index)
        {
            if (!isValidNewPoint(point)) return;

            validatePointContext(point, index);
            _items.Insert(index, point);
        }

        /// <summary>
        /// Removes the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public override void Remove(int index)
        {
            StressStrainPoint pointRemoved = _items[index];

            // Cannot remove origin
            if (pointRemoved.IsOrigin()) return;

            _items.Remove(pointRemoved);

            // Curve must always have 1 negative and 1 positive point
            int originIndex = indexOfOrigin();
            if (originIndex == 0 && pointRemoved.Strain < 0)
            {   // Last negative value was removed
                prependDefaultNegative();
            }
            else if (originIndex == _items.Count - 1 && pointRemoved.Strain > 0)
            {   // Last positive value was removed
                appendDefaultPositive();
            }
        }


        /// <summary>
        /// Indexes the of origin.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int indexOfOrigin()
        {
            return _items.IndexOf(StressStrainPoint.Origin());
        }

        /// <summary>
        /// Determines whether [is valid new point] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if [is valid new point] [the specified point]; otherwise, <c>false</c>.</returns>
        private bool isValidNewPoint(StressStrainPoint point)
        {
            if (point == null) return false;
            validatePoint(point);
            return (!replacedDefault(point));
        }

        /// <summary>
        /// Replaces the appropriate default stress-strain points.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if a default point was replaced, <c>false</c> otherwise.</returns>
        private bool replacedDefault(StressStrainPoint point)
        {
            if (_hasDefaultPositive &&
                point.Strain > 0)
            {
                _items[2] = point;
                return true;
            }

            if (_hasDefaultNegative &&
                point.Strain < 0)
            {
                _items[0] = point;
                return true;
            }

            return false;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates the point context.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentException">
        /// The point IDs must be input in numerically increasing order, except that 'None' values are allowed anywhere.
        /// or
        /// The strains must increase monotonically.
        /// </exception>
        protected override void validatePointContext(StressStrainPoint point, int index)
        {
            validatePoint(point);
            if (!pointIdIsNumericallyIncreasingOrZero(point.PointID, index))
            {
                throw new ArgumentException("The point IDs must be input in numerically increasing order, except that 'None' values are allowed anywhere.");
            }

            if (!xCoordinatessChangeMonotonically(point, index))
            {
                throw new ArgumentException("The strains must increase monotonically.");
            }
        }

        /// <summary>
        /// Validates the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <exception cref="ArgumentException">
        /// No duplicate point ID values are allowed except for 'None'.
        /// or
        /// Points that have a negative strain must have a zero or negative stress. " +
        ///                                             "Similarly, points that have a positive strain must have a zero or positive stress." +
        ///                                             "Point IDs must have the same corresponding sign.
        /// </exception>
        private void validatePoint(StressStrainPoint point)
        {
            if (pointIdIsDuplicateNonzero(point.PointID))
            {
                throw new ArgumentException("No duplicate point ID values are allowed except for 'None'.");
            }

            if (!stressStrainHaveAppropriateSign(point))
            {
                throw new ArgumentException("Points that have a negative strain must have a zero or negative stress. " +
                                            "Similarly, points that have a positive strain must have a zero or positive stress." +
                                            "Point IDs must have the same corresponding sign.");
            }
        }

        /// <summary>
        /// Determines whether [has duplicate nonzero value].
        /// No duplicate point ID values are allowed except for <see cref="eStressStrainPointID.None" />.
        /// </summary>
        /// <param name="pointId">The point identifier.</param>
        /// <returns><c>true</c> if [has duplicate nonzero value]; otherwise, <c>false</c>.</returns>
        private bool pointIdIsDuplicateNonzero(eStressStrainPointID pointId)
        {
            return pointId != eStressStrainPointID.None && _items.Any(p => p.PointID == pointId);
        }

        /// <summary>
        /// Point identifier is numerically increasing nonzero.
        /// The point IDs must be input in numerically increasing order, except that 0 (None) values are allowed anywhere.
        /// </summary>
        /// <param name="pointId">The point identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool pointIdIsNumericallyIncreasingOrZero(eStressStrainPointID pointId, int index)
        {
            if (pointId == 0) return true;
            if (pointId < 0)
            {
                return _items[index + 1].PointID > pointId;
            }
            return _items[index - 1].PointID < pointId;
        }

        /// <summary>
        /// Determines whether [has appropriate sign].
        /// Points that have a negative strain must have a zero or negative stress.
        /// Similarly, points that have a positive strain must have a zero or positive stress.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if [has appropriate sign]; otherwise, <c>false</c>.</returns>
        private bool stressStrainHaveAppropriateSign(StressStrainPoint point)
        {
            return (point.PointID == eStressStrainPointID.None || Math.Sign((int)point.PointID) == Math.Sign(point.Strain)) &&
                   (Math.Abs(point.Stress) < Constants.Tolerance ||
                   Math.Sign(point.Stress) == Math.Sign(point.Strain));
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
