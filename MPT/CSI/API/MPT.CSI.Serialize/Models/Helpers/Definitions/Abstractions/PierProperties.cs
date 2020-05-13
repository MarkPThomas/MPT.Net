// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-29-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-29-2018
// ***********************************************************************
// <copyright file="PierProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions
{
    /// <summary>
    /// Properties associated with a pier at a given story.
    /// </summary>
    public class PierProperties : ModelProperty
    {
        /// <summary>
        /// The story name associated with the properties.
        /// </summary>
        /// <value>The story names.</value>
        public string StoryName { get; set; }

        /// <summary>
        /// The number of area objects in the pier at the story.
        /// </summary>
        /// <value>The number of area objects.</value>
        public int NumberOfAreaObjects { get; set; }

        /// <summary>
        /// The number of line objects in the pier at the story.
        /// </summary>
        /// <value>The number of line objects.</value>
        public int NumberOfLineObjects { get; set; }
        
        /// <summary>
        /// The pier local axis angle at each story, defined as the angle between the global x-axis and the pier local 2-axis.
        /// </summary>
        /// <value>The axis angles.</value>
        public double AxisAngles { get; set; }

        /// <summary>
        /// The width of the pier at the bottom of each story.
        /// </summary>
        /// <value>The width bottom.</value>
        public double WidthBottom { get; set; }

        /// <summary>
        /// The thickness of the pier at the bottom of each story.
        /// </summary>
        /// <value>The thickness bottom.</value>
        public double ThicknessBottom { get; set; }

        /// <summary>
        /// The width of the pier at the top of each story.
        /// </summary>
        /// <value>The width top.</value>
        public double WidthTop { get; set; }

        /// <summary>
        /// The thickness of the pier at the top of each story.
        /// </summary>
        /// <value>The thickness top.</value>
        public double ThicknessTop { get; set; }

        /// <summary>
        /// The name of the pier material property at each story.
        /// </summary>
        /// <value>The name of the material property.</value>
        public string MaterialPropertyName { get; set; }

        /// <summary>
        /// The x-coordinate of the center of gravity at the bottom of each story.
        /// </summary>
        /// <value>The center of gravity bottom x.</value>
        public double CenterOfGravityBottomX { get; set; }

        /// <summary>
        /// The y-coordinate of the center of gravity at the bottom of each story.
        /// </summary>
        /// <value>The center of gravity bottom y.</value>
        public double CenterOfGravityBottomY { get; set; }

        /// <summary>
        /// The z-coordinate of the center of gravity at the bottom of each story.
        /// </summary>
        /// <value>The center of gravity bot z.</value>
        public double CenterOfGravityBottomZ { get; set; }

        /// <summary>
        /// The x-coordinate of the center of gravity at the top of each story.
        /// </summary>
        /// <value>The center of gravity top x.</value>
        public double CenterOfGravityTopX { get; set; }

        /// <summary>
        /// The y-coordinate of the center of gravity at the top of each story.
        /// </summary>
        /// <value>The center of gravity top y.</value>
        public double CenterOfGravityTopY { get; set; }

        /// <summary>
        /// The z-coordinate of the center of gravity at the top of each story.
        /// </summary>
        /// <value>The center of gravity top z.</value>
        public double CenterOfGravityTopZ { get; set; }



        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
