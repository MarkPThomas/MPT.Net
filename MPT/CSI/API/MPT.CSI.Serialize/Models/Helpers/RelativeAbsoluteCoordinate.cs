// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="RelativeAbsoluteCoordinate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Class RelativeAbsoluteCoordinate.
    /// </summary>
    public class RelativeAbsoluteCoordinate : ModelProperty
    {
        #region Fields & Properties
        /// <summary>
        /// The relative distance
        /// </summary>
        private double _relativeDistance;
        /// <summary>
        /// The relative location.
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The relative distance.</value>
        public double RelativeDistance
        {
            get => _relativeDistance;
            set
            {
                _relativeDistance = Math.Max(0, value);
                updateActualDistance();
            }
        }

        /// <summary>
        /// The actual distance
        /// </summary>
        private double _actualDistance;
        /// <summary>
        /// The absolute location. [L]
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The actua distance start bracing.</value>
        public double ActualDistance
        {
            get => _actualDistance;
            set
            {
                _actualDistance = Math.Max(0, value);
                updateRelativeDistance();
            }
        }

        /// <summary>
        /// The location.
        /// Relative vs. absolute output is indicated by <see cref="UseRelativeDistance" />
        /// </summary>
        /// <value>The distance start bracing.</value>
        public double Distance => UseRelativeDistance ? RelativeDistance : ActualDistance;


        /// <summary>
        /// The length that the distances are relative to. [L]
        /// Minimum value is enforced as 0.
        /// Locations are automatically updated for values greater than 0.
        /// </summary>
        /// <value>The length.</value>
        public double Length { get; protected set; }

        /// <summary>
        /// True: Relative distances are used.
        /// False: Absolute distances are used.
        /// </summary>
        /// <value><c>true</c> if [use relative distance]; otherwise, <c>false</c>.</value>
        public bool UseRelativeDistance { get; set; }
        #endregion

        #region Initialization & Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeAbsoluteCoordinate" /> class.
        /// </summary>
        /// <param name="length">The length that the distances are relative to. [L]
        /// Minimum value is enforced as 0.
        /// Locations are automatically updated for values greater than 0.</param>
        public RelativeAbsoluteCoordinate(double length)
        {
            SetLength(length);
        }

        /// <summary>
        /// Sets the length.
        /// Minimum value is enforced as 0.
        /// Locations are automatically updated for values greater than 0.
        /// </summary>
        /// <param name="length">The length.</param>
        public void SetLength(double length)
        {
            Length = Math.Max(0, length); 
            if (UseRelativeDistance)
            {
                updateActualDistance();
            }
            else
            {
                updateRelativeDistance();
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Updates the relative distance.
        /// </summary>
        protected void updateRelativeDistance()
        {
            if (Length <= 0) return;
            _relativeDistance = updateRelativeDistance(_actualDistance, Length);
        }

        /// <summary>
        /// Updates the actual distance.
        /// </summary>
        protected void updateActualDistance()
        {
            if (Length <= 0) return;
            _actualDistance = updateActualDistance(_relativeDistance, Length);
        }

        /// <summary>
        /// Updates the relative distance.
        /// </summary>
        /// <param name="actualDistance">The actual distance.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.Double.</returns>
        protected double updateRelativeDistance(double actualDistance, double length)
        {
            return length / actualDistance;
        }

        /// <summary>
        /// Updates the actual distance.
        /// </summary>
        /// <param name="relativeDistance">The relative distance.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.Double.</returns>
        protected double updateActualDistance(double relativeDistance, double length)
        {
            return length * relativeDistance;
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