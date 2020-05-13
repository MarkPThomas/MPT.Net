// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="NodeLoads.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads
{
    /// <summary>
    /// Struct NodeLoads
    /// </summary>
    public class NodeLoad : Load
    {
        /// <summary>
        /// The name of the coordinate system for the load.
        /// This is either Local or the name of a defined coordinate system.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }

        /// <summary>
        /// The load pattern step for the load.
        /// In most cases this item does not apply and will be returned as 0.
        /// </summary>
        /// <value>The load pattern steps.</value>
        public int LoadPatternSteps { get; set; }

        /// <summary>
        /// The forces assigned along the local or global axis direction, depending on the specified <see cref="CoordinateSystem" />.
        /// </summary>
        /// <value>The force.</value>
        public API.Core.Helpers.Loads Force { get; set; }
    }
}
