// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-08-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-08-2019
// ***********************************************************************
// <copyright file="NTC_2008_Overwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete
{
    /// <summary>
    /// Class NTC_2008_Overwrites.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete.ConcreteDesignOverwriteProperties" />
    public class NTC_2008_Overwrites : ConcreteDesignOverwriteProperties
    {
        /// <summary>
        /// Frame Types used for ductility considerations in seismic design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// The dc high
            /// </summary>
            [Description("DC High")]
            DCHigh = 1,

            /// <summary>
            /// The dc medium
            /// </summary>
            [Description("DC Medium")]
            DCMedium = 2,

            /// <summary>
            /// The dc low
            /// </summary>
            [Description("DC Low")]
            DCLow = 3,

            /// <summary>
            /// The secondary
            /// </summary>
            Secondary = 4
        }
        /// <summary>
        /// This item is used for ductility considerations in seismic design.
        /// Program determined value means that it defaults to the highest ductility requirement.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes? FrameType { get; set; }


        /// <summary>
        /// The "Theta" is the angle between the concrete compression strut and the beam axis perpendicular to the shear force.
        /// It "Tan(Theta)" ranges between 0.4 and 1.0. (EC2 6.2.3(2)).
        /// Specifying 0 means the value is program determined.
        /// Program determined value means it is taken from the concrete preferences.
        /// </summary>
        /// <value>The tangent theta.</value>
        public double TangentTheta { get; set; } = 0;


        /// <summary>
        /// Correction Factor.
        /// Correction factor that depends on axial load, used with the Nominal Curvature method.
        /// See EC2 5.8.8.3(3) for details.
        /// This item is taken as 1 by default.
        /// It is not calculated.
        /// Specifying 0 means the value is program default which is 1.
        /// </summary>
        /// <value>The kr.</value>
        public double Kr { get; set; } = 0;


        /// <summary>
        /// Creep Factor.
        /// Factor taking account of creep.
        /// See EC2 5.8.8.3(4) for details.
        /// This item is taken as 1 by default.
        /// It is not calculated.
        /// Specifying 0 means the value is program default which is 1.
        /// </summary>
        /// <value>The k phi.</value>
        public double KPhi { get; set; } = 0;
    }
}
