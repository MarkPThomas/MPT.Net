// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-25-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-25-2019
// ***********************************************************************
// <copyright file="DimensionalPreferences.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    /// <summary>
    /// Class DimensionalPreferences.
    /// </summary>
    public class DimensionalPreferences
    {
        /// <summary>
        /// The program auto merge tolerance. [L]
        /// </summary>
        /// <value>The merge tolerance.</value>
        public double MergeTolerance { get; internal set; }


        /// <summary>
        /// Gets the fine grid spacing. [L]
        /// </summary>
        /// <value>The fine grid spacing.</value>
        public double FineGridSpacing { get; internal set; }


        /// <summary>
        /// Gets the nudge distance. [L]
        /// </summary>
        /// <value>The nudge distance.</value>
        public double NudgeDistance { get; internal set; }



        /// <summary>
        /// The program selection tolerance. [pixels]
        /// </summary>
        /// <value>The screen selection tolerance.</value>
        public int ScreenSelectionTolerance { get; internal set; }

        /// <summary>
        /// The program snap tolerance. [pixels]
        /// </summary>
        /// <value>The screen snap tolerance.</value>
        public int ScreenSnapTolerance { get; internal set; }


        /// <summary>
        /// Gets the screen line thickness. [pixels]
        /// </summary>
        /// <value>The screen line thickness.</value>
        public int ScreenLineThickness { get; internal set; }


        /// <summary>
        /// Gets the printer line thickness. [pixels]
        /// </summary>
        /// <value>The printer line thickness.</value>
        public int PrinterLineThickness { get; internal set; }


        /// <summary>
        /// Gets the maximum size of the font. [points]
        /// </summary>
        /// <value>The maximum size of the font.</value>
        public int MaxFontSize { get; internal set; }


        /// <summary>
        /// Gets the minimum size of the font. [points]
        /// </summary>
        /// <value>The minimum size of the font.</value>
        public int MinFontSize { get; internal set; }


        /// <summary>
        /// Gets the automatic zoom step. [percent]
        /// </summary>
        /// <value>The automatic zoom step.</value>
        public int AutoZoomStep { get; internal set; }


        /// <summary>
        /// Gets the shrink factor. [percent]
        /// </summary>
        /// <value>The shrink factor.</value>
        public int ShrinkFactor { get; internal set; }


        /// <summary>
        /// Gets the maximum line length in text file. [characters]
        /// </summary>
        /// <value>The maximum line length in text file.</value>
        public int MaxLineLengthInTextFile { get; internal set; }

    }
}
