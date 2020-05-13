// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="SolidBlockTemplate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout.Templates
{
    /// <summary>
    /// Creates a new template model of a Solid.
    /// Do not use this function to add to an existing model.
    /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    /// </summary>
    public class SolidBlockTemplate : ModelProperty
    {
        /// <summary>
        /// The total width of the solid block measured in the global X direction. [L]
        /// </summary>
        /// <value>The width x.</value>
        public double WidthX { get; set; }

        /// <summary>
        /// The total width of the solid block measured in the global Y direction. [L]
        /// </summary>
        /// <value>The width y.</value>
        public double WidthY { get; set; }

        /// <summary>
        /// The total height of the solid block measured in the global Z direction. [L]
        /// </summary>
        /// <value>The height.</value>
        public double Height { get; set; }

        /// <summary>
        /// The number of solid objects in the global X direction of the block.
        /// </summary>
        /// <value>The number of x divisions.</value>
        public int NumberOfXDivisions { get; set; } = 5;

        /// <summary>
        /// The number of solid objects in the global Y direction of the block.
        /// </summary>
        /// <value>The number of y divisions.</value>
        public int NumberOfYDivisions { get; set; } = 8;

        /// <summary>
        /// The number of solid objects in the global Z direction of the block.
        /// </summary>
        /// <value>The number of z divisions.</value>
        public int NumberOfZDivisions { get; set; } = 10;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SolidBlockTemplate" /> had restraints provided at the base.
        /// </summary>
        /// <value><c>true</c> if restraint; otherwise, <c>false</c>.</value>
        public bool AddRestraints { get; set; } = true;

        /// <summary>
        /// The solid property used for the solid block.
        /// This must either be Default or the name of a defined solid property.
        /// </summary>
        /// <value>The solid.</value>
        public string Solid { get; set; } = Constants.DEFAULT;


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
