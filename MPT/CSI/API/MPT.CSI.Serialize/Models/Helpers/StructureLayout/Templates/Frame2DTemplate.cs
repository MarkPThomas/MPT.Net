// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="Frame2D.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout.Templates
{
    /// <summary>
    /// Creates a new template model of a 2D Frame.
    /// Do not use this function to add to an existing model.
    /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    /// </summary>
    public class Frame2DTemplate : ModelProperty
    {
        /// <summary>
        /// Template type.
        /// </summary>
        /// <value>The type of template.</value>
        public e2DFrameType TemplateType { get; set; }

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
        /// The number of bays.
        /// </summary>
        /// <value>The number bays.</value>
        public int NumberOfBays { get; set; }

        /// <summary>
        /// Width of each bay. [L]
        /// </summary>
        /// <value>The width of the bay.</value>
        public double BayWidth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Frame2DTemplate"/> had restraints provided at the base.
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
        /// The frame section property used for all braces in the frame.
        /// This must either be Default or the name of a defined frame section property.
        /// This item does not apply when <see cref="TemplateType" /> = <see cref="e2DFrameType.PortalFrame" />.
        /// </summary>
        /// <value>The brace.</value>
        public string Brace { get; set; } = Constants.DEFAULT;


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
