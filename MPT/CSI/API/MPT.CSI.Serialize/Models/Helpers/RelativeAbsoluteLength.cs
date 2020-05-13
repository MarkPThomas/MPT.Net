// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="RelativeAbsoluteLength.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Class RelativeAbsoluteLength.
    /// </summary>
    public class RelativeAbsoluteLength : ModelProperty
    {
        #region Fields & Properties
        /// <summary>
        /// The start location
        /// </summary>
        protected RelativeAbsoluteCoordinate _startLocation;
        /// <summary>
        /// The relative location of the start.
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The relative distance start.</value>
        public double RelativeDistanceStart
        {
            get => _startLocation.RelativeDistance;
            set => _startLocation.RelativeDistance = value;
        }

        /// <summary>
        /// The actual location of the start. [L]
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The actual distance start.</value>
        public double ActualDistanceStart
        {
            get => _startLocation.ActualDistance;
            set => _startLocation.ActualDistance = value;
        }


        /// <summary>
        /// The end location
        /// </summary>
        protected RelativeAbsoluteCoordinate _endLocation;
        /// <summary>
        /// The relative location of the end.
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The relative distance end.</value>
        public double RelativeDistanceEnd
        {
            get => _endLocation.RelativeDistance;
            set => _endLocation.RelativeDistance = value;
        }

        /// <summary>
        /// The actual location of the end. [L]
        /// Minimum value is enforced as 0.
        /// </summary>
        /// <value>The actual distance end.</value>
        public double ActualDistanceEnd
        {
            get => _endLocation.ActualDistance;
            set => _endLocation.ActualDistance = value;
        }


        /// <summary>
        /// The distance start.
        /// Relative vs. absolute output is indicated by <see cref="UseRelativeDistance" />
        /// </summary>
        /// <value>The distance start.</value>
        public double DistanceStart => UseRelativeDistance ? RelativeDistanceStart : ActualDistanceStart;

        /// <summary>
        /// The distance end.
        /// Relative vs. absolute output is indicated by <see cref="UseRelativeDistance" />
        /// </summary>
        /// <value>The distance end.</value>
        public double DistanceEnd => UseRelativeDistance ? RelativeDistanceEnd : ActualDistanceEnd;


        /// <summary>
        /// The length
        /// </summary>
        private double _length;
        /// <summary>
        /// The length that the distances are relative to. [L]
        /// </summary>
        /// <value>The length.</value>
        public double Length { get; protected set; }

        /// <summary>
        /// True: Relative distances are used.
        /// False: Absolute distances are used.
        /// </summary>
        /// <value><c>true</c> if [use relative distance]; otherwise, <c>false</c>.</value>
        public bool UseRelativeDistance { get; internal set; }
        #endregion

        #region Initialization & Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeAbsoluteLength" /> class.
        /// </summary>
        /// <param name="length">The length that the distances are relative to. [L]</param>
        public RelativeAbsoluteLength(double length)
        {
            _length = getLength(length);
            _startLocation = new RelativeAbsoluteCoordinate(_length);
            _endLocation = new RelativeAbsoluteCoordinate(_length);
        }

        /// <summary>
        /// Sets the length.
        /// Minimum value is enforeced as the global-specified tolerance.
        /// </summary>
        /// <param name="length">The length.</param>
        public void SetLength(double length)
        {
            _length = getLength(length);
            _startLocation.SetLength(_length);
            _endLocation.SetLength(_length);
        }

        /// <summary>
        /// Sets the use relative distance.
        /// </summary>
        /// <param name="useRelativeDistance">if set to <c>true</c> [use relative distance].</param>
        public void SetUseRelativeDistance(bool useRelativeDistance)
        {
            _startLocation.UseRelativeDistance = useRelativeDistance;
            _endLocation.UseRelativeDistance = useRelativeDistance;
        }
        #endregion

        #region Protected

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>System.Double.</returns>
        protected double getLength(double length)
        {
            return Math.Max(Constants.Tolerance, length);
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