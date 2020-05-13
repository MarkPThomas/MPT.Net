// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="LoadFunction.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Class LoadFunction.
    /// </summary>
    public class LoadResponseSpectrum : ModelProperty
    {
        internal string LoadName { get; set; }
        public virtual LoadPattern Load { get; internal set; }


        internal string FunctionName { get; set; }
        // TODO: Replace function with object
        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>The function.</value>
        public string Function { get; set; }

        /// <summary>
        /// Name of the coordinate system associated with the load.
        /// If this item is a blank string, the Global coordinate system is assumed.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }


        /// <summary>
        /// The angle between the acceleration local-1 axis and the +X-axis of the coordinate system specified by <see cref="Components.Grids.CoordinateSystems" />.
        /// The rotation is about the Z-axis of the specified coordinate system. [deg].
        /// </summary>
        /// <value>The angle.</value>
        public double Angle { get; set; }

        /// <summary>
        /// ScaleFactor factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.
        /// </summary>
        /// <value>The scale factor.</value>
        public double ScaleFactor { get; set; } = 1;


        /// <summary>
        /// The direction of the load.
        /// </summary>
        /// <value>The direction.</value>
        public eDegreeOfFreedom Direction { get; set; }


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
