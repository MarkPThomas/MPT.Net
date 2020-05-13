// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="Frame3D.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout.Templates
{
    /// <summary>
    /// Creates a new template model of a 3D Frame.
    /// Do not use this function to add to an existing model.
    /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    /// </summary>
    public class Frame3DTemplate : ModelProperty
    {
        /// <summary>
        /// Template type.
        /// </summary>
        /// <value>The type of template.</value>
        public e3DFrameType TemplateType { get; set; }

        /// <summary>
        /// The number of stories.
        /// </summary>
        /// <value>The number stories.</value>
        public int NumberOfStories { get; set; }

        /// <summary>
        /// Height of each story. [L]
        /// </summary>
        /// <value>The height of the story.</value>
        public double StoryHeight { get; set; }

        /// <summary>
        /// The number bays along the global x-direction.
        /// </summary>
        /// <value>The number of bays x.</value>
        public int NumberOfBaysX { get; set; }

        /// <summary>
        /// The number bays along the global y-direction.
        /// </summary>
        /// <value>The number of bays y.</value>
        public int NumberOfBaysY { get; set; }

        /// <summary>
        /// Width of each bay along the global x-direction. [L]
        /// </summary>
        /// <value>The bay width x.</value>
        public double BayWidthX { get; set; }

        /// <summary>
        /// Width of each bay along the global y-direction. [L]
        /// </summary>
        /// <value>The bay width y.</value>
        public double BayWidthY { get; set; }

        /// <summary>
        /// The number of divisions for each floor area object in the global x-direction.
        /// This item does not apply when <paramref name="templateType" /> = <see cref="e3DFrameType.OpenFrame" /> or <see cref="e3DFrameType.PerimeterFrame" />.
        /// </summary>
        /// <value>The number of x divisions.</value>
        private int NumberOfXDivisions { get; set; } = 4;

        /// <summary>
        /// The number of divisions for each floor area object in the global y-direction.
        /// This item does not apply when <paramref name="templateType" /> = <see cref="e3DFrameType.OpenFrame" /> or <see cref="e3DFrameType.PerimeterFrame" />.
        /// </summary>
        /// <value>The number of y divisions.</value>
        private int NumberOfYDivisions { get; set; } = 4;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Frame3DTemplate" /> had restraints provided at the base.
        /// </summary>
        /// <value><c>true</c> if restraint; otherwise, <c>false</c>.</value>
        public bool AddRestraints { get; set; } = true;

        /// <summary>
        /// The frame section property used for all beams in the frame.
        /// This must either be Default or the name of a defined frame section property.
        /// </summary>
        /// <value>The beam.</value>
        public string Beam { get; set; } = Constants.DEFAULT;

        /// <summary>
        /// The frame section property used for all columns in the frame.
        /// This must either be Default or the name of a defined frame section property.
        /// </summary>
        /// <value>The column.</value>
        public string Column { get; set; } = Constants.DEFAULT;

        /// <summary>
        /// The shell section property used for all floor slabs in the frame.
        /// This must either be Default or the name of a defined shell section property.
        /// This item does not apply when <see cref="TemplateType" /> = <see cref="e3DFrameType.OpenFrame" /> or <see cref="e3DFrameType.PerimeterFrame" />.
        /// </summary>
        /// <value>The area.</value>
        public string Area { get; set; } = Constants.DEFAULT;


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
