// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="IS_456_2000_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete
{
    /// <summary>
    /// Class IS_456_2000_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete.ConcreteDesignOverwriteProperties" />
    public class IS_456_2000_Overwrites : ConcreteDesignOverwriteProperties
    {
        /// <summary>
        /// Frame Types used for ductility considerations in seismic design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// The sway
            /// </summary>
            Ductile = 1,

            /// <summary>
            /// The ordinary
            /// </summary>
            Ordinary = 2,

            /// <summary>
            /// The non sway
            /// </summary>
            NonSway = 3
        }
        /// <summary>
        /// This item is used for ductility considerations in seismic design.
        /// Program determined value means that it defaults to the highest ductility requirement.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes? FrameType { get; set; }
    }
}
