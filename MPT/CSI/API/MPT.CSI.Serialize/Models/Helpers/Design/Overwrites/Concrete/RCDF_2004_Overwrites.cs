// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="RCDF_2004_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete
{
    /// <summary>
    /// Class RCDF_2004_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete.ConcreteDesignOverwriteProperties" />
    public class RCDF_2004_Overwrites : ConcreteDesignOverwriteProperties
    {
        /// <summary>
        /// Frame Types used for ductility considerations in seismic design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// The sway special
            /// </summary>
            [Description("Sway Special")]
            SwaySpecial = 1,

            /// <summary>
            /// The sway ordinary
            /// </summary>
            [Description("Sway Ordinary")]
            SwayOrdinary = 3,

            /// <summary>
            /// The non sway
            /// </summary>
            NonSway = 4
        }
        /// <summary>
        /// This item is used for ductility considerations in seismic design.
        /// Program determined value means that it defaults to the highest ductility requirement.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes? FrameType { get; set; }
    }
}
