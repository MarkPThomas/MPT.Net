// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="SteelDeckTemplate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.StructureLayout.Templates
{
    /// <summary>
    /// Creates a new steel deck model from template.
    /// </summary>
    public class SteelDeckTemplate : ModelProperty
    {
        /// <summary>
        /// The number of stories in the model.
        /// </summary>
        /// <value>The number of stories.</value>
        public int NumberOfStories { get; set; }

        /// <summary>
        /// The story height that will be used for all stories in the model, except the bottom story. [L].
        /// </summary>
        /// <value>The height of the typical story.</value>
        public double TypicalStoryHeight { get; set; }

        /// <summary>
        /// The story height will be used for the bottom story. [L].
        /// </summary>
        /// <value>The height of the bottom story.</value>
        public double BottomStoryHeight { get; set; }

        /// <summary>
        /// The number of grid lines in the X direction.
        /// </summary>
        /// <value>The number of lines x.</value>
        public int NumberOfLinesX { get; set; }

        /// <summary>
        /// The number of grid lines in the Y direction.
        /// </summary>
        /// <value>The number of lines y.</value>
        public int NumberOfLinesY { get; set; }

        /// <summary>
        /// The uniform spacing for grid lines in the X direction. [L].
        /// </summary>
        /// <value>The spacing x.</value>
        public double SpacingX { get; set; }

        /// <summary>
        /// The uniform spacing for grid lines in the Y direction. [L].
        /// </summary>
        /// <value>The spacing y.</value>
        public double SpacingY { get; set; }


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
