// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="FrameLoadsTemperature.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
{
    /// <summary>
    /// Struct FrameLoadsTemperature
    /// </summary>
    public class LoadTemperature : Load
    {
        /// <summary>
        /// Indicates the type of temperature load.
        /// </summary>
        /// <value>The type of the temperature load.</value>
        public eLoadTemperatureType TemperatureLoadType { get; set; }

        /// <summary>
        /// Temperature load values, [T] for <see cref="TemperatureLoadType" /> = Temperature, [T/L] for all others.
        /// </summary>
        /// <value>The temperature load value.</value>
        public double Value { get; set; }

        /// <summary>
        /// The joint pattern name, if any, used to specify the temperature load.
        /// </summary>
        /// <value>The name of the joint pattern.</value>
        public string JointPatternName { get; set; }
    }
}
