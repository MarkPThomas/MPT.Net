// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameLateralBracing.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameLateralBracing.
    /// </summary>
    public class FrameLateralBracing : ApiProperty
    {
        /// <summary>
        /// The bracing type assigned.
        /// </summary>
        /// <value>The bracing types.</value>
        public eBracingType BracingType { get; set; }

        /// <summary>
        /// The bracing location along the depth of the cross section.
        /// </summary>
        /// <value>The bracing locations.</value>
        public eBracingLocation BracingCrossSectionLocation { get; set; }

        /// <summary>
        /// The absolute/relative location of the point bracing or start/end of the distributed bracing.
        /// This item does not apply to the end location when <see cref="BracingType" /> = <see cref="eBracingType.Point" />.
        /// Minimum values are enforced as 0.
        /// </summary>
        /// <value>The relative distance start bracing.</value>
        public RelativeAbsoluteLength BracingLocations { get; protected set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameLateralBracing"/> class.
        /// </summary>
        /// <param name="frameLength">Length of the frame.</param>
        public FrameLateralBracing(double frameLength = 0)
        {
            BracingLocations = new RelativeAbsoluteLength(frameLength);
        }



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
