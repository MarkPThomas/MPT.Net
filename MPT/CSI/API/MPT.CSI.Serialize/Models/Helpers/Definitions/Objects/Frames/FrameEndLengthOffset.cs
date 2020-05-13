// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameEndLengthOffset.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames
{
    /// <summary>
    /// Class FrameEndLengthOffset.
    /// </summary>
    public class FrameEndLengthOffset : ModelProperty
    {
        /// <summary>
        /// True: The end length offsets are automatically determined by the program from object connectivity.
        /// </summary>
        /// <value><c>true</c> if [automatic offset]; otherwise, <c>false</c>.</value>
        public bool AutoOffset { get; set; }

        /// <summary>
        /// The offset length along the 1-axis of the frame object at the I-End of the frame object. [L]
        /// </summary>
        /// <value>The length i end.</value>
        public double LengthIEnd { get; set; }

        /// <summary>
        /// The offset length along the 1-axis of the frame object at the J-End of the frame object. [L]
        /// </summary>
        /// <value>The length j end.</value>
        public double LengthJEnd { get; set; }

        /// <summary>
        /// The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.
        /// </summary>
        /// <value>The rigid zone factor.</value>
        public double RigidZoneFactor { get; set; }


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
