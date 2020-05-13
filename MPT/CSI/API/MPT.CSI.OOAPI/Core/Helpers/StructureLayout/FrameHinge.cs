// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameHinge.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameHinge.
    /// </summary>
    public class FrameHinge : ApiProperty
    {
        /// <summary>
        /// The hinge number.
        /// </summary>
        /// <value>The hinge number.</value>
        public int HingeNumber { get; set; }

        /// <summary>
        /// The name of the generated hinge property.
        /// </summary>
        /// <value>The name of the generated property.</value>
        public string GeneratedPropertyName { get; set; }

        /// <summary>
        /// The type of hinge.
        /// </summary>
        /// <value>The type of the hinge.</value>
        public eHingeType HingeType { get; set; }

        /// <summary>
        /// The hinge behavior.
        /// </summary>
        /// <value>The hinge behavior.</value>
        public eHingeBehavior HingeBehavior { get; set; }

        /// <summary>
        /// The source of the generated hinge property.
        /// The source is either Auto or the name of a defined (not generated) hinge property.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// The relative distance of the hinge along the frame object.
        /// </summary>
        /// <value>The relative distance.</value>
        public double RelativeDistance { get; set; }


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
